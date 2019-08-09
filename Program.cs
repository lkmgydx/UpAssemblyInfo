using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using WSTools.WSLog;

namespace UpAssemblyInfo
{
    class Program
    {
        private static string fileTemp = "tempKey.temp";
        private static bool del = false;

        static void Main(string[] args)
        {
            Log.LogPath = @"d:\vv.txt";
            Log.LogEnable = false;
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(Environment.CurrentDirectory);
            Log.log("当前目录:" + di.FullName);
            string file = Path.Combine(di.Parent.Parent.FullName, "Properties", "AssemblyInfo.cs");
            if (!File.Exists(file))
            {
                Log.log("不存在AssemblyInfo,退出");
                return;
            }
            Log.log("当前文件名:" + file);
            del = args.Length != 0;
            string copyRight = IniFile.getValue("CopyRight");
            if (copyRight == null)
            {
                copyRight = "Copyright © 微声技术 2019";
            }
            Log.log("copyRight:" + copyRight);
            string companyName = IniFile.getValue("CompanyName");
            Log.log("CompanyName:" + companyName);

            string savePath = Path.Combine(new FileInfo(file).Directory.FullName, fileTemp);
            Log.log("savePath:" + savePath);
            try
            {
                if (del)
                {
                    try
                    {
                        File.Copy(savePath, file, true);
                    }
                    catch (Exception ex)
                    {
                        Log.logError("反写值失败!", ex);
                    }
                    File.Delete(savePath);
                }
                else
                {
                    Log.log("writeInfo:" + savePath);
                    File.Copy(file, savePath, true);
                    AssemblyInfoClass ac = new AssemblyInfoClass(file);
                    foreach (var i in IniFile.keys)
                    {
                        ac.upInfo(i, IniFile.getValue(i));
                    }
                    ac.AssemblyTrademark = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
            catch (Exception ex)
            {
                Log.logError("操作失败!", ex);
            }

        }
    }
}
