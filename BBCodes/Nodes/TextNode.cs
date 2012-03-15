/*
 * User: elijah
 * Date: 3/13/2012
 * Time: 3:30 PM
 */
using System;
using System.Collections.Generic;

namespace BBCodes.Nodes
{
    /// <summary>
    /// Description of TextNode.
    /// </summary>
    public class TextNode : BBCodes.Nodes.Node
    {
        public TextNode()
        {
            Text = "";
        }
        
        public TextNode(string texT)
        {
            Text = texT;
        }
        
        public string Text
        {get; set; }
        
        public override string ToHTML()
        {
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //foreach (Node n in this)
            //    sb.Append(n.ToHTML());
            //return sb.ToString();
            return Text;
        }
        
        public override string[] NodeNames {
            get {
                return new string[]  {""};
            }
        }
        
    }
}
