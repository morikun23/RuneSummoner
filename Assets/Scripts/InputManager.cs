using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	/// <summary>
	/// Singleton
	/// </summary>
	private static InputManager instance = null;
	public static InputManager Instance {
		get {
			if (instance == null) {
				var obj = new GameObject("InputManager");
				instance = obj.AddComponent<InputManager>();
			}
			return instance;
		}
	}

	public enum State {
        Up,     //指が上がっている（画面に触れていない）
        Down,   //指が下がっている（画面に触れている）
    }

    public State state = State.Up;

	State beforState = State.Up;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0)) state = State.Down;
        else state = State.Up;
        
        switch (state) {
            case State.Up:      OnFingerIsUp();   break;
            case State.Down:    OnFingerIsDown(); break;
        }
	}
    
    void OnFingerIsUp() {
		if(beforState == State.Down) {
			beforState = State.Up;
			
		}	
    }

    void OnFingerIsDown() {
		if (beforState == State.Up) {
			beforState = State.Down;
		}
	}
}
