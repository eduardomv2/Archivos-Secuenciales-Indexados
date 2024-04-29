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

namespace Archivos_Secuenciales_Indexados
{
    public partial class Form1 : Form
    {
        // Ruta del archivo secuencial
        private const string filePath = "sequentialFile.txt";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            int index = (int)numIndex.Value;
            string text = txtInput.Text;

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"{index}: {text}");
            }

            txtInput.Clear();
            lblOutput.Text = "Text added with index " + index;

        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            if (File.Exists(filePath))
            {
                string content = File.ReadAllText(filePath);
                lblOutput.Text = "All file content:\n" + content;
            }
            else
            {
                lblOutput.Text = "File does not exist.";
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (File.Exists(filePath))
            {
                int index = (int)numIndex.Value;
                var lines = File.ReadAllLines(filePath);
                var newLines = lines.Where(l => !l.StartsWith($"{index}:")).ToList();

                File.WriteAllLines(filePath, newLines);

                lblOutput.Text = $"Removed index {index}.";
            }
            else
            {
                lblOutput.Text = "File does not exist.";
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (File.Exists(filePath))
            {
                int index = (int)numIndex.Value;
                var lines = File.ReadAllLines(filePath);
                var result = lines.FirstOrDefault(l => l.StartsWith($"{index}:"));

                if (result != null)
                {
                    lblOutput.Text = $"Found at index {index}: {result.Split(':')[1].Trim()}";
                }
                else
                {
                    lblOutput.Text = "Index not found.";
                }
            }
            else
            {
                lblOutput.Text = "File does not exist.";
            }
        }
    }
}
