using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelData")]
public class LevelData : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private string name;
    [SerializeField] private List<SctructTileTypeAmount> listOfTiles;
    [SerializeField] private List<EnemyData> enemies;
    [SerializeField] private List<WeaponData> weapons;

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

    public List<EnemyData> Enemies
    {
        get { return enemies; }
        set { enemies = value; }
    }

    public List<WeaponData> Weapons
    {
        get { return weapons; }
        set { weapons = value; }
    }
}