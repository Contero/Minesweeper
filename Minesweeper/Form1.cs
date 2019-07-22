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

        private int rows = 9,
            cols = 9,
            bombs = 10;
        private Cell[] field;

        public Form1()
        {
            InitializeComponent();
        }

        private void beginnerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rows = 9;
            cols = 9;
            bombs = 10;
            setup();

            intermediateToolStripMenuItem.Checked = false;
            advancedToolStripMenuItem.Checked = false;
            customToolStripMenuItem.Checked = false;
        }

        private void intermediateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rows = 16;
            cols = 16;
            bombs = 40;
            setup();

            beginnerToolStripMenuItem.Checked = false;
            advancedToolStripMenuItem.Checked = false;
            customToolStripMenuItem.Checked = false;


        }

        private void advancedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rows = 16;
            cols = 30;
            bombs = 99;
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
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //TODO: intialize form

            setup();
            //start game
        }



        /*
         * Sets up field
         */
        private void setup()
        {
            //make minefield
            //field = new Cell[rows * cols];

            this.SuspendLayout();

            mineTable.Controls.Clear();
            mineTable.RowStyles.Clear();
            mineTable.RowCount = 1;
            mineTable.ColumnCount = 1;

            for (int r = 0; r < rows; r++)
            {
                if (mineTable.RowCount < (r+1))
                {
                    mineTable.RowCount = r+1;
                    mineTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                                 
                }

                for (int c = 0; c< cols; c++)
                {
                    if (mineTable.ColumnCount < (c + 1))
                    {
                        mineTable.ColumnCount = c + 1;
                        mineTable.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

                    }

                    mineTable.Controls.Add(new Cell(),c,r);
                }
            }

            this.ResumeLayout();

            //mineTable.Height = 25 * rows;
            //mineTable.Width = 25 * cols;
            //mineTable.Margin = new Padding(0);
           
        }
    }
}
