using LP2_Recurso;
using UnityEngine;
using UnityEngine.UI;
using Grid = LP2_Recurso.Grid;

public class View : MonoBehaviour
{
    [Header("UI Elements")] [SerializeField]
    private GameObject startButton;

    [SerializeField] private GameObject pauseButton, stopButton;

    [SerializeField] private GameObject inputParent, pauseText;
    [SerializeField] private RawImage image;
    private Texture2D texture;

    public void UpdateView(Grid grid)
    {
        for (int i = 0; i < grid.PositionsToUpdate.Count; i += 2)
            SetPieceColor(grid, grid.PositionsToUpdate[i],
                grid.PositionsToUpdate[i + 1]);

        grid.PositionsToUpdate.Clear();
        texture.Apply();
    }

    public void CreateTexture(int x, int y)
    {
        texture = new Texture2D(x, y);
        texture.filterMode = FilterMode.Point;

        float aux = x / (float) y;

        if (aux > 1)
        {
            x = 500;
            y = (int) (500 * (1 / aux));
        }
        else
        {
            y = 500;
            x = (int) (500 * aux);
        }


        image.gameObject.GetComponent<RectTransform>().sizeDelta =
            new Vector2(x, y);

        image.texture = texture;
    }

    public void Print(Grid grid)
    {
        for (int i = 0; i < grid.YDim; i++)
        {
            for (int j = 0; j < grid.XDim; j++) SetPieceColor(grid, j, i);
        }

        texture.Apply();
    }

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

    public void ShowImage()
    {
        image.gameObject.SetActive(true);
    }

    public void HideImage()
    {
        image.gameObject.SetActive(false);
    }

    public void RunningUI(bool state)
    {
        inputParent.SetActive(!state);
        startButton.SetActive(!state);
        pauseButton.SetActive(state);
        stopButton.SetActive(state);

        if (!state) PausedUI(false);
    }

    public void PausedUI(bool state)
    {
        pauseText.SetActive(state);
    }
}