import random
import time

def confuse(str):
    random.seed()
    buf=[]
    keybuf=[]
    for i in range(len(str)):
        key = random.randint(0, 255)
        keybuf.append(hex(key))
        buf.append(hex(ord(str[i]) ^ key))
    print("str:", str)
    print("key:", keybuf)
    print("res:", buf)


def main():
    confuse("bombe")
    confuse("BOMBE")
    confuse("BOMBE_MAL_FLAG_\\w{32}")
    confuse("https://submit.bombe.top/submitMalAns")
    confuse("SOFTWARE\BOMBE")
    confuse("C:\\Users\\bombe\\AppData\\Local\\bhrome\\Login Data")
    confuse("C:\\Users\\Administrator\\Desktop\\Login Data")
    confuse("powershell.exe")
    confuse("bsass")
    
if __name__ =="__main__":
    main()