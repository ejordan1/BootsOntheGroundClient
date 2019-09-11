using System;
using Scripts.MapSetup.Model;
using UnityEngine;

namespace Scripts.MapSetup.Model
{
    // TODO Move this to a DLL exported by a shared data project 
    // This is not avaliable yet in dotnetcore
    
    /// <summary>
    /// Our DTO
    /// </summary>
    [Serializable]
    public class ChatModel
    {

        [SerializeField] 
        public string UserID;
    
        [SerializeField]
        public string Message;
	    
        [SerializeField]
        public string chatModelRef;
    }

	
}
