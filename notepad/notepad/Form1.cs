using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
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
            openFileDialog1.DefaultExt = "txt";
            openFileDialog1.Filter = "File |*.txt";
            openFileDialog1.Title = "Open File";
            openFileDialog1.Multiselect = false;

        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (!isFileChanged)
            {
                isFileChanged = true;
                this.Text = "*" + this.Text;
            }
        }

        public void SaveDocument(object sender, EventArgs e)
        {
            if (fn == string.Empty)
            {

                if (saveFileDialog1.ShowDialog() ==
                                    DialogResult.OK)
                {
                    fn = saveFileDialog1.FileName;


                    this.Text = "Notepad | by Mihai Lungu   " + fn;
                }
            }
            isFileChanged = false;
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
            isFileChanged = false;
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
            isFileChanged = false;
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
            isFileChanged = false;

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isFileChanged = false;
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

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1 = new FontDialog();
            if (fontDialog1.ShowDialog() == DialogResult.OK & !String.IsNullOrEmpty(richTextBox1.Text))
            {
                richTextBox1.SelectionFont = fontDialog1.Font;
            }
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1 = new ColorDialog();
            if (colorDialog1.ShowDialog() == DialogResult.OK & !String.IsNullOrEmpty(richTextBox1.Text))
            {
                richTextBox1.SelectionColor = colorDialog1.Color;
            }
        }

        private void timeDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = richTextBox1.Text + DateTime.Now;
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Redo();
        }
        private int currentIndex = 0;
        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            Font font = richTextBox1.SelectionFont ?? richTextBox1.Font;
            Color color = richTextBox1.SelectionColor;
            float lineHeight = font.GetHeight(e.Graphics);
            float yPosition = e.MarginBounds.Top;
            int linesPerPage = (int)(e.MarginBounds.Height / lineHeight);
            int lineCount = 0;
            string[] lines = richTextBox1.Lines;

            while (lineCount < linesPerPage && currentIndex < lines.Length)
            {
                string line = lines[currentIndex];
                e.Graphics.DrawString(line, font, new SolidBrush(color), e.MarginBounds.Left, yPosition, new StringFormat());
                yPosition += lineHeight;
                lineCount++;
                currentIndex++;
            }

            if (currentIndex < lines.Length)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
                currentIndex = 0;
            }
        }
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(PrintPage);

            PrintDialog pdi = new PrintDialog();
            pdi.Document = pd;

            if (pdi.ShowDialog() == DialogResult.OK)
            {
                pd.Print();
            }

        }
    }
}
