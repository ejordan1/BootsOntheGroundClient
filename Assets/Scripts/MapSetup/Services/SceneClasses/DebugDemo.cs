using System.Collections.Generic;
using System.Linq;
using MapSetup.Model;
using MapSetup.Model.Responses;
using MapSetup.Services;
using MyMvcProject.Data;

using UnityEngine;
using UnityEngine.UI;

namespace Scripts.MapSetup.Services
{
    public class DebugDemo : MonoBehaviour
    {
        public string[] messageTypes = new string[]
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
        
        public string ServerPath = "ws://localhost:5000/";

        //public ChatClient _client;

        public Text _type;
        public Text _userId;
        public Text _s1;
        public Text _s2;
        public Text _i1;

        public Text _typeR;
        public Text _userIdR;
        public Text _s1R;
        public Text _s2R;
        public Text _i1R;


        void Start()
        {
            _type = GameObject.Find("Type").GetComponent<Text>();
            _userId = GameObject.Find("UserId").GetComponent<Text>();
            _s1 = GameObject.Find("S1").GetComponent<Text>();
            _s2 = GameObject.Find("S2").GetComponent<Text>();
            _i1 = GameObject.Find("I1").GetComponent<Text>();
            
            _typeR = GameObject.Find("TypeR").GetComponent<Text>();
            _userIdR = GameObject.Find("UserIdR").GetComponent<Text>();
            _s1R = GameObject.Find("S1R").GetComponent<Text>();
            _s2R = GameObject.Find("S2R").GetComponent<Text>();
            _i1R = GameObject.Find("I1R").GetComponent<Text>();

           
        }

        void OnDestroy() //complicated delegate system (not complicated)
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
            ClientStaticData._client = new ChatClient();
            ClientStaticData._client.OnClose += _client_OnClose;
            ClientStaticData._client.OnOpen += _client_OnOpen;
           /* ClientStaticData._client.OnChat += _client_OnChat;*/
            ClientStaticData._client.Open(ServerPath);
            ClientStaticData._client.debugDemo = this; 
            CallService._client = ClientStaticData._client;
        }

        [ContextMenu("Close")]
        public void Close()
        {
            OnDestroy();
        }
        
       
        public void Send()
        {
            if (messageTypes.Contains(_type.text))
            {
                ClientStaticData._client.Send(basicMake(), _type.text); //the 
            }
            else
            {
                Debug.Log("not a valid message type!");
            }

        }


        public BasicModel basicMake()
        {
            BasicModel b = new BasicModel();
            b.userId = _userId.text;
            b.s1 = _s1.text;
            b.s2 = _s2.text;
            b.i1 = int.Parse(_i1.text);

            return b;
        }
        
        public void receive(JContainer jC, BasicModel model)
        {
            _typeR.text = jC.type;
            _userIdR.text = model.userId;
            _s1R.text = model.s1;
            _s2R.text = model.s2;
            _i1R.text = "" + model.i1; //to string


        }


    }
}
