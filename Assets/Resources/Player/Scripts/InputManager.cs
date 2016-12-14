using UnityEngine;
using System.Collections;

/// <summary>
/// クラス名：森田　勝
/// 作成日　：H28/11/19
/// 概要　　：入力の状態を扱うクラス
///			　タップ、スワイプ、長押しといった機能を扱うクラスを
///			　使い分けてます。
/// </summary>
public class InputManager : MonoBehaviour {

	/// <summary>
	/// Singleton
	/// </summary>
	private static InputManager instance;
	
	public static InputManager Instance {
		get {
			if(instance == null) {
				instance = new GameObject("InputManager").AddComponent<InputManager>();
			}
			return instance;
		}
	}

	public enum State {
		Up,		//指が上がっている
		Down	//指が下がっている（入力中）
	}

	enum InputType {
		Free,	//何もしていない
		Tap,	//タップ
		Press,	//長押し
		Swipe	//スワイプ
	}

	private InputType m_currentType;

	//入力経過時間
	private float m_elapsedTime;
	//入力の開始座標
	private Vector3 m_startPosition;
	//入力の終了座標
	private Vector3 m_endPosition;
	//入力開始座標から移動した距離
	private float m_inputLength;
	//指が下がっている（入力中か）
	private bool m_isDown;

	private State m_currentState;

	public State CurrentState {
		get { return m_currentState; }
	}

	//現在、指しているターゲット
	private GameObject m_currentTarget;
	// Use this for initialization
	void Start () {
		m_currentState = State.Up;
		m_currentType = InputType.Free;
	}

	// Update is called once per frame
	void Update() {

		if (Input.GetMouseButtonDown(0)) {	
			BeginInput();
		}
		if (Input.GetMouseButton(0)) {
			ReadInput();
		}
		if(Input.GetMouseButtonUp(0)) {
			EndInput();
		}

	}

	public bool Tap(GameObject target) {
		bool result =
			(m_currentTarget == target) &&
			(m_currentType == InputType.Tap);

		return result;
	}

	public bool Swipe(GameObject target) {
		bool result = 
			(m_currentTarget == target) && 
			(m_currentType == InputType.Swipe);

		return result;
	}

	public bool Press(GameObject target) {
		bool result =
					(m_currentTarget == target) &&
					(m_currentType == InputType.Swipe);

		return result;
	}

	private void BeginInput() {
		m_currentState = State.Down;
		m_startPosition = Input.mousePosition;
		m_isDown = true;
		m_elapsedTime = 0;
		m_inputLength = 0;

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;
		if(Physics.Raycast(ray,out hitInfo)) {
			m_currentTarget = hitInfo.collider.gameObject;
		}
		else {
			m_currentTarget = null;
		}
	}

	private void ReadInput() {
		
		m_endPosition = Input.mousePosition;
		m_elapsedTime += Time.deltaTime;
		m_inputLength = Vector3.Distance(m_startPosition , m_endPosition);

		if(m_elapsedTime < 1.0f) {
			//入力してから一定時間まではタップとみなす
			m_currentType = InputType.Tap;
		}
		else if(m_inputLength > 100) {
			//入力してから一定距離指を動かすとスワイプとみなす
			m_currentType = InputType.Swipe;
		}
		else {
			//一定時間以上指を動かさずいると長押しとみなす
			//※すでにスワイプとみなされている場合は除く
			if(m_currentType != InputType.Swipe)
			m_currentType = InputType.Press;
		}
	}

	private void EndInput() {
		m_currentState = State.Up;
		
	}
}
