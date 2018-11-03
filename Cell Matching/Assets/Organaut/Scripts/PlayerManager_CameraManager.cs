using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityScript.Steps;

namespace BIOME
{
	public class PlayerManager_CameraManager : MonoBehaviour
	{
		[Header("Settings")] 
		
		public float LookSpeed = 1.0f;
		public float targetFPS = 60.0f;
		public bool InvertedVertical = true;
		[SerializeField]private float _rotationUpdateMinimumDistanceDegreeChange = 0.5f;
		[SerializeField]private AnimationCurve _rotationLerpCurve;

		[Header("Internal")] 
		
		private PlayerManager _playerManager;

		private Transform _camera;
		private Transform _cameraTarget;
		
		private float _rotationOldHorizontal;
		private float _rotationOldVertical;
		private float _rotationCurrentHorizontal;
		private float _rotationCurrentVertical;
		private float _rotationTargetHorizontal;
		private float _rotationTargetVertical;
		private float _rotationNewHorizontal;
		private float _rotationNewVertical;
		
		private float _rotationMaxHorizontal = float.MaxValue;
		private float _rotationMinHorizontal = float.MinValue;
		private float _rotationMaxVertical = 90;
		private float _rotationMinVertical = -90;

		private float _rotationLerpStep;
		
		private void OnEnable()
		{
			Setup();
		}

		private void Setup()
		{
			_playerManager = GetComponent<PlayerManager>();
			
			if (_camera == null) _camera = _playerManager._playerCamera.transform;
			
			if (_cameraTarget == null) _cameraTarget = Instantiate(new GameObject("camera_target"), transform).transform;
			_camera.parent = _cameraTarget;
		}

//		LateUpdate ensures that physical updates do not lag behind camera updates.
		void LateUpdate()
		{
			if (Time.timeScale <= 0) return;

			UpdateMouseMovement();

			UpdateTarget();

			if (!DestinationReached())
			{
				RotateTwoardsTarget();
				SetCameraTargetTransform();
			}

			if (_rotationLerpStep > 1 && _rotationLerpStep < 2)
			{
				_rotationLerpStep = 3;
			}
		}

		private void SetCameraTargetTransform()
		{
			_cameraTarget.rotation = Quaternion.Euler(_rotationCurrentVertical, _rotationCurrentHorizontal, 0);
		}

		private void RotateTwoardsTarget()
		{
			_rotationLerpStep += 1/targetFPS; //(1.0f/Time.deltaTime) for a real time estimate of FPS
			float curveStep = _rotationLerpCurve.Evaluate(_rotationLerpStep);
			
			_rotationCurrentHorizontal = Mathf.Lerp(_rotationOldHorizontal, _rotationTargetHorizontal, curveStep);
			_rotationCurrentVertical = Mathf.Lerp(_rotationOldVertical, _rotationTargetVertical, curveStep);
		}

		private void UpdateMouseMovement()
		{
			_rotationNewHorizontal += Input.GetAxis("Mouse X") * LookSpeed;
			_rotationNewHorizontal = Mathf.Clamp(_rotationNewHorizontal, _rotationMinHorizontal, _rotationMaxVertical);
			
			if (InvertedVertical) _rotationNewVertical += -Input.GetAxis("Mouse Y") * LookSpeed;
			else _rotationNewVertical += -Input.GetAxis("Mouse Y") * LookSpeed;
			_rotationNewVertical = Mathf.Clamp(_rotationNewVertical, _rotationMinVertical, _rotationMaxVertical);
		}

		private void UpdateTarget()
		{
			if (TargetHasChanged(_rotationTargetHorizontal, _rotationNewHorizontal)||TargetHasChanged(_rotationTargetVertical, _rotationNewVertical))
			{
				_rotationTargetHorizontal = _rotationNewHorizontal;
				_rotationTargetVertical = _rotationNewVertical;
				
				_rotationOldHorizontal = _rotationCurrentHorizontal;
				_rotationOldVertical = _rotationCurrentVertical;
				
				_rotationLerpStep = 0;
			}
		}

		private bool TargetHasChanged(float target, float newTarget)
		{
			return Mathf.RoundToInt(target/_rotationUpdateMinimumDistanceDegreeChange) != Mathf.RoundToInt(newTarget/_rotationUpdateMinimumDistanceDegreeChange);
		}

		private bool DestinationReached()
		{
			return _rotationLerpStep >= 1;
		}

		public Vector3 GetCameraDirection()
		{
			return _cameraTarget.forward;
		}

		public Vector3 GetCameraPerpendicularDirection()
		{
			return _cameraTarget.right;
		}
	}
}
