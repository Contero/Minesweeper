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

            //mineTable.Refresh();

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

          

            //mineTable.Height = 25 * rows;
            //mineTable.Width = 25 * cols;
            //mineTable.Margin = new Padding(0);
           
        }
    }
}
