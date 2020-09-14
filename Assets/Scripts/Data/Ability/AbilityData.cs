using System;
using System.Diagnostics;
using DefaultNamespace;
using DefaultNamespace.Ability;
using DefaultNamespace.Stat;
using Singleton;
using UnityEngine;
using Debug = UnityEngine.Debug;

[Serializable]
public abstract class AbilityData : ScriptableObject
{
    private string id;

    //[SerializeField] private Stat stat;
    [SerializeField] private Sprite sprite;
    [SerializeField] private string path;

    private AbilityDataJSON data;

    //public Stat Stat => stat;

    public Sprite Sprite => sprite;

    public string Path => path;

    public AbilityDataJSON Data => data;

    public void Init()
    {
        GenerateId();
        data = new AbilityDataJSON(path);
    }

    private void GenerateId()
    {
        if (id is null)
        {
            Guid guid = Guid.NewGuid();
            id = guid.ToString();
        }
    }

    public abstract void Use();
}