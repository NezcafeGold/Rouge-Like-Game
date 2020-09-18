using System;
using DefaultNamespace.Ability;
using LitJson;
using UnityEngine;

namespace DefaultNamespace
{
    public class AbilityDataJSON
    {
        private string name;

        private string description;

        //Сколько очков требуется для удачи
        private int luckCount;

        //Сколько клеток должно быть у абилки
        private int cellLuckCount;
        private AbilitityWhatEnum abilitityWhatEnum;
        private AbilityWhenEnum abilityWhenEnum;
        private AbilityDoEnum abilityDoEnum;

        private ColorEnum color;

        //Число, которое используется в описании! Важно, чтобы в описании число совпадало с этим
        private int valueForAbility;

        public string Name => name;

        public string Description => description;

        public int LuckCount => luckCount;

        public int CellLuckCount => cellLuckCount;

        public AbilitityWhatEnum AbilitityWhatEnum => abilitityWhatEnum;

        public AbilityWhenEnum AbilityWhenEnum => abilityWhenEnum;

        public AbilityDoEnum AbilityDoEnum => abilityDoEnum;

        public ColorEnum Color => color;

        public int ValueForAbility => valueForAbility;

        public AbilityDataJSON(string path)
        {
            Init(path);
        }

        private void Init(string path)
        {
            TextAsset jsonTextFile = Resources.Load<TextAsset>(path);
            JsonData data = JsonMapper.ToObject(jsonTextFile.ToString());
            Debug.Log("");
            string local = LocalizationManager.CurrentLanguage;

            name = data[local]["name"].ToString();
            description = data[local]["description"].ToString();
            valueForAbility = (int) data["valueForAbility"];
            luckCount = (int) data["luckCount"];
            cellLuckCount = (int) data["cellLuckCount"];
            abilitityWhatEnum =
                (AbilitityWhatEnum) Enum.Parse(typeof(AbilitityWhatEnum), data["abilityWhatEnum"].ToString());
            abilityWhenEnum = (AbilityWhenEnum) Enum.Parse(typeof(AbilityWhenEnum), data["abilityWhenEnum"].ToString());
            abilityDoEnum = (AbilityDoEnum) Enum.Parse(typeof(AbilityDoEnum), data["abilityDoEnum"].ToString());
            color = (ColorEnum) Enum.Parse(typeof(ColorEnum), data["color"].ToString());
        }
    }
}