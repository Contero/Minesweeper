namespace Minesweeper
{
    partial class NameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.NameEntry = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Closer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NameEntry
            // 
            this.NameEntry.Location = new System.Drawing.Point(157, 48);
            this.NameEntry.Name = "NameEntry";
            this.NameEntry.Size = new System.Drawing.Size(217, 22);
            this.NameEntry.TabIndex = 0;
            this.NameEntry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NameEntry_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Enter Your Name:";
            // 
            // Closer
            // 
            this.Closer.Location = new System.Drawing.Point(299, 105);
            this.Closer.Name = "Closer";
            this.Closer.Size = new System.Drawing.Size(75, 23);
            this.Closer.TabIndex = 2;
            this.Closer.Text = "OK";
            this.Closer.UseVisualStyleBackColor = true;
            this.Closer.Click += new System.EventHandler(this.Close_Click);
            // 
            // NameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 162);
            this.Controls.Add(this.Closer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.NameEntry);
            this.Name = "NameForm";
            this.Text = "New Fastest Time";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NameForm_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox NameEntry;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Closer;
    }
}