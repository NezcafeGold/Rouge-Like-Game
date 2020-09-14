using System;
using LitJson;
using UnityEngine;

namespace DefaultNamespace.Ability
{
    [Obsolete]
    public class JsonParser
    {
        public T GetData<T>(string path)
        {
            TextAsset jsonTextFile = Resources.Load<TextAsset>(path);
            T data = JsonMapper.ToObject<T>(jsonTextFile.text);
            return data;
        }
    }
}