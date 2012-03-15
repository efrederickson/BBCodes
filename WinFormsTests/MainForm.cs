/*
 * User: elijah
 * Date: 3/13/2012
 * Time: 4:45 PM
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BBCodes;
using BBCodes.Nodes;
using BBCodes.Visitors;

namespace WinFormsTests
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        public MainForm()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            textBox1.Text = @"[img width=50 height=40]http://google.com/favicon.ico[/img]";
        }
        
        void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                BBCode.Strictness = ParseStrictness.IgnoreErrors;
                webBrowser1.DocumentText = BBCode.Parse(textBox1.Text);
                
                BBCodeParser bParser = new BBCodeParser(true);
                bParser.Strictness = ParseStrictness.IgnoreErrors;
                bParser.Parse(textBox1.Text);
                IGenerator gen = new BBCodes.Visitors.CodeGenerator();
                //IGenerator gen = new XmlTreeGenerator().Generate(bParser.Output);
                textBox1.Text = gen.Generate(bParser.Output);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }
        
        string GenTree(Node p)
        {
            StringBuilder s = new StringBuilder();
            s.Append(p.GetType());
            foreach (Node n in p)
            {
                s.Append(n.GetType().FullName);
                if (n.InnerNodes.Count != 0)
                    s.Append(GenTree(n));
            }
            return s.ToString();
        }
    }
}
