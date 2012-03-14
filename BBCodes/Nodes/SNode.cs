/*
 * User: elijah
 * Date: 3/13/2012
 * Time: 3:54 PM
 */
using System;

namespace BBCodes.Nodes
{
    /// <summary>
    /// The [s] node
    /// </summary>
    public class SNode : Node
    {
        public SNode()
        {
        }
        
        public override string ToHTML()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (Node n in this)
                sb.Append(n.ToHTML());
            return "<s>" + sb.ToString() + "</s>";
        }
    }
}
