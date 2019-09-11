using System;
using UnityEngine;

namespace MapSetup.Model.ModelClasses
{
    [Serializable]
    public class MemberModel
    {
        
        public enum userState{lobbying, committed, forming, ready, battling, waiting};
        [SerializeField]
        public string userId;
        [SerializeField]
        public userState state;
        
        public MemberModel(string incomingUserId)
        {
            userId = incomingUserId;
            state = userState.lobbying;
        }
      
    }
}