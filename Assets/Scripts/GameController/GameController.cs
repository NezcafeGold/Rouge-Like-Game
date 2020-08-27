using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Singleton;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : Singleton<GameController>
{
    private string levelName;
    private TileCellValues[,] tileCellValuesArray;
    private const int SIDE_SIZE = 5;

    private void Awake()
    {
        tileCellValuesArray = new TileCellValues[SIDE_SIZE, SIDE_SIZE];
        for (int i = 0; i < SIDE_SIZE; i++)
        {
            for (int j = 0; j < SIDE_SIZE; j++)
            {
                tileCellValuesArray[i, j] = new TileCellValues(i, j);
            }
        }

        ReRollRandom();
    }

    public TileCellValues[,] TileCellValuesArray
    {
        get { return tileCellValuesArray; }
    }

    public void ReRollRandom()
    {
        SetRandomPlayer();
        SetRandomTileCellData();
    }

    private void SetRandomTileCellData()
    {
        var lvlController = LevelController.Instance;

        var currentLvl = lvlController.GetCurrentLevel();

        List<SctructTileTypeAmount> listCardAmount = currentLvl.ListOfTiles;

        foreach (var cardAmount in listCardAmount)
        {
            int amount = cardAmount.amount;
            TypeTileCardData typeTileCardData = cardAmount.typeTileCardData;

            int j = 0;
            while (j < amount)
            {
                Random.InitState((int) DateTime.Now.Ticks);
                var x = Random.Range(0, SIDE_SIZE);
                var y = Random.Range(0, SIDE_SIZE);
                if (!tileCellValuesArray[x, y].PlayerIsHere && tileCellValuesArray[x, y].TypeTileCardData==null)
                {
                    tileCellValuesArray[x, y].TypeTileCardData = typeTileCardData;
                    j++;
                }
            }
        }
        Debug.Log("");
    }

    private void SetRandomPlayer()
    {
        Random.InitState((int) DateTime.Now.Ticks);
        var x = Random.Range(0, SIDE_SIZE);
        var y = Random.Range(0, SIDE_SIZE);
        tileCellValuesArray[x, y].PlayerIsHere = true;
        Debug.Log("Player on "+x+"/"+y);
    }
}