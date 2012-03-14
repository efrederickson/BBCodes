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
            
            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }
        
        void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                BBCodeParser p = new BBCodeParser(true);
                p.Strictness = ParseStrictness.IgnoreErrors;
                p.Parse(textBox1.Text);
                string t = p.ToHTML();
                //foreach (Node n in p.Output)
                //    t += GenTree(n);
                webBrowser1.DocumentText = t;
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
