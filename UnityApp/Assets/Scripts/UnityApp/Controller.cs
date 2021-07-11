using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using LP2_Recurso;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Grid = LP2_Recurso.Grid;

public class Controller : MonoBehaviour, IController
{
    [SerializeField]
    private InputField xDimIn, yDimIn, swapRateIn, reprRateIn, selRateIn;

    private List<string> events;
    private bool paused;
    private Poisson poisson;
    private System.Random rnd;
    private float swapRate, reprRate, selRate;
    private View view;
    private int xDim, yDim;

    private void Start()
    {
        view = GetComponent<View>();
        events = new List<string>();
        rnd = new System.Random();
        paused = false;
    }

    public void TogglePause()
    {
        paused = !paused;
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif

#if UNITY_STANDALONE
        Application.Quit(0);
#endif
    }

    public void StartSimulation()
    {
        if (xDimIn.text == string.Empty ||
            yDimIn.text == string.Empty ||
            swapRateIn.text == string.Empty ||
            reprRateIn.text == string.Empty ||
            selRateIn.text == string.Empty)
            return;

        CultureInfo culture = CultureInfo.InvariantCulture;

        xDim = Convert.ToInt32(xDimIn.text, culture);
        yDim = Convert.ToInt32(yDimIn.text, culture);

        swapRate = Convert.ToSingle(swapRateIn.text, culture);
        reprRate = Convert.ToSingle(reprRateIn.text, culture);
        selRate = Convert.ToSingle(selRateIn.text, culture);

        StartCoroutine("SimulationCoroutine");
    }

    private void GenerateEvents()
    {
        int numSwap, numRepr, numSel;

        numSwap = poisson.Next(swapRate);
        numRepr = poisson.Next(reprRate);
        numSel = poisson.Next(selRate);

        events.Clear();

        for (int i = 0; i < numSwap; i++) events.Add("Swap");

        for (int i = 0; i < numRepr; i++) events.Add("Reproduction");

        for (int i = 0; i < numSel; i++) events.Add("Selection");

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

    private IEnumerator SimulationCoroutine()
    {
        Grid grid = new Grid(xDim, yDim);
        view.CreateTexture(xDim, yDim);
        poisson = new Poisson(xDim, yDim);

        grid.Fill();
        view.Print(grid);

        while (true)
        {
            if (!paused)
            {
                GenerateEvents();

                foreach (string currentEvent in events)
                    RunEvent(grid, currentEvent);

                view.UpdateView(grid);
            }

            yield return new WaitForSeconds(0.05f);
        }
    }
}