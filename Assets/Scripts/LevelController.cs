using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Level[] levels;
    [SerializeField] private string lvlName;

    public Level[] Levels
    {
        get { return levels; }
    }

    public string LvlName
    {
        get { return lvlName; }
        set { lvlName = value; }
    }
}
