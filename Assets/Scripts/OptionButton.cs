using UnityEngine;
using System.Collections;

public class OptionButton : MonoBehaviour {

	//ウィンドウが表示されている状態か
	public bool isWindowShowing = false;

	//表示するウィンドウオブジェクト
	public GameObject window;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClicked() {
		if (isWindowShowing) {
			//ウィンドウが表示されているなら閉じる
			isWindowShowing = false;
		}
		else {
			//ウィンドウが表示されていないのなら表示する
			isWindowShowing = true;
		}
	}
}
