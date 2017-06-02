using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour {
	public static CameraFollow instance;
	public Text countText;
	private int count;

	public Transform target;

	void Start () {
		if (instance == null) 
			instance = this;
		count = 0;
		SetCountText();		
	}

	void LateUpdate () {		
		transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
	}
	
	void SetCountText() {
		countText.text = "Score: " + count.ToString();
	}

	public void UpdateCount() {
		count++;
		SetCountText();
	}	
}
