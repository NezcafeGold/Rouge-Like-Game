using System;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject gridCell;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject levelController;
    private string levelName;
    private GameObject[,] gridField;

    private const int SIDE_SIZE = 5;

    private void Awake()
    {
        gridField = new GameObject[SIDE_SIZE, SIDE_SIZE];
    }

    private void Start()
    {
        for (var i = 0; i < SIDE_SIZE; i++)
        {
            for (var j = 0; j < SIDE_SIZE; j++)
            {
                var cellObj = Instantiate(gridCell);

                cellObj.transform.SetParent(panel.transform);
                cellObj.GetComponent<RectTransform>().localScale = Vector3.one;
                var gridScript = cellObj.GetComponent<GridCell>();
                gridScript.XCord = i;
                gridScript.YCord = j;
                gridField[i, j] = cellObj;
            }
        }

        SetPlayerOnStartGame();
        SetTilesData();
    }

    private void SetTilesData()
    {
        var lvlContrScr = levelController.GetComponent<LevelController>();
        levelName = lvlContrScr.LvlName;
        var levels = lvlContrScr.Levels;
        foreach (Level lvl in levels)
        {
            if (levelName == lvl.Name)
            {
                var lvlScr = lvl.GetComponent<Level>();
                int[] tileValues = lvlScr.TailValue;
                TileCard[] tilesCard = lvlScr.TilesCard;
                for (int i = 0; i < tilesCard.Length; i++)
                {
                    int tileV = tileValues[i];
                    TileCard tileCard = tilesCard[i];
                    int j = 0;
                    while (j < tileV)
                    {
                        Random.InitState((int) DateTime.Now.Ticks);
                        var x = Random.Range(0, SIDE_SIZE);
                        var y = Random.Range(0, SIDE_SIZE);
                        var gridCellScr = gridField[x, y].GetComponent<GridCell>();
                        if (gridCellScr.tileCard == null && !gridCellScr.PlayerIsHere)
                        {
                            gridCellScr.tileCard = tileCard;
                            j++;
                            gridCellScr.UpdateDataTile();
                        }
                    }
                }
            }
        }
    }

    private void SetPlayerOnStartGame()
    {
        Random.InitState((int) DateTime.Now.Ticks);
        var x = Random.Range(0, SIDE_SIZE);
        var y = Random.Range(0, SIDE_SIZE);
        var gridScript = gridField[x, y].GetComponent<GridCell>();
        gridScript.PlayerIsHere = true;
        gridScript.setPlayer(player);
    }


    public void MovePlayerToDirection(DraggedDirection direction)
    {
        var gridScript = player.transform.parent.GetComponent<GridCell>();
        int xMove = 0;
        int yMove = 0;
        switch (direction)
        {
            case DraggedDirection.Up:
                xMove--;
                break;
            case DraggedDirection.Down:
                xMove++;
                break;
            case DraggedDirection.Right:
                yMove++;
                break;
            case DraggedDirection.Left:
                yMove--;
                break;
            default:
                return;
        }


        try
        {
            GameObject cellToMove = gridField[gridScript.XCord + xMove, gridScript.YCord + yMove];
            if (cellToMove.GetComponent<GridCell>().isEnableToMove)
            {
                //TODO КОРУТИНА НА АНИМАЦИЮ
                player.transform.SetParent(cellToMove.transform);
                player.transform.localPosition = new Vector3(0, 0, 0);
                cellToMove.GetComponent<GridCell>().visitTile();
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}