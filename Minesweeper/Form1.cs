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
            
            game.Rows = 9;
            game.Cols = 9;
            game.Mines = 10;
            game.Newgame = true;
            setup();

            intermediateToolStripMenuItem.Checked = false;
            advancedToolStripMenuItem.Checked = false;
            customToolStripMenuItem.Checked = false;
        }

        private void intermediateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game.Rows = 16;
            game.Cols = 16;
            game.Mines = 40;
            game.Newgame = true;
            setup();

            beginnerToolStripMenuItem.Checked = false;
            advancedToolStripMenuItem.Checked = false;
            customToolStripMenuItem.Checked = false;
        }

        private void advancedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game.Rows = 16;
            game.Cols = 30;
            game.Mines = 99;
            game.Newgame = true;
            setup();

            intermediateToolStripMenuItem.Checked = false;
            beginnerToolStripMenuItem.Checked = false;
            customToolStripMenuItem.Checked = false;


        }

        private void customToolStripMenuItem_Click(object sender, EventArgs e)
        {
            intermediateToolStripMenuItem.Checked = false;
            beginnerToolStripMenuItem.Checked = false;
            advancedToolStripMenuItem.Checked = false;

            game.Newgame = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //start first game in beginner mode
            game = new Game(9, 9, 10, mineTable);
            setup();
        }

        /*
         * Sets up minefield
         */
        private void setup()
        {
            //TODO: Suspend layout is still slow, find a better way
            this.SuspendLayout();

            mineTable.Controls.Clear();
            mineTable.RowStyles.Clear();
            mineTable.RowCount = 1;
            mineTable.ColumnCount = 1;

            for (int r = 0; r < game.Rows; r++)
            {
                if (mineTable.RowCount < (r+1))
                {
                    mineTable.RowCount = r+1;
                    mineTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));          
                }

                for (int c = 0; c< game.Cols; c++)
                {
                    if (mineTable.ColumnCount < (c + 1))
                    {
                        mineTable.ColumnCount = c + 1;
                        mineTable.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                    }

                    mineTable.Controls.Add(new Cell(c, r, game),c,r);
                }
            }

            this.ResumeLayout();
           
        }
    }
}
