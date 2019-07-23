using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
    class Game
    {
        public bool Newgame { get; set; }
        public int Rows { get; set; }
        public int Cols { get; set; }
        public int Mines { get; set; }

        private TableLayoutPanel mineTable;

        public Game(int rows, int cols, int mines, TableLayoutPanel mineTable)
        {
            Newgame = true;
            Rows = rows;
            Cols = cols;
            Mines = mines;
            this.mineTable = mineTable;
        }

        /*
         * Handles Mouse down, records if it is right or left to detect both
         */
        public void MouseDownHandle(Cell sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                sender.LeftButton = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                sender.RightButton = true;
            }
        }

        /*
         * Handles MouseUp, fires different methods for left, right, and both
         */
        public void MouseUpHandle(Cell sender, MouseEventArgs e)
        {
            if (sender.LeftButton && sender.RightButton)
            {
                Flag(sender);
                sender.LeftButton = false;
            }
            else if (sender.LeftButton)
            {
                Uncover(sender);
                sender.LeftButton = false;
            }
            else if (sender.RightButton)
            {
                Flag(sender);
                sender.RightButton = false;
            }
        }

        /*
         * Left Click - uncovers a cell, lays new minefield if it's the first cell uncovered
         */
        public void Uncover(Cell C)
        {
            List<Cell> surrounding;
            //if its the frst cell uncovered in a new game, lay a new field with this
            //cell safe
            if (Newgame)
            {
                LayMineField(C);
            }

            //if it's not flagged, and still up, check for mine
            if (C.CellState == State.UP && !C.Flagged)
            {
                C.CellState = State.DOWN;

                if (C.HasBomb)
                {
                    //todo: game over
                    C.ForeColor = Color.Red;
                }
                else
                {
                    if (C.TouchesCount > 0)
                    {
                        C.Text = C.TouchesCount.ToString();
                    }
                    else // if uncovered was 0, uncover surrounding also
                    {
                        surrounding = GetSurrounding(C);
                        foreach (Cell c in surrounding)
                        {
                            Uncover(c);
                        }
                    }

                    
                }
                C.Refresh();
            }
        }

        /*
         * right click - adds or removes a cell
         */
        public void Flag(Cell C)
        {
            if (C.CellState == State.UP)
            {
                C.Flagged = !C.Flagged;

                C.Text = (C.Flagged) ? "F" : "";

            }
        }

        /*
         * Lays Minefield from first cell selected
         */
        private void LayMineField(Cell C)
        {
            Newgame = false;
            var laidMines = new List<Tuple<int, int>>();
            Random random = new Random();
            int x = 1, y = 1;
            Tuple<int, int> coord;
            int minecount = 0;
            List<Cell> surrounding;

            //init field, aka lay some mines
            for (int i = 0; i < Mines; i++)
            {
                do
                {
                    y = random.Next(Rows);
                    x = random.Next(Cols);

                    coord = new Tuple<int, int>(x, y);

                } while (x == C.X && y == C.Y
                    && !laidMines.Contains(coord));

                laidMines.Add(coord);

                ((Cell)mineTable.GetControlFromPosition(x, y)).HasBomb = true;
                (mineTable.GetControlFromPosition(x, y)).Text = "*";

            }

            //figure out the number of mines each cell touches
            foreach (Cell cell in mineTable.Controls)
            {
                minecount = 0;
                if (!cell.HasBomb)
                {
                    surrounding = GetSurrounding(cell);

                    foreach (Cell surroundingCell in surrounding)
                    {
                        if (surroundingCell.HasBomb)
                        {
                            minecount++;
                        }
                    }
                    cell.TouchesCount = minecount;

                }
            }
        }

        /*
         * Accepts reference to a cell
         * Returns a list of cells surrounding that cell
         * Watches out for borders
         */
        private List<Cell> GetSurrounding(Cell cell)
        {
            List<Cell> list = new List<Cell>();

            //for edge cells don't look out of table bounds or kablooey
            int xMin, xMax, yMin, yMax;

            xMin = (cell.X == 0) ? 0 : cell.X - 1;
            yMin = (cell.Y == 0) ? 0 : cell.Y - 1;

            xMax = (cell.X == Cols - 1) ? Cols - 1 : cell.X + 1;
            yMax = (cell.Y == Rows - 1) ? Rows - 1 : cell.Y + 1;

            for (int r = yMin; r <= yMax; r++)
            {
                for (int c = xMin; c <= xMax; c++)
                {
                    if (!((Cell)mineTable.GetControlFromPosition(c, r)).Equals(cell))
                    {
                        list.Add((Cell)mineTable.GetControlFromPosition(c, r));
                    }
                }
            }

            return list;
        }
    }
}
