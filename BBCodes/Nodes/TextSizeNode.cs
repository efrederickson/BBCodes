/*
 * User: elijah
 * Date: 3/15/2012
 * Time: 2:48 PM
 */
using System;
using System.Text;

namespace BBCodes.Nodes
{
    /// <summary>
    /// the [size=X] node
    /// </summary>
    public class TextSizeNode : Node
    {
        public TextSizeNode()
        {
        }
        
        public override string ToHTML()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Node n in this)
                sb.Append(n.ToHTML());
            return "<font size=\"" + Arguments[0].Item1 + "\">" + sb.ToString() + "</font>";
        }
        
        public override string[] NodeNames {
            get {
                return new string[] { "size" };
            }
        }
    }
}
