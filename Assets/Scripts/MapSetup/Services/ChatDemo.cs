using MapSetup.Model;
using MapSetup.Model.Responses;
using MapSetup.Services;
using MyMvcProject.Data;

using UnityEngine;

namespace Scripts.MapSetup.Services
{
    public class ChatDemo : MonoBehaviour
    {
        public string ServerPath = "ws://localhost:5000/";

        public ChatClient _client;

        public string UserName = "NICK";
        public string s1Field = "HELLO WORLD";
        public int i1Field = 4;
        public int i2Field = 5;
        public string mapSelected;
    

		void OnDestroy() //complicated delegate system (not complicated)
		//that is all this is. it assigns these methods to the client delegate stuff
        {
            if (_client != null)
            {
                _client.Close();
                _client.OnClose -= _client_OnClose;
                _client.OnOpen -= _client_OnOpen;
               /* _client.OnChat -= _client_OnChat;*/
                _client = null;
            }
        }

        private void _client_OnOpen()
        {
            Debug.Log("Chat is open");
        }

        private void _client_OnClose()
        {
            Debug.Log("Chat is closed");
        }

        [ContextMenu("Open")]
        public void Open()
        {
            OnDestroy();
            _client = new ChatClient();
            _client.OnClose += _client_OnClose;
            _client.OnOpen += _client_OnOpen;
           /* _client.OnChat += _client_OnChat;*/
            _client.Open(ServerPath);
          //  _client.chatDemo = this; //make sure this doesn't brake stuff
            CallService._client = _client;
        }

        [ContextMenu("Close")]
        public void Close()
        {
            OnDestroy();
        }

        /*[ContextMenu("ChatSend")]
        public void Send()
        {
            BasicModel b = new BasicModel();
            b.userId = UserName;
            b.s1 = "chat";
         
            _client.Send(b, "mapChosen"); //the type is not the class, it is the type of message      
        }*/
        
        [ContextMenu("userJoined")]
        public void usreJoined()
        {

            BasicModel b = new BasicModel();
            b.userId = UserName;
            b.s1 = "userJoined";
            Debug.Log(b.s1);
         
            _client.Send(b, "userJoined"); //the type is not the class, it is the type of message
               
        }
        
       
        [ContextMenu("userLeft")]
        public void userLeft()
        {

           BasicModel b = new BasicModel();
            b.userId = UserName;
            b.s1 = "userLeft";
            Debug.Log(b.s1);
         
            _client.Send(b, "userLeft"); //the type is not the class, it is the type of message
               
        }
        
        [ContextMenu("mapSuggest")]
        public void mapSuggest()
        {

            BasicModel b = new BasicModel();
            b.userId = UserName;
            b.s1 = "map1";
            Debug.Log(b.s1);
         
            _client.Send(b, "mapSuggest"); //the 
               
        }
         
        [ContextMenu("commit")]
        public void commit()
        {

            BasicModel b = new BasicModel();
            b.userId = UserName;
            b.s1 = "commit";
            Debug.Log(b.s1);
         
            _client.Send(b, "commit"); //the 
        }
        
        [ContextMenu("formationSet")]
        public void formationSet()
        {

           BasicModel b = new BasicModel();
            b.userId = UserName;
            b.s1 = "formationSet";
            Debug.Log(b.s1);
         
            _client.Send(b, "formationSet"); //the 
               
        }
        
        [ContextMenu("battleEnd")]
        public void battleEnded()
        {

            BasicModel b = new BasicModel();
            b.userId = UserName;
            b.s1 = "battleEnd";
            Debug.Log(b.s1);
         
            _client.Send(b, "battleEnd"); //the 
               
        }
        
        [ContextMenu("returnToLobby")]
        public void returnToLobby()
        {

            BasicModel b = new BasicModel();
            b.userId = UserName;
            b.s1 = "returnToLobby";
            Debug.Log(b.s1);
         
            _client.Send(b, "returnToLobby"); //the 
               
        }
        

        
       
    }
}




/* [ContextMenu("CreateParty")]
        public void CreateParty()
        {

            ChatModel c = new ChatModel()
            {
                type = "createParty",
                s1 = s1Field,
                UserId = UserName,
       
         

              
            };
            _client.Send(c);
               
        }
        
        [ContextMenu("joinParty")]
        public void joinParty()
        {

        
            ChatModel c = new ChatModel()
            {
                type = "joinParty",
                i1 = i1Field,
                UserId = UserName,
                s2 = "derp",
             

            };
            _client.Send(c);
               
        }
        
        [ContextMenu("getParties")]
        public void GetParties()
        {

        
            ChatModel c = new ChatModel()
            {
                type = "getParties",
                s1 = s1Field,
                UserId = UserName,
                s2 = "derp",
             

            };
            _client.Send(c);
               
        }*/