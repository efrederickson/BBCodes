/*
 * User: elijah
 * Date: 3/15/2012
 * Time: 3:54 PM
 */
using System;
using System.Text;

namespace BBCodes.Nodes
{
    /// <summary>
    /// Description of TableHeaderNode.
    /// </summary>
    public class TableHeaderNode : Node
    {
        public TableHeaderNode()
        {
        }
        
        public override string ToHTML()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Node n in this)
                sb.Append(n.ToHTML());
            return "<th>" + sb.ToString() + "</th>";
        }
        
        public override string[] NodeNames {
            get {
                return new string[] { "th" };
            }
        }
    }
}
