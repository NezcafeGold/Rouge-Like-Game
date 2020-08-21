using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour
{
    private GameObject[,] tileGOArray;

    public GameObject[,] TileGoArray
    {
        get { return tileGOArray; }
        set { tileGOArray = value; }
    }

    private void Start()
    {
        TileCellValues[,] tileCellValuesArray = GameController.Instance.TileCellValuesArray;
        tileGOArray = new GameObject[5, 5];
        int x = 0;
        int y = 0;

        foreach (Transform t in transform)
        {
            tileGOArray[x, y] = t.gameObject;
            t.GetComponent<TileCell>().TileСellValues = tileCellValuesArray[x, y];
            t.GetComponent<TileCell>().UpdateTileCell();
            x++;
            if (x > 4)
            {
                y++;
                x = 0;
            }

            if (y > 4)
            {
                y = 0;
            }
            Debug.Log(x+"+"+y);
        }
        Debug.Log("");
    }

    public GameObject GetTileCellByCord(int x, int y)
    {
        return tileGOArray[x, y];
    }
}