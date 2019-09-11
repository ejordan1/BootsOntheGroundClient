using System;
using System.Collections.Generic;
using MapSetup.Model.ModelClasses;
using Model.Units;

using UnityEngine;


//I should probably just get this stuff on the server anyways.
namespace MapSetup.Model
{
    [Serializable]
    public class PartyModel
    {
        public PartyModel()
        {
            partyMembers = new List<MemberModel>();
            finalPrepList = new List<UnitPrep>();
            mapHistory = new List<MapModel>();
            suggestedMaps = new List<MapModel>();
            inviteRequests = new List<string>();
        }

        [SerializeField]
        public List<MemberModel> partyMembers;

        [SerializeField]
        public MapModel mapSelected;

        public PartyServerModel.PartyState partyState;
        
        //this is shitty.
        
        //unit prep from map or this class?
        public List<UnitPrep> finalPrepList;
        
        public List<MapModel> mapHistory;

        public List<MapModel> suggestedMaps; //not by name at the moment

        public List<string> inviteRequests;

    }
   
}