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
        public Cell()
        {
            this.Width = CELL_SIZE;
            this.Height = CELL_SIZE;
            this.Margin = new Padding(0);
            this.Dock = DockStyle.Fill;
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            //this.FlatStyle
            //this.FlatAppearance.BorderColor =  Color.Green;
            this.Paint += onPaint;

        }

        private void onPaint(Object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle,
            SystemColors.ControlLightLight, 5, ButtonBorderStyle.Outset,
            SystemColors.ControlLightLight, 5, ButtonBorderStyle.Outset,
            SystemColors.ControlLightLight, 5, ButtonBorderStyle.Outset,
            SystemColors.ControlLightLight, 5, ButtonBorderStyle.Outset);
        }


    }

    enum Content
    {
        BOMB,
        FLAG,
        NUMBER
    }
}
