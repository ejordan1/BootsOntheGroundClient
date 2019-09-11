using System;
using UnityEngine;

namespace FixMath.NET
{
    [Serializable]
    public class Fix64Param
    {
        [SerializeField] private float _rawValue;

        public Fix64 GetValue()
        {
            return (Fix64)_rawValue;
        }
    }
}