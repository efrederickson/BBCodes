/*
 * User: elijah
 * Date: 3/15/2012
 * Time: 4:03 PM
 */
using System;
using System.Text;

namespace BBCodes.Nodes
{
    /// <summary>
    /// Description of GoogleVideoNode.
    /// </summary>
    public class GoogleVideoNode : Node
    {
        public GoogleVideoNode()
        {
        }
        
        public override string ToHTML()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Node n in this)
                sb.Append(n.ToHTML());
            return @"<embed style=""width:400px; height:325px;"" id=""VideoPlayback"" type=""application/x-shockwave-flash"" src=""http://video.google.com/googleplayer.swf?docId=" + sb.ToString() + @"&hl=en""></embed>";
            //<embed style="width:400px; height:325px;" id="VideoPlayback" type="application/x-shockwave-flash" src="http://video.google.com/googleplayer.swf?docId=3966673435136338279&hl=en"></embed>
        }
        
        public override string[] NodeNames {
            get {
                return new string[] { "gvideo" };
            }
        }
    }
}
