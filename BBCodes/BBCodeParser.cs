/*
 * User: elijah
 * Date: 3/13/2012
 * Time: 3:24 PM
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BBCodes.Nodes;

namespace BBCodes
{
    /// <summary>
    /// The BBCode Parser
    /// </summary>
    public class BBCodeParser
    {
        public readonly List<Tuple<string, Node>> Nodes = new List<Tuple<string, Node>>();
        public List<Node> Output = new List<Node>();
        public ParseStrictness Strictness = ParseStrictness.ThrowErrors;
        public bool InterpretEscapes = true;
        int index = 0;
        
        public BBCodeParser()
        {
            
        }
        
        /// <summary>
        /// Creates a parser with the default BBCode tags
        /// </summary>
        /// <param name="generateDefaultTags"></param>
        public BBCodeParser(bool generateDefaultTags)
        {
            Nodes.Add(new Tuple<string, Node>("b", new BNode()));
            Nodes.Add(new Tuple<string, Node>("i", new INode()));
            Nodes.Add(new Tuple<string, Node>("s", new SNode()));
            Nodes.Add(new Tuple<string, Node>("u", new UNode()));
            Nodes.Add(new Tuple<string, Node>("url", new URLNode()));
            Nodes.Add(new Tuple<string, Node>("img", new ImageNode()));
            Nodes.Add(new Tuple<string, Node>("quote", new QuoteNode()));
            Nodes.Add(new Tuple<string, Node>("code", new CodeNode()));
            Nodes.Add(new Tuple<string, Node>("youtube", new YoutubeNode()));
            Nodes.Add(new Tuple<string, Node>("ol", new OrderedListNode()));
            Nodes.Add(new Tuple<string, Node>("ul", new UnorderedListNode()));
            Nodes.Add(new Tuple<string, Node>("list", new UnorderedListNode()));
            Nodes.Add(new Tuple<string, Node>("*", new ListItemNode()));
            Nodes.Add(new Tuple<string, Node>("li", new ListItemNode()));
        }
        
        /// <summary>
        /// Parses the bbcode text into the Node AST
        /// </summary>
        /// <param name="bbcode"></param>
        public void Parse(string bbcode)
        {
            bool isInNode = false;
            bool isInList = false;
            StringReader sr = new StringReader(bbcode);
            int nIndex = -1; // UGLY HAX
            List<Node> nodes = new List<Node>();
            
            while (sr.Peek() != -1)
            {
                char c = (char)sr.Read();
                index++;
                if (c == '[')
                {
                    
                    // handle bbcode node
                    
                    if ((char)sr.Peek() == '/')
                    {
                        sr.Read(); // read '/'
                        index++;
                        // read to end of node
                        string nName = "";
                        while (true)
                        {
                            c = (char)sr.Read();
                            index++;
                            if (c == ']')
                            {
                                break;
                            }
                            else
                            {
                                nName += c;
                            }
                        }
                        //Node nToRemove = FindLastNodeFromName(nodes, nName);
                        //int index2 = nodes.IndexOf(nToRemove);
                        //isInNode = false;
                        //if (index2 == 0)
                        //    Output.Add(nodes[index2]);
                        //else
                        //    nodes[index2 - 1].InnerNodes.Add(nodes[index2]);
                        //nodes.RemoveAt(index2);
                        
                        isInNode = false;
                        if (nIndex == 0)
                            Output.Add(nodes[nIndex]);
                        else
                            nodes[nIndex - 1].InnerNodes.Add(nodes[nIndex]);
                        nodes.RemoveAt(nIndex--);
                    }
                    else
                    {
                        // start a node
                        
                        string nodeName = "";
                        string nodeArg = "";
                        bool isInNodeName = true;
                        
                        while (true)
                        {
                            c = (char)sr.Read();
                            index++;
                            
                            if (c == '=')
                                isInNodeName = false;
                            else if (c == ']')
                                break;
                            else
                            {
                                if (isInNodeName)
                                    nodeName += c;
                                else
                                    nodeArg += c;
                            }
                        }
                        
                        Node n = CreateNodeFromName(nodeName);
                        if (n == null)
                            continue;
                        
                        if (isInList)
                        {
                            if (nodes[nIndex].GetType() == typeof(ListItemNode) && n.GetType() == typeof(ListItemNode))
                            {
                                Node n2 = FindLastNode(nodes, typeof(ListNode));
                                int index2 = nodes.IndexOf(n2);
                                nodes[index2].InnerNodes.Add(nodes[index2]);
                                nodes.RemoveAt(index2);
                            }
                        }
                        
                        nodes.Add(n);
                        nIndex++;
                        
                        if (string.IsNullOrEmpty(nodeArg.Trim()))
                        {
                            // ... do nothing
                        }
                        else
                        {
                            // TODO: such as height=10 width=20
                            nodes[nIndex].Arguments.Add(new Tuple<string, string>(nodeArg, nodeArg));
                        }
                        isInNode = true;
                        if (n.GetType() == typeof(ListNode))
                            isInList = true;
                    }
                }
                else if (c == '\\' && this.InterpretEscapes)
                {
                    string s = Unescape(ref sr);
                    if (isInNode)
                    {
                        if (nodes[nIndex] == null)
                            continue;
                        // append to the text
                        nodes[nIndex].InnerNodes.Add(new TextNode(c.ToString()));
                        
                    }
                    else
                    {
                        // add text node
                        Output.Add(new TextNode() { Text = c.ToString() });
                    }
                }
                else
                {
                    if (isInNode)
                    {
                        if (nodes.Count <= nIndex || nodes[nIndex] == null)
                            continue;
                        // append to the text
                        nodes[nIndex].InnerNodes.Add(new TextNode(c.ToString()));
                        
                    }
                    else
                    {
                        // add text node
                        Output.Add(new TextNode() { Text = c.ToString() });
                    }
                }
            }
            // cuz indexes start at 0, it needs to be -1
            if (nIndex != -1)
            {
                HandleError("Unterminated BBCode: " + nodes[0].GetType().Name);
                foreach (Node n in nodes)
                    Output.Add(n);
            }
        }
        
        string Unescape(ref StringReader sr)
        {
            if (sr.Peek() == -1)
            {
                HandleError("Unexpected end of stream!");
                return "\\";
            }
            string ret = "";
            char c = (char)sr.Read();
            index++;
            switch (c) {
                case 'n':
                    ret = "\n";
                    break;
                case '[':
                    ret = "[";
                    break;
                case ']':
                    ret = "]";
                    break;
                case 'r':
                    ret = "\r";
                    break;
                case 't':
                    ret = "\t";
                    break;
                default:
                    ret = "\\" + c;
                    break;
            }
            return ret;
        }
        
        Node CreateNodeFromName(string nodeName)
        {
            Node ret = null;
            
            foreach (Tuple<string, Node> t in this.Nodes)
            {
                if (t.Item1.ToLower() == nodeName.ToLower())
                {
                    ret = (Node)t.Item2.GetType().GetConstructors()[0].Invoke(new object[0]);
                    //new object[] { new string[] { arg }});
                }
            }
            if (ret == null)
                HandleError("No node found for type '" + nodeName + "'!");
            return ret;
        }
        
        Node FindLastNode(List<Node> nodes, Type nodetype)
        {
            nodes.Reverse();
            foreach (Node n in nodes)
                if (n.GetType() == nodetype)
                    return n;
            HandleError("Could not find node of type '" + nodetype.Name + "'");
            return null;
        }
        
        Node FindLastNodeFromName(List<Node> nodes, string name)
        {
            nodes.Reverse();
            foreach (Node n in nodes)
            {
                foreach (Tuple<string, Node> t in Nodes)
                {
                    Type nType = t.Item2.GetType();
                    if (n.GetType() == nType && t.Item1 == name)
                        return n;
                }
            }
            HandleError("Could find a node with name '" + name + "'");
            return null;
        }
        
        void HandleError(string message)
        {
            switch (Strictness) {
                case ParseStrictness.IgnoreErrors:
                    // ...
                    break;
                case ParseStrictness.ThrowErrors:
                    // ERROR TIME :D
                    throw new Exception(message);
                default:
                    throw new Exception("Invalid value for ParseStrictness");
            }
        }
        
        /// <summary>
        /// Generates HTML for the parsed BBCode.
        /// Doesn't add &lt;html&gt; or &lt;body&gt;
        /// </summary>
        /// <returns></returns>
        public string ToHTML()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Node n in Output)
            {
                sb.Append(n.ToHTML());
            }
            return sb.ToString();
        }
    }
}