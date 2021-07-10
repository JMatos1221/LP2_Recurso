using System;

namespace LP2_Recurso
{
    class Program
    {
        static void Main(string[] args)
        {
            Grid grid = new Grid(10, 10);
            ConsoleController controller = new ConsoleController(grid, 0.2f, 0.2f, 1.2f);
            ConsoleView view = new ConsoleView(controller);

            controller.Run(view);
        }
    }
}
