/*
 * User: elijah
 * Date: 3/15/2012
 * Time: 3:19 PM
 */
using System;
using System.Text;
using BBCodes.Nodes;

namespace BBCodes.Visitors
{
    /// <summary>
    /// Generates XML for a BBCode AST
    /// </summary>
    public class XMLGenerator : IGenerator
    {
        StringBuilder sb = new StringBuilder();
        int indent = 0;
        
        public string Generate(System.Collections.Generic.List<BBCodes.Nodes.Node> nodes)
        {
            sb.Clear();
            sb.AppendLine("<BBCode>");
            indent++;
            foreach (Nodes.Node n in nodes)
                Visit(n);
            sb.AppendLine("</BBCode>");
            return sb.ToString();
        }
        
        void Visit(Node n)
        {
            if (n is TextNode)
            {
                sb.Append(n.ToHTML());
                return;
            }
            Rep();
            sb.Append("<" + n.NodeNames[0]);
            if (n.Arguments.Count != 0)
            {
                foreach (Tuple<string, string> t in n.Arguments)
                {
                    sb.Append(" " + t.Item1 + "=\"" + t.Item2 + "\"");
                }
            }
            
            if (n.InnerNodes.Count == 0)
            {
                sb.AppendLine(" />");
            }
            else
            {
                sb.Append(">");
                indent++;
                foreach (Node n2 in n.InnerNodes)
                {
                    Visit(n2);
                }
                indent--;
                sb.AppendLine("</" + n.NodeNames[0] + ">");
            }
        }
        
        void Rep()
        {
            for (int i = 0; i < indent; i++)
                sb.Append("    ");
        }
    }
}
