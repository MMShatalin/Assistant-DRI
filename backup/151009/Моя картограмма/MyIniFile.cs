using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Моя_картограмма
{
 

        public class IniFile
        {
            private String File = "";

            [DllImport("kernel32")]
            private static extern long WritePrivateProfileInt(String Section, String Key, int Value, String FilePath);
            [DllImport("kernel32")]
            private static extern long WritePrivateProfileString(String Section, String Key, String Value, String FilePath);
            [DllImport("kernel32")]
            private static extern int GetPrivateProfileInt(String Section, String Key, int Default, String FilePath);
            [DllImport("kernel32")]
            private static extern int GetPrivateProfileString(String Section, String Key, String Default, StringBuilder retVal, int Size, String FilePath);

            public IniFile(String IniFile)
            {
                this.File = IniFile;
            }

            public String ReadString(String Section, String Key, String Default)
            {
                StringBuilder StrBu = new StringBuilder(255);
                GetPrivateProfileString(Section, Key, Default, StrBu, 255, File);
                return StrBu.ToString();
            }

            public int ReadInt(String Section, String Key, int Default)
            {
                return GetPrivateProfileInt(Section, Key, Default, File);
            }

            public void WriteString(String Section, String Key, String Value)
            {
                WritePrivateProfileString(Section, Key, Value, File);
            }

            public void WriteInt(String Section, String Key, int Value)
            {
                WritePrivateProfileInt(Section, Key, Value, File);
            }
        }




    
}
