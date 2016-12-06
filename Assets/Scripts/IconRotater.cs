using UnityEngine;
using System.Collections;

public class IconRotater : MonoBehaviour {

	[SerializeField]
	[Tooltip("回転速度")]
	float rotateSpeed;
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.back);
	}
}
