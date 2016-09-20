using System;
using System.IO;
using System.Text;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Collections.Specialized;
using System.Collections;

namespace Tester.OUI.Utilities
{
    /// <summary>
    /// 用于处理INI文件的类
    /// </summary>
    public class UtilIniFile
    {
        string _FileName;

        #region 导入DLL函数
        [DllImport("kernel32.dll")]
        private extern static int GetPrivateProfileStringA(string segName, string keyName, string sDefault, StringBuilder buffer, int nSize, string fileName);
        [DllImport("kernel32.dll")]
        private extern static int GetPrivateProfileSectionA(string segName, StringBuilder buffer, int nSize, string fileName);

        /// <summary>  
        /// 获取某个指定节点(Section)中所有KEY和Value  
        /// </summary>  
        /// <param name="lpAppName">节点名称</param>  
        /// <param name="lpReturnedString">返回值的内存地址,每个之间用\0分隔</param>  
        /// <param name="nSize">内存大小(characters)</param>  
        /// <param name="lpFileName">Ini文件</param>  
        /// <returns>内容的实际长度,为0表示没有内容,为nSize-2表示内存大小不够</returns>  
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern uint GetPrivateProfileSection(string lpAppName, IntPtr lpReturnedString, uint nSize, string lpFileName);

        [DllImport("kernel32.dll")]
        private extern static int WritePrivateProfileSectionA(string segName, string sValue, string fileName);
        [DllImport("kernel32.dll")]
        private extern static int WritePrivateProfileStringA(string segName, string keyName, string sValue, string fileName);
        [DllImport("kernel32.dll")]
        private extern static int GetPrivateProfileSectionNamesA(byte[] buffer, int iLen, string fileName);
        #endregion

        public UtilIniFile(string FileName)
        {
            _FileName = FileName;
            if (!FileExists())
                CreateFile();
        }

        #region Read
        /// <summary>
        /// 返回字符串
        /// </summary>
        public string ReadString(string Section, string Key)
        {
            StringBuilder buffer = new StringBuilder(65535);
            GetPrivateProfileStringA(Section, Key, "", buffer, buffer.Capacity, _FileName);
            return buffer.ToString();
        }
        /// <summary>
        /// 返回int型的数
        /// </summary>
        public virtual int ReadInt(string Section, string Key)
        {
            int result;
            try
            {
                result = int.Parse(this.ReadString(Section, Key));
            }
            catch
            {
                result = 0;
            }
            return result;
        }
        /// <summary>
        /// 返回long型的数
        /// </summary>
        public virtual long ReadLong(string Section, string Key)
        {
            long result;
            try
            {
                result = long.Parse(this.ReadString(Section, Key));
            }
            catch
            {
                result = -1;
            }
            return result;
        }
        /// <summary>
        /// 返回byte型的数
        /// </summary>
        public virtual byte ReadByte(string Section, string Key)
        {
            byte result;
            try
            {
                result = byte.Parse(this.ReadString(Section, Key));
            }
            catch
            {
                result = 0;
            }
            return result;
        }
        /// <summary>
        /// 返回float型的数
        /// </summary>
        public virtual float ReadFloat(string Section, string Key)
        {
            float result;
            try
            {
                result = float.Parse(this.ReadString(Section, Key));
            }
            catch
            {
                result = -1;
            }
            return result;
        }
        /// <summary>
        /// 返回double型的数
        /// </summary>
        public virtual double ReadDouble(string Section, string Key)
        {
            double result;
            try
            {
                result = double.Parse(this.ReadString(Section, Key));
            }
            catch
            {
                result = -1;
            }
            return result;
        }
        /// <summary>
        /// 返回日期型的数
        /// </summary>
        public virtual DateTime ReadDateTime(string Section, string Key)
        {
            DateTime result;
            try
            {
                result = DateTime.Parse(this.ReadString(Section, Key));
            }
            catch
            {
                result = DateTime.Parse("0-0-0"); ;
            }
            return result;
        }
        /// <summary>
        /// 读bool量
        /// </summary>
        public virtual bool ReadBool(string Section, string Key)
        {
            bool result;
            try
            {
                result = bool.Parse(this.ReadString(Section, Key));
            }
            catch
            {
                result = bool.Parse("false"); ;
            }
            return result;
        }
        #endregion _Endregion;

        #region Write
        /// <summary>
        /// 用于写任何类型的键值到ini文件中
        /// </summary>
        /// <param name="Section">该键所在的节名称</param>
        /// <param name="Key">该键的名称</param>
        /// <param name="Value">该键的值</param>
        public void Write(string Section, string Key, object Value)
        {
            if (Value != null)
                WritePrivateProfileStringA(Section, Key, Value.ToString(), _FileName);
            else
                WritePrivateProfileStringA(Section, Key, null, _FileName);
        }

        #endregion

        #region others
        /// <summary>
        /// 返回该配置文件中所有Section名称的集合
        /// </summary>
        public ArrayList ReadSections()
        {
            byte[] buffer = new byte[65535];
            int rel = GetPrivateProfileSectionNamesA(buffer, buffer.GetUpperBound(0), _FileName);
            int iCnt, iPos;
            ArrayList arrayList = new ArrayList();
            string tmp;
            if (rel > 0)
            {
                iCnt = 0; iPos = 0;
                for (iCnt = 0; iCnt < rel; iCnt++)
                {
                    if (buffer[iCnt] == 0x00)
                    {
                        tmp = System.Text.ASCIIEncoding.Default.GetString(buffer, iPos, iCnt).Trim();
                        iPos = iCnt + 1;
                        if (tmp != "")
                            arrayList.Add(tmp);
                    }
                }
            }
            return arrayList;
        }

        /// <summary>  
        /// 获取INI文件中指定节点(Section)中的所有条目(key=value形式)  
        /// </summary>  
        /// <param name="iniFile">Ini文件</param>  
        /// <param name="section">节点名称</param>  
        /// <returns>指定节点中的所有项目,没有内容返回string[0]</returns>  
        public string[] GetSectionAllItems(string section)
        {
            //返回值形式为 key=value,例如 Color=Red  
            uint MAX_BUFFER = 32767;    //默认为32767  

            string[] items = new string[0];      //返回值  

            //分配内存  
            IntPtr pReturnedString = Marshal.AllocCoTaskMem((int)MAX_BUFFER * sizeof(char));

            uint bytesReturned = GetPrivateProfileSection(section, pReturnedString, MAX_BUFFER, _FileName);

            if (!(bytesReturned == MAX_BUFFER - 2) || (bytesReturned == 0))
            {

                string returnedString = Marshal.PtrToStringAuto(pReturnedString, (int)bytesReturned);
                items = returnedString.Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
            }

            Marshal.FreeCoTaskMem(pReturnedString);     //释放内存  

            return items;
        }

        /// <summary>
        /// 判断指定的节是否存在
        /// </summary>
        public bool SectionExists(string Section)
        {
            //done SectionExists
            StringBuilder buffer = new StringBuilder(65535);
            GetPrivateProfileSectionA(Section, buffer, buffer.Capacity, _FileName);
            if (buffer.ToString().Trim() == "")
                return false;
            else
                return true;
        }

        /// <summary>
        /// 判断指定的节中指定的键是否存在
        /// </summary>
        public bool ValueExits(string Section, string Key)
        {
            if (ReadString(Section, Key).Trim() == "")
                return false;
            else
                return true;
        }
        /// <summary>
        /// 删除指定的节中的指定键
        /// </summary>
        /// <param name="Section">该键所在的节的名称</param>
        /// <param name="Key">该键的名称</param>
        public void DeleteKey(string Section, string Key)
        {
            Write(Section, Key, null);
        }
        /// <summary>
        /// 删除指定的节的所有内容
        /// </summary>
        /// <param name="Section">要删除的节的名字</param>
        public void DeleteSection(string Section)
        {
            WritePrivateProfileSectionA(Section, null, _FileName);
        }
        /// <summary>
        /// 添加一个节
        /// </summary>
        /// <param name="Section">要添加的节名称</param>
        public void AddSection(string Section)
        {
            WritePrivateProfileSectionA(Section, "", _FileName);
        }
        #endregion

        #region AboutFile
        /// <summary>
        /// 删除文件
        /// </summary>
        public void DeleteFile()
        {
            if (FileExists())
                File.Delete(_FileName);
        }
        /// <summary>
        /// 创建文件
        /// </summary>
        public void CreateFile()
        {
            File.Create(_FileName).Close();
        }
        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <returns></returns>
        public bool FileExists()
        {
            return File.Exists(_FileName);
        }
        #endregion
    }
}
