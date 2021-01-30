using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    class Faceinator
    {
        public GraphicsLibrary state { get;  set; }

        public void draw(PaintEventArgs e, Drawinator drawinator)
        {
            drawinator.Draw(Panels.FACE, e, state, 0, 0);
        }
    }
}
