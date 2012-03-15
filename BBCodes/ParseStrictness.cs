/*
 * User: elijah
 * Date: 3/13/2012
 * Time: 3:31 PM
 */
using System;

namespace BBCodes
{
    /// <summary>
    /// How the parser should handle errors
    /// </summary>
    [Serializable]
    public enum ParseStrictness
    {
        /// <summary>
        /// The parser will ignore all errors
        /// </summary>
        IgnoreErrors,
        
        /// <summary>
        /// The parser will hate having an error in the BBCode
        /// </summary>
        ThrowErrors
    }
}
