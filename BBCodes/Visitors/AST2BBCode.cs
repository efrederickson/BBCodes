/*
 * User: elijah
 * Date: 3/15/2012
 * Time: 10:04 AM
 */
using System;
using System.Collections.Generic;
using System.Text;
using BBCodes.Nodes;

namespace BBCodes.Visitors
{
    /// <summary>
    /// Converts a list of nodes back into BBCode
    /// </summary>
    public class AST2BBCode
    {
        StringBuilder Output = new StringBuilder();
        
        public AST2BBCode()
        {
        }
        
        void Visit(Node n)
        {
            if (n is TextNode)
            {
                // actually just text
                Output.Append(n.ToHTML());
            }
            else
            {
                // add teh nodes
                Output.Append("[" + n.NodeNames[0]);
                VisitArgs(n.Arguments);
                Output.Append("]");
                foreach (Node n2 in n)
                    Visit(n2);
                Output.Append("[/" + n.NodeNames[0] + "]");
            }
        }
        
        void VisitArgs(List<Tuple<string, string>> arguments)
        {
            if (arguments.Count == 0)
                return;
            if (arguments.Count == 1)
                Output.Append("=" + arguments[0].Item1);
            else
            {
                foreach (Tuple<string, string> arg in arguments)
                    Output.Append(" " + arg.Item1 + "=" + arg.Item2);
            }
        }
        
        public string ToBBCode(List<Node> nodes)
        {
            foreach (Node n in nodes)
            {
                Visit(n);
            }
            return Output.ToString();
        }
    }
}
