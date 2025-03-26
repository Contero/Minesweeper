using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
    [Serializable]
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
    [Serializable]
    public enum GameModes
    {
        EASY,
        INTERMEDIATE,
        ADVANCED,
        CUSTOM
    }

    class Game
    {
        public bool Newgame { get; set; }
        public bool GameOver { get; set; }
        public int Rows { get; set; }
        public int Cols { get; set; }
        public int Mines { get; set; }
        public int ToUncover { get; set; }
        Settings settings = new Settings();
        private int scale;
        public bool IsCheating { get; set; }
        public int LastCheatX { get; set; }
        public int LastCheatY { get; set; }
        private int uncovered = 0,
            flagged = 0;
        Form1 form;
        private Minefield minefield;
        private Drawinator drawinator = new Drawinator();
        public Faceinator faceinator = new Faceinator();
        private MineCounter mineCounter = new MineCounter();
        private Timerinator timerinator;
        private readonly GameMode EASY = new GameMode(9, 9, 10);
        private readonly GameMode INTERMEDIATE = new GameMode(16, 16, 40);
        private readonly GameMode ADVANCED = new GameMode(16, 30, 99);
        private GameMode CUSTOM = new GameMode(0, 0, 0);
        private GameModes gameMode;
        
        public bool LeftButton { get; set; } = false;
        public bool RightButton { get; set; } = false;

        public Game(Form1 form)
        {
            Newgame = true;
            this.form = form;
            SetGameMode(settings.getGameMode());
            form.SetModeCheck(settings.getGameMode());
            
            timerinator = new Timerinator(form.getTimer());
            scale = settings.getScale();
            setScale(scale);
        }

        public Settings GetSettings()
        {
            return settings;
        }

        public void setScale(int scale)
        {
            this.scale = scale;
            form.SetHeaderTableHeight(scale);
            drawinator.setScale(scale);
            timerinator.scale = scale;
        }

        /*
         * Accepts a standard game mode
         * changes the game parameters to that mode
         */
         public void SetGameMode(GameModes gameMode)
        {
            this.gameMode = gameMode;
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
                case GameModes.CUSTOM:
                    Rows = CUSTOM.rows;
                    Cols = CUSTOM.cols;
                    Mines = CUSTOM.mines;
                    ToUncover = CUSTOM.toUncover;
                    break;
            }

            settings.SetGameMode(gameMode);
            // if (gameMode = advanced) save params
        }

        /*
         * Handles Mouse down, records if it is right or left to detect both
         * ignore if game over
         */
        public void MouseDownHandle(Object sender, MouseEventArgs e)
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

                // determine cell
                int x, y;
                x = e.X / (scale * 16);
                y = e.Y / (scale * 16);

                Cell cell = minefield.getCell(x, y);

                //show worried face
                if ((cell.CellState == CellState.DOWN && RightButton && LeftButton)
                    || (cell.CellState ==CellState.UP && LeftButton))
                {
                    faceinator.state = GraphicsLibrary.WORRIED;
                    form.Refresh();
                }

                //if chord show surrounding as down
                if (LeftButton && RightButton && cell.CellState == CellState.DOWN)
                {
                    List<Cell> Surrounding = GetSurrounding(minefield.getField(), cell);
                    foreach (Cell c in Surrounding)
                    {
                        if (c.CellState == CellState.UP
                            && !c.Flagged)
                        {
                            c.TempDown = true;
                            c.CellState = CellState.DOWN;
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
        public void MouseUpHandle(Object sender, MouseEventArgs e)
        {
            // determine cell
            int x, y;
            x = e.X / (scale * 16);
            y = e.Y / (scale * 16);

            Cell cell = minefield.getCell(x, y);

           if (LeftButton && RightButton)
            {
                ChordClick(cell);
                LeftButton = false;
                RightButton = false;
            }
            else if (LeftButton)
            {
                Uncover(cell);
                LeftButton = false;
            }
            else if (RightButton)
            {
                Flag(cell);
                RightButton = false;
            }

            if (!GameOver)
            {
                faceinator.state = GraphicsLibrary.SMILE_UP;
            }
            form.Refresh();
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
            minefield = new Minefield(Rows, Cols);
            form.doubleBufferedPanel1.Size = new Size(Cols * 16 * scale, Rows * 16 * scale);
            faceinator.state = GraphicsLibrary.SMILE_UP;
            mineCounter.count = Mines;
            timerinator.Reset();
            form.Refresh();
        }

        /*
         * Left Click - uncovers a cell, lays new minefield if it's the first cell uncovered
         */
        public void Uncover(Cell C)
        {
            List<Cell> surrounding;
            //if its the first cell uncovered in a new game, lay a new field with this
            //cell safe
            if (Newgame)
            {
                LayMineField(C);
                timerinator.Start();
            }

            // if it's not flagged, and still up, check for mine
            if (C.CellState == CellState.UP && !C.Flagged)
            {
                C.CellState = CellState.DOWN;

                if (C.HasBomb)
                {
                    timerinator.Stop();
                    faceinator.state = GraphicsLibrary.DEAD;
                    C.tripped = true;
                    GameOver = true;
                }
                else
                {
                    if (C.TouchesCount == 0)
                    {
                        surrounding = GetSurrounding(minefield.getField(), C);
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
                        timerinator.Stop();
                        faceinator.state = GraphicsLibrary.COOL;

                        if (gameMode != GameModes.ADVANCED)
                        {
                            form.Refresh();
                            if (timerinator.getTime() < settings.GetScore(gameMode).Time)
                            {
                                // get name
                                NameForm nameForm = new NameForm();
                                nameForm.ShowDialog();
                                string name = nameForm.PlayerName;
                                settings.SetScore(gameMode, timerinator.getTime(), name);
                                var hs = new HighScores(settings);
                                hs.ShowDialog();
                            }
                        }
                    }
                }
            }
        }
            
        /*
         * right click - adds or removes a flag
         */
        public void Flag(Cell C)
        {
            if (C.CellState == CellState.UP)
            {
                C.Flagged = !C.Flagged;

                if (C.Flagged)
                {
                    flagged++;
                }
                else
                {
                    flagged--;
                }

                mineCounter.count = (Mines - flagged);
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

            if (cell.CellState == CellState.DOWN
                && cell.TouchesCount > 0)
            {
                surrounding = GetSurrounding(minefield.getField(), cell);

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
                        c.CellState = CellState.UP;  
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

                minefield.getCell(x, y).HasBomb = true;
            }

            //figure out the number of mines each cell touches
            Cell[,] field = minefield.getField();

            foreach (Cell cell in field)
            {
                minecount = 0;
                if (!cell.HasBomb)
                {
                    surrounding = GetSurrounding(field, cell);

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
        private List<Cell> GetSurrounding(Cell[,] field, Cell cell)
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
                    if (!field[c, r].Equals(cell))
                    {
                        list.Add(field[c, r]);
                    }
                }
            }

            return list;
        }

        public void draw(PaintEventArgs e)
        {
            minefield.draw(e, drawinator, GameOver);
        }

        public void drawFace(PaintEventArgs e)
        {
            faceinator.draw(e, drawinator);
        }

        public void drawCounter(PaintEventArgs e)
        {
            mineCounter.Draw(e, drawinator);                
        }

        public void drawTimer(PaintEventArgs e)
        {
            timerinator.Draw(e, drawinator);
        }

        public void Tick()
        {
            timerinator.Tick();
            form.Refresh();
        }

        public void Cheat(MouseEventArgs e)
        {
            // determine cell
            int x, y;
            x = e.X / (scale * 16);
            y = e.Y / (scale * 16);

            Cell cell = minefield.getCell(x, y);

            if (LastCheatX != x || LastCheatY != y)
            {
                LastCheatX = x;
                LastCheatY = y;


                if (cell.CellState == CellState.UP)
                {
                    if (cell.HasBomb)
                    {
                        form.Text = "Minesweeper.";
                    }
                    else
                    {
                        form.Text = "Minesweeper";
                    }
                }
                else
                {
                    form.Text = "Minesweeper";
                }

            }
        }
    }
}
