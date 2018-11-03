using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace BIOME
{
	[RequireComponent(typeof(PlayerManager_CameraManager))]
	[RequireComponent(typeof(PlayerManager_MovementManager))]
	public class PlayerManager : NetworkBehaviour
	{
		public GameObject _playerCamera;
		private GameObject _sceneCamera;
		
		private void OnEnable()
		{
			Setup();
			if(_sceneCamera!=null)_sceneCamera.SetActive(false);
		}

		private void OnDisable()
		{
			if(_sceneCamera!=null)_sceneCamera.SetActive(true);
		}

		void Setup()
		{
			if(_sceneCamera==null) _sceneCamera = GameObject.Find("scene_camera");
			if(_playerCamera==null) _playerCamera = transform.Find("player_camera").gameObject;

		}

		// Update is called once per frame
		void Update()
		{

		}
	}
}