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
            if (this.Arguments.Count == 0)
            {
                // basic IMG only
                return "<img src=\"" + sb.ToString() + "\" alt=\"" + sb.ToString() + "\" />";
            }
            else
            {
                string ret = "<img src=\"" + sb.ToString() + "\" ";
                foreach (Tuple<string, string> arg in Arguments)
                {
                    if (arg.Item1.ToLower().Trim() == "width")
                        ret += "width=" + arg.Item2 + " ";
                    else if (arg.Item1.ToLower().Trim() == "height")
                        ret += "height=" + arg.Item2 + " ";
                }
                return ret + " />";
            }
        }
        
        public override string[] NodeNames {
            get {
                return new string[]  {"img"};
            }
        }
    }
}
