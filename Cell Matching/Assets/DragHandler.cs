using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	public OrganelleLabel Host { get; set; }

	public void OnPointerDown(PointerEventData data) {
		Debug.Log(Input.mousePosition.ToString());
	}

	public void OnPointerUp(PointerEventData data) {
		Debug.Log(Input.mousePosition.ToString());
	}
}
