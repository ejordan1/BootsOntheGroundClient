using System;
using System.ComponentModel;
using MapSetup.Model;
using MapSetup.Services;
using Scripts.MapSetup.Model;
using UnityEngine;
using WebSocketSharp;

namespace Scripts.MapSetup.Services
{
    /// <summary>
    /// Wrapps the socket with strong typing
    /// </summary>
    public class ChatClient
    {
        public DebugDemo debugDemo;
        
        public event Action<ChatModel> OnChat = delegate { };

        public event Action OnOpen = delegate { };

        public event Action OnClose = delegate { };

        
        //here are the delegate variables that get assigned stuff
		//the actual sending and receiving of the file is really easy
        
        private WebSocket _socket;
        
        //

        public void Open(string url)
        {
            Close();

            _socket = new WebSocket(url);

            _socket.OnOpen += _socket_OnOpen;
            _socket.OnClose += _socket_OnClose;
            _socket.OnMessage += _socket_OnMessage;
            _socket.OnError += _socket_OnError;

            _socket.Connect();
        }

        public void Close()
        {
            if (_socket != null)
            {
                _socket.OnOpen -= _socket_OnOpen;
                _socket.OnClose -= _socket_OnClose;
                _socket.OnMessage -= _socket_OnMessage;
                _socket.OnError -= _socket_OnError;

                _socket.Close();
                _socket = null;
                OnClose();
            }
        }

		/*public void SendChatModel(ChatModel model)
		{
			if (_socket != null)
				
				_socket.Send(JsonUtility.ToJson(model));
		}*/

		public void Send(object model, string type)
        {
			if (_socket != null)
			{
			   
				JContainer j = new JContainer();
			    j.json = JsonUtility.ToJson(model);
			    j.type = type;
				_socket.Send(JsonUtility.ToJson(j));
			  
			}
		
        }

        //

        private void _socket_OnError(object sender, ErrorEventArgs e)
        {
            MonoHelper.InvokeOnMainThread(() =>
            {
                Debug.LogException(e.Exception);
            });
        }
        
        private void _socket_OnMessage(object sender, MessageEventArgs e)
        {
            Debug.Log("hgv");
            MonoHelper.InvokeOnMainThread(() =>
            {
                ChatModel model = JsonUtility.FromJson<ChatModel>(e.Data);
               
                        ChatService.call(model);
                
                
            });
        }

        private void _socket_OnClose(object sender, CloseEventArgs e)
        {
            MonoHelper.InvokeOnMainThread(OnClose);
        }

        private void _socket_OnOpen(object sender, EventArgs e)
        {
            MonoHelper.InvokeOnMainThread(OnOpen);
        }
    }
}
