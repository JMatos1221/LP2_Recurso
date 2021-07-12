using System;
using System.Globalization;

namespace LP2_Recurso
{
    /// <summary>
    /// Main Class That runs the ConsoleApp
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();

            p.Run(args);
        }
        
        /// <summary>
        /// Runs the program
        /// </summary>
        /// <param name="args">Console arguments</param>
        private void Run(string[] args)
        {
            /// Throw exception if there aren't enough arguments
            if (args.Length < 5)
            {
                throw (new IndexOutOfRangeException("Not enough arguments given."));
            }

            /// Throw exception if there are too many arguments
            if (args.Length > 5)
            {
                throw (new IndexOutOfRangeException("Too many arguments given."));
            }

            /// If the number of arguments is correct
            else
            {
                /// Grid width and height
                int x, y;
                /// Event rates
                float swapRate, reprRate, selRate;

                try
                {
                    /// Invariant Culture for parsing
                    CultureInfo culture = CultureInfo.InvariantCulture;

                    /// Arguments parsing to the wanted values
                    x = Convert.ToInt32(args[0]);
                    y = Convert.ToInt32(args[1]);

                    swapRate = Convert.ToSingle(args[2], culture);
                    reprRate = Convert.ToSingle(args[3], culture);
                    selRate = Convert.ToSingle(args[4], culture);
                }
                catch
                {
                    /// Throw exception if one or more values 
                    /// has the incorrect format
                    throw (new FormatException("One or more values had the incorrect format. X_Dimenson:int Y_Dimension:int Swap_Rate:float Reproduction_Rate:float Selection_Rate:float"));
                }

                /// If the grid size is too small
                if (x < 2 || y < 2)
                {
                    /// Throw format exception with error message
                    throw (new FormatException("Grid size is too small. X and Y need to be 2 or greater."));
                }
                /// If the swap rates are out of the required range
                else if (swapRate < -1 || swapRate > 1 ||
                reprRate < -1 || reprRate > 1 ||
                selRate < -1 || selRate > 1)
                {
                    /// Throw format exception with error message
                    throw (new FormatException("One or more rates is out of range, must be between -1 and 1."));
                }

                /// Creates the Model ,View and Controller instances
                /// and runs the simulation
                Grid grid = new Grid(x, y);
                ConsoleController controller = new ConsoleController(grid, swapRate, reprRate, selRate);
                ConsoleView view = new ConsoleView(controller);

                controller.Run(view);
            }
        }
    }
}
