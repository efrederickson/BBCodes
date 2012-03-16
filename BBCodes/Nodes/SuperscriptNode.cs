/*
 * User: elijah
 * Date: 3/15/2012
 * Time: 4:32 PM
 */
using System;

namespace BBCodes.Nodes
{
    /// <summary>
    /// Description of SuperscriptNode.
    /// </summary>
    public class SuperscriptNode : Node
    {
        public SuperscriptNode()
        {
        }
        
        public override string ToHTML()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (Node n in this)
                sb.Append(n.ToHTML());
            return "<sup>" + sb.ToString() + "</sup>";
        }
        
        public override string[] NodeNames {
            get {
                return new string[] { "sup" };
            }
        }
    }
}
