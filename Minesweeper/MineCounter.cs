using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    class MineCounter
    {
        private GraphicsLibrary digit1, digit2, digit3;
        private int _count;
        public int count 
        { 
            get { return _count; }  
            set {
                _count = value;
                digit1 = getGraphic((_count / 100) % 10);
                digit2 = getGraphic((_count / 10) % 10);
                digit3 = getGraphic((_count) % 10);
            } 
        }
        public int scale { get; set; }

        private GraphicsLibrary getGraphic(int i)
        {
            GraphicsLibrary digit;
            Enum.TryParse("T_NUM" + i.ToString(), out digit);

            return digit;
        }

        public void Draw(PaintEventArgs e, Drawinator drawinator)
        {
            drawinator.Draw(Panels.MINES, e, digit1, 0, 0);
            drawinator.Draw(Panels.MINES, e, digit2, 1, 0);
            drawinator.Draw(Panels.MINES, e, digit3, 2, 0);
        }
        
    }
}
