/*
 * User: elijah
 * Date: 3/13/2012
 * Time: 4:42 PM
 */
using System;

namespace BBCodes.Nodes
{
    /// <summary>
    /// Description of QuoteNode.
    /// </summary>
    public class QuoteNode : Node
    {
        public QuoteNode()
        {
        }
        
        public override string ToHTML()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (Node n in this)
                sb.Append(n.ToHTML());
            
            if (Arguments.Count != 0)
                return "<blockquote cite=\"" + Arguments[0].Item1 + "\">" + sb.ToString() + "</blockquote>";
            else
                return "<blockquote>" + sb.ToString() + "</blockquote>";
        }
    }
}
