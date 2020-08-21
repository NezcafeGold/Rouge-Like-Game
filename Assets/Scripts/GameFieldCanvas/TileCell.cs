using UnityEngine;
using UnityEngine.UI;

public class TileCell : MonoBehaviour
{
    [SerializeField] private Sprite startedTile;
    private TileCellValues tileCellValues;

    public TileCellValues TileСellValues
    {
        get { return tileCellValues; }
        set { tileCellValues = value; }
    }
   

    public void UpdateTileCell()
    {
        if(tileCellValues.PlayerIsHere)
        {
            SetPlayer(Player.Instance.gameObject);
            gameObject.GetComponent<Image>().sprite = startedTile;
        }
    }

    public void SetPlayer(GameObject player)
    {
        player.transform.SetParent(transform);
        player.transform.localPosition = new Vector3(0, 0, 0);
    }

    //When a player step on this cell
    public void VisitTile()
    {
        if (!TileСellValues.IsVisited)
        {
            gameObject.GetComponent<Image>().sprite = tileCellValues.TypeTileCardData.visitedTile;
        }
    }

    public void CheckTileShirt()
    {
        if (!tileCellValues.IsVisited && !tileCellValues.IsStartPoint)
        {
            gameObject.GetComponent<Image>().sprite = tileCellValues.TypeTileCardData.openedTile;
            //PlayerSetup.Instance.SubtractStamina(1);
        }
        
    }
}