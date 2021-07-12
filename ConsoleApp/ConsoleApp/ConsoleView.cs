using System;

namespace LP2_Recurso
{
    /// <summary>
    /// Display the program on Console
    /// </summary>
    public class ConsoleView : IConsoleView
    {
        /// <summary>
        /// Controller instance
        /// </summary>
        ConsoleController controller;

        /// <summary>
        /// Console View constructor
        /// </summary>
        /// <param name="controller">Controller instance to use</param>
        public ConsoleView(ConsoleController controller)
        {
            this.controller = controller;
        }

        /// <summary>
        /// Prints the grid the first time
        /// </summary>
        /// <param name="grid">Grid to print</param>
        public void Print(Grid grid)
        {
            /// Clears the console and hides the cursor
            Console.Clear();
            Console.CursorVisible = false;

            /// Cycles throught every grid position and prints it's value
            for (int i = 0; i < grid.YDim; i++)
            {
                for (int j = 0; j < grid.XDim; j++)
                {
                    /// Selects the color to display based on the grid piece
                    SelectPieceColor(grid, j, i);

                    System.Console.Write(" ");
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Updates the grid changed positions
        /// </summary>
        /// <param name="grid">Grid to update</param>
        public void Update(Grid grid)
        {
            /// Cycles through the grid positions and updates them
            for (int i = 0; i < grid.PositionsToUpdate.Count; i += 2)
            {
                /// Sets the cursor the position to update
                Console.SetCursorPosition(grid.PositionsToUpdate[i], grid.PositionsToUpdate[i + 1]);

                /// Selects the color to display based on the grid piece
                SelectPieceColor(grid, grid.PositionsToUpdate[i], grid.PositionsToUpdate[i + 1]);

                Console.Write(" ");
            }

            grid.PositionsToUpdate.Clear();
        }

        private void SelectPieceColor(Grid grid, int x, int y)
        {
            /// Get the pieces coordinates and puts on the selected Color
            switch (grid.GetPiece(x, y))
            {
                /// For Empty Cells black color
                case Piece.None:
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                /// For Rocks Cells Blue Color 
                case Piece.Rock:
                    Console.BackgroundColor = ConsoleColor.Blue;
                    break;
                /// For Papers cells Green Color
                case Piece.Paper:
                    Console.BackgroundColor = ConsoleColor.Green;
                    break;
                /// For Scissors Cells Red Color 
                case Piece.Scissors:
                    Console.BackgroundColor = ConsoleColor.Red;
                    break;

            }
        }

        /// <summary>
        /// Reads the user input
        /// </summary>
        public void GetInput()
        {
            /// If a key was pressed, read it
            if (Console.KeyAvailable)
            {
                /// Reads the available key
                ConsoleKey key = Console.ReadKey().Key;

                /// Executes key action if it has one
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

        public void End(Grid grid)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(0, grid.YDim);
            Console.CursorVisible = true;
        }
    }
}