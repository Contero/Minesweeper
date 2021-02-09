using System;
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
            refresh();
        }

        private void refresh()
        {
            HighScore[] scores = settings.GetScores();

            EasyName.Text = scores[0].Name;
            EasyScore.Text = scores[0].Time.ToString() + " seconds";
            IntermediateName.Text = scores[1].Name;
            IntermediateScore.Text = scores[1].Time.ToString() + " seconds";
            AdvancedName.Text = scores[2].Name;
            AdvancedScore.Text = scores[2].Time.ToString() + " seconds";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            settings.Reset();
            refresh();
        }
    }
}
