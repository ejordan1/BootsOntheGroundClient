using System;
using System.Collections.Generic;
using MyMvcProject.Data;
using UnityEngine;

namespace MapSetup.Model.Responses
{
    [Serializable]
    public class LoadUnitsResponse : Response
    {
       [SerializeField]
        public List<UnitListContainer> unitPrepJsons;
    }
}