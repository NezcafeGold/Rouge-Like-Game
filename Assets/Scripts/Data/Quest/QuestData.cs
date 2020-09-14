using System;
using UnityEngine;

[CreateAssetMenu(menuName = "QuestData")]
[Serializable]
public class QuestData : ScriptableObject
{

   public string QuestName;
   public string PathToJSON;
   public Sprite SpriteQuest;

}