using System;

namespace LP2_Recurso
{
    public class Grid
    {
        private Piece[,] pieces;
        int xDim, yDim;
        Random rnd;

        public Grid(int x, int y)
        {
            xDim = x;
            yDim = y;
            pieces = new Piece[xDim, yDim];
            rnd = new Random();
        }

        public void Fill()
        {
            int genPiece;

            for (int i = 0; i < xDim; i++)
                for (int j = 0; j < yDim; j++)
                {
                    genPiece = rnd.Next(0, 4);

                    pieces[i, j] = (Piece)genPiece;
                }
        }

        private (int, int, int, int) GetPositions()
        {
            int x1, x2, y1, y2;

            x1 = rnd.Next(0, xDim);
            y1 = rnd.Next(0, yDim);

            x2 = x1 + (rnd.NextDouble() < 0.5 ? -1 : 1);
            y2 = y1 + (rnd.NextDouble() < 0.5 ? -1 : 1);

            if (x2 < 0) x2 += xDim;
            else if (x2 == xDim) x2 -= xDim;

            if (y2 < 0) y2 += yDim;
            else if (y2 == yDim) y2 -= yDim;

            return (x1, y1, x2, y2);
        }

        private void SetPiece(int x, int y, Piece piece)
        {
            pieces[x, y] = piece;
        }
    }
}