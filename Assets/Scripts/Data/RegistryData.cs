using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public interface IData
    {
        string Id { get; }
    }
    
    public interface IAssetRegistry<TData>
        where TData : class, IData
    {
        TData this[int i] { get; }
        IEnumerator<TData> GetEnumerator();
        TData GetById(string id);
    }

    [Serializable]
    public abstract class RegistryData : IData
    {
        [SerializeField] public string Name;

        public string Id
        {
            get { return Name; }
        }
    }
}
