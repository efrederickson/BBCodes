/*
 * User: elijah
 * Date: 3/15/2012
 * Time: 3:06 PM
 */
using System;
using System.Web;
using System.Web.SessionState;

namespace BBCodes
{
    /// <summary>
    /// A handler that allows BBCode to be used as web pages
    /// </summary>
    public class BBCodeWebHandler : IHttpHandler, IRequiresSessionState
    {
        /// <summary>
        /// This handler is reusable
        /// </summary>
        public bool IsReusable
        {
            get { return true; }
        }

        /// <summary>
        /// Processes an HTTP Web request for a BBCode file
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            try
            {
                context.Response.Write(BBCode.Parse(System.IO.File.ReadAllText(context.Request.PhysicalPath)));
            }
            catch (Exception e)
            {
                context.Response.Write("<h1>Error Loading BBCode File " + context.Request.PhysicalPath + "</h1><br />");
                context.Response.Write("<br /> <font color='red'>");
                context.Response.Write(e.ToString().Replace("\n", "<br />"));
                context.Response.Write("</font>");
            }
        }
    }
}
