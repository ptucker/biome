using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OrganelleLabel  {
	private Vector2 _loc, _dest;
	private string _name;
	private int _fontSize = 50;
	private bool _dragging = false, _hit = false;

	private GameObject cube;
	private TextMesh mesh;

	public string Name {
		get { return _name; }
		set { 
			_name = value;
			mesh.text = Name;
			cube.name = Name;
		}
	}
	public Vector2 Loc {
		get { return _loc; }
		set { _loc = value; SetPosition();}
	}

	public Vector2 Dest {
		get { return _dest; }
		set { _dest = value; }
	}

	public Transform transform {
		get { return cube.transform; }
	}

	public int FontSize {
		get { return _fontSize; }
		set { _fontSize = value; mesh.fontSize = FontSize; }
	}

	public Vector3 Center {
		get { return mesh.GetComponent<Renderer>().bounds.center; }
	}

	public bool Dragging {
		get { return _dragging; }
		set 
		{ 
			_dragging = value;
			SetMeshColor();
		}
	}

	public bool Hit {
		get { return _hit; }
		set {
			_hit = value;
			SetMeshColor();
		}
	}

	public OrganelleLabel() {
		cube = new GameObject();
		mesh = cube.GetComponent<TextMesh>();
		if (mesh == null) {
			//Need to drop the MeshFilter since it conflicts with the TextMesh
			GameObject.DestroyImmediate(cube.GetComponent<MeshFilter>());
			cube.AddComponent<TextMesh>();
			mesh = cube.GetComponent<TextMesh>();
		}
		mesh.name = Name;
		var renderer = cube.GetComponent<MeshRenderer>();
		var arial = (Font)Resources.GetBuiltinResource (typeof(Font), "Arial.ttf");
		mesh.color = Color.black;
		mesh.transform.localScale = new Vector3(0.1F, 0.1F, 0.1F);
		mesh.fontSize = FontSize;
		renderer.material = arial.material;
		
		mesh.font = arial;
	}

	public bool Contains(Vector3 point) {
		var renderer = mesh.GetComponent<Renderer>();
		point.z = renderer.bounds.center.z;
		bool ret = renderer.bounds.Contains(point);
		Debug.Log(string.Format("{0}: {1} in {2}? {3}", Name, point.ToString(), renderer.bounds.ToString(), ret));
		return ret;
	}

	private void SetPosition() {
		//Debug.Log(string.Format("Moving {0} to <{1},{2}>", Name, _loc.x, _loc.y));
		cube.transform.position = new Vector3(_loc.x, _loc.y, -1.0F);
	}

	private void SetMeshColor() {
		mesh.color = _hit ? Color.green : (_dragging ? Color.red : Color.black);
	}
}
