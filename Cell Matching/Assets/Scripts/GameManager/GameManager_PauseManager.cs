using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BIOME
{
	public class GameManager_PauseManager : MonoBehaviour
	{
		private GameManager _gameManager;
		
		
		private void OnEnable()
		{
			Setup();
			_gameManager.PauseGameEvent += PauseGame;
			_gameManager.ResumeGameEvent += ResumeGame;
		}

		private void OnDisable()
		{
			_gameManager.PauseGameEvent -= PauseGame;
			_gameManager.ResumeGameEvent -= ResumeGame;
		}

		void Setup()
		{
			_gameManager = GetComponent<GameManager>();
		}

		void PauseGame()
		{
			Time.timeScale = 0f;
		}

		void ResumeGame()
		{
			Time.timeScale = 1f;
		}
	}
}