using System;

namespace LP2_Recurso
{
    public class Grid
    {
        private Piece[,] pieces;
        Random rnd;

        public Grid(int x, int y)
        {
            pieces = new Piece[x, y];
            rnd = new Random();
        }

        public void Fill()
        {
            int genPiece;

            for (int i = 0; i < pieces.GetLength(0); i++)
                for (int j = 0; j < pieces.GetLength(1); j++)
                {
                    genPiece = rnd.Next(0, 4);

                    pieces[i, j] = (Piece)genPiece;
                }
        }
    }
}