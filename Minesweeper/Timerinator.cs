using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    class Timerinator
    {
        Timer timer;
        int seconds = 0;
        private GraphicsLibrary digit1, digit2, digit3;

        public Timerinator(Timer timer)
        {
            this.timer = timer;
        }

        public void Reset()
        {
            seconds = 0;
        }

        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }

        public void Tick()
        {
            seconds++;
        }

        public int scale { get; set; }

        public int getTime() { return seconds; }

        private GraphicsLibrary getGraphic(int i)
        {
            GraphicsLibrary digit;
            Enum.TryParse("T_NUM" + i.ToString(), out digit);

            return digit;
        }

        public void Draw(PaintEventArgs e, Drawinator drawinator)
        {
            digit1 = getGraphic((seconds / 100) % 10);
            digit2 = getGraphic((seconds / 10) % 10);
            digit3 = getGraphic((seconds) % 10);

            drawinator.Draw(Panels.MINES, e, digit1, 0, 0);
            drawinator.Draw(Panels.MINES, e, digit2, 1, 0);
            drawinator.Draw(Panels.MINES, e, digit3, 2, 0);
        }

    }
}
