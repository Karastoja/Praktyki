using System;
using System.IO;
using System.Windows.Forms;

namespace EdycjaTekstu
{
    public partial class Form1 : Form
    {
        private string currentFilePath;

        public Form1()
        {
            InitializeComponent();
            this.Load += new EventHandler(Form1_Load);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Moja aplikacja";
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            TextBox textBox1 = new TextBox(); // utwórz nowy obiekt TextBox
            textBox1.Multiline = true;
            textBox1.Dock = DockStyle.Fill;
            this.Controls.Add(textBox1); // dodaj TextBox do formularza
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                currentFilePath = openFileDialog.FileName;
                try
                {
                    string text = File.ReadAllText(currentFilePath); 
                    if (textBox1 != null)
                    {
                        textBox1.Text = text; 
                    }
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    currentFilePath = saveFileDialog.FileName;
                }
                else
                {
                    return;
                }
            }
            try
            {
                File.WriteAllText(currentFilePath, textBox1.Text);
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                currentFilePath = saveFileDialog.FileName;
                try
                {
                    File.WriteAllText(currentFilePath, textBox1.Text);
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentFilePath))
            {
                try
                {
                    File.WriteAllText(currentFilePath, textBox1.Text);
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
