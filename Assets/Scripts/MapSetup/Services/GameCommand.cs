using System;
using System.Linq;
using MapSetup.Model;
using MapSetup.Model.ModelClasses;
using MapSetup.Services;
using Scripts.MapSetup.Services;
using UnityEngine;


namespace MyMvcProject.Data
{
    public abstract class GameCommand
    {
        protected JContainer jC;
        
        public GameCommand(JContainer jCon)
        {
            jC = jCon;

        }

        public abstract void Execute();

        public const string formationAdded = "dfdfdfd";
    }

    public class UserJoinedCommand : GameCommand
    {
        private BasicModel bM;

        public UserJoinedCommand(JContainer jCon) : base(jCon)
        {
            bM = JsonUtility.FromJson<BasicModel>(jCon.json);
        }

        public override void Execute()
        {
            switch (jC.type)
            {
                case "userJoined":
                {
                    Debug.Log("Deserialized obj to : " + bM.GetType());
                    Debug.Log(bM.userId + " says " + bM.s1 + " of type " + jC.type);
                    ClientStaticData.currentParty.partyMembers.Add(new MemberModel(bM.userId));
                    break;
                }

                case "userLeft":
                {
                    Debug.Log("Deserialized obj to : " + bM.GetType());
                    Debug.Log(bM.userId + " says " + bM.s1 + " of type " + jC.type);
                    var member = ClientStaticData.currentParty.partyMembers
                        .FirstOrDefault(m => m.userId == bM.userId);
                    ClientStaticData.currentParty.partyMembers.Remove(member);
                    break;
                }
            }
        }
    }

    public class MapChooseCommand : GameCommand
    {
        private BasicModel bM;
        
        public MapChooseCommand(JContainer jCon) : base(jCon)
        {
            bM = JsonUtility.FromJson<BasicModel>(jCon.json);
        }

        public override void Execute()
        {
            Debug.Log("MAP WAS CHOSEN");

            switch (jC.type)
            {
                case "mapChoose":
                {
                    
                    Debug.Log("Deserialized obj to : " + bM.GetType());
                    Debug.Log(bM.userId + " says " + bM.s1 + " of type " + jC.type);
                    ClientStaticData.currentParty.mapSelected = HomeUtil.mapIdsToMaps(bM.s1);
                    HomeUtil.deCommitAll();
                    //SocketHandler.commit();
                    break;
                }

                case "mapSuggest":
                {
                    
                    Debug.Log("Deserialized obj to : " + bM.GetType());
                    Debug.Log(bM.userId + " says " + bM.s1 + " of type " + jC.type);
                    ClientStaticData.currentParty.suggestedMaps.Add(HomeUtil.mapIdsToMaps(bM.s1));
                    break;
                }
            }
     
        }
    }


    public class BattleStateCommand : GameCommand
    {
        private BasicModel bM;

        public BattleStateCommand(JContainer jCon) : base(jCon)
        {
            bM = JsonUtility.FromJson<BasicModel>(jCon.json);
        }

        public override void Execute()
        {
            switch (jC.type)
            {
                
                case "commit":
                {
                    
                    Debug.Log("Deserialized obj to : " + bM.GetType());
                    Debug.Log(bM.userId + " says " + bM.s1 + " of type " + jC.type);
                    var member = ClientStaticData.currentParty.partyMembers
                        .FirstOrDefault(m => m.userId == bM.userId);
                    if (member.state == MemberModel.userState.lobbying)
                    {
                        member.state = MemberModel.userState.committed;
                    }
                    else
                    {
                        Debug.Log("member commited from something other than lobbying, shouldn't happen");
                    }

                    
                    break;
                }
                   
                case "formationSet":
                {
                    
                    
                    Debug.Log("Deserialized obj to : " + bM.GetType());
                    Debug.Log(bM.userId + " says " + bM.s1 + " of type " + jC.type);
                    break;
                }
                    
                    //probably should receieve the map name to verify same map
                case "formationPhase":
                {   
                    
                    //playmap is static
                    HomeUtil.homePlayMap(ClientStaticData.currentParty.mapSelected);
                    break;
                }
                    
                    //the client needs to request troops from the server!
                case "requestTroops":
                {
                    
                    Debug.Log("Deserialized obj to : " + bM.GetType());
                    Debug.Log(bM.userId + " says " + bM.s1 + " of type " + jC.type);
 
                    //tells home to request troops
                    HomeUtil.homeRequestTroops();
                    
                    break;
                }
                    
                case "battleEnded":
                {
                    
                    Debug.Log("Deserialized obj to : " + bM.GetType());
                    Debug.Log(bM.userId + " says " + bM.s1 + " of type " + jC.type);

                    break;
                }

                case "returnToLobby":
                {
                    
                    
                    Debug.Log("Deserialized obj to : " + bM.GetType());
                    Debug.Log(bM.userId + " says " + bM.s1 + " of type " + jC.type);
                    break;
                }
            }
           

           
        }
    }
    
    


}


































//SocketService.broadCast(model, SocketService._connections);


   /*
                case "createParty":
                    leaveCurrentParty(model, client);
                    PartyModel party = new PartyModel();
                    party.partyId = r.Next(100000, 999999);
                    client.currentParty = party;
                    SocketService._currentParties.Add(party);
                    party.users.Add(new UserModel(model.UserId));

                    ChatModel cPresponse = new ChatModel();
                    cPresponse.type = "partyCreated";
                    cPresponse.i1 = client.currentParty.partyId;
                    SocketService.broadCast(cPresponse, client);
                  
                    
                    break;

                case "joinParty" :
                     leaveCurrentParty(model, client);
                    var partyModel = SocketService._currentParties.FirstOrDefault<PartyModel>(p => p.partyId == model.i1);
                    if (partyModel != null)
                    {
                        partyModel.users.Add(new UserModel(model.UserId));
                        client.currentParty = partyModel;
                        ChatModel jPresponse = new ChatModel();
                        jPresponse.type = "joinedParty";
                        jPresponse.i1 = partyModel.partyId;
                        string s = JsonConvert.SerializeObject(partyModel);
                        jPresponse.json1 = s;
                        SocketService.broadCast(jPresponse, client);
                    }
                    else
                    {
                       sendError("joinedPartyFail", client);
                        
                    }
                    break;

                case "leaveParty" :
                    leaveCurrentParty(model, client);
                    sendTypeMessage("left party", client);
                    break;
 
                case "getParties" :
                    ChatModel chat = new ChatModel();
                    chat.type = "currentParties";
                    StringList stringList = new StringList();
                    foreach (PartyModel thisParty in SocketService._currentParties)
                       {    
                           stringList.ints.Add(thisParty.partyId);
                    }
                    string partyIdList = JsonConvert.SerializeObject(stringList);
                    chat.json1 = partyIdList;
                    SocketService.broadCast(chat, client);
                    break;
*/






/*       public static void leaveCurrentParty(ChatModel model, ChatClient client)
       {
           if (client.currentParty != null)
           {
               var user = client.currentParty.users.FirstOrDefault<UserModel>(u => u.userId == client.userId);
               client.currentParty.users.Remove(user);
               //dispose of user in all aspects of party
           }
       }

       public static bool isInParty(ChatModel model, ChatClient client)
       {
           if (client.currentParty != null)
           {
               var user = client.currentParty.users.FirstOrDefault<UserModel>(u => u.userId == client.userId);
               if (user != null)
               {
                       return true;
               }
               
           }
           return false;
       }*/
       
       
       
       
        /*  switch (model.type)
            {
                
                case "mapSelect" :
                   
                {
                    ChatResponse mapSelectResponse = new ChatResponse();
                    mapSelectResponse.JContainer = model;
                    SocketService.broadCast(mapSelectResponse, SocketService._connections); 
                }
                   
                    break;
                
                case "mapSuggest" :
                   
                {
                    ChatResponse mapSuggestResponse = new ChatResponse();
                    mapSuggestResponse.JContainer = model;
                    SocketService.broadCast(mapSuggestResponse, SocketService._connections); 
                }
                   
                    break;
                
               
                
                case "commit" :
                    //copies the model to the response model
                    if (!string.IsNullOrEmpty(client.currentParty.mapIDSelected))
                    {
                        ChatResponse commitResponse = new ChatResponse();
                        commitResponse.JContainer = model;
                        SocketService.broadCast(commitResponse, SocketService._connections);
                        if (allCommit(client))
                        {
                            requestUnits(); 
                        }

                    }
                    else
                    {
                       sendError("no map selected!", client);
                    }

                    break;
             
                case "formationReady" :
                   
                    {
                        ChatResponse readyResponse = new ChatResponse();
                        readyResponse.JContainer = model;
                        SocketService.broadCast(readyResponse, SocketService._connections); 
                    }
                   
                    break;
                    
                    
                case "unitsUpload" :
                     SocketService.staticParty.unitPrepJsons.Add(model.json);
                    //here I will start a 5 second timer, wait for all of the jsons to be uploaded, start battle
                    break;
                    
                case "battleEnded" :
                     //later

                    break;
                    
                    
            }*/