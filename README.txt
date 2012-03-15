BBCodes is a library that provides an easy way to use BBCode.

Features
 - Easily extensible AST type
 - Multiple options to customize parser, such as how to treat syntax errors
 - Also has an emoji/smiley parser!
 - Makes it easy to refactor BBCode, with support for outputing the AST into 
   1. HTML (builtin to parser and Nodes)
   2. BBCode (yes, it can re-create BBCode from the Nodes. Useful for refactoring)
   3. Xml Tree (XML representation of the BBCode AST)
   4. XML (an xml representation)
 - Allows you to write web pages in BBCode
 - Small
   - Only reference is on System.Web for the web handler
   - Compiled DLL is under 50 KB
 - more coming soon!


To Use the WebHandler:
Create a new ASP.NET web application project.
Change the web.config to include the following within <system.web>:

<httpHandlers>
<add verb="*" path="*.bbc" type="BBCodes.BBCodeWebHandler,BBCodes"/>
<!-- do this for all BBCode page types that you want to use, such as bbc, bbcode, etc -->
</httpHandlers>

Then add a reference to the BBCodes.dll file,
and finally write your BBCode page as a text file with a *.bbc or whatever extension you have specified

Copyright (C) 2012 mlnlover11 Productions under the WTFPL license.