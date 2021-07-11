using System;

namespace LP2_Recurso
{
    public class ConsoleView : IConsoleView
    {
        ConsoleController controller;

        public ConsoleView(ConsoleController controller)
        {
            this.controller = controller;
        }

        public void Print(Grid grid)
        {
            Console.Clear();
            Console.CursorVisible = false;

            for (int i = 0; i < grid.YDim; i++)
            {
                for (int j = 0; j < grid.XDim; j++)
                {
                    SelectPieceColor(grid, j, i);

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

        public void GetInput()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.Spacebar:
                        controller.TogglePause();
                        break;

                    case ConsoleKey.Escape:
                        controller.Quit();
                        break;
                }
            }
        }
    }
}