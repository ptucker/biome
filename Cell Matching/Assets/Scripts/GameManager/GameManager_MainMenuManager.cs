using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BIOME
{
	public class GameManager_MainMenuManager : MonoBehaviour
	{
		private GameManager _gameManager;
		
		
		private void OnEnable()
		{
			Setup();
			_gameManager.MenuOpenEvent += OpenMenu;
			_gameManager.MenuCloseEvent += CloseMenu;
		}

		private void OnDisable()
		{
			_gameManager.MenuOpenEvent -= OpenMenu;
			_gameManager.MenuCloseEvent -= CloseMenu;
		}

		void Setup()
		{
			_gameManager = GetComponent<GameManager>();
		}

		void OpenMenu()
		{
			_gameManager.MainMenu.SetActive(true);
		}

		void CloseMenu()
		{
			_gameManager.MainMenu.SetActive(false);
		}
	}
}