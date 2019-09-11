
using Scripts.MapSetup.Model;
using UnityEngine;

namespace MapSetup.Services  //shoulnd't be static. should be thorugh zenect
{
    public static class ChatService
    {
        public static void call(ChatModel chatModel)
        {
            Debug.Log(chatModel.chatModelRef + " " + chatModel.Message + " " + chatModel.UserID); 
        }
        
       
    }
}