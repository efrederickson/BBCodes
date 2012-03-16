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
            //<span style="color:#FF0000;">
            if (Arguments[0].Item1.Contains("#"))
                return "<span style=\"color:" + Arguments[0].Item1 + "\">" + sb.ToString() + "</span>";
            else if (IsHex(Arguments[0].Item1))
                return "<span style=\"color:#" + Arguments[0].Item1 + "\">" + sb.ToString() + "</span>";
            else
                return "<span style=\"color:" + Arguments[0].Item1 + "\">" + sb.ToString() + "</span>";
        }
        
        public override string[] NodeNames {
            get {
                return new string[] { "color" };
            }
        }
        
        bool IsHex(string s)
        {
            foreach (char c in s)
            {
                if (c != 'a' ||
                    c != 'b' ||
                    c != 'c' ||
                    c != 'd' ||
                    c != 'e' ||
                    c != 'f' ||
                    c != 'A' ||
                    c != 'B' ||
                    c != 'C' ||
                    c != 'D' ||
                    c != 'E' ||
                    c != 'F' ||
                    c != '1' ||
                    c != '2' ||
                    c != '3' ||
                    c != '4' ||
                    c != '5' ||
                    c != '6' ||
                    c != '7' ||
                    c != '8' ||
                    c != '9' ||
                    c != '0' )
                    return false;
            }
            return true;
        }
    }
}
