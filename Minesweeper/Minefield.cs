using System;
using System.Windows.Forms;

namespace Minesweeper
{
	public class Minefield 
	{
		int rows = 0, cols = 0;
		Cell[,] field;

		public Minefield(int rows, int cols)
		{
			this.rows = rows;
			this.cols = cols;

			field = new Cell[cols, rows];

			for (int r = 0; r < rows; r++)
			{
				for (int c = 0; c < cols; c++)
				{
					field[c,r] = new Cell(c , r );
				}
			}
		}

		public Cell[,] getField()
        {
			return field;
        }

		public Cell getCell(int x, int y)
        {
			return field[x, y];
        }

		public void draw(PaintEventArgs e, Drawinator d, bool gameOver)
        {
			foreach(Cell c in field)
            {
				c.draw(e, d, gameOver);
            }
        }
	}
}
