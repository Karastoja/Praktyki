
namespace EdycjaTekstu
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.openToolStripMenuItem = new System.Windows.Forms.Button();
            this.saveToolStripMenuItem = new System.Windows.Forms.Button();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.Button();
            this.exitToolStripMenuItem = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(706, 329);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Location = new System.Drawing.Point(12, 358);
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(112, 43);
            this.openToolStripMenuItem.TabIndex = 1;
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.UseVisualStyleBackColor = true;
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Location = new System.Drawing.Point(155, 358);
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(112, 43);
            this.saveToolStripMenuItem.TabIndex = 2;
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.UseVisualStyleBackColor = true;
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Location = new System.Drawing.Point(299, 358);
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(112, 43);
            this.saveAsToolStripMenuItem.TabIndex = 3;
            this.saveAsToolStripMenuItem.Text = "Save as";
            this.saveAsToolStripMenuItem.UseVisualStyleBackColor = true;
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Location = new System.Drawing.Point(436, 358);
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(112, 43);
            this.exitToolStripMenuItem.TabIndex = 4;
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.UseVisualStyleBackColor = true;
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.exitToolStripMenuItem);
            this.Controls.Add(this.saveAsToolStripMenuItem);
            this.Controls.Add(this.saveToolStripMenuItem);
            this.Controls.Add(this.openToolStripMenuItem);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button openToolStripMenuItem;
        private System.Windows.Forms.Button saveToolStripMenuItem;
        private System.Windows.Forms.Button saveAsToolStripMenuItem;
        private System.Windows.Forms.Button exitToolStripMenuItem;
    }
}

