using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LineGauge : MonoBehaviour {

	private LineMaker lineMaker;

	public Image image;
	public float currentLines;

	// Use this for initialization
	void Start() {
		
		lineMaker = GameObject.Find("LineMaker").GetComponent<LineMaker>();
	}

	// Update is called once per frame1
	void Update() {
		
		if(lineMaker._scene == LineMaker.LINE_MAKE_SCENE.MAKEING) {
			if( currentLines <= 0 ) {
				currentLines = 0;
			}
		}
		image.fillAmount = (float)(lineMaker.max - lineMaker.count) / (float)lineMaker.max;
	}
}
