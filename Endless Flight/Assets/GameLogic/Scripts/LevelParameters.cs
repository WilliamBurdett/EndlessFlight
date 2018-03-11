using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelParameters : MonoBehaviour {
    public Vector2 center;
    [SerializeField]private float xMaxDistance;
    [SerializeField]private float yMaxDistance;
    public bool onRails;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Space)) {
	        onRails = !onRails;
        }
	}

    public float GetMaxX() {
        return xMaxDistance + center.x;
    }

    public float GetMinX() {
        return -xMaxDistance + center.x;
    }

    public float GetMaxY() {
        return yMaxDistance + center.y;
    }

    public float GetMinY() {
        return -yMaxDistance + center.y;
    }
}
