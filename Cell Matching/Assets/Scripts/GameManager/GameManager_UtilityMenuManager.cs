using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace BIOME
{
	public class GameManager_UtilityMenuManager : MonoBehaviour {
		private GameManager _gameManager;
		private NetworkManager _networkManager;

		private GameObject DebugOrganaut;
		
		public delegate void GameObjectEventHandler(GameObject debugOrganaut);
		
		public event GameObjectEventHandler SpawnedDebugOrganautEvent;
		public event GameObjectEventHandler RemoveDebugOrganautEvent;
		
		private void OnEnable()
		{
			Setup();
			_gameManager.UtilityMenu.GetComponentInChildren<NotificationBoard>().NotificationRecieved += ReceivedNotification;
		}

		private void OnDisable()
		{
			_gameManager.UtilityMenu.GetComponentInChildren<NotificationBoard>().NotificationRecieved -= ReceivedNotification;
		}

		void Setup()
		{
			_gameManager = GetComponent<GameManager>();
			_networkManager = GetComponent<NetworkManager>();
		}

		public void SendNotification(string message)
		{
			_gameManager.UtilityMenu.GetComponentInChildren<NotificationBoard>().CreateNotification(message);
		}

		void ReceivedNotification(string message)
		{
//			Debug.Log("Notification Received: "+message);
		}
		
		public void SpawnOrganaut()
		{

//			if (Application.isEditor)
//			{
//				if (SceneView.lastActiveSceneView != null)
//					DebugOrganaut = Instantiate(_networkManager.playerPrefab,
//						SceneView.lastActiveSceneView.camera.transform.position,
//						SceneView.lastActiveSceneView.camera.transform.rotation);
//				else
//				{
//					_gameManager.SendNotification("If the scene menu is open the Organaut will spawn at the scene camera position");
//					DebugOrganaut = Instantiate(_networkManager.playerPrefab, Vector3.zero, Quaternion.identity);
//				}
//			}
//			else
//			{
				DebugOrganaut = Instantiate(_networkManager.playerPrefab, Vector3.zero, Quaternion.identity);
//			}
			CallEventSpawnedDebugOrganaut(DebugOrganaut);
		}

		public void RemoveOrganaut()
		{
			if (DebugOrganaut != null)
			{
				CallEventRemoveDebugOrganaut(DebugOrganaut);
				Destroy(DebugOrganaut);
			}else Debug.LogAssertion("No Debug Organaut Current In Scene");
		}
		
		public void CallEventSpawnedDebugOrganaut(GameObject debugOrganaut)
		{
			if (SpawnedDebugOrganautEvent != null)
			{
				SpawnedDebugOrganautEvent(debugOrganaut);
			}
			else
			{
				Debug.LogAssertion("Nothing Subscribed To SpawnDebugOrganautEvent");
			}
		}
		public void CallEventRemoveDebugOrganaut(GameObject debugOrganaut)
		{
			if (RemoveDebugOrganautEvent != null)
			{
				RemoveDebugOrganautEvent(debugOrganaut);
			}
			else
			{
				Debug.LogAssertion("Nothing Subscribed To RemoveDebugOrganautEvent");
			}
		}
	}
}