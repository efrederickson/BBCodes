/*
 * User: elijah
 * Date: 3/13/2012
 * Time: 4:51 PM
 */
using System;

namespace BBCodes.Nodes
{
    /// <summary>
    /// Description of YoutubeNode.
    /// </summary>
    public class YoutubeNode : Node
    {
        public YoutubeNode()
        {
        }
        
        public override string ToHTML()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (Node n in this)
                sb.Append(n.ToHTML());
            // TODO
            return "<video>http://youtube.com/watch?v=" + sb.ToString() + "</video>";
        }
        
        public override string[] NodeNames {
            get {
                return new string[]  {"youtube"};
            }
        }
    }
}
