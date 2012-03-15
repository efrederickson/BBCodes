/*
 * User: elijah
 * Date: 3/13/2012
 * Time: 4:39 PM
 */
using System;

namespace BBCodes.Nodes
{
    /// <summary>
    /// The [img] node
    /// </summary>
    public class ImageNode : Node
    {
        public ImageNode()
        {
        }
        
        public override string ToHTML()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (Node n in this)
                sb.Append(n.ToHTML());
            // basic IMG only
            return "<img src=\"" + sb.ToString() + "\" alt=\"IMAGE\" />";
        }
        
        public override string[] NodeNames {
            get {
                return new string[]  {"img"};
            }
        }
    }
}
