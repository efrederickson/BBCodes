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
            //<iframe width="420" height="315" src="http://www.youtube.com/embed/QH2-TGUlwu4" frameborder="0" allowfullscreen></iframe>

            return "<iframe width=\"420\" height=\"315\" src=\"http://youtube.com/embed/" + sb.ToString() + "\" frameborder=\"0\" allowfullscreen></iframe>";
        }
        
        public override string[] NodeNames {
            get {
                return new string[]  {"youtube"};
            }
        }
    }
}
