using System;
using MapSetup.Model;
using UnityEngine;

namespace MyMvcProject.Data
{
    [Serializable]
    public class BasicModel
    {
        public BasicModel()
        {
            userId = ClientStaticData.UserID;
        }

        [SerializeField]
        public string userId;

        [SerializeField]
        public int i1;

        [SerializeField]
        public string s1;

        [SerializeField]
        public string s2;

    }
}