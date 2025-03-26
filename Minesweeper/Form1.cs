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
        private HighScores statistics;
                
        public Form1()
        {
            InitializeComponent();
            
        }

        private void beginnerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetModeCheck(GameModes.EASY); 
            game.SetGameMode(GameModes.EASY);
            game.NewGame();   
        }

        private void intermediateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetModeCheck(GameModes.INTERMEDIATE);
            game.SetGameMode(GameModes.INTERMEDIATE);
            game.NewGame();
        }

        private void advancedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetModeCheck(GameModes.ADVANCED);
            game.SetGameMode(GameModes.ADVANCED);
            game.NewGame();
        }

        private void customToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetModeCheck(GameModes.CUSTOM);

            game.NewGame();
        }

        public void SetModeCheck(GameModes mode)
        {
            switch (mode)
            {
                case GameModes.EASY:
                    beginnerToolStripMenuItem.Checked = true;
                    intermediateToolStripMenuItem.Checked = false;
                    advancedToolStripMenuItem.Checked = false;
                    customToolStripMenuItem.Checked = false;
                    break;
                case GameModes.INTERMEDIATE:
                    intermediateToolStripMenuItem.Checked = true;
                    beginnerToolStripMenuItem.Checked = false;
                    advancedToolStripMenuItem.Checked = false;
                    customToolStripMenuItem.Checked = false;
                    break;
                        case GameModes.ADVANCED:
                    advancedToolStripMenuItem.Checked = true;
                    intermediateToolStripMenuItem.Checked = false;
                    beginnerToolStripMenuItem.Checked = false;
                    customToolStripMenuItem.Checked = false;
                    break;
                case GameModes.CUSTOM:
                    customToolStripMenuItem.Checked = true;
                    intermediateToolStripMenuItem.Checked = false;
                    beginnerToolStripMenuItem.Checked = false;
                    advancedToolStripMenuItem.Checked = false;
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //start first game in beginner mode
            game = new Game(this);
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
            headerTable.ColumnStyles[4].Width = 39 * scale;
            headerTable.ColumnStyles[2].Width = 27 * scale;
            headerTable.Height = 37 * scale;
            headerTable.Padding = new Padding(scale * 3);
            counterPanel.Width = 39 * scale;
            counterPanel.Height = 26 * scale;
            timerPanel.Width = 39 * scale;
            timerPanel.Height = 26 * scale;
        }

        private void counterPanel_Paint(object sender, PaintEventArgs e)
        {
            game.drawCounter(e);
        }

        public Timer getTimer()
        {
            return this.Timer;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            game.Tick();
        }

        private void timerPanel_Paint(object sender, PaintEventArgs e)
        {
            game.drawTimer(e);
        }

        private void statisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statistics = new HighScores(game.GetSettings());
            statistics.ShowDialog();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C)
            {
                if (!game.IsCheating)
                {
                    doubleBufferedPanel1.MouseMove += doubleBufferedPanel1_MouseMove;
                    game.IsCheating = true;
                }
                else
                {
                    doubleBufferedPanel1.MouseMove -= doubleBufferedPanel1_MouseMove;
                    game.IsCheating = false;
                    Text = "Minesweeper";
                }
            }
        }

        private void doubleBufferedPanel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (game.IsCheating || true)
            {
                game.Cheat(e);
            }
        }
    }
}
