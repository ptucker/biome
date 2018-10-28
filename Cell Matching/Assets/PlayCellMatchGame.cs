using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCellMatchGame : MonoBehaviour {

	string jsonFile = @"..\GameData\celldrec.json";

	List<OrganelleLabel> labels;
	OrganelleLabel dragTarget;
	bool mouseState = false;
	Vector3 screenSpace, offset, origPosition;

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
		//Debug.Log(mouseState);
        if (Input.GetMouseButtonDown (0)) {
			var mousePoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
			Debug.Log(string.Format("<{0},{1}> -> {2}", Input.mousePosition.x, Input.mousePosition.y, Camera.main.ScreenToWorldPoint(mousePoint).ToString()));
            dragTarget = GetClickedObject(Camera.main.ScreenToWorldPoint(mousePoint));
            if (dragTarget != null) {
				origPosition = dragTarget.transform.position;
				Debug.Log(string.Format("Hit {0}", dragTarget.Name));
                mouseState = true;
				dragTarget.Dragging = true;
                screenSpace = Camera.main.WorldToScreenPoint (dragTarget.transform.position);
                offset = dragTarget.transform.position - Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, dragTarget.Center.z));
            }
        }
        if (Input.GetMouseButtonUp (0)) {
            mouseState = false;
			dragTarget.Dragging = false;

			if (dragTarget.Contains(new Vector3(dragTarget.Dest.x, dragTarget.Dest.y, dragTarget.Center.z)))
				Debug.Log("Item placed correctly!");
			else {
				Debug.Log("Misplaced item");
				dragTarget.transform.position = origPosition;
			}
        }
        if (mouseState) {
            //keep track of the mouse position
            var curScreenSpace = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, dragTarget.Center.z);
 
            //convert the screen mouse position to world point and adjust with offset
            var curPosition = Camera.main.ScreenToWorldPoint (curScreenSpace) + offset;
 
            //update the position of the object in the world
            dragTarget.transform.position = curPosition;

			dragTarget.Hit = dragTarget.Contains(new Vector3(dragTarget.Dest.x, dragTarget.Dest.y, dragTarget.Center.z));

        }
	}

	OrganelleLabel GetClickedObject(Vector3 mousePoint)
    {
        OrganelleLabel target = null;

		foreach (var o in labels) {
			if (o.Contains(mousePoint)) {
				target = o;
				break;
			}
		}
 
        return target;
    }
}
