/*
 * User: elijah
 * Date: 3/15/2012
 * Time: 9:19 AM
 */
using System;
using BBCodes.Nodes;

namespace BBCodes
{
    /// <summary>
    /// Static way to manage BBCode and Smiley Parsers
    /// </summary>
    public class BBCode
    {
        private BBCode()
        {
        }
        
        static BBCodeParser bParser = new BBCodeParser(true);
        static SmileyParser sParser = new SmileyParser(true);
        
        /// <summary>
        /// How the BBCode parser should handle errors
        /// </summary>
        public static ParseStrictness Strictness
        {
            get
            {
                return bParser.Strictness;
            }
            set
            {
                bParser.Strictness = value;
            }
        }
        
        /// <summary>
        /// Whether the BBCode Parser should interpret escapes or not
        /// </summary>
        public static bool ParseEscapedCharacters
        {
            get
            {
                return bParser.InterpretEscapedCharacters;
            }
            set
            {
                bParser.InterpretEscapedCharacters = value;
            }
        }
        
        /// <summary>
        /// Parses the string through both BBCode and Smiley parsers
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string Parse(string code)
        {
            bParser.Output.Clear();
            bParser.Parse(code);
            string ret = bParser.ToHTML();
            ret = sParser.Parse(ret);
            return ret;
        }
        
        /// <summary>
        /// Adds an emoji/smiley to the smiley parser
        /// </summary>
        /// <param name="emoji"></param>
        /// <param name="imgurl"></param>
        public static void AddSmiley(string emoji, string imgurl)
        {
            sParser.Smileys.Add(new Tuple<string, string>(emoji, imgurl));
        }
        
        /// <summary>
        /// Adds a parsable node to the BBCode parser
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="n"></param>
        public static void AddBBCodeNode(string nodeName, Node n)
        {
            bParser.Nodes.Add(n);
        }
        
        /// <summary>
        /// Escapes string for suitable use in the BBCode parser
        /// </summary>
        /// <param name="old"></param>
        /// <returns></returns>
        public static string Escape(string old)
        {
            return old.Replace("\\", "\\\\").Replace("[", "\\[").Replace("]", "\\]");
        }
        
        /// <summary>
        /// Unescapes string for use in BBCode parser
        /// </summary>
        /// <param name="old"></param>
        /// <returns></returns>
        public static string Unescape(string old)
        {
            return old.Replace("\\\\", "\\").Replace("\\[", "[").Replace("\\]", "]");
        }
    }
}
