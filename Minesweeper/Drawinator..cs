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
        }
        // faces
        RectangleF SMILE_UP = new RectangleF(0, 24, 26, 26),
        SMILE_DOWN = new RectangleF(27, 24, 26, 26),
        WORRIED = new RectangleF(54, 24, 26, 26),
        COOL = new RectangleF(81, 24, 26, 26),
        DEAD = new RectangleF(108, 24, 26, 26),
        // Cells
        UP = new RectangleF(0, 51, 16, 16),
        CLEAR = new RectangleF(17, 51, 16, 16),
        FLAGGED = new RectangleF(34, 51, 16, 16),
        QUESTION_UP = new RectangleF(52, 51, 16, 16),
        QUESTION_DOWN = new RectangleF(68, 51, 16, 16),
        BOMB = new RectangleF(85, 51, 16, 16),
        BOMB_RED = new RectangleF(102, 51, 16, 16),
        BOM_WRONG = new RectangleF(119, 51, 16, 16),
        NUM1 = new RectangleF(0, 68, 16, 16),
        NUM2 = new RectangleF(17, 68, 16, 16),
        NUM3 = new RectangleF(34, 68, 16, 16),
        NUM4 = new RectangleF(51, 68, 16, 16),
        NUM5 = new RectangleF(68, 68, 16, 16),
        NUM6 = new RectangleF(85, 68, 16, 16),
        NUM7 = new RectangleF(103, 68, 16, 16),
        NUM8 = new RectangleF(119, 68, 16, 16);


        public void setScale(int scale)
        {
            scaleFactor = scale;
        }
        public void Draw(Panels panel, System.Windows.Forms.PaintEventArgs e, GraphicsLibrary image, int x, int y)
        {
            RectangleF toDraw = QUESTION_UP;
            switch (panel)
            {
                case Panels.MINEFIELD:
                    switch (image)
                    {
                        case GraphicsLibrary.UP:
                            toDraw = UP;
                            break;
                        case GraphicsLibrary.CLEAR:
                            toDraw = CLEAR;
                            break;
                        case GraphicsLibrary.NUM1:
                            toDraw = NUM1;
                            break;
                        case GraphicsLibrary.NUM2:
                            toDraw = NUM2;
                            break;
                        case GraphicsLibrary.NUM3:
                            toDraw = NUM3;
                            break;
                        case GraphicsLibrary.NUM4:
                            toDraw = NUM4;
                            break;
                        case GraphicsLibrary.NUM5:
                            toDraw = NUM5;
                            break;
                        case GraphicsLibrary.NUM6:
                            toDraw = NUM6;
                            break;
                        case GraphicsLibrary.NUM7:
                            toDraw = NUM7;
                            break;
                        case GraphicsLibrary.NUM8:
                            toDraw = NUM8;
                            break;
                        case GraphicsLibrary.BOMB:
                            toDraw = BOMB;
                            break;
                        case GraphicsLibrary.FLAGGED:
                            toDraw = FLAGGED;
                            break;
                        case GraphicsLibrary.QUESTION_UP:
                            toDraw = QUESTION_UP;
                            break;
                        case GraphicsLibrary.BOMB_RED:
                            toDraw = BOMB_RED;
                            break;
                        case GraphicsLibrary.BOM_WRONG:
                            toDraw = BOM_WRONG;
                            break;
                    }

                    e.Graphics.DrawImage(sheet, new RectangleF(x * 16 * scaleFactor, y * 16 * scaleFactor, 16 * scaleFactor, 16 * scaleFactor), toDraw, GraphicsUnit.Pixel);
                    break;
                case Panels.FACE:
                    switch (image)
                    {
                        case GraphicsLibrary.SMILE_UP:
                            toDraw = SMILE_UP;
                            break;
                        case GraphicsLibrary.SMILE_DOWN:
                            toDraw = SMILE_DOWN;
                            break;
                        case GraphicsLibrary.WORRIED:
                            toDraw = WORRIED;
                            break;
                        case GraphicsLibrary.DEAD:
                            toDraw = DEAD;
                            break;
                        case GraphicsLibrary.COOL:
                            toDraw = COOL;
                            break;

                    }
                    e.Graphics.DrawImage(sheet, new Rectangle(0, 0, scaleFactor * 26, scaleFactor * 26), toDraw, GraphicsUnit.Pixel);
                    break;
                case Panels.MINES:
                    toDraw = rectangles[image];
                    e.Graphics.DrawImage(sheet, new Rectangle((x * scaleFactor * 13) - x * scaleFactor, 0, scaleFactor * 13, scaleFactor * 26), toDraw, GraphicsUnit.Pixel);
                    break;
            }
        }
    }
}
