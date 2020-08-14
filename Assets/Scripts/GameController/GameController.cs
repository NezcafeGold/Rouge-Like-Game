using System;
using System.Collections;
using System.Collections.Generic;
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
    public Animator PlayerAnimator;


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
        levelName = lvlContrScr.CurrentLevel.Name;

        var levels = lvlContrScr.Levels;
        foreach (Level lvl in levels)
        {
            if (levelName == lvl.Name)
            {
                var lvlScr = lvl.GetComponent<Level>();
                List<SctructTileTypeAmount> listCardAmount = lvlScr.ListOfTiles;

                foreach (var cardAmount in listCardAmount)
                {
                    int amount = cardAmount.amount;
                    TileCard tileCard = cardAmount.tileCard;

                    int j = 0;
                    while (j < amount)
                    {
                        Random.InitState((int) DateTime.Now.Ticks);
                        var x = Random.Range(0, SIDE_SIZE);
                        var y = Random.Range(0, SIDE_SIZE);
                        var gridCellScr = gridField[x, y].GetComponent<GridCell>();
                        if (gridCellScr.TileCard == null && !gridCellScr.PlayerIsHere)
                        {
                            gridCellScr.TileCard = tileCard;
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
       //player.GetComponent<Player>().SetGridCell(gridField[x, y]);
 
        gridScript.setPlayer(player);
    }


    public IEnumerator MovePlayerToDirection(DraggedDirection direction)
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
        }

        GameObject cellToMove = null;
        try
        {
            cellToMove = gridField[gridScript.XCord + xMove, gridScript.YCord + yMove];
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

        if (cellToMove != null && cellToMove.GetComponent<GridCell>().isEnableToMove)
        {
            //TODO КОРУТИНА НА АНИМАЦИЮ
            yield return StartCoroutine(Animation(direction));

            PlayerAnimator.SetFloat("RunRight", 0);
            PlayerAnimator.SetFloat("RunLeft", 0);
            PlayerAnimator.SetFloat("RunUp", 0);
            PlayerAnimator.SetFloat("RunDown", 0);
            
            player.transform.SetParent(cellToMove.transform);
            player.transform.localPosition = new Vector3(0, 0, 0);
            cellToMove.GetComponent<GridCell>().visitTile();
            Messenger.Broadcast(GameEvent.PLAYER_MOVE_ON_CELL,
                cellToMove.GetComponent<GridCell>()); //CardPanel.ShowCardsFromTile
            PlayerSetup.GetPlayerSetup().SubtractStamina(1);
        }


        yield return null;
    }

    private IEnumerator Animation(DraggedDirection direction)
    {
        if (direction == DraggedDirection.Left)
        {
            PlayerAnimator.SetFloat("RunLeft", 1);           
        }
        if (direction == DraggedDirection.Right)
        {
            PlayerAnimator.SetFloat("RunRight", 1);
        }
        if (direction == DraggedDirection.Up)
        {
            PlayerAnimator.SetFloat("RunUp", 1);
        }
        if (direction == DraggedDirection.Down)
        {
            PlayerAnimator.SetFloat("RunDown", 1);
        }
        yield return new WaitForSeconds(2.8f);
    }
}