using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationBoard : MonoBehaviour
{
    [SerializeField] private GameObject _contentBoard;
    [SerializeField] private GameObject _notification;
    
    public float MessageShowTime = 10f;
    public float MessageFadeTime = 3f;

    private int _notificationCount;
    private Image _image;
    
    
    private void Start()
    {
        StartCoroutine(Test());
        _image = GetComponent<Image>();
    }


    public void CreateEvent(string message)
    {
        GameObject eventObject = Instantiate(_notification, _contentBoard.transform);
        eventObject.transform.SetParent(_contentBoard.transform);
        eventObject.transform.SetAsLastSibling();
        eventObject.GetComponentsInChildren<Text>()[0].text = message;
        eventObject.GetComponent<FadeAndDie>().ShowTime = MessageShowTime;
        eventObject.GetComponent<FadeAndDie>().FadeTime = MessageFadeTime;
        eventObject.GetComponent<FadeAndDie>().Death += NotificationDeath;
        eventObject.SetActive(true);
        _notificationCount++;
        if (_notificationCount == 1) StartCoroutine(Fade(0, 0.6f));
    }

    private void NotificationDeath()
    {
        _notificationCount--;
        if (_notificationCount < 1)
        {
            StartCoroutine(Fade(0.6f, 0));
        }
    }
	
    IEnumerator Fade (float fromValue, float toValue, float fadeTimeSeconds=2, float stepsPerSecond=30)
    {
        // Little messy. Could be turned into a target with rounding to check when arrived.
        if(fromValue>toValue)
        while (_image.color.a>toValue)
        {
            _image.color = new Color(0, 0, 0, _image.color.a - 1/(fadeTimeSeconds*stepsPerSecond));
            yield return new WaitForSeconds(1/stepsPerSecond);
        }
        else if(fromValue<toValue)
        while (_image.color.a<toValue)
        {
            _image.color = new Color(0, 0, 0, _image.color.a + 1/(fadeTimeSeconds*stepsPerSecond));
            yield return new WaitForSeconds(1/stepsPerSecond);
        }
    }
    
    IEnumerator Test()
    {
        yield return new WaitForSeconds(1);
        CreateEvent("Hey I'm A Notification!");
        yield return new WaitForSeconds(1);
        CreateEvent("Me Too!");
        yield return new WaitForSeconds(0.5f);
        CreateEvent("Dont Let Me Fade Away! D:");
    }
}
