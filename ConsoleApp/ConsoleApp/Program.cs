using System;
using System.Globalization;

namespace LP2_Recurso
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 5)
            {
                throw (new IndexOutOfRangeException("Not enough arguments given."));
            }
            if (args.Length > 5)
            {
                throw (new IndexOutOfRangeException("Too many arguments given."));
            }
            else
            {
                int x, y;
                float swapRate, reprRate, selRate;

                try
                {
                    CultureInfo culture = CultureInfo.InvariantCulture;

                    x = Convert.ToInt32(args[0]);
                    y = Convert.ToInt32(args[1]);

                    swapRate = Convert.ToSingle(args[2], culture);
                    reprRate = Convert.ToSingle(args[3], culture);
                    selRate = Convert.ToSingle(args[4], culture);
                }
                catch
                {
                    throw (new FormatException("One or more values had the incorrect format. X_Dimenson:int Y_Dimension:int Swap_Rate:float Reproduction_Rate:float Selection_Rate:float"));
                }

                if (x < 2 || y < 2)
                {
                    throw (new FormatException("Grid size is too small. X and Y need to be 2 or greater."));
                }
                else if (swapRate < -1 || swapRate > 1 ||
                reprRate < -1 || reprRate > 1 ||
                selRate < -1 || selRate > 1)
                {
                    throw (new FormatException("One or more rates is out of range, must be between -1 and 1."));
                }

                Grid grid = new Grid(x, y);
                ConsoleController controller = new ConsoleController(grid, swapRate, reprRate, selRate);
                ConsoleView view = new ConsoleView(controller);

                controller.Run(view);
            }
        }
    }
}
