using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private Level[] levels;
    [SerializeField] public  Level CurrentLevel;

    void Awake()
    {

    }
    
    public Level[] Levels
    {
        get { return levels; }
    }

}
