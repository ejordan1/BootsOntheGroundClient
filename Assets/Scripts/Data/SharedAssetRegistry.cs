using System;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable UnassignedField.Global

namespace Data
{
    [Serializable]
    public class SharedAssetRegistry<TData> : ScriptableObject, IAssetRegistry<TData>
        where TData : class, IData
    {
        [SerializeField] public List<TData> Data;

        public TData GetById(string itemId)
        {
            return Data.Find(data => data.Id == itemId);
        }

        public TData this[int i]
        {
            get { return Data[i]; }
        }

        public IEnumerator<TData> GetEnumerator()
        {
            return Data.GetEnumerator();
        }
    }
}