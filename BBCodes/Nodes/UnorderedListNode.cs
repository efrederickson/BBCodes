/*
 * User: elijah
 * Date: 3/14/2012
 * Time: 1:26 PM
 */
using System;
using System.Text;

namespace BBCodes.Nodes
{
    /// <summary>
    /// The [ul] tag
    /// </summary>
    public class UnorderedListNode : ListNode
    {
        public UnorderedListNode()
        {
        }
        
        public override string ToHTML()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<ul>");
            foreach (Node n in this) // should be [li] or [*] (ListItemNode)
                sb.Append(n.ToHTML());
            sb.Append("</ul>");
            return sb.ToString();
        }
    }
}
