namespace Minesweeper
{
    partial class HighScores
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.AdvancedScore = new System.Windows.Forms.Label();
            this.IntermediateScore = new System.Windows.Forms.Label();
            this.EasyScore = new System.Windows.Forms.Label();
            this.AdvancedName = new System.Windows.Forms.Label();
            this.IntermediateName = new System.Windows.Forms.Label();
            this.EasyName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(41, 148);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(137, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Reset Scores";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(210, 148);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "OK";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Beginner:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Intermediate:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Advanced:";
            // 
            // AdvancedScore
            // 
            this.AdvancedScore.AutoSize = true;
            this.AdvancedScore.Location = new System.Drawing.Point(132, 97);
            this.AdvancedScore.Name = "AdvancedScore";
            this.AdvancedScore.Size = new System.Drawing.Size(46, 17);
            this.AdvancedScore.TabIndex = 7;
            this.AdvancedScore.Text = "label4";
            // 
            // IntermediateScore
            // 
            this.IntermediateScore.AutoSize = true;
            this.IntermediateScore.Location = new System.Drawing.Point(132, 65);
            this.IntermediateScore.Name = "IntermediateScore";
            this.IntermediateScore.Size = new System.Drawing.Size(46, 17);
            this.IntermediateScore.TabIndex = 6;
            this.IntermediateScore.Text = "label5";
            // 
            // EasyScore
            // 
            this.EasyScore.AutoSize = true;
            this.EasyScore.Location = new System.Drawing.Point(132, 35);
            this.EasyScore.Name = "EasyScore";
            this.EasyScore.Size = new System.Drawing.Size(46, 17);
            this.EasyScore.TabIndex = 5;
            this.EasyScore.Text = "label6";
            // 
            // AdvancedName
            // 
            this.AdvancedName.AutoSize = true;
            this.AdvancedName.Location = new System.Drawing.Point(251, 97);
            this.AdvancedName.Name = "AdvancedName";
            this.AdvancedName.Size = new System.Drawing.Size(46, 17);
            this.AdvancedName.TabIndex = 10;
            this.AdvancedName.Text = "label7";
            // 
            // IntermediateName
            // 
            this.IntermediateName.AutoSize = true;
            this.IntermediateName.Location = new System.Drawing.Point(251, 65);
            this.IntermediateName.Name = "IntermediateName";
            this.IntermediateName.Size = new System.Drawing.Size(46, 17);
            this.IntermediateName.TabIndex = 9;
            this.IntermediateName.Text = "label8";
            // 
            // EasyName
            // 
            this.EasyName.AutoSize = true;
            this.EasyName.Location = new System.Drawing.Point(251, 35);
            this.EasyName.Name = "EasyName";
            this.EasyName.Size = new System.Drawing.Size(46, 17);
            this.EasyName.TabIndex = 8;
            this.EasyName.Text = "label9";
            // 
            // HighScores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 205);
            this.Controls.Add(this.AdvancedName);
            this.Controls.Add(this.IntermediateName);
            this.Controls.Add(this.EasyName);
            this.Controls.Add(this.AdvancedScore);
            this.Controls.Add(this.IntermediateScore);
            this.Controls.Add(this.EasyScore);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "HighScores";
            this.Text = "Fastest Mine Sweepers";
            this.Activated += new System.EventHandler(this.HighScores_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label AdvancedScore;
        private System.Windows.Forms.Label IntermediateScore;
        private System.Windows.Forms.Label EasyScore;
        private System.Windows.Forms.Label AdvancedName;
        private System.Windows.Forms.Label IntermediateName;
        private System.Windows.Forms.Label EasyName;
    }
}