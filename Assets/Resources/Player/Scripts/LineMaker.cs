using UnityEngine;
using System.Collections;

public class LineMaker : MonoBehaviour {

	public enum Phase {
		Free,       //何もしてない
		Making,    //描いている
		Completion, //完成
	}

	private Vector2[] linePositions;

	private const int MAX_LINE_OBJECT = 100;
	public int max {
		get { return MAX_LINE_OBJECT; }
	}
	private int pointNum = 0;
	public int count {
		get { return pointNum; }
	}
	private Vector3 mousePos;

	private LineRenderer lineRenderer;

	Vector2 beforePos;
	[SerializeField]
	public bool moveOk;
	// Use this for initialization
	void Start() {
		linePositions = new Vector2[MAX_LINE_OBJECT];
		lineRenderer = GetComponent<LineRenderer>();
		moveOk = false;
	}

	// Update is called once per frame
	void Update() {
		
	}
}