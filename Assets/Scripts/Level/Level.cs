using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]private int id;
    [SerializeField]private string name;

    [SerializeField] private List<SctructTileTypeAmount> listOfTiles;

    public List<SctructTileTypeAmount> ListOfTiles
    {
        get { return listOfTiles; }
        set { listOfTiles = value; }
    }

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

}