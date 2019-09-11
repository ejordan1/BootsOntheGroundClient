using System;
using System.Collections.Generic;
using Model.Units;
using UnityEngine;

namespace MapSetup.Model
{
    [Serializable]
    public class UnitPrepsModel
    {
        public UnitPrepsModel()
        {
            
        }
        
        public UnitPrepsModel(List<UnitPrep> units)
        {
            unitPrepList = units;
        }

        [SerializeField]
        public List<UnitPrep> unitPrepList;
        
    }
}
