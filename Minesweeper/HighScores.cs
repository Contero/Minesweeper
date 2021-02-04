﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class HighScores : Form
    {
        Settings settings;
        public HighScores(Settings settings)
        {
            InitializeComponent();
            this.settings = settings;
        }

        private void HighScores_Load(object sender, EventArgs e)
        {
            HighScore[] scores = settings.GetScores();

            EasyName.Text = scores[0].Name;
            EasyScore.Text = scores[1].Time.ToString() + " seconds";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
