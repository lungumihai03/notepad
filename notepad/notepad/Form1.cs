using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace notepad
{
   

    public partial class Form1 : Form
    {
        private string fn = string.Empty;
        public bool isFileChanged;

        public Form1()
        {
            InitializeComponent();
            saveFileDialog1.DefaultExt = "*.txt";
            saveFileDialog1.Filter = "Text File (*.txt)|*.txt| All files |*.*";
            saveFileDialog1.Title = "Salveaza documentul";
            //private System.Windows.Forms.OpenFileDialog openFileDialog1;
            
    }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (!isFileChanged)
            {
                isFileChanged = true;
                this.Text = "*" + this.Text;
            }
        }

        private void SaveDocument()
        {
            
        }


        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            
            if (fn == string.Empty)
            {
              
                if (saveFileDialog1.ShowDialog() ==
                                    DialogResult.OK)
                {
                    fn = saveFileDialog1.FileName;

                    
                    this.Text = "Notepad | by Mihai Lungu   " + fn ;
                }
            }

            if (fn != string.Empty)
                try
                {
                  
                    System.IO.FileInfo fi =
                        new System.IO.FileInfo(fn);

                    
                    System.IO.StreamWriter sw = fi.CreateText();

                   
                    sw.Write(richTextBox1.Text);

                   
                    sw.Close();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Eroare \n" +
                        exc.ToString(), "Notepad",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            this.Text = "Notepad | by Mihai Lungu   ";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2= new Form2();
            f2.ShowDialog();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            this.Text = "Notepad | by Mihai Lungu   ";
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = string.Empty;

            
            if (openFileDialog1.ShowDialog() ==
                                DialogResult.OK)
            {
                fn = openFileDialog1.FileName;

                this.Text ="Notepad | by Mihai Lungu   " + fn;

                try
                {
                    
                    System.IO.StreamReader sr =
                        new System.IO.StreamReader(fn);

                    richTextBox1.Text = sr.ReadToEnd();
                    richTextBox1.SelectionStart = richTextBox1.TextLength;

                    sr.Close();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Eroare in citirea fisierului.\n" +
                        exc.ToString(), "Notepad",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            
        }

        
    }
}
