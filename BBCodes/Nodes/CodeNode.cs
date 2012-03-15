/*
 * User: elijah
 * Date: 3/13/2012
 * Time: 4:44 PM
 */
using System;
using System.Text;

namespace BBCodes.Nodes
{
    /// <summary>
    /// Description of CodeNode.
    /// </summary>
    public class CodeNode : Node
    {
        public CodeNode()
        {
        }
        
        public override string ToHTML()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Node n in this)
                sb.Append(n.ToHTML());
            return "<code>" + sb.ToString() + "</code>";
        }
        
        public override string[] NodeNames {
            get {
                return new string[]  {"code"};
            }
        }
    }
}
