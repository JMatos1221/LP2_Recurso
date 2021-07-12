using LP2_Recurso;
using UnityEngine;
using UnityEngine.UI;
using Grid = LP2_Recurso.Grid;

/// <summary>
/// Visualization of the Simulation
/// </summary>
public class View : MonoBehaviour
{
    /// Button Game Object
    [Header("UI Elements")]
    [SerializeField]
    private GameObject startButton;
    /// Pause and Stop Button Game objects
    [SerializeField] private GameObject pauseButton, stopButton;
    /// Input and pause text Game objects 
    [SerializeField] private GameObject inputParent, pauseText;
    /// 
    [SerializeField] private RawImage image;
    /// Represents the grid 
    private Texture2D texture;

    /// <summary>
    /// Updated the grid's changed positions
    /// </summary>
    /// <param name="grid">Grid to update</param>
    public void UpdateView(Grid grid)
    {
        /// Update every position that changed
        for (int i = 0; i < grid.PositionsToUpdate.Count; i += 2)
            SetPieceColor(grid, grid.PositionsToUpdate[i],
                grid.PositionsToUpdate[i + 1]);

        grid.PositionsToUpdate.Clear();
        /// Applies the texture in order to update visualization
        texture.Apply();
    }

    /// <summary>
    /// Creates a new texture, changes the image size 
    /// and sets the image texture to the created one
    /// </summary>
    /// <param name="x">Texture width, same as the grid width</param>
    /// <param name="y">Texture height, same as the grid height</param>
    public void CreateTexture(int x, int y)
    {
        /// Creates a new texture with the given size
        texture = new Texture2D(x, y);
        /// Sets the texture filter mode to point filter, 
        /// for a clean representation
        texture.filterMode = FilterMode.Point;

        /// Image ratio
        float aux = x / (float)y;

        /// Resize the image to always have 500 pixels of width or height
        /// depending on it's ratio
        if (aux > 1)
        {
            x = 500;
            y = (int)(500 * (1 / aux));
        }
        else
        {
            y = 500;
            x = (int)(500 * aux);
        }

        /// Sets the image size
        image.gameObject.GetComponent<RectTransform>().sizeDelta =
            new Vector2(x, y);

        /// Sets the image texture to the created one
        image.texture = texture;
    }

    /// <summary>
    /// Prints the grid
    /// </summary>
    /// <param name="grid">Grid to print</param>
    public void Print(Grid grid)
    {
        for (int i = 0; i < grid.YDim; i++)
        {
            for (int j = 0; j < grid.XDim; j++) SetPieceColor(grid, j, i);
        }

        texture.Apply();
    }

    /// <summary>
    /// Set Current Piece Color
    /// </summary>
    /// <param name="grid">Grid to get piece from</param>
    /// <param name="x">Position X</param>
    /// <param name="y">Position Y</param>
    private void SetPieceColor(Grid grid, int x, int y)
    {
        switch (grid.GetPiece(x, y))
        {
            case Piece.None:
                texture.SetPixel(x, y, Color.black);

                break;

            case Piece.Rock:
                texture.SetPixel(x, y, Color.blue);

                break;

            case Piece.Paper:
                texture.SetPixel(x, y, Color.green);

                break;

            case Piece.Scissors:
                texture.SetPixel(x, y, Color.red);

                break;
        }
    }

    /// <summary>
    /// Shows the image that represents the grid  
    /// </summary>
    public void ShowImage()
    {
        image.gameObject.SetActive(true);
    }

    /// <summary>
    /// Hides the image that represents the grid
    /// </summary>
    public void HideImage()
    {
        image.gameObject.SetActive(false);
    }

    /// <summary>
    /// Sets the running UI on or off
    /// </summary>
    /// <param name="state">Running UI state</param>
    public void RunningUI(bool state)
    {
        inputParent.SetActive(!state);
        startButton.SetActive(!state);
        pauseButton.SetActive(state);
        stopButton.SetActive(state);

        if (!state) PausedUI(false);
    }

    /// <summary>
    /// Shows or hides the pause UI
    /// </summary>
    /// <param name="state">Show state</param>
    public void PausedUI(bool state)
    {
        pauseText.SetActive(state);
    }
}