using System;
using MyMvcProject.Data;

using UnityEngine;

namespace MapSetup.Model.Responses
{
    [Serializable]
    public class BasicResponse : Response
    {
        [SerializeField] public BasicModel basicModel;
    }
}