﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
    struct GameMode
    {
        readonly public int rows,
            cols,
            mines,
            toUncover;
        public GameMode(int rows, int cols, int mines)
        {
            this.rows = rows;
            this.cols = cols;
            this.mines = mines;
            toUncover = (rows * cols) - mines;
        }
    }

    enum GameModes
    {
        EASY,
        INTERMEDIATE,
        ADVANCED
    }

    class Game
    {
        public bool Newgame { get; set; }
        public bool GameOver { get; set; }
        public int Rows { get; set; }
        public int Cols { get; set; }
        public int Mines { get; set; }
        public int ToUncover { get; set; }
        private readonly Button face;
        private int uncovered = 0,
            flagged = 0;
        private TableLayoutPanel mineTable;
        private readonly GameMode EASY = new GameMode(9, 9, 10);
        private readonly GameMode INTERMEDIATE = new GameMode(16, 16, 40);
        private readonly GameMode ADVANCED = new GameMode(16, 30, 99);
        Form1 form;
        public bool LeftButton { get; set; } = false;
        public bool RightButton { get; set; } = false;

        public Game(int rows, int cols, int mines, Form1 form)
        {
            Newgame = true;
            Rows = rows;
            Cols = cols;
            Mines = mines;
            ToUncover = (rows * cols - mines);
            this.mineTable = form.mineTable;
            this.face = form.NewGame;
            this.form = form;
        }

        /*
         * Accepts a standard game mode
         * changes the game parameters to that mode
         */
         public void SetGameMode(GameModes gameMode)
        {
            switch (gameMode)
            {
                case GameModes.EASY:
                    Rows = EASY.rows;
                    Cols = EASY.cols;
                    Mines = EASY.mines;
                    ToUncover = EASY.toUncover;
                    break;
                case GameModes.INTERMEDIATE:
                    Rows = INTERMEDIATE.rows;
                    Cols = INTERMEDIATE.cols;
                    Mines = INTERMEDIATE.mines;
                    ToUncover = INTERMEDIATE.toUncover;
                    break;
                case GameModes.ADVANCED:
                    Rows = ADVANCED.rows;
                    Cols = ADVANCED.cols;
                    Mines = ADVANCED.mines;
                    ToUncover = ADVANCED.toUncover;
                    break;
            }
        }

        /*
         * Handles Mouse in, if in a chord click push surrounding cells down 
         */ 
         public void MouseInHandle(Cell sender, MouseEventArgs e)
        {

        }

        /*
         * Handles Mouse down, records if it is right or left to detect both
         * ignore if game over
         */
        public void MouseDownHandle(Cell sender, MouseEventArgs e)
        {
            if (!GameOver)
            {

                if (e.Button == MouseButtons.Left)
                {
                    LeftButton = true;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    RightButton = true;
                }

                //show worried face
                if ((sender.CellState == State.DOWN && RightButton && LeftButton)
                    || (sender.CellState ==State.UP && LeftButton))
                {
                    this.face.Text = ":o";
                }

                //go back to smiley if chorded on up cell
                if (sender.CellState == State.UP && RightButton && LeftButton)
                {
                    this.face.Text = ":)";
                }

                //if chord show surrounding as down
                if (LeftButton && RightButton && sender.CellState == State.DOWN)
                {
                    List<Cell> Surrounding = GetSurrounding(sender);
                    foreach (Cell cell in Surrounding)
                    {
                        if (cell.CellState == State.UP
                            && !cell.Flagged)
                        {
                            cell.TempDown = true;
                            cell.CellState = State.DOWN;
                        }
                    }

                    form.Refresh();
                }
            }
        }

        /*
         * Handles MouseUp, fires different methods for left, right, and both
         * If in game over state, sender won't have button toggles set so
         * nothing will fire
         */
        public void MouseUpHandle(Cell sender, MouseEventArgs e)
        {
            if (LeftButton && RightButton)
            {
                ChordClick(sender);
                LeftButton = false;
                RightButton = false;
            }
            else if (LeftButton)
            {
                Uncover(sender);
                LeftButton = false;
            }
            else if (RightButton)
            {
                Flag(sender);
                RightButton = false;
            }

            if (!GameOver)
            {
                this.face.Text = ":)";
            }
            
        }

        /*
         * Resets for a new game
         */
        public void NewGame()
        {
            uncovered = 0;
            Newgame = true;
            GameOver = false;
            flagged = 0;

            // set up minefield

            //TODO: Suspend layout is still slow, find a better way
            form.SuspendLayout();

            mineTable.Controls.Clear();
            mineTable.RowStyles.Clear();
            mineTable.RowCount = 1;
            mineTable.ColumnCount = 1;

            for (int r = 0; r < Rows; r++)
            {
                if (mineTable.RowCount < (r + 1))
                {
                    mineTable.RowCount = r + 1;
                    mineTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                }

                for (int c = 0; c < Cols; c++)
                {
                    if (mineTable.ColumnCount < (c + 1))
                    {
                        mineTable.ColumnCount = c + 1;
                        mineTable.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                    }

                    mineTable.Controls.Add(new Cell(c, r, this), c, r);
                }
            }
            face.Text = ":)";
            form.mineCounter.Text = Mines.ToString();
            form.ResumeLayout();
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
                    C.BackColor = Color.Red;
                    C.FlatAppearance.MouseOverBackColor = Color.Red;
                    C.Text = "*";
                    face.Text = "X(";

                    //show all unflagged mines
                    foreach (Cell cell in mineTable.Controls)
                    {
                        if (cell.HasBomb && !cell.Flagged && !cell.Equals(C))
                        {
                            cell.Text = "*";
                        }
                    }

                    GameOver = true;

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

                    uncovered++;
                    
                    //check for winner
                    if (uncovered == ToUncover)
                    {
                        GameOver = true;
                        face.Text = "8)";
                    }

                }
                C.Refresh();
            }
        }

        /*
         * right click - adds or removes a flag
         */
        public void Flag(Cell C)
        {
            if (C.CellState == State.UP)
            {
                C.Flagged = !C.Flagged;

                if (C.Flagged)
                {
                    flagged++;
                    form.mineCounter.Text = (int.Parse(form.mineCounter.Text) - 1).ToString();
                }
                else
                {
                    flagged--;
                    form.mineCounter.Text = (int.Parse(form.mineCounter.Text) + 1).ToString();
                }

                C.Text = (C.Flagged) ? "F" : "";

            }
        }

        /*
         * Chord Click
         * If chords on a number that is equal to the number of mines touching
         * uncovers all other touching cells in up state
         */
         public void ChordClick(Cell cell)
        {
            List<Cell> surrounding;
            int flaggedNear = 0;

            if (cell.CellState == State.DOWN
                && cell.TouchesCount > 0)
            {
                surrounding = GetSurrounding(cell);

                foreach (Cell c in surrounding)
                {
                    if (c.Flagged)
                    {
                        flaggedNear++;
                    }   
                }
                foreach (Cell c in surrounding)
                {
                    if (c.TempDown)
                    {
                        c.TempDown = false;
                        c.CellState = State.UP;  
                    }
                    if (flaggedNear == cell.TouchesCount)
                    {
                        Uncover(c);
                    }
                }
                 
                form.Refresh();
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

                } while ((x == C.X && y == C.Y)
                    || laidMines.Contains(coord));

                laidMines.Add(coord);

                ((Cell)mineTable.GetControlFromPosition(x, y)).HasBomb = true;
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
                
                    switch (minecount)
                    {
                        case 1:
                            cell.ForeColor = Color.Blue;
                            break;
                        case 2:
                            cell.ForeColor = Color.Green;
                            break;
                        case 3:
                            cell.ForeColor = Color.Red;
                            break;
                        case 4:
                            cell.ForeColor = Color.Purple;
                            break;
                        case 5:
                            cell.ForeColor = Color.Maroon;
                            break;
                        case 6:
                            cell.ForeColor = Color.Turquoise;
                            break;
                        case 7:
                            cell.ForeColor = Color.Black;
                            break;
                        case 8:
                            cell.ForeColor = Color.Gray;
                            break;
                    }

                    cell.Font = new Font(cell.Font, FontStyle.Bold);
                   
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
