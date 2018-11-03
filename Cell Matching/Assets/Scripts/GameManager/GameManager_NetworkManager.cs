using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.SceneManagement;


namespace BIOME
{
    public class GameManager_NetworkManager : MonoBehaviour
    {
        private GameManager _gameManager;
        private NetworkManager _networkManager;
        
        public delegate void NetworkManagerEventHandler();
        
        public event NetworkManagerEventHandler JoinedLocalAreaNetworkGameEvent;
        public event NetworkManagerEventHandler LeaveLocalAreaNetworkGameEvent;
        public event NetworkManagerEventHandler StartedServerLocalAreaNetworkGameEvent;
        public event NetworkManagerEventHandler StopServerLocalAreaNetworkGameEvent;
        
        public bool NoConnection
        {
            get
            {
                return _networkManager.client == null || _networkManager.client.connection == null || _networkManager.client.connection.connectionId == -1;
            }
        }
        
        private void OnEnable()
        {
            Setup();
        }

        private void OnDisable()
        {
        }

        void Setup()
        {
            _gameManager = GetComponent<GameManager>();
            _networkManager = GetComponent<NetworkManager>();
        }
        
        public void StartServerLocalAreaNetworkGame()
        {
            if (NoConnection && !NetworkClient.active && !NetworkServer.active)
            {
                _gameManager.SendNotification("Starting Server");
                
                _networkManager.StartHost();
                _networkManager.ServerChangeScene("Transcribe_Translate");
            }
            CallEventStartedServerLocalAreaNetworkGame();
        }
        
        public void JoinLocalAreaNetworkGame()
        {
            if (NoConnection && !NetworkClient.active && !NetworkServer.active && !_networkManager.IsClientConnected())
            {
                _gameManager.SendNotification("Searching For Game");
                _networkManager.StartClient().RegisterHandler (MsgType.Disconnect, FailedToFindGame);
            }

            CallEventJoinedLocalAreaNetworkGame();
        }
        
        void FailedToFindGame(NetworkMessage message)
        {
            _gameManager.SendNotification("Failed To Find Game");
//            if (NetworkServer.active || _networkManager.IsClientConnected())  
            _networkManager.StopHost();
            
            if (!NetworkClient.active && !NetworkServer.active)
            {
                StartServerLocalAreaNetworkGame();
            }
        }
        
        public void LeaveLocalAreaNetworkGame()
        {
            if (NetworkServer.active || NetworkClient.active)
            {
                if (NetworkServer.active || _networkManager.IsClientConnected())  _networkManager.StopHost();
                
                _gameManager.GetComponent<GameManager_SceneManager>().LoadScene("Main_Menu");
                _gameManager.SendNotification("Disconnected From Game");
            }

            CallEventLeaveLocalAreaNetworkGame();
        }


        public void CallEventJoinedLocalAreaNetworkGame()
        {
            if (JoinedLocalAreaNetworkGameEvent != null)
            {
                JoinedLocalAreaNetworkGameEvent();
            }
            else
            {
                Debug.LogAssertion("Nothing Subscribed To JoinedLocalAreaNetworkGame");
            }
        }
        public void CallEventLeaveLocalAreaNetworkGame()
        {
            if (LeaveLocalAreaNetworkGameEvent != null)
            {
                LeaveLocalAreaNetworkGameEvent();
            }
            else
            {
                Debug.LogAssertion("Nothing Subscribed To LeaveLocalAreaNetworkGame");
            }
        }
        public void CallEventStartedServerLocalAreaNetworkGame()
        {
            if (StartedServerLocalAreaNetworkGameEvent != null)
            {
                StartedServerLocalAreaNetworkGameEvent();
            }
            else
            {
                Debug.LogAssertion("Nothing Subscribed To StartedServerLocalAreaNetworkGame");
            }
        }
        public void CallEventStopServerLocalAreaNetworkGame()
        {
            if (StopServerLocalAreaNetworkGameEvent != null)
            {
                StopServerLocalAreaNetworkGameEvent();
            }
            else
            {
                Debug.LogAssertion("Nothing Subscribed To StopServerLocalAreaNetworkGame");
            }
        }
    }
}