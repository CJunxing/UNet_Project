using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 永远朝向摄像机
/// </summary>

public class LookAtCamera : MonoBehaviour {
    
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(Camera.main.transform);
	}
}
