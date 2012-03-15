/*
 * User: elijah
 * Date: 3/15/2012
 * Time: 2:51 PM
 */
using System;

namespace BBCodes.Nodes
{
    /// <summary>
    /// The [color] node
    /// </summary>
    public class TextColorNode : Node
    {
        public TextColorNode()
        {
        }
        
        public override string ToHTML()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (Node n in this)
                sb.Append(n.ToHTML());
            
            return "<font color=\"" + Arguments[0].Item1 + "\">" + sb.ToString() + "</font>";
        }
        
        public override string[] NodeNames {
            get {
                return new string[] { "color" };
            }
        }
    }
}
