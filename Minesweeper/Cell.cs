using System;
using System.Windows.Forms;

namespace Minesweeper
{
    public class Cell
    {
        public int TouchesCount { get; set; }
        public CellState CellState { get; set; } = CellState.UP;

        public bool Flagged { get; set; } = false;
        public bool HasBomb { get; set; } = false;
        public bool TempDown { get; set; } = false;
        public bool tripped { get; set; } = false;

        public int X { get; }
        public int Y { get; }

        public Cell(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public void draw(PaintEventArgs e, Drawinator d, bool gameOver)
        {
            if (!gameOver)
            {
                if (Flagged)
                    d.Draw(Panels.MINEFIELD, e, GraphicsLibrary.FLAGGED, X, Y);
                else if (CellState == CellState.UP)                
                    d.Draw(Panels.MINEFIELD, e, GraphicsLibrary.UP, X, Y);
                else if (HasBomb && gameOver)
                    d.Draw(Panels.MINEFIELD, e, GraphicsLibrary.BOMB, X, Y);
                else if (TouchesCount == 0 || TempDown)
                {
                    d.Draw(Panels.MINEFIELD, e, GraphicsLibrary.CLEAR, X, Y);
                }
                else if (TouchesCount > 0)
                {
                    GraphicsLibrary touches;
                    Enum.TryParse("NUM" + TouchesCount.ToString(), out touches);
                    d.Draw(Panels.MINEFIELD, e, touches, X, Y);
                }
            }
            else
            {
                if (Flagged && HasBomb)
                    d.Draw(Panels.MINEFIELD, e, GraphicsLibrary.FLAGGED, X, Y);
                else if (Flagged && !HasBomb)
                    d.Draw(Panels.MINEFIELD, e, GraphicsLibrary.BOM_WRONG, X, Y);
                else if (tripped)
                    d.Draw(Panels.MINEFIELD, e, GraphicsLibrary.BOMB_RED, X, Y);
                else if (HasBomb)
                    d.Draw(Panels.MINEFIELD, e, GraphicsLibrary.BOMB, X, Y);
                else if (CellState == CellState.UP)
                    d.Draw(Panels.MINEFIELD, e, GraphicsLibrary.UP, X, Y);
                else if (TouchesCount > 0)
                {
                    GraphicsLibrary touches;
                    Enum.TryParse("NUM" + TouchesCount.ToString(), out touches);
                    d.Draw(Panels.MINEFIELD, e, touches, X, Y);
                }
                else
                    d.Draw(Panels.MINEFIELD, e, GraphicsLibrary.CLEAR, X, Y);
            }
        }
    }
}
