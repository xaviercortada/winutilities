using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileSplitter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (File.Exists(openFileDialog1.FileName))
                {
                    textBox1.Text = openFileDialog1.FileName;
                }
                else
                {
                    textBox1.Text = "";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DoSplit();
        }

        private void DoSplit()
        {
            int i = 0;
            String oldFile = textBox1.Text;
            String newfile = getNewFilename(oldFile);
            StreamWriter writer = File.CreateText(newfile);
            using (StreamReader reader = File.OpenText(textBox1.Text))
            {
                String s = "";
                String separator = textBox2.Text;
                while (i < 50 && ((s = reader.ReadLine()) != null))
                {
                    if (!s.Equals(separator))
                    {
                        writer.WriteLine(s);
                    }else{
                        writer.Flush();
                        writer.Close();
                        newfile = getNewFilename(oldFile);
                        writer = File.AppendText(newfile);
                        i++;
                    }
                }
            }
            writer.Flush();
            writer.Close();              

        }

        private String getNewFilename(String oldFile)
        {
            String path = Path.GetDirectoryName(oldFile);
            String fname = Path.GetFileNameWithoutExtension(oldFile);
            String ext = Path.GetExtension(oldFile);
            int i = 0;

            String newFile = Path.Combine(path, fname + String.Format("_{0}",i) + ext);

            while (File.Exists(newFile))
            {
                i++;
                newFile = Path.Combine(path, fname + String.Format("_{0}",i) + ext);
            }

            return newFile;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0)
            {
                button2.Enabled = true;
            }
        }
    }
}
