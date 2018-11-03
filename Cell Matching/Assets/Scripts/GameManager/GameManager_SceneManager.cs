using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BIOME
{
	public class GameManager_SceneManager : MonoBehaviour {
		private GameManager _gameManager;
		
		
		private void OnEnable()
		{
			Setup();
			SceneManager.sceneLoaded += SceneLoaded;
		}

		private void OnDisable()
		{
			SceneManager.sceneLoaded -= SceneLoaded;
		}

		void Setup()
		{
			_gameManager = GetComponent<GameManager>();
		}

		void SceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
		{
//			Debug.Log(scene.name+" Loaded");
			if (scene.name == "Main_Menu")
			{
				_gameManager.CallEventMenuOpen();
			}
			else _gameManager.CallEventMenuClose();


			if (scene.name == "Transcribe_Translate")
			{
				_gameManager.GetComponent<GameManager_NetworkManager>().JoinLocalAreaNetworkGame();
			}
		}

		public void LoadScene(string sceneName)
		{
			if (sceneName == "Main_Menu") _gameManager.GetComponent<GameManager_NetworkManager>().LeaveLocalAreaNetworkGame();
			SceneManager.LoadSceneAsync(sceneName);
		}

		public void ReloadScene()
		{
			SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
		}
		
		
	}
}