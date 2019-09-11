using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;
using MyMvcProject.Map;
using MyMvcProject.Data;
using Scripts.MapSetup.Services;
using MapSetup.Model;
using MapSetup.Model.ModelClasses;
using MapSetup.Model.Responses;
using MapSetup.Services;
using Model.Units;
using UnityEditor;
// userState key {lobbying = 0, committed = 1, forming = 2, ready = 3, battling = 4, waiting = 5};
namespace Requests
{
	
	public class PartyDataRequest : MonoBehaviour
	{
		public string ServerPath = "http://localhost:5000/api/PartyData";

		
		public string userID = ClientStaticData.UserID;
	
		public void GetPartyData()
		{
			StartCoroutine(GetPartyDataAsync());
		}

		IEnumerator GetPartyDataAsync()
		{
			Debug.Log("PartyDataAsyncAsync...");
			yield return 1;

			var task = RestClient.Get(ServerPath); //rest client get

			yield return task.SendWebRequest(); 

			if (task.isNetworkError)
			{
				Debug.LogError(task.error);
			}
			else
			{
				
				var joinRe = task.Deserialize<PartyDataResponse>();
				if (joinRe != null && joinRe.serverParty != null)
				{
					parseJoinRe(joinRe.serverParty);

				}
				else
				{
					Debug.Log("join re party was null ");
				}
			}

			task.Dispose();
		}

		public void parseJoinRe(PartyServerModel _serverParty)
		{

			foreach (MemberModelInt membInt in _serverParty.members)
			{
				MemberModel newMemb = getMember(membInt);
				ClientStaticData.currentParty.partyMembers.Add(newMemb);
			}
			
			if (!string.IsNullOrEmpty(_serverParty.mapIDSelected))
			{
				ClientStaticData.currentParty.mapSelected = HomeUtil.mapIdsToMaps(_serverParty.mapIDSelected);
			}

			ClientStaticData.currentParty.partyState = getPartyState(_serverParty.state);

			ClientStaticData.currentParty.mapHistory = HomeUtil.mapIdsToMaps(_serverParty.mapHistory);
			ClientStaticData.currentParty.suggestedMaps = HomeUtil.mapIdsToMaps(_serverParty.suggestedMaps);
			ClientStaticData.currentParty.inviteRequests = _serverParty.inviteRequests;
			
		}


		//had to pass the party state as an int, not enum
		public PartyServerModel.PartyState getPartyState(int serverState)
		{
			switch (serverState)
			{
				case 0: return PartyServerModel.PartyState.lobby;
				case 1: return PartyServerModel.PartyState.formation;
				case 2: return PartyServerModel.PartyState.battle;
			}
			return PartyServerModel.PartyState.lobby;
		}
		
		public MemberModel getMember(MemberModelInt memb)
		{
			MemberModel newMemb = new MemberModel(memb.userId);
			
			switch (memb.state)
			{
				case 0: newMemb.state =  MemberModel.userState.lobbying;
					break;
				case 1: newMemb.state = MemberModel.userState.committed;
					break;
				case 2: newMemb.state = MemberModel.userState.forming;
					break;
				case 3: newMemb.state = MemberModel.userState.ready;
					break;
				case 4:
					newMemb.state = MemberModel.userState.battling;
					break;
				case 5: newMemb.state = MemberModel.userState.waiting;
					break;
			}
			
			return newMemb;
		}

	}
}
