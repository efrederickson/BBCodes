/*
 * User: elijah
 * Date: 3/14/2012
 * Time: 1:33 PM
 */
using System;
using System.Collections.Generic;

namespace BBCodes
{
    /// <summary>
    /// Description of SmileyParser.
    /// </summary>
    public class SmileyParser
    {
        /// <summary>
        /// Contains the smiley, and the image URL
        /// </summary>
        public List<Tuple<string, string>> Smileys = new List<Tuple<string, string>>();
        
        public SmileyParser()
        {
        }
        
        public SmileyParser(bool createDefault)
        {
            if (createDefault)
            {
                
            }
        }
        
        public string Parse(string smiles)
        {
            foreach (Tuple<string, string> t in Smileys)
                smiles = smiles.Replace(t.Item1, "<img src=\"" + t.Item2 + "\" alt=\"" + t.Item1 + "\"/>");
            return smiles;
        }
    }
}
