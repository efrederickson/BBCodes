/*
 * User: elijah
 * Date: 3/13/2012
 * Time: 4:02 PM
 */
using System;

namespace BBCodes.Nodes
{
    /// <summary>
    /// The [u] node
    /// </summary>
    public class UNode : Node
    {
        public UNode()
        {
        }
        
        public override string ToHTML()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (Node n in this)
                sb.Append(n.ToHTML());
            return "<u>" + sb.ToString() + "</u>";
        }
    }
}
