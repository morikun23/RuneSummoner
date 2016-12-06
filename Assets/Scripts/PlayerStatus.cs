using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {

	//現在のパーティ
	public GameObject[] MyParty;

	//現在使用しているキャラ
	public GameObject usingCharacter;

	void Start() {
		usingCharacter = MyParty[0];
	}

	void Update() {

	}
	public void CharaChange(string name) {

	}
}

