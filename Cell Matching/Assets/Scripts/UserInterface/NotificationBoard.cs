using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BIOME
{
    public class NotificationBoard : MonoBehaviour
    {
        [SerializeField] private GameObject _contentBoard;
        [SerializeField] private GameObject _notification;

        public float MessageShowTime = 10f;
        public float MessageFadeTime = 3f;

        private int _notificationCount;
        private Image _image;

        public delegate void NotificationBoardEventHandler(string message);
        public event NotificationBoardEventHandler NotificationRecieved;

        private void Start()
        {
//            StartCoroutine(Test());
            _image = GetComponent<Image>();
        }


        public void CreateNotification(string message)
        {
            GameObject notificationObject = Instantiate(_notification, _contentBoard.transform);
            notificationObject.transform.SetParent(_contentBoard.transform);
            notificationObject.transform.SetAsFirstSibling();
            notificationObject.GetComponentInChildren<Text>().text = message;
            notificationObject.GetComponent<FadeAndDie>().ShowTime = MessageShowTime;
            notificationObject.GetComponent<FadeAndDie>().FadeTime = MessageFadeTime;
            notificationObject.GetComponent<FadeAndDie>().Death += NotificationDeath;
            notificationObject.SetActive(true);
            if (NotificationRecieved != null) NotificationRecieved(message);
            _notificationCount++;
            if (_notificationCount == 1) StartCoroutine(Fade(0, 0.6f));
        }

        private void NotificationDeath()
        {
            _notificationCount--;
            if (_notificationCount < 1)
            {
                if(isActiveAndEnabled)StartCoroutine(Fade(0.6f, 0));
            }
        }

        private IEnumerator Fade(float fromValue, float toValue, float fadeTimeSeconds = 2, float stepsPerSecond = 30)
        {
            // Little messy. Could be turned into a target with rounding to check when arrived.
            if (fromValue > toValue)
                while (_image.color.a > toValue) // Fade Out
                {
                    _image.color = new Color(0, 0, 0, _image.color.a - 1 / (fadeTimeSeconds * stepsPerSecond));
                    yield return new WaitForSeconds(1 / stepsPerSecond);
                }
            else if (fromValue < toValue)
                while (_image.color.a < toValue) // Fade In
                {
                    _image.color = new Color(0, 0, 0, _image.color.a + 1 / (fadeTimeSeconds * stepsPerSecond));
                    yield return new WaitForSeconds(1 / stepsPerSecond);
                }
        }

        IEnumerator Test()
        {
            yield return new WaitForSeconds(1);
            CreateNotification("Hey I'm A Notification!");
            yield return new WaitForSeconds(1);
            CreateNotification("Me Too!");
            yield return new WaitForSeconds(0.5f);
            CreateNotification("Dont Let Me Fade Away! D:");
        }
    }
}
