using System;
using System.Collections.Generic;
using Model.Units;
using UnityEngine;

namespace MapSetup.Model.ModelClasses
{
    [Serializable]
    public class UnitPrepUploadModel
    {
        
        [SerializeField]
        public string userId;
        
        [SerializeField]
        public string unitPreps;
        
    }
}