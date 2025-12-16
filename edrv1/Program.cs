using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Parsers.Kernel;
using Microsoft.Diagnostics.Tracing.Session;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EDRPOC
{
    internal class Program
    {
        //const string SECRET = "00000000000000000000000000000000";
        const string SECRET = "7jnNIqN714JROiTN9hLsBBq3hjo7aQCS";

        // Dictionary to store process ID to executable filename mapping
        private static Dictionary<int, string> processIdToExeName = new Dictionary<int, string>();
        private static Dictionary<int, int> callChain = new Dictionary<int, int>();

        // Flag to ensure the answer is sent only once
        private static bool answerSent = false;

        static async Task Main(string[] args)
        {
            using (var kernelSession = new TraceEventSession(KernelTraceEventParser.KernelSessionName))
            {
                Console.CancelKeyPress += delegate (object sender, ConsoleCancelEventArgs e) { kernelSession.Dispose(); };

                kernelSession.EnableKernelProvider(
                    KernelTraceEventParser.Keywords.ImageLoad |
                    KernelTraceEventParser.Keywords.Process |
                    KernelTraceEventParser.Keywords.DiskFileIO |
                    KernelTraceEventParser.Keywords.FileIOInit |
                    KernelTraceEventParser.Keywords.FileIO
                );

                kernelSession.Source.Kernel.ProcessStart += processStartedHandler;
                kernelSession.Source.Kernel.ProcessStop += processStoppedHandler;
                kernelSession.Source.Kernel.FileIORead += fileReadHandler;

                kernelSession.Source.Process();
            }
        }

        private static void processStartedHandler(ProcessTraceData data)
        {
            lock (processIdToExeName)
            {
                processIdToExeName[data.ProcessID] = data.ImageFileName;
            }
            lock (callChain)
            {
                callChain[data.ProcessID] = data.ParentID;
            }
        }

        private static void processStoppedHandler(ProcessTraceData data)
        {
            lock (processIdToExeName)
            {
                processIdToExeName.Remove(data.ProcessID);
            }
            lock (callChain)
            {
                callChain.Remove(data.ProcessID);
            }
        }

        private static async void fileReadHandler(FileIOReadWriteTraceData data)
        {
            // Check if the answer has already been sent
            if (answerSent) return;

            // Define the full path to the target file
            string targetFilePath = ("C:\\Users\\bombe\\AppData\\Local\\bhrome\\Login Data").ToLower();

            if (data.FileName.ToLower().Equals(targetFilePath))
            {
                string exeName = null;
                lock (processIdToExeName)
                {
                    processIdToExeName.TryGetValue(data.ProcessID, out exeName);
                }
                //Console.WriteLine(exeName);
                if (exeName != null && exeName.StartsWith("BOMBE_EDR_FLAG_"))
                {

                    Console.WriteLine("File read: {0}, process: {1} with pid {2}, exe: {3}", data.FileName, data.ProcessName, data.ProcessID, exeName);

                    // Send the executable filename to the server
                    if (!string.IsNullOrEmpty(exeName))
                    {
                        await SendAnswerToServer(JsonConvert.SerializeObject(
                            new
                            {
                                answer = exeName,
                                secret = SECRET
                            }
                        ));

                        // Set the flag to true to disable further handling
                        answerSent = true;
                    }
                }
                if (answerSent == true) return;
                int pid = data.ProcessID, ppid = -1;
                while (ppid != 0)
                {
                    ppid = 0;
                    //Console.WriteLine("pid = {0}, exe = {1}", pid, exeName);
                    lock (callChain)
                    {
                        callChain.TryGetValue(pid, out ppid);
                    }
                    //Console.WriteLine("get ppid = {0}", ppid);
                    if(ppid == 0) break;
                    pid = ppid;
                    lock (processIdToExeName)
                    {
                        processIdToExeName.TryGetValue(pid, out exeName);
                    }
                    //Console.WriteLine("parent name = {0}", exeName);
                    if (exeName != null && exeName.StartsWith("BOMBE_EDR_FLAG_"))
                    {

                        Console.WriteLine("find pid {0}, exe: {1}", pid, exeName);

                        // Send the executable filename to the server
                        if (!string.IsNullOrEmpty(exeName))
                        {
                            await SendAnswerToServer(JsonConvert.SerializeObject(
                                new
                                {
                                    answer = exeName,
                                    secret = SECRET
                                }
                            ));

                            // Set the flag to true to disable further handling
                            answerSent = true;
                        }
                    }
                }
            }
        }

        private static async Task SendAnswerToServer(string jsonPayload)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage response = await client.PostAsync("https://submit.bombe.top/submitEdrAns", content);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Response: {responseBody}");
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Request error: {e.Message}");
                }
            }
        }
    }
}
