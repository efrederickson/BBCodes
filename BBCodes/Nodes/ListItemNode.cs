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
    /// Description of ListItemNode.
    /// </summary>
    public class ListItemNode : Node
    {
        public ListItemNode()
        {
        }
        
        public override string ToHTML()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Node n in this)
                sb.Append(n.ToHTML());
            return "<li>" + sb.ToString() + "</li>";
        }
    }
}
