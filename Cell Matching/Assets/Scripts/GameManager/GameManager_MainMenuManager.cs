using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BIOME
{
	public class GameManager_MainMenuManager : MonoBehaviour
	{
		private GameManager _gameManager;
		private GameObject _content;
		
		
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
			_content = _gameManager.MainMenu.transform.Find("content").gameObject;
		}

		void OpenMenu()
		{
			_content.SetActive(true);
		}

		void CloseMenu()
		{
			_content.SetActive(false);
		}
	}
}