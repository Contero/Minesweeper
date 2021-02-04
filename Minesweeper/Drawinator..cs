using System.Collections.Generic;
using System.Drawing;


namespace Minesweeper
{
    public class Drawinator
    {
        int scaleFactor = 2;

        Bitmap sheet = new Bitmap(@"..\..\minesweeper-sprites.png");
        Dictionary<GraphicsLibrary, RectangleF> rectangles = new Dictionary<GraphicsLibrary, RectangleF>();

        public Drawinator()
        {
            rectangles.Add(GraphicsLibrary.T_NUM1, new RectangleF(0, 0, 13, 23));
            rectangles.Add(GraphicsLibrary.T_NUM2, new RectangleF(14, 0, 13, 23));
            rectangles.Add(GraphicsLibrary.T_NUM3, new RectangleF(28, 0, 13, 23));
            rectangles.Add(GraphicsLibrary.T_NUM4, new RectangleF(42, 0, 13, 23));
            rectangles.Add(GraphicsLibrary.T_NUM5, new RectangleF(56, 0, 13, 23));
            rectangles.Add(GraphicsLibrary.T_NUM6, new RectangleF(70, 0, 13, 23));
            rectangles.Add(GraphicsLibrary.T_NUM7, new RectangleF(84, 0, 13, 23));
            rectangles.Add(GraphicsLibrary.T_NUM8, new RectangleF(98, 0, 13, 23));
            rectangles.Add(GraphicsLibrary.T_NUM9, new RectangleF(112, 0, 13, 23));
            rectangles.Add(GraphicsLibrary.T_NUM0, new RectangleF(126, 0, 13, 23));
            // faces
            rectangles.Add(GraphicsLibrary.SMILE_UP, new RectangleF(0, 24, 26, 26));
            rectangles.Add(GraphicsLibrary.SMILE_DOWN, new RectangleF(27, 24, 26, 26));
            rectangles.Add(GraphicsLibrary.WORRIED, new RectangleF(54, 24, 26, 26));
            rectangles.Add(GraphicsLibrary.COOL, new RectangleF(81, 24, 26, 26));
            rectangles.Add(GraphicsLibrary.DEAD, new RectangleF(108, 24, 26, 26));
            // Cells
            rectangles.Add(GraphicsLibrary.UP, new RectangleF(0, 51, 16, 16));
            rectangles.Add(GraphicsLibrary.CLEAR, new RectangleF(17, 51, 16, 16));
            rectangles.Add(GraphicsLibrary.FLAGGED, new RectangleF(34, 51, 16, 16));
            rectangles.Add(GraphicsLibrary.QUESTION_UP, new RectangleF(52, 51, 16, 16));
            rectangles.Add(GraphicsLibrary.QUESTION_DOWN, new RectangleF(68, 51, 16, 16));
            rectangles.Add(GraphicsLibrary.BOMB, new RectangleF(85, 51, 16, 16));
            rectangles.Add(GraphicsLibrary.BOMB_RED, new RectangleF(102, 51, 16, 16));
            rectangles.Add(GraphicsLibrary.BOM_WRONG, new RectangleF(119, 51, 16, 16));
            rectangles.Add(GraphicsLibrary.NUM1, new RectangleF(0, 68, 16, 16));
            rectangles.Add(GraphicsLibrary.NUM2, new RectangleF(17, 68, 16, 16));
            rectangles.Add(GraphicsLibrary.NUM3, new RectangleF(34, 68, 16, 16));
            rectangles.Add(GraphicsLibrary.NUM4, new RectangleF(51, 68, 16, 16));
            rectangles.Add(GraphicsLibrary.NUM5, new RectangleF(68, 68, 16, 16));
            rectangles.Add(GraphicsLibrary.NUM6, new RectangleF(85, 68, 16, 16));
            rectangles.Add(GraphicsLibrary.NUM7, new RectangleF(103, 68, 16, 16));
            rectangles.Add(GraphicsLibrary.NUM8, new RectangleF(119, 68, 16, 16));
        }

        public void setScale(int scale)
        {
            scaleFactor = scale;
        }
        public void Draw(Panels panel, System.Windows.Forms.PaintEventArgs e, GraphicsLibrary image, int x, int y)
        {
            switch (panel)
            {
                case Panels.MINEFIELD:
                    e.Graphics.DrawImage(sheet, new RectangleF(x * 16 * scaleFactor, y * 16 * scaleFactor, 16 * scaleFactor, 16 * scaleFactor), rectangles[image], GraphicsUnit.Pixel);
                    break;
                case Panels.FACE:
                    e.Graphics.DrawImage(sheet, new Rectangle(0, 0, scaleFactor * 26, scaleFactor * 26), rectangles[image], GraphicsUnit.Pixel);
                    break;
                case Panels.MINES:
                    e.Graphics.DrawImage(sheet, new Rectangle(x * scaleFactor * 12, 0, scaleFactor * 13, scaleFactor * 26), rectangles[image], GraphicsUnit.Pixel);
                    break;
            }
        }
    }
}
