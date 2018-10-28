
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BIOME
{
	public class GameManager_UtilityMenuManager : MonoBehaviour {
		private GameManager _gameManager;
		
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
		}

		public void SendNotification(string message)
		{
			_gameManager.UtilityMenu.GetComponentInChildren<NotificationBoard>().CreateNotification(message);
		}

		void ReceivedNotification(string message)
		{
			Debug.Log("Notification Received: "+message);
		}
	}
}