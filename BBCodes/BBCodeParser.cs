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
    [Serializable]
    public class BBCodeParser
    {
        public NodeList Nodes = new NodeList();
        public List<Node> Output = new List<Node>();
        public ParseStrictness Strictness = ParseStrictness.ThrowErrors;
        public bool InterpretEscapedCharacters = false;
        int index = 0;
        
        /// <summary>
        /// Creates a parser with default tags
        /// </summary>
        public BBCodeParser()
            : this(true)
        {
            
        }
        
        /// <summary>
        /// Creates a parser with the default BBCode tags
        /// </summary>
        /// <param name="generateDefaultTags"></param>
        public BBCodeParser(bool generateDefaultTags)
        {
            if (generateDefaultTags)
            {
                Nodes.Add(new BNode());
                Nodes.Add(new INode());
                Nodes.Add(new SNode());
                Nodes.Add(new UNode());
                Nodes.Add(new URLNode());
                Nodes.Add(new ImageNode());
                Nodes.Add(new QuoteNode());
                Nodes.Add(new CodeNode());
                Nodes.Add(new YoutubeNode());
                Nodes.Add(new OrderedListNode());
                Nodes.Add(new UnorderedListNode());
                Nodes.Add(new ListItemNode());
                Nodes.Add(new TextSizeNode());
                Nodes.Add(new TextColorNode());
                Nodes.Add(new CenterNode());
                Nodes.Add(new TableNode());
                Nodes.Add(new TableHeaderNode());
                Nodes.Add(new TableCellNode());
                Nodes.Add(new TableContentCellNode());
                Nodes.Add(new GoogleVideoNode());
                Nodes.Add(new EmailNode());
                Nodes.Add(new SubscriptNode());
                Nodes.Add(new SuperscriptNode());
            }
        }
        
        /// <summary>
        /// Parses the bbcode text into the Node AST
        /// </summary>
        /// <param name="bbcode"></param>
        public void Parse(string bbcode)
        {
            Output.Clear();
            
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
                        bool found = false;
                        foreach (string n in nodes[nodes.Count - 1].NodeNames)
                            if (n.ToLower() == nName.ToLower())
                                found = true;
                        if (found == false)
                            HandleError("Expected [/" + nodes[nodes.Count - 1].NodeNames[0] + "] not [/" + nName + "]");
                        /*
                        Node nToRemove = FindLastNodeFromName(nodes, nName);
                        int index2 = nodes.IndexOf(nToRemove);
                        if (index2 == 0)
                            Output.Add(nodes[index2]);
                        else
                            nodes[index2 - 1].InnerNodes.Add(nodes[index2]);
                        nodes.RemoveAt(index2);
                         */
                        if (nIndex == -1)
                        {
                            HandleError("End node '" + nName + "' found before any nodes were started");
                            continue;
                        }
                        
                        if (nIndex != -1 && nodes[nIndex] is ListNode)
                            isInList = false;
                        if (nIndex == 0)
                            Output.Add(nodes[nIndex]);
                        else
                            nodes[nIndex - 1].InnerNodes.Add(nodes[nIndex]);
                        nodes.RemoveAt(nIndex--);
                        
                        
                        if (nodes.Count == 0)
                            isInNode = false;
                    }
                    else
                    {
                        // start a node
                        
                        string fullNodeText = "";
                        string nodeName = "";
                        List<Tuple<string, string>> nodeArgs = new List<Tuple<string, string>>();
                        bool isInNodeName = true;
                        
                        // read all the node text
                        while (true)
                        {
                            if (sr.Peek() == -1)
                            {
                                HandleError("Unexpected End Of Stream");
                                break;
                            }
                            c = (char)sr.Read();
                            index++;
                            
                            if (c == ']')
                                break;
                            else
                                fullNodeText += c;
                            
                        }
                        // would be like [url=http://google.com] (1 arg)
                        if (fullNodeText.CountOf('=') >= 1 && fullNodeText.Contains(" ") == false)
                        {
                            string nArg = "";
                            foreach (char c2 in fullNodeText)
                            {
                                if (c2 == '=')
                                    isInNodeName = false;
                                else
                                {
                                    if (isInNodeName)
                                        nodeName += c2;
                                    else
                                        nArg += c2;
                                }
                            }
                            nodeArgs.Add(new Tuple<string, string>(nArg, nArg));
                        } // would be like [img width=10 height=20]
                        else if (fullNodeText.CountOf('=') > 1 && fullNodeText.Contains(" "))
                        {
                            nodeName = fullNodeText.Substring(0, fullNodeText.IndexOf(' ')).Trim();
                            fullNodeText = fullNodeText.Substring(fullNodeText.IndexOf(' ')).Trim();
                            isInNodeName = false;
                            
                            string arg = "", val = "";
                            bool isInVal = false;
                            foreach (char c2 in fullNodeText)
                            {
                                if (c2 == ' ')
                                {
                                    nodeArgs.Add(new Tuple<string, string>(arg, val));
                                    arg = "";
                                    val = "";
                                    isInVal = false;
                                }
                                else if (isInVal)
                                {
                                    val += c2;
                                }
                                else
                                {
                                    // e.g. http://someurl.com/index.bbc?val=pie
                                    if (c2 == '=' && isInVal)
                                        val += c2;
                                    else if (c2 == '=' && isInVal == false)
                                        isInVal = true;
                                    else
                                        arg += c2;
                                }
                            }
                            if (arg.Trim() != "" && val.Trim() != "")
                                nodeArgs.Add(new Tuple<string, string>(arg, val));
                        }
                        else
                        {
                            nodeName = fullNodeText;
                        }
                        
                        Node n = CreateNodeFromName(nodeName);
                        if (n == null)
                            continue;
                        
                        if (isInList)
                        {
                            if (nodes[nIndex] is ListItemNode && n is ListItemNode)
                            {
                                Node n2 = FindLastNode(nodes, typeof(ListNode));
                                int index2 = nodes.IndexOf(n2);
                                Node tmpNode = nodes[index2 + 1];
                                nodes[index2].InnerNodes.Add(tmpNode);
                                nodes.RemoveAt(index2 + 1);
                                nIndex--;
                            }
                        }
                        
                        n.Arguments.AddRange(nodeArgs);
                        nodes.Add(n);
                        nIndex++;
                        isInNode = true;
                        if (n is ListNode)
                            isInList = true;
                    }
                }
                else if (c == '\\' && this.InterpretEscapedCharacters)
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
                    // text adding is now optimized to hold more than 1 char per TextNode.
                    if (isInNode)
                    {
                        if (nodes.Count <= nIndex || nodes[nIndex] == null)
                            continue;
                        // append to the text
                        if (nodes[nIndex].InnerNodes.Count != 0 && nodes[nIndex].InnerNodes[nodes[nIndex].InnerNodes.Count - 1] is TextNode)
                            (nodes[nIndex].InnerNodes[nodes[nIndex].InnerNodes.Count - 1] as TextNode).Text += c.ToString();
                        else
                            nodes[nIndex].InnerNodes.Add(new TextNode(c.ToString()));
                        
                    }
                    else
                    {
                        // add text node
                        if (Output.Count != 0 && Output[Output.Count - 1] is TextNode)
                            (this.Output[Output.Count - 1] as TextNode).Text += c.ToString();
                        else
                            Output.Add(new TextNode() { Text = c.ToString() });
                    }
                }
            }
            // cuz indexes start at 0, it needs to be -1
            if (nodes.Count != 0)//(nIndex != -1)
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
            
            foreach (Node t in this.Nodes.Nodes)
            {
                foreach (string nName in t.NodeNames)
                {
                    if (nName.ToLower().Trim() == nodeName.ToLower().Trim())
                    {
                        ret = (Node)t.GetType().GetConstructors()[0].Invoke(new object[0]);
                        //new object[] { new string[] { arg }});
                    }
                }
            }
            if (ret == null)
                HandleError("No node found for type '" + nodeName + "'!");
            return ret;
        }
        
        Node FindLastNode(List<Node> nodes, Type nodetype)
        {
            for (int i = 0; i < nodes.Count; i++)//(Node n in nodes)
            {
                Node n = nodes[i];
                if (n.GetType() == nodetype || n.GetType().IsSubclassOf(nodetype))
                    return n;
            }
            return null;
        }
        
        Node FindLastNodeFromName(List<Node> nodes, string name)
        {
            nodes.Reverse();
            foreach (Node n in nodes)
            {
                foreach (Node t in Nodes.Nodes)
                {
                    Type nType = t.GetType();
                    bool foundName = false;
                    foreach (string nName in t.NodeNames)
                        if (nName.ToLower().Trim() == name.ToLower().Trim())
                            foundName = true;
                    //if (n.GetType() == nType && foundName)
                    if (foundName)
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
        /// Doesn't add &lt;html&gt; or &lt;body&gt;...
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