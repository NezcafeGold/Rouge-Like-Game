
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;

public class GridCell : MonoBehaviour
{
    [SerializeField] private Sprite shirtTile;
    [SerializeField] private Sprite visitedTile;
    private int xCord;
    private int yCord;
    private bool playerIsHere = false;
    public Tile tileData;

    public bool isEnableToMove = true;
    private GameObject player;

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

    private void Start()
    {
        if (!playerIsHere)
            gameObject.GetComponent<Image>().sprite = shirtTile;
        else
            gameObject.GetComponent<Image>().sprite = visitedTile;
    }

    public void setPlayer(GameObject _player)
    {
        player = _player;
        player.transform.SetParent(gameObject.transform);
        player.transform.localPosition = new Vector3(0, 0, 0);
        playerIsHere = true;
    }

    public void openTile()
    {
        gameObject.GetComponent<Image>().sprite = visitedTile;
    }
}