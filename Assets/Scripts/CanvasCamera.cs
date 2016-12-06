using UnityEngine;
using System.Collections;

public class CanvasCamera : MonoBehaviour {

    private Camera targetCamera;


    // Use this for initialization
    void Start () {
        targetCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        this.GetComponent<Canvas>().worldCamera = targetCamera;
    }

	
	// Update is called once per frame
	void Update () {
	
	}
}
