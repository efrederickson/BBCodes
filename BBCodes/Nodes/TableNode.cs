/*
 * User: elijah
 * Date: 3/15/2012
 * Time: 3:52 PM
 */
using System;
using System.Text;

namespace BBCodes.Nodes
{
    /// <summary>
    /// Description of TableNode.
    /// </summary>
    public class TableNode : Node
    {
        public TableNode()
        {
        }
        
        public override string ToHTML()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Node n in this)
                sb.Append(n.ToHTML());
            return "<table>" + sb.ToString() + "</table>";
        }
        
        public override string[] NodeNames {
            get {
                return new string[] { "table" };
            }
        }
    }
}
