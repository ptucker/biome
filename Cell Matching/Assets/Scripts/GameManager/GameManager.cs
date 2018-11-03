using UnityEngine;
using UnityEngine.SceneManagement;

namespace BIOME
{
	[RequireComponent(typeof(GameManager_NetworkManager))]
	[RequireComponent(typeof(GameManager_SceneManager))]
	[RequireComponent(typeof(GameManager_UtilityMenuManager))]
//	[RequireComponent(typeof(GameManager_LevelManager))] // Not added yet
	[RequireComponent(typeof(GameManager_MainMenuManager))]
	[RequireComponent(typeof(GameManager_PauseManager))]
	public class GameManager : MonoBehaviour
	{
		public static GameManager Instance;
		
		public GameObject MainMenu;
		public GameObject UtilityMenu;
		
		public bool developerMode;
		public GameObject DeveloperMenu;
		
		private bool _isGameOver;
		private bool _isMenuActive;
		
		public delegate void GameManagerEventHandler();

		public event GameManagerEventHandler RestartGameEvent;
		public event GameManagerEventHandler EndGameEvent;
	
		public event GameManagerEventHandler MenuOpenEvent;
		public event GameManagerEventHandler MenuCloseEvent;
		public event GameManagerEventHandler PauseGameEvent;
		public event GameManagerEventHandler ResumeGameEvent;

		
		public bool IsGameOver() { return _isGameOver; }
		public bool IsMenuActive() { return _isMenuActive; }

		private void Awake()
		{
			Setup();
		}

		private void Setup()
		{
			if(!Instance) Instance = this;
			else Destroy(gameObject);
			
			DontDestroyOnLoad(gameObject);

			if (MainMenu==null) Debug.LogAssertion("Set Main Menu Reference In Game Manager");
			else DontDestroyOnLoad(Instance);
			if (DeveloperMenu==null) Debug.LogAssertion("Set Developer Menu Reference In Game Manager");
			else if(!developerMode) DeveloperMenu.SetActive(false);
		}

		public void CallEventRestartGame()
		{
			if (RestartGameEvent != null)
			{
				_isGameOver = false;
				RestartGameEvent();
			}
			else
			{
				Debug.LogAssertion("Nothing Subscribed To RestartGame");
			}
		}
		public void CallEventEndGame()
		{
			if (EndGameEvent != null)
			{
				_isGameOver = true;
				EndGameEvent();
			}
			else
			{
				Debug.LogAssertion("Nothing Subscribed To EndGame");
			}
		}
		public void CallEventMenuOpen()
		{
			if (MenuOpenEvent != null)
			{
				_isMenuActive = true;
				MenuOpenEvent();
			}
			else
			{
				Debug.LogAssertion("Nothing Subscribed To MenuOpen");
			}
		}
		public void CallEventMenuClose()
		{
			if (MenuCloseEvent != null)
			{
				_isMenuActive = false;
				MenuCloseEvent();
			}
			else
			{
				Debug.LogAssertion("Nothing Subscribed To MenuClose");
			}
		}
		public void CallEventPauseGame()
		{
			if (PauseGameEvent != null)
			{
				PauseGameEvent();
			}
			else
			{
				Debug.LogAssertion("Nothing Subscribed To PauseGame");
			}
		}
		public void CallEventResumeGame()
		{
			if (ResumeGameEvent != null)
			{
				ResumeGameEvent();
			}
			else
			{
				Debug.LogAssertion("Nothing Subscribed To ResumeGame");
			}
		}

		public void Quit()
		{
			if(SceneManager.GetActiveScene().name=="Main_Menu") Application.Quit();
			else GetComponent<GameManager_SceneManager>().LoadScene("Main_Menu");
		}

		public void SendNotification(string message)
		{
			GetComponent<GameManager_UtilityMenuManager>().SendNotification(message);
		}
	}
}
