using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    private int xCord;
    private int yCord;

    public bool isEnableToMove = true;

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

    private GameObject player;

    public void setPlayer(GameObject _player)
    {
        player = _player;
        player.transform.SetParent(gameObject.transform);
        player.transform.localPosition = new Vector3(0,0,0);
    }
    
}
