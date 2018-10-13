using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCellMatchGame : MonoBehaviour {

	string jsonFile = @"C:\Users\ptucker\source\repos\biome\GameData\cellrec.json";

	List<OrganelleLabel> labels;

	// Use this for initialization
	void Start () {
		LoadGameData gamedata = new LoadGameData() {
			JsonFile = jsonFile
		};
		gamedata.Load();

		labels = gamedata.OrganelleLabels;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0)) {
			Debug.Log(Input.mousePosition.ToString());
		}
	}
}
