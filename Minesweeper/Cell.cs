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
        private List<Cell> touches;
        private int TouchesCount { get; }
        private Content content;
        const int CELL_SIZE = 25;
        State state = State.UP;

        public Cell()
        {
            this.Width = CELL_SIZE;
            this.Height = CELL_SIZE;
            this.Margin = new Padding(0);
            this.Dock = DockStyle.Fill;
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            //this.FlatStyle
            this.FlatAppearance.MouseOverBackColor = Color.Transparent;
            this.FlatAppearance.MouseDownBackColor = Color.Transparent;
            this.Paint += onPaint;
            this.MouseDown += MouseDownHandler;

        }

        private void onPaint(Object sender, PaintEventArgs e)
        {
            if (state == State.UP)
            {
                ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle,
                SystemColors.ControlLightLight, 5, ButtonBorderStyle.Outset,
                SystemColors.ControlLightLight, 5, ButtonBorderStyle.Outset,
                SystemColors.ControlLightLight, 5, ButtonBorderStyle.Outset,
                SystemColors.ControlLightLight, 5, ButtonBorderStyle.Outset);
            }
        }
        private void MouseDownHandler(Object sender, EventArgs e)
        {
            state = State.DOWN;
            //this.FlatAppearance.BorderSize = 0;    
        }
    }

    enum Content
    {
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
