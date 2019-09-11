using System;
using System.Collections.Generic;
using MapSetup.Model.ModelClasses;
using UnityEngine;

namespace MapSetup.Model
{
    [Serializable]
    public class PartyServerModel
    
    
    {
        
        public enum PartyState{lobby, formation, battle}
        
        /*  [SerializeField] 
          public int partyId;*/
        
        [SerializeField]
        public List<MemberModelInt> members;

        [SerializeField]
        public string mapIDSelected;

        [SerializeField]
        public int state;
        
        [SerializeField]
        public List<string> mapHistory;

        [SerializeField]
        public List<string> suggestedMaps;

        [SerializeField]
        public List<string> inviteRequests;
        
        [SerializeField]
        public List<string> unitPrepJsons;

    }
}