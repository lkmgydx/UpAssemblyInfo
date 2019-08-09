using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace UpAssemblyInfo
{

    class IniFile
    {
        private static string iniFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.ini");
        private static Dictionary<string, string> dict = new Dictionary<string, string>();
        static IniFile()
        {
            string[] args = File.ReadAllLines(iniFile);
            for (int i = 0; i < args.Length; i++)
            {
                var str = args[i];
                var index = str.IndexOf("=");
                if (index == -1 || index == 0)
                {
                    continue;
                }
                dict.Add(str.Substring(0, index), str.Substring(index + 1));
            }
        }

        public static Dictionary<string, string>.KeyCollection keys
        {
            get
            {
                return dict.Keys;
            }
        }

        public static string getValue(string key)
        {
            string rst;
            dict.TryGetValue(key, out rst);
            return rst;
        }
    }
}
