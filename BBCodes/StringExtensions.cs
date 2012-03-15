/*
 * User: elijah
 * Date: 3/15/2012
 * Time: 2:04 PM
 */
using System;

namespace BBCodes
{
    /// <summary>
    /// random crap
    /// </summary>
    static class StringExtensions
    {
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
