/*
 * User: elijah
 * Date: 3/15/2012
 * Time: 1:59 PM
 */
using System;
using System.Collections.Generic;

namespace BBCodes.Visitors
{
    /// <summary>
    /// Description of IGenerator.
    /// </summary>
    public interface IGenerator
    {
        string Generate(List<Nodes.Node> nodes);
    }
}
