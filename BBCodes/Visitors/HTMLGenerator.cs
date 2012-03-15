/*
 * User: elijah
 * Date: 3/15/2012
 * Time: 3:09 PM
 */
using System;
using System.Text;

namespace BBCodes.Visitors
{
    /// <summary>
    /// Generates HTML
    /// </summary>
    public class HTMLGenerator : IGenerator
    {
        /// <summary>
        /// You could just use the ToHTML on the BBCodeParser class, or the BBCode class.
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public string Generate(System.Collections.Generic.List<BBCodes.Nodes.Node> nodes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Nodes.Node n in nodes)
                sb.Append(n.ToHTML());
            return sb.ToString();
        }
    }
}
