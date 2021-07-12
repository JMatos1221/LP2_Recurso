using System;
using System.Collections.Generic;

namespace LP2_Recurso
{
    public class ConsoleController : IController
    {
        Grid grid;
        List<string> events;
        bool running, paused;
        double swapRate, reprRate, selRate;
        Poisson poisson;
        Random rnd;

        public ConsoleController(Grid grid, float swap, float repr, float sel)
        {
            this.grid = grid;
            swapRate = swap;
            reprRate = repr;
            selRate = sel;
            events = new List<string>();
            poisson = new Poisson(grid.XDim, grid.YDim);
            rnd = new Random();
            paused = false;
        }

        public void Run(IConsoleView view)
        {
            running = true;

            grid.Fill();

            view.Print(grid);

            while (running)
            {
                if (!paused)
                {
                    GenerateEvents();

                    foreach (string currentEvent in events)
                        RunEvent(currentEvent);

                    view.Update(grid);
                }
                view.GetInput();
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(0, grid.YDim);
            Console.CursorVisible = true;
        }

        private void RunEvent(string currentEvent)
        {
            switch (currentEvent)
            {
                case "Swap":
                    grid.Swap();
                    break;

                case "Reproduction":
                    grid.Reproduction();
                    break;

                case "Selection":
                    grid.Selection();
                    break;
            }
        }

        private void GenerateEvents()
        {
            int numSwap, numRepr, numSel;

            numSwap = poisson.Next(swapRate);
            numRepr = poisson.Next(reprRate);
            numSel = poisson.Next(selRate);

            events.Clear();

            for (int i = 0; i < numSwap; i++)
            {
                events.Add("Swap");
            }

            for (int i = 0; i < numRepr; i++)
            {
                events.Add("Reproduction");
            }

            for (int i = 0; i < numSel; i++)
            {
                events.Add("Selection");
            }

            events = FisherShuffle(events);
        }

        private List<string> FisherShuffle(List<string> eventList)
        {
            int j;
            string aux;

            for (int i = 0; i < eventList.Count; i++)
            {
                j = rnd.Next(0, eventList.Count);

                if (j != i)
                {
                    aux = eventList[j];
                    eventList[j] = eventList[i];
                    eventList[i] = aux;
                }
            }

            return eventList;
        }

        public void TogglePause()
        {
            paused = !paused;
        }

        public void Quit()
        {
            running = false;
        }
    }
}