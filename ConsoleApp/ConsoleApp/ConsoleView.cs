using System;

namespace LP2_Recurso
{
    public class ConsoleView
    {
        ConsoleController controller;

        public ConsoleView(ConsoleController controller)
        {
            this.controller = controller;
        }

        public void Print(Grid grid)
        {
            Console.SetCursorPosition(0, 0);

            for (int i = 0; i < grid.XDim; i++)
            {
                for (int j = 0; j < grid.YDim; j++)
                {
                    SelectPieceColor(grid, i, j);

                    System.Console.Write(" ");
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
            }
        }
        public void Update(Grid grid)
        {
            for (int i = 0; i < grid.PositionsToUpdate.Count; i += 2)
            {
                Console.SetCursorPosition(grid.PositionsToUpdate[i], grid.PositionsToUpdate[i + 1]);

                SelectPieceColor(grid, grid.PositionsToUpdate[i], grid.PositionsToUpdate[i + 1]);

                Console.Write(" ");
            }

            grid.PositionsToUpdate.Clear();
        }

        private void SelectPieceColor(Grid grid, int x, int y)
        {
            switch (grid.GetPiece(x, y))
            {
                case Piece.None:
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;

                case Piece.Rock:
                    Console.BackgroundColor = ConsoleColor.Blue;
                    break;

                case Piece.Paper:
                    Console.BackgroundColor = ConsoleColor.Green;
                    break;

                case Piece.Scissors:
                    Console.BackgroundColor = ConsoleColor.Red;
                    break;

            }
        }
    }
}