using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class GridCell : MonoBehaviour
{
    [SerializeField] private Sprite closedShirt;
    [SerializeField] private Sprite visitedTile;
    private int xCord;
    private int yCord;
    private bool playerIsHere = false;
    private bool startPoint;
    private bool visited = false;
    private TileCard tileCard;
    private Sprite openShirt;

    public bool isEnableToMove = true;
    private GameObject player;


    public TileCard TileCard
    {
        get { return tileCard; }
        set { tileCard = value; }
    }

    public int XCord
    {
        get { return xCord; }
        set { xCord = value; }
    }

    public int YCord
    {
        get { return yCord; }
        set { yCord = value; }
    }

    public bool PlayerIsHere
    {
        get { return playerIsHere; }
        set { playerIsHere = value; }
    }

    public bool Visited
    {
        get { return visited; }
        set { visited = value; }
    }

    void Awake()
    {

    }

    private void Start()
    {
        if (!playerIsHere)
            gameObject.GetComponent<Image>().sprite = closedShirt;
        else
            gameObject.GetComponent<Image>().sprite = visitedTile;

        startPoint = playerIsHere;
    }

    public void setPlayer(GameObject _player)
    {
        player = _player;
        player.transform.SetParent(gameObject.transform);
        player.transform.localPosition = new Vector3(0, 0, 0);
        playerIsHere = true;
    }

    //When a player step on this cell
    public void visitTile()
    {
        if (!visited)
        {
            gameObject.GetComponent<Image>().sprite = visitedTile;
        }
        
    }

    public void UpdateDataTile()
    {
        openShirt = tileCard.sprite;
    }

    public void CheckTileShirt()
    {
        if (!visited && !startPoint)
            gameObject.GetComponent<Image>().sprite = openShirt;
    }
}