using System.Collections;
using System.Collections.Generic;
using Singleton;
using UnityEngine;

public class LevelController : Singleton<LevelController>
{
    [SerializeField] private LevelData[] levels;
    [SerializeField] private LevelData currentLevel;

    public LevelData GetCurrentLevel()
    {
        return currentLevel;
    }

    public LevelData SetCurrentLevel(int id)
    {
        foreach (var lvl in levels)
        {
            if (lvl.Id == id)
            {
                currentLevel = lvl;
                break;
            }
        }
        return currentLevel;
    }
}