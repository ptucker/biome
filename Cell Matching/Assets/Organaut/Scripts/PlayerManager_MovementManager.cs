using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace BIOME
{
	public class PlayerManager_MovementManager : NetworkBehaviour
	{
		private PlayerManager _playerManager;
		private GameObject _playerCamera;
		private GameObject _sceneCamera;
		
		[Header("Settings")] 
		
		public float MovementSpeed = 1.0f;
		
		[Header("Internal")] 
		
		private float _movementHorizontal;
		private float _movementVertical;
		
		private float _movementSpeedDampener = 100.0f;
		
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
			_playerManager = GetComponent<PlayerManager>();
			if (_playerCamera == null) _playerCamera = _playerManager._playerCamera;
		}

		void Update()
		{
			if (!GameManager.Instance.GetComponent<GameManager_NetworkManager>().NoConnection)
				if (!isLocalPlayer) return;

			UpdateMovement();
		}

		private void UpdateMovement()
		{
			UpdateInput();
			Move();
		}

		private void UpdateInput()
		{
			_movementHorizontal = Input.GetAxis("Horizontal") * MovementSpeed/_movementSpeedDampener;
			_movementVertical = Input.GetAxis("Vertical") * MovementSpeed/_movementSpeedDampener;
		}

		private void Move()
		{
			// Chande this to velocity/acceleration for improved movement
			
			Vector3 cameraForwardDirection = GetComponent<PlayerManager_CameraManager>().GetCameraDirection();
			transform.Translate(cameraForwardDirection*_movementVertical);
			Vector3 cameraperpendicularDirection =
				GetComponent<PlayerManager_CameraManager>().GetCameraPerpendicularDirection();
			transform.Translate(cameraperpendicularDirection*_movementHorizontal);
		}
	}
}
