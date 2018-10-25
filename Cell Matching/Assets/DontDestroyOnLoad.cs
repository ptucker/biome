using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BIOME
{
	public class DontDestroyOnLoad : MonoBehaviour
	{
		private static GameObject _instance ;
 
		private void Awake()
		{
			Setup();
		}

		private void Setup()
		{
			if(!_instance)
				_instance = gameObject;
			else
				Destroy(gameObject);
 
 
			DontDestroyOnLoad(gameObject);
		}
	}
}