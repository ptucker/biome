using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ShowCellImage  {

	Sprite sprite;
	Texture2D tex = null;

	public string CellImage { get; set; }

	// Use this for initialization
	public void Show() {
		LoadImage();

		AddImageToGameObject();
	}
	
	void LoadImage() {
		byte[] fileData;
	
		if (File.Exists(CellImage)) {
			Debug.Log(string.Format("showing {0}", CellImage));
			fileData = File.ReadAllBytes(CellImage);
			tex = new Texture2D(2, 2);
			tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
		}
		else {
			Debug.Log(string.Format("can't find {0}", CellImage));
			tex = Resources.Load<Texture2D>(CellImage);
		}
	}

	void AddImageToGameObject() {
		if (tex != null) {
			Sprite newSprite = Sprite.Create(tex, new Rect(0f, 0f, tex.width, tex.height), new Vector2(0.5f, 0.5f),128f);
			GameObject sprGameObj = new GameObject();
			var names = CellImage.Split('/','\\','.');
			sprGameObj.name = names.Length > 2 ? names[names.Length-2] : CellImage;
			sprGameObj.AddComponent<SpriteRenderer>();
			SpriteRenderer sprRenderer = sprGameObj.GetComponent<SpriteRenderer>();
			float scale = 1250F / tex.height;
			sprGameObj.transform.localScale = new Vector3(scale, scale, scale);
			sprRenderer.sprite = newSprite;
		}
		else
			Debug.Log("No cell texture to show.");
	}
}
