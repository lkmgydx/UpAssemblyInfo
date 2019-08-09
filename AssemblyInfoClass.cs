using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace UpAssemblyInfo
{
    class AssemblyInfoClass
    {
        private string fileContent = "";

        private string filePath = "";
        /// <summary>
        /// 程序集说明
        /// </summary>
        public string AssemblyTitle
        {
            get
            {
                return getAttr("AssemblyTitle");
            }
            set
            {
                upInfo("AssemblyTitle", value);
            }
        }
        /// <summary>
        /// 版权信息
        /// </summary>
        public string AssemblyCopyright
        {
            get
            {
                return getAttr("AssemblyCopyright");
            }
            set
            {
                upInfo("AssemblyCopyright", value);
            }
        }
        /// <summary>
        /// 公司名称信息
        /// </summary>
        public string AssemblyCompany
        {
            get
            {
                return getAttr("AssemblyCompany");
            }
            set
            {
                upInfo("AssemblyCompany", value);
            }
        }

        /// <summary>
        /// 商标信息
        /// </summary>
        public string AssemblyTrademark
        {
            get
            {
                return getAttr("AssemblyTrademark");
            }
            set
            {
                upInfo("AssemblyTrademark", value);
            }
        }

        public AssemblyInfoClass(string path)
        {
            if (!File.Exists(path))
            {
                throw new Exception("文件不存在!" + path);
            }
            fileContent = File.ReadAllText(path);
            this.filePath = path;
        }
        /// <summary>
        /// 自动保存
        /// </summary>
        public bool AuToSAVE { get; set; } = true;

        /// <summary>
        /// 获取节点属性
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string getAttr(string name)
        {
            Match ma = new Regex("^\\[assembly: " + name + "\\(\\\"" + "(.*)" + "\\\"\\)\\]", RegexOptions.Multiline).Match(fileContent);
            if (ma.Success)
            {
                return ma.Groups[1].Value;
            }
            return "";
        }

        public bool upInfo(string name, string value)
        {
            if (value == null)
            {
                value = "";
            }
            string regStr = "(?<=\\[assembly: " + name + "\\(\\\"" + ")(.*)(?=" + "\\\"\\)\\])";
            if (!new Regex(regStr).Match(this.fileContent).Success)
            {
                return false;
            }
            this.fileContent = Regex.Replace(this.fileContent, regStr, value);
            if (AuToSAVE)
            {
                File.WriteAllText(this.filePath, this.fileContent, new System.Text.UTF8Encoding(true));
            }
            return true;
        }
    }
}
