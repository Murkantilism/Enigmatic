﻿using UnityEngine;
using System.Collections;

public class DropPlatform : MonoBehaviour {
	Vector3 origin;
	bool destroy = false;

	// Use this for initialization
	void Start () {
		origin = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (!destroy)
			transform.position -= new Vector3(0, 0.04f, 0);
	}

	public void Reset() {
		transform.position = origin;
		destroy = true;
		GameObject.DestroyImmediate(this);
	}
}
