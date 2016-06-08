using System.Web.Mvc;

namespace Cibb.Web.HtmlHelpers
{
    public static class NewLine
    {
        public static string changeLine(this HtmlHelper html, string title)
        {
            if (title.IndexOf(" ") < 0)
                return title;
            else
                return title.Replace(" ", "<br/>");
        }
    }
}
