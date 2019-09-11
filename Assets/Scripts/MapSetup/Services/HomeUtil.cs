using System;
using System.Collections.Generic;
using System.Linq;
using MapSetup.Model;
using MapSetup.Model.ModelClasses;
using Scripts.MapSetup.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MapSetup.Services
{
    public static class HomeUtil
    {

        public static Home home;

        public static void deCommitAll()
        {
            foreach (MemberModel member in ClientStaticData.currentParty.partyMembers)
            {
                Debug.Log("ONE MEMBER");
                if (member.state == MemberModel.userState.committed
                    || member.state == MemberModel.userState.lobbying)
                {
                    member.state = MemberModel.userState.lobbying;
                }
                else
                {
                    Debug.Log("member was neither commited nor lobbying, shouldn't happen when map is selected");
                }
            }
        }

        public static List<MapModel> mapIdsToMaps(List<String> mapIds)
        {
            List<MapModel> maplist = new List<MapModel>();
            for (int i = 0; i < mapIds.Count; i++)
            {
                var map = StaticAllData.allMaps.FirstOrDefault(m => m._mapName == mapIds[i]);
                maplist.Add(map);
            }
            return maplist;
        }

        public static MapModel mapIdsToMaps(string mapId)
        {
            var map = StaticAllData.allMaps.FirstOrDefault(m => m._mapName == mapId);
            return map;
        }


        public static void homePlayMap(MapModel map)
        {
            home.PlayMap(map);   
        }
        
        public static void homeRequestTroops()
        {
            home.unitsGetRequest();
        }
        
        public static void startHomeGame()
        {
            home.startGame();
        }

        public static void postMap()
        {
            home.postStaticAllMap();
        }

    }
}