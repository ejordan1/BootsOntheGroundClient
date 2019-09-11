using System;
using UnityEngine;

namespace MapSetup.Model.ModelClasses
{
    [Serializable]
    public class MemberModelInt
    {
        
        // public enum userState{lobbying, committed, forming, ready, battling, waiting};
        [SerializeField]
        public string userId;
        [SerializeField]
        public int state;
        
        
        //should never need constructor
        public MemberModelInt(string incomingUserId)
        {
            userId = incomingUserId;
            state = 0;
        }
      
    }
}