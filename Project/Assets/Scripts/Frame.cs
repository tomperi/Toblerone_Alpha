using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frame : MonoBehaviour {


	void Start () 
	{
	}

	void Update () 
	{
		if (Input.GetMouseButtonDown (1)) 
		{
			Vector3 NewPosition = GetComponent<Transform> ().position;
			NewPosition.x -= 10;
			this.GetComponent<Transform> ().position = NewPosition;
		}
	}
}
