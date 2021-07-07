namespace LP2_Recurso
{
    public class Grid
    {
        private Piece[,] pieces;

        public Grid(int x, int y)
        {
            pieces = new Piece[x, y];
        }
    }
}