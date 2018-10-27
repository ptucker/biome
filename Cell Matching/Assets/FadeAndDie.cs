using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class FadeAndDie : MonoBehaviour
{
	public float ShowTime = 7;
	public float FadeTime = 3;
	private float FramesPerSecond = 30;
	
	public delegate void FadeAndDieEventHandler();

	public event FadeAndDieEventHandler Death;
	
	void Start ()
	{
		StartCoroutine(Fade(ShowTime, FadeTime, FramesPerSecond));
	}
	
	IEnumerator Fade (float showTimeSeconds,float fadeTimeSeconds, float stepsPerSecond)
	{
		yield return new WaitForSeconds(showTimeSeconds);
		var group = gameObject.GetComponent<CanvasGroup>();
		while (group.alpha>0)
		{
			group.alpha -= 1/(fadeTimeSeconds*stepsPerSecond);
			yield return new WaitForSeconds(1/stepsPerSecond);
		}
		Destroy(gameObject);
	}

	private void OnDestroy()
	{
		if (Death != null) Death();
	}
}
