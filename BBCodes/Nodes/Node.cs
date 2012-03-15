/*
 * User: elijah
 * Date: 3/13/2012
 * Time: 3:25 PM
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace BBCodes.Nodes
{
    /// <summary>
    /// The BBCode base node
    /// </summary>
    public abstract class Node : IEnumerable<Node>
    {
        /// <summary>
        /// Resolves the node into HTML
        /// </summary>
        public abstract string ToHTML();
        
        public Node()
        {
            //this.InnerText = innertext;
            InnerNodes = new List<Node>();
            Arguments = new List<Tuple<string, string>>();
        }
        
        /// <summary>
        /// The internal text
        /// </summary>
        public List<Node> InnerNodes
        {
            get; set;
        }
        
        public List<Tuple<string, string>> Arguments
        { get; set; }
        
        public IEnumerator<Node> GetEnumerator()
        {
            return InnerNodes.GetEnumerator();
        }
        
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return InnerNodes.GetEnumerator();
        }
        
        public abstract string[] NodeNames
        {
            get;
        }
    }
}
