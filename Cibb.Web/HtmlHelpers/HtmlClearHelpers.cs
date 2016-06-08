using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Cibb.Web.HtmlHelpers
{
    public static class HtmlClearHelpers
    {
        public static string HtmlClear(this HtmlHelper html, string htmlStream,int length)
        {
            if (htmlStream == null)
            {
                return string.Empty;
            }
            /*
             * 最好把所有的特殊HTML标记都找出来，然后把与其相对应的Unicode字符一起影射到Hash表内，最后一起都替换掉
             */
            //先单独测试,成功后,再把所有模式合并
            //注:这两个必须单独处理
            //去掉嵌套了HTML标记的JavaScript:(<script)[\\s\\S]*(</script>)
            //去掉css标记:(<style)[\\s\\S]*(</style>)
            //去掉css标记:\\..*\\{[\\s\\S]*\\}
            htmlStream = Regex.Replace(htmlStream, "(<script)[\\s\\S]*?(</script>)|(<style)[\\s\\S]*?(</style>)", " ", RegexOptions.IgnoreCase);
            //htmlStream = RemoveTag(htmlStream, "script");
            //htmlStream = RemoveTag(htmlStream, "style");

            //去掉普通HTML标记:<[^>]+>
            //替换空格:&nbsp;|&amp;|&shy;|&#160;|&#173;
            htmlStream = Regex.Replace(htmlStream, "<[^>]+>|&nbsp;|&amp;|&shy;|&#160;|&#173;|&bull;|&lt;|&gt;|&ldquo;|&rdquo;|&mdash;", " ", RegexOptions.IgnoreCase);
            //htmlStream = RemoveTag(htmlStream);

            //替换左尖括号
            //htmlStream = Regex.Replace(htmlStream, "&lt;", "<");

            //替换右尖括号
            //htmlStream = Regex.Replace(htmlStream, "&gt;", ">");

            //替换空行
            //htmlStream = Regex.Replace(htmlStream, "[\n|\r|\t]", " ");//[\n|\r][\t*| *]*[\n|\r]
            htmlStream = Regex.Replace(htmlStream, "(\r\n[\r|\n|\t| ]*\r\n)|(\n[\r|\n|\t| ]*\n)", "\r\n");
            htmlStream = Regex.Replace(htmlStream, "[\t| ]{1,}", " ");
            if (length != 0)
            {
                if (htmlStream.Length > length)
                    htmlStream = htmlStream.Substring(0, length);
            }
            return htmlStream.Trim();

        }
        public static string Cut(this HtmlHelper html, string htmlStream, int length) 
        {
            Regex reg = new Regex(@"[A-Za-z0-9\s]",RegexOptions.Compiled);
            MatchCollection mc=reg.Matches(htmlStream);

            char[] stringChar = htmlStream.ToCharArray();
            StringBuilder sb = new StringBuilder();
            int nLength = 0;

            for (int i = 0; i < stringChar.Length; i++) {
                if (reg.IsMatch((stringChar[i]).ToString()))
                {
                    nLength += 1;
                }
                else {
                    nLength += 2;
                }
                if (nLength <= length)
                {
                    sb.Append(stringChar[i]);
                }
                else
                {
                    break;
                }
            }
            
            return sb.ToString();
        }
    }
}