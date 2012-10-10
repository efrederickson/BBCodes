/*
 * User: elijah
 * Date: 10/10/2012
 * Time: 10:40 AM
 * Copyright 2012 LoDC
 */
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BBCodes
{
    /// <summary>
    /// Description of NodeList.
    /// </summary>
    [Serializable]
    public class NodeList
    {
        private List<Nodes.Node> nodes = new List<Nodes.Node>();
        
        public NodeList()
        {
        }
        
        public void Add<T>(T node) where T : Nodes.Node, new()
        {
            nodes.Add(node);
        }
        
        public ReadOnlyCollection<Nodes.Node> Nodes
        {
            get
            {
                return nodes.AsReadOnly();
            }
        }
    }
}
