using DefaultNamespace;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]private int id;
    [SerializeField]private string name;
    [SerializeField]private Tile[] tiles;
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

    public Tile[] Tiles
    {
        get { return tiles; }
        set { tiles = value; }
    }

    public int[] TailValue
    {
        get { return tailValue; }
        set { tailValue = value; }
    }
}