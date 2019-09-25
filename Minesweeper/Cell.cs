using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Minesweeper
{
    class Cell : Button
    {
        public int TouchesCount { get; set; }
        public Content CellContent { get; set; }
        const int CELL_SIZE = 25;
        public State CellState { get; set; } = State.UP;

        public bool Flagged { get; set; } = false;
        public bool HasBomb { get; set; } = false;
        public bool TempDown { get; set; } = false;

        public int X { get; }
        public int Y { get; }
        private Game game;

        public Cell(int X, int Y, Game game)
        {
            this.X = X;
            this.Y = Y;
            this.game = game;
            this.Width = CELL_SIZE;
            this.Height = CELL_SIZE;
            this.Margin = new Padding(0);
            this.Dock = DockStyle.Fill;
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.FlatAppearance.MouseOverBackColor = Color.Transparent;
            this.FlatAppearance.MouseDownBackColor = Color.Transparent;
            this.Paint += onPaint;
            this.MouseDown += (_, args) => { game.MouseDownHandle(this, args); };
            this.MouseUp += (_, args) => { game.MouseUpHandle(this, args); };
        }

        /*
         * Adds 3d effect if state is up
         */
        private void onPaint(Object sender, PaintEventArgs e)
        {
            if (CellState == State.UP)
            {
                ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle,
                SystemColors.ControlLightLight, 5, ButtonBorderStyle.Outset,
                SystemColors.ControlLightLight, 5, ButtonBorderStyle.Outset,
                SystemColors.ControlLightLight, 5, ButtonBorderStyle.Outset,
                SystemColors.ControlLightLight, 5, ButtonBorderStyle.Outset);
            }
        }
    }

    enum Content
    {
        EMPTY,
        BOMB,
        FLAG,
        NUMBER
    }

    enum State
    {
        UP,
        DOWN
    }
}
