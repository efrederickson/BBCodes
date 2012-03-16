/*
 * User: elijah
 * Date: 3/15/2012
 * Time: 3:56 PM
 */
using System;
using System.Text;

namespace BBCodes.Nodes
{
    /// <summary>
    /// Description of TableContentCellNode.
    /// </summary>
    public class TableContentCellNode : Node
    {
        public TableContentCellNode()
        {
        }
        
        public override string ToHTML()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Node n in this)
                sb.Append(n.ToHTML());
            return "<td>" + sb.ToString() + "</td>";
        }
        
        public override string[] NodeNames {
            get {
                return new string[] { "td" };
            }
        }
    }
}
