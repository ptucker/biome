using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadGameData {

	public string JsonFile {get;set;}
	ShowCellImage ShowCellImage {get;set;}
	ShowLabels ShowLabels {get;set;}

	public List<OrganelleLabel> OrganelleLabels {
		get { return ShowLabels.OrganelleLabels; }
	}

	// Use this for initialization
	public void Load () {
		string gamedata = null;
		if (File.Exists(JsonFile))
			gamedata = File.ReadAllText(JsonFile);
		else {
			var defaultFile = Resources.Load<TextAsset>("default");
			gamedata = defaultFile.text;
		}

		CellRecData data = JsonUtility.FromJson<CellRecData>(gamedata);

		ShowCellImage = new ShowCellImage() {
			CellImage = data.cellimage
		};
		ShowCellImage.Show();

		ShowLabels = new ShowLabels() {
			OrganelleData = data.organelles
		};
		ShowLabels.Show();
	}
	
	[System.Serializable]
	public class OrganelleData {
		public string name;
		public int destx;
		public int desty;
	}

	[System.Serializable]
	public class CellRecData {
		public string cellimage;
		public OrganelleData[] organelles;
	}

	public string defaultGameData = "{\"cellimage\":\"animal_cell_round1\", \"organelles\":[{\"name\":\"Ribosome\", \"destx\":-1.7,\"desty\":2.0}, {\"name\":\"Nucleus\", \"destx\":2.0,\"desty\":2.5}, {\"name\":\"Endoplasmic Reticulum\", \"destx\":2.7,\"desty\":0.5}, {\"name\":\"Golgi Body\", \"destx\":1.7,\"desty\":-1.1}, {\"name\":\"Cell Membrane\", \"destx\":0.6,\"desty\":-2.1}, {\"name\":\"Vacuole\", \"destx\":-0.7,\"desty\":-2.1}, {\"name\":\"Lysosome\", \"destx\":-1.8,\"desty\":-1.8}, {\"name\":\"Mitochondria\", \"destx\":-3.1,\"desty\":0.0}, {\"name\":\"Cytoplasm\", \"destx\":-2.1,\"desty\":1.8}]}";
	
	// "{\"cellimage\":\"C:/Users/ptucker/source/repos/biome/CommonImages/animal_cell_round1.png\", \"organelles\":[ {\"name\":\"Ribosome\", \"destx\":0,\"desty\":0}, {\"name\":\"Nucleus\", \"destx\":0,\"desty\":0}, {\"name\":\"Endoplasmic Reticulum\", \"destx\":0,\"desty\":0}, {\"name\":\"Golgi Body\", \"destx\":0,\"desty\":0}, {\"name\":\"Cell Membrane\", \"destx\":0,\"desty\":0}, {\"name\":\"Vacuole\", \"destx\":0,\"desty\":0}, {\"name\":\"Lysosome\", \"destx\":0,\"desty\":0}, {\"name\":\"Mitochondria\", \"destx\":0,\"desty\":0}, {\"name\":\"Cytoplasm\", \"destx\":0,\"desty\":0}]}\"";
}
