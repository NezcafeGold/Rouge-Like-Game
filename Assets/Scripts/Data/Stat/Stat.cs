using System;
using UnityEngine;

namespace DefaultNamespace.Stat
{
    [Serializable]
    public abstract class Stat : MonoBehaviour
    {
        private int value;

        public void ChangeValue(int value)
        {
            this.value = value;
        }

        public abstract void CheckValue();

        public abstract void UpdateUI();
    }
}