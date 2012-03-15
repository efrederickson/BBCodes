/*
 * User: elijah
 * Date: 3/15/2012
 * Time: 2:04 PM
 */
using System;

namespace BBCodes
{
    /// <summary>
    /// Random crap, used in the BBCodeParser
    /// </summary>
    static class StringExtensions
    {
        /// <summary>
        /// Returns the number of times that the specified char is found
        /// </summary>
        /// <param name="s"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static int CountOf(this string s, char c)
        {
            int count = 0;
            foreach (char c2 in s)
                if (c2 == c)
                    count += 1;
            return count;
        }
    }
}
