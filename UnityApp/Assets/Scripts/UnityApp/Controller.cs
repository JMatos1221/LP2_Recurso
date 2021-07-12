using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using LP2_Recurso;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Grid = LP2_Recurso.Grid;

/// <summary>
/// Controller Class
/// </summary>
public class Controller : MonoBehaviour, IController
{
    /// Input field for X dimension 
    [Header("Input")] [SerializeField] private InputField xDimIn;

    /// Input field for Y dimension
    /// Input field for Events Ration
    [SerializeField]
    private InputField yDimIn, swapRateIn, reprRateIn, selRateIn;
    /// List of events
    private List<string> events;
    /// Boolean to pause the simulation
    private bool paused;
    /// Instance of poisson
    private Poisson poisson;
    /// Instance of random 
    private System.Random rnd;
    /// Events Ratio
    private double swapRate, reprRate, selRate;
    /// Instace of View
    private View view;
    /// X Dimension Y Dimension
    private int xDim, yDim;

    /// <summary>
    /// Start Method
    /// </summary>
    private void Start()
    {
        /// Get instance components 
        view = GetComponent<View>();
        events = new List<string>();
        rnd = new System.Random();
        paused = false;
    }

    /// <summary>
    ///  Pauses the Simulation
    /// </summary>
    public void TogglePause()
    {
        paused = !paused;
        view.PausedUI(paused);
    }

    /// <summary>
    /// Quits the simulation
    /// </summary>
    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif

#if UNITY_STANDALONE
        Application.Quit(0);
#endif
    }

    /// <summary>
    /// Tries to start the simulation
    /// </summary>
    public void StartSimulation()
    {
        /// If any input parameter is empty, do nothing
        if (xDimIn.text == string.Empty ||
            yDimIn.text == string.Empty ||
            swapRateIn.text == string.Empty ||
            reprRateIn.text == string.Empty ||
            selRateIn.text == string.Empty)
            return;

        /// InvariantCulture for string parsing
        CultureInfo culture = CultureInfo.InvariantCulture;

        /// Convert input fields to their formats and save
        xDim = Convert.ToInt32(xDimIn.text, culture);
        yDim = Convert.ToInt32(yDimIn.text, culture);

        swapRate = Convert.ToDouble(swapRateIn.text, culture);
        reprRate = Convert.ToDouble(reprRateIn.text, culture);
        selRate = Convert.ToDouble(selRateIn.text, culture);

        /// Start the simulation
        StartCoroutine("SimulationCoroutine");
    }

    /// <summary>
    /// Generates the amount of each event type and fills the event list
    /// with each event's amount
    /// </summary>
    private void GenerateEvents()
    {
        /// Event types amount
        int numSwap, numRepr, numSel;

        /// Generates each event type amount
        numSwap = poisson.Next(swapRate);
        numRepr = poisson.Next(reprRate);
        numSel = poisson.Next(selRate);

        /// Clears event list
        events.Clear();

        /// Fills the event list with the Swap event
        for (int i = 0; i < numSwap; i++) events.Add("Swap");

        /// Fills the event list with the Reproduction event
        for (int i = 0; i < numRepr; i++) events.Add("Reproduction");

        /// Fills the event list with the Selection event
        for (int i = 0; i < numSel; i++) events.Add("Selection");

        events = FisherShuffle(events);
    }

    /// <summary>
    /// Shuffles the list using the Fisher-Yates algorithm
    /// </summary>
    /// <param name="eventList">Event list previously generated</param>
    /// <returns>Shuffled event list</returns>
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

    /// <summary>
    /// Runs the given event
    /// </summary>
    /// <param name="grid">Grid to run the event on</param>
    /// <param name="currentEvent">Current event to run</param>
    private void RunEvent(Grid grid, string currentEvent)
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
    /// Simulation Coroutine, runs the simulation with the 
    /// parameters previously given 
    /// </summary>
    /// <returns>Delay of 0.05 seconds</returns>
    private IEnumerator SimulationCoroutine()
    {
        /// Set the UI to the running state
        view.RunningUI(true);

        /// Show the image where the simulation will be represented
        view.ShowImage();

        /// Initialize a new Grid instance
        Grid grid = new Grid(xDim, yDim);

        /// Create the texture to represent the simulation grid
        view.CreateTexture(xDim, yDim);

        /// Initialize a new Poisson instance with the grid size
        poisson = new Poisson(xDim, yDim);

        /// Fill the simulation grid
        grid.Fill();

        /// Print the simulation grid the first time
        view.Print(grid);

        /// Simulation loop
        while (true)
        {
            /// If not paused
            if (!paused)
            {
                /// Generates the events for the current frame
                GenerateEvents();

                /// Iterate through each event and run it
                foreach (string currentEvent in events)
                    RunEvent(grid, currentEvent);

                /// Update changed positions
                view.UpdateView(grid);
            }

            /// Delay between the next simulation update
            yield return new WaitForSeconds(0.05f);
        }
    }

    /// <summary>
    /// Stops the simulation
    /// </summary>
    public void StopSimulation()
    {
        paused = false;

        StopAllCoroutines();

        view.HideImage();

        view.RunningUI(false);
    }
}