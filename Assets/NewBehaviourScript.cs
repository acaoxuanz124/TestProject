﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debuger.Init();
	}
	
	// Update is called once per frame
	void Update () {

	}
    private void OnGUI()
    {
        Debuger.OnGUI();
    }
}
