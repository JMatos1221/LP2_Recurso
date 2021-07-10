using System;
using System.Threading;
using System.Collections.Generic;

namespace LP2_Recurso
{
    public class ConsoleController
    {
        Grid grid;
        List<string> events;
        bool running;
        float swapRate, reprRate, selRate;
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
        }

        public void Run(ConsoleView view)
        {
            running = true;

            grid.Fill();

            view.Print(grid);

            while (running)
            {
                GenerateEvents();

                foreach (string currentEvent in events)
                    RunEvent(currentEvent);

                view.Update(grid);
            }
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
    }
}