using System.Collections.Generic;
using Scripts.MapSetup.Services;

namespace MapSetup.Model
{
    
    public static class ClientStaticData
    {
        public static string UserID { get; set; }
        public static string Token { get; set; }
        public static string DeviceID { get; set; }
        public static List<PartyModel> invites = new List<PartyModel>();
        public static PartyModel currentParty = new PartyModel();
        public static ChatClient _client;
        
    }
    
   
}