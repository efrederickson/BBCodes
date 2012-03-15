/*
 * User: elijah
 * Date: 3/13/2012
 * Time: 3:26 PM
 */
using System;

namespace BBCodes.Nodes
{
    /// <summary>
    /// The [b] [/b] node
    /// </summary>
    public class BNode : Node
    {
        public BNode()
        {
            
        }
        
        public override string ToHTML()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (Node n in this)
                sb.Append(n.ToHTML());
            return "<b>" + sb.ToString() + "</b>";
        }
        
        public override string[] NodeNames {
            get {
                return new string[] {"b"};
            }
        }
    }
}
