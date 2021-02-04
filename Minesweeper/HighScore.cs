using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
 [Serializable]
    public class HighScore
    {
        public string Name { get; set; }
        public int Time { get; set; }
        public HighScore(int Time, string Name)
        {
            this.Time = Time;
            this.Name = Name;
        }
    }
}
