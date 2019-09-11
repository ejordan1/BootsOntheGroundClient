using System;
using UnityEngine;

namespace MapSetup.Model
{
    [Serializable]
    public class JContainer
    {
        [SerializeField] 
        public string type;
        [SerializeField] 
        public string json;

    }
}