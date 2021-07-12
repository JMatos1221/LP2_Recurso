using System;
using System.Collections.Generic;

namespace LP2_Recurso
{
    /// <summary>
    /// Console Controller
    /// </summary>
    public class ConsoleController : IController
    {
        /// Instance of grid
        Grid grid;
        /// A list of Events
        List<string> events;
        /// Booleans to pause the simulation and run the program
        bool running, paused;
        /// Events ratio
        double swapRate, reprRate, selRate;
        /// Poisson instance
        Poisson poisson;
        /// Random Instance 
        Random rnd;

        /// <summary>
        /// Controller constructor
        /// </summary>
        /// <param name="grid">Grid instance to run the controller with</param>
        /// <param name="swap">Swap event rate</param>
        /// <param name="repr">Reproduction event rate</param>
        /// <param name="sel">Selection event rate</param>
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

        /// <summary>
        /// Run the controller loop
        /// </summary>
        /// <param name="view"></param>
        public void Run(IConsoleView view)
        {
            /// Sets the running state to true
            running = true;

            /// Fills the created grid with random piece types
            grid.Fill();

            /// Prints the grid the first time
            view.Print(grid);

            /// If the simulation is running
            while (running)
            {
                /// Checks if the simulation is paused
                if (!paused)
                {
                    /// Generates the events for the current frame
                    GenerateEvents();

                    /// Iterates through the generated events and run them one by one
                    foreach (string currentEvent in events)
                        RunEvent(currentEvent);

                    /// Updates the grid on the changed positions
                    view.Update(grid);

                }
                /// Checks for user input
                view.GetInput();
            }

            view.End(grid);
        }

        /// <summary>
        /// Verifies the current event 
        /// and applies the current event 
        /// </summary>
        /// <param name="currentEvent"></param>
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

        /// <summary>
        /// Generates the events for the current frame
        /// </summary>
        private void GenerateEvents()
        {
            /// Amount of times each event type will occur
            int numSwap, numRepr, numSel;

            /// Gets the number of each type of event based on their event rate
            numSwap = poisson.Next(swapRate);
            numRepr = poisson.Next(reprRate);
            numSel = poisson.Next(selRate);

            /// Clears the event list and fills it with the 
            /// ammount of events previously calculated
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

        /// <summary>
        /// Shuffles the event list using the Fisher-Yates algorithm
        /// </summary>
        /// <param name="eventList">Event list (not shuffled)</param>
        /// <returns>Shuffled event list</returns>
        private List<string> FisherShuffle(List<string> eventList)
        {
            int j;
            string aux;
            /// Shuffles the events 
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

        /// <summary>
        /// Pauses/Unpauses the simulation
        /// </summary>
        public void TogglePause()
        {
            paused = !paused;
        }

        /// <summary>
        /// Quits the simulation
        /// </summary>
        public void Quit()
        {
            running = false;
        }
    }
}