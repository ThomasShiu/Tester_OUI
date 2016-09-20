using System;
using System.Collections.Generic;
using System.Text;
using Brooks.WinSECS;
using System.IO;
using Tester.OUI.Utilities;

namespace Tester.OUI.Winsecs
{
    public class SecsHelper
    {
        const int SmlIndent = 2;

        static string wsSML(SECSItem item, int indent)
        {
            StringBuilder strReturn = new StringBuilder("");
            string str = "";
            if (item == null) return strReturn.ToString();
            var indentStr = new string(' ', indent);
            strReturn.Append(indentStr);
            strReturn.Append("<");
            strReturn.Append(ToSML(item.Format));
            strReturn.Append(" [");
            strReturn.Append(item.ItemCount);
            strReturn.Append("] ");
            switch (item.Format)
            {
                case SECS_FORMAT.LIST:
                    strReturn.Append("\n");
                    int count = item.ItemCount;
                    for (int i = 0; i < count; i++)
                    {
                        str = wsSML(item.Item(i), indent + SmlIndent);
                        strReturn.Append(str);
                    }
                    strReturn.Append(indentStr);
                    break;
                case SECS_FORMAT.ASCII:
                case SECS_FORMAT.JIS8:
                    strReturn.Append("\'");
                    strReturn.Append(item.Value);
                    strReturn.Append('\'');
                    break;
                default:
                    strReturn.Append(item.Value);
                    break;
            }
            strReturn.Append(">" + "\n");
            return strReturn.ToString();
        }

        static void Write(TextWriter writer, SECSItem item, int indent)
        {
            if (item == null) return;
            var indentStr = new string(' ', indent);
            writer.Write(indentStr);
            writer.Write('<');
            writer.Write(ToSML(item.Format));
            writer.Write(" [");
            writer.Write(item.ItemCount);
            writer.Write("] ");
            switch (item.Format)
            {
                case SECS_FORMAT.LIST:
                    writer.WriteLine();
                    int count = item.ItemCount;
                    for (int i = 0; i < count; i++)
                        Write(writer, item.Item(i), indent + SmlIndent);
                    writer.Write(indentStr);
                    break;
                case SECS_FORMAT.ASCII:
                case SECS_FORMAT.JIS8:
                    writer.Write('\'');
                    writer.Write(item.ToString());
                    writer.Write('\'');
                    break;
                default:
                    writer.Write(item.ToString());
                    break;
            }
            writer.WriteLine('>');
        }

        public static void RecordSecsMessage(SECSMessage secsMsg)
        {
            try
            {
                string sMsg = "S" + secsMsg.Stream + "F" + secsMsg.Function;

                sMsg += wsSML(secsMsg.Root.Item(0), 2);

                UtilLog.SecsInfo(sMsg);
            }
            catch (Exception ex)
            {

            }
        }

         static string ToSML(SECS_FORMAT format)
        {
            switch (format)
            {
                case SECS_FORMAT.LIST: return "L";
                case SECS_FORMAT.BINARY: return "B";
                case SECS_FORMAT.BOOLEAN: return "Boolean";
                case SECS_FORMAT.ASCII: return "A";
                case SECS_FORMAT.JIS8: return "J";
                case SECS_FORMAT.I8: return "I8";
                case SECS_FORMAT.I1: return "I1";
                case SECS_FORMAT.I2: return "I2";
                case SECS_FORMAT.I4: return "I4";
                case SECS_FORMAT.F8: return "F8";
                case SECS_FORMAT.F4: return "F4";
                case SECS_FORMAT.U8: return "U8";
                case SECS_FORMAT.U1: return "U1";
                case SECS_FORMAT.U2: return "U2";
                default/* SecsFormat.U4 */: return "U4";
            }
        }
   
    }
}
