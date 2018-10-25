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
			Debug.Log(scene.name+" Loaded");
		}

		public void LoadScene(string sceneName)
		{
			SceneManager.LoadSceneAsync(sceneName);
		}

		public void ReloadScene()
		{
			SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
		}
		
		
	}
}