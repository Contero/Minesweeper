using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
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

        private void doubleBufferedPanel1_Paint(object sender, PaintEventArgs e)
        {
            game.draw(e);
        }

        private void doubleBufferedPanel1_MouseUp(object sender, MouseEventArgs e)
        {
            game.MouseUpHandle(sender, e);
        }

        private void doubleBufferedPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            game.MouseDownHandle(sender, e);
        }

        private void doubleBufferedPanel2_Paint(object sender, PaintEventArgs e)
        {
            game.drawFace(e);
        }

        private void doubleBufferedPanel2_MouseDown(object sender, MouseEventArgs e)
        {
            game.faceinator.state = GraphicsLibrary.SMILE_DOWN;
            Refresh();
        }

        private void doubleBufferedPanel2_MouseUp(object sender, MouseEventArgs e)
        {
            game.faceinator.state = GraphicsLibrary.SMILE_UP;
            game.NewGame();
        }

        public void SetHeaderTableHeight(int scale)
        {
            doubleBufferedPanel2.Height = 26 * scale;
            doubleBufferedPanel2.Width = 26 * scale;
            tableLayoutPanel1.RowStyles[1].Height = 34 * scale;
            headerTable.ColumnStyles[0].Width = 39 * scale;
            headerTable.ColumnStyles[4].Width = 32 * scale;
            headerTable.ColumnStyles[2].Width = 27 * scale;
            headerTable.Height = 37 * scale;
            headerTable.Padding = new Padding(scale * 3);
            counterPanel.Width = 39 * scale;
            counterPanel.Height = 26 * scale;
        }

        private void counterPanel_Paint(object sender, PaintEventArgs e)
        {
            game.drawCounter(e);
        }
    }
}
