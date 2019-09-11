using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using MapSetup.Model;
using MapSetup.Model.Responses;
using MapSetup.Services;
using MyMvcProject.Data;
using Requests;
using UnityEngine;
using UnityEngine.UI;

//I think the problem is that it is not calling the on destroy

namespace Scripts.MapSetup.Services
{
    public static class  SocketHandler 
    {
        public static string[] messageTypes = new string[]
        {
            "userJoined",
            "userLeft",
            "mapChoose",
            "mapSuggest",
            "commit",
            "formationSet",
            "battleEnd",
            "returnToLobby" 
        };
        
        public static string ServerPath = "ws://localhost:5000/";

        //public ChatClient _client;

        public static void OnDestroy() //complicated delegate system (not complicated)
		//that is all this is. it assigns these methods to the client delegate stuff
        {
            if (ClientStaticData._client != null)
            {
                ClientStaticData._client.Close();
                ClientStaticData._client.OnClose -= _client_OnClose;
                ClientStaticData._client.OnOpen -= _client_OnOpen;
               /* ClientStaticData._client.OnChat -= _client_OnChat;*/
                ClientStaticData._client = null;
            }
        }

        private static void _client_OnOpen()
        {
            Debug.Log("Chat is open");
        }

        private static void _client_OnClose()
        {
            Debug.Log("Chat is closed");
        }

        [ContextMenu("Open")]
        public static void Open()
        {
            OnDestroy();
            ClientStaticData._client = new ChatClient();
            ClientStaticData._client.OnClose += _client_OnClose;
            ClientStaticData._client.OnOpen += _client_OnOpen;
           //ClientStaticData._client.OnChat += _client_OnChat;
            ClientStaticData._client.Open(ServerPath);
         
            CallService._client = ClientStaticData._client;
        }

        [ContextMenu("Close")]
        public static void Close()
        {
            OnDestroy();
        }
        
 
       

    }
}
