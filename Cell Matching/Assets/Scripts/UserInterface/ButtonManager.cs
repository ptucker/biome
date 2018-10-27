using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace BIOME
{
	public class ButtonManager : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
	{
		[Header("Resources")]
		public AudioClip HoverSound;
		public AudioClip ClickSound;
		
		private float _volume = 0.4f;
		
		public List<GameObject> EnableObjects;
		public List<GameObject> DiableObjects;
		public List<GameObject> ToggleObjects;

		private AudioSource HoverSource { get { return GetComponent<AudioSource>(); } }
		private AudioSource ClickSource { get { return GetComponent<AudioSource>(); } }

		void Start()
		{
			gameObject.AddComponent<AudioSource>();
			HoverSource.clip = HoverSound;
			ClickSource.clip = ClickSound;
			HoverSource.volume = _volume;
			ClickSource.volume = _volume;

			HoverSource.playOnAwake = false;
			ClickSource.playOnAwake = false;
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			PlaySounds(HoverSound);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			PlaySounds(ClickSound);
			AbleObjects();
		}

		private void PlaySounds(AudioClip sound)
		{
			if(ClickSource.clip != null)
			{
				ClickSource.PlayOneShot(sound);
			}
		}

		private void AbleObjects()
		{
			if (EnableObjects.Count > 0)
			{
				foreach (GameObject enableObject in EnableObjects)
				{
					enableObject.SetActive(true);
				}
			}
			if (DiableObjects.Count > 0)
			{
				foreach (GameObject diableObjects in DiableObjects)
				{
					diableObjects.SetActive(false);
				}
			}
			if (ToggleObjects.Count > 0)
			{
				foreach (GameObject toggleObjects in ToggleObjects)
				{
					if(!toggleObjects.activeSelf) toggleObjects.SetActive(true);
					else toggleObjects.SetActive(false);
				}
			}
		}

	}
}
