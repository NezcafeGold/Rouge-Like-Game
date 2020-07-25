using DefaultNamespace;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]private int id;
    [SerializeField]private string name;
    [SerializeField]private TileCard[] tilesCard;
    [SerializeField]private int[] tailValue;

    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public TileCard[] TilesCard
    {
        get { return tilesCard; }
        set { tilesCard = value; }
    }

    public int[] TailValue
    {
        get { return tailValue; }
        set { tailValue = value; }
    }
}