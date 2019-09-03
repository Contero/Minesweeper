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
    public partial class Form1 : Form
    {
        private Game game;
                
        public Form1()
        {
            InitializeComponent();
        }

        private void beginnerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game.SetGameMode(GameModes.EASY);
            game.NewGame(); 

            intermediateToolStripMenuItem.Checked = false;
            advancedToolStripMenuItem.Checked = false;
            customToolStripMenuItem.Checked = false;
        }

        private void intermediateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game.SetGameMode(GameModes.INTERMEDIATE);
            game.NewGame();

            beginnerToolStripMenuItem.Checked = false;
            advancedToolStripMenuItem.Checked = false;
            customToolStripMenuItem.Checked = false;
        }

        private void advancedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game.SetGameMode(GameModes.ADVANCED);
            game.NewGame();

            intermediateToolStripMenuItem.Checked = false;
            beginnerToolStripMenuItem.Checked = false;
            customToolStripMenuItem.Checked = false;
        }

        private void customToolStripMenuItem_Click(object sender, EventArgs e)
        {
            intermediateToolStripMenuItem.Checked = false;
            beginnerToolStripMenuItem.Checked = false;
            advancedToolStripMenuItem.Checked = false;

            game.NewGame();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //start first game in beginner mode
            game = new Game(9,9,10, this);
            game.NewGame();
        }

        private void NewGame_Click(object sender, EventArgs e)
        {
            game.NewGame();
        }
    }
}
