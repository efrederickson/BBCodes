/*
 * User: elijah
 * Date: 3/15/2012
 * Time: 4:33 PM
 */
using System;

namespace BBCodes.Nodes
{
    /// <summary>
    /// Description of EmailNode.
    /// </summary>
    public class EmailNode : Node
    {
        public EmailNode()
        {
        }
        
        public override string ToHTML()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (Node n in this)
                sb.Append(n.ToHTML());
            return "<a href=\"mailto:" + Arguments[0].Item1 + "\">" + sb.ToString() + "</a>";
        }
        
        public override string[] NodeNames {
            get {
                return new string[] { "email" };
            }
        }
    }
}
