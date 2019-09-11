
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using MapSetup.Model;
using MapSetup.Model.Responses;
using Model.Units;
using MyMvcProject.Data;
using Scripts.MapSetup.Services;
using UnityEngine;

namespace MapSetup.Services  //shoulnd't be static. should be thorugh zenect
{
    
    public static class CallService
    {
        public static ChatClient _client;

        private static Dictionary<string, Type> _commandTypes = new Dictionary<string, Type>
        {
            {"userJoined", typeof(UserJoinedCommand)},
            {"userLeft", typeof(UserJoinedCommand)},
            {"mapChoose", typeof(MapChooseCommand)},
            {"mapSuggest", typeof(MapChooseCommand)},
            {"commit", typeof(BattleStateCommand)},
            {"formationSet", typeof(BattleStateCommand)},
            {"battleEnd", typeof(BattleStateCommand)},
            {"returnToLobby", typeof(BattleStateCommand)}

        };
        
        public static void Call(JContainer model)
        {
            Debug.Log(model.type);
            Debug.Log(model.json);
            
            var commandType = _commandTypes[model.type];
            
            Debug.Log(commandType.ToString());
            
            
            var command = (GameCommand)Activator.CreateInstance(commandType, model);
            
            command.Execute();
        }
        

        
      /*  
        public static void call(ChatModel model)
        {
            
            
            switch (model.type)
            {
                
                case "" :

                    break;
                case "userJoined" :
                    Debug.Log("user joined " + model.UserId);
                    ClientStaticData.currentParty.partyMembers.Add(new MemberModel(model.UserId));
                    break;
                    
                case "userDispose" :
                    Debug.Log("user disposed " + model.UserId);
                    var mModel = ClientStaticData.currentParty.partyMembers.FirstOrDefault<MemberModel>(m => m.UserID == model.UserId);
                    ClientStaticData.currentParty.partyMembers.Remove(mModel);
                    break;
                    
                case "mapChosen" :
                    ClientStaticData.currentParty.mapIDSelected = model.s2;
                    break;
                    
                case "mapSuggested" :
                    var map = StaticAllData.allMaps.FirstOrDefault<MapModel>(m => m._mapName == model.s2);
                    ClientStaticData.currentParty.suggestedMaps[model.UserId] = map;
                    break;
                    
                case "commit" :
                    var mModel3 = ClientStaticData.currentParty.partyMembers.FirstOrDefault<MemberModel>(m => m.UserID == model.UserId);
                    if (mModel3.state == MemberModel.userState.lobbying)
                    mModel3.state = MemberModel.userState.committed;
                    Debug.Log(mModel3.UserID + " is now " + mModel3.state);
                    
                    break;
                    
                case "countDown" :

                    break;
                    
                case "formationPhase" :

                    break;
                    
                case "formationSet" :
                    var mModel4 = ClientStaticData.currentParty.partyMembers.FirstOrDefault<MemberModel>(m => m.UserID == model.UserId);
                    if (mModel4.state == MemberModel.userState.forming)
                        mModel4.state = MemberModel.userState.ready;
                    Debug.Log(mModel4.UserID + " is now " + mModel4.state);
                    break;
                    
                case "requestUnits" :

                    UnitPrepsModel units = new UnitPrepsModel();
                    units.UnitPreps = StaticAllData.currentMap._formationUnits;
                    string s = JsonUtility.ToJson(units);
                    ChatModel chat = new ChatModel();
                    chat.UserId = ClientStaticData.UserID;
                    chat.type = "unitsUpload";
                    chat.json1 = s;
                    _client.Send(chat);
                    
                    break;
                    
                case "battleStarted" :

                    break;
                case "battleEnded" :

                    break;
                    
                case "error" :
                    Debug.Log(model.s1);
                    break;
                    */
                /*case "currentParties" :
                Debug.Log("Current Parties");
                Intlist intList = JsonUtility.FromJson<Intlist>(model.json1);
                for (int i = 0; i < intList.ints.Count; i++)
                {
                    Debug.Log(intList.ints[i]);
                }
                break;
                
            case "joinedParty" :
               
                PartyServerModel psm = JsonUtility.FromJson<PartyServerModel>(model.json1);
                Debug.Log("joined party " + psm.partyId);
                break;
            }

        }*/


        
       
    }
}



 /*  Debug.Log(chatModel.UserId + chatModel.type);
            if (chatModel.UserId != null)
            {
                Debug.Log("userId = " + chatModel.UserId);
            }
            else
            {
                Debug.Log("no user Id!");
            }
            if (chatModel.type != null)
            {
                Debug.Log(chatModel.type);
            }
            else
            {
                Debug.Log("type is null!");
            }
            if (chatModel.i1 != 0)
            {
                Debug.Log("i1 = " + chatModel.i1);
            }
            if (chatModel.i2 != 0)
            {
                Debug.Log("i2 = " + chatModel.i2);
            }
            if (chatModel.i3 != 0)
            {
                Debug.Log("i3 = " + chatModel.i3);
            }
            if (chatModel.i4 != 0)

            {
                Debug.Log("i4 = " + chatModel.i4);
            }
            if (!string.IsNullOrEmpty(chatModel.s1))
            {
                Debug.Log("s1 = " + chatModel.s1);
            }
            if (!string.IsNullOrEmpty(chatModel.s2))
            {
                Debug.Log("s2 = " + chatModel.s2);
            }
            if (!string.IsNullOrEmpty(chatModel.s3))
            {
                Debug.Log("s3 = " + chatModel.s3);
            }
            if (!string.IsNullOrEmpty(chatModel.s4))
            {
                Debug.Log("s4 = " + chatModel.s4);
            }

            
            
            
            
            if (!string.IsNullOrEmpty(chatModel.json1) )
            {
                obj1practice obj1 = JsonUtility.FromJson<obj1practice>(chatModel.json1);
                Debug.Log(obj1.objString);
            }
            if (!string.IsNullOrEmpty(chatModel.json2))
            {
                obj1practice obj2 = JsonUtility.FromJson<obj1practice>(chatModel.json2);
                Debug.Log(obj2.objString);

            }

            if (!string.IsNullOrEmpty(chatModel.json1))
            {
                PartyServerModel party = JsonUtility.FromJson<PartyServerModel>(chatModel.json1);
                foreach (string s in party.inviteRequests)
                {
                    Debug.Log(s);
                }

            }
            */