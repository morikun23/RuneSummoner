using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GatshaManager : MonoBehaviour {

	GameManager gameManager;
	GameObject myScene;

	public enum State{
		Waiting, //ガチャ回転前
		Start,   //ガチャ回転中
		End      //ガチャの結果
	}
	public State currentState = State.Waiting;

	//魔法陣発光イメージオブジェクト
	private Image MahoujinEffectImage,YajirushiImage,GetItemImage;
	private GameObject MahoujinObj,MahoujinTextObj,GetItemObj;
	private Animator GatshaAnim;
	private float mahoujinEffect = 0;
	float _direction, gatshaTime;
	private GameObject GetTextObj,RarityObj;
	private GameObject CanvasClone;
	private string[] wanko;
	private int[] RarityLevel;
	private int level;
	private List<GameObject> StarObj = new List<GameObject>();
	// Use this for initialization
	void Start () {
		StartCoroutine ("Restart");
		gameManager = GetComponentInParent<GameManager>();
	}

	// Update is called once per frame
	void Update () {
		//クリックするとStateがStartに切り替わる
		//魔法陣の色と透過率が固定されて回転がはじまる
		if (Input.GetMouseButtonDown (0)) {
			if (currentState == State.Waiting) {
				currentState = State.Start;
				MahoujinEffectImage.color = new Color (1, 1, 1, 1);
				YajirushiImage.color = new Color (1, 1, 1, 0);
				MahoujinTextObj.SetActive (false);
				gatshaTime = Time.time + 3f;
			} else if(currentState == State.End) {
				//ガチャのリセット
				GetTextObj.SetActive (false);
				RarityObj.SetActive (false);
				StarObj [0].SetActive (false);
				StarObj [1].SetActive (false);
				StarObj [2].SetActive (false);
				GetItemImage.color = new Color (1, 1, 1, 0);
				MahoujinTextObj.SetActive (true);
				_direction = 0;
				MahoujinObj.transform.rotation = Quaternion.Euler(0,0,0);
				level = UnityEngine.Random.Range (0, 4);
				Sprite GImage = Resources.Load ("GameScenes/Gatsha/" + wanko[level],typeof(Sprite)) as Sprite;
				GetItemImage.sprite = GImage;
				GatshaAnim.SetBool ("gatshaStart", false);
				currentState = State.Waiting;
			}
		}
		switch (currentState) {
		case State.Waiting:
			MahoujinEffectImage.color = new Color (1, 1, 1, mahoujinEffect*2f);
			YajirushiImage.color =  new Color (1, 1, 1, mahoujinEffect);
			if (mahoujinEffect >= 1) {
				_direction = Time.deltaTime * -1;
			} else if(mahoujinEffect <= 0){
				_direction = Time.deltaTime;
			}
			mahoujinEffect += _direction;
			break;
		case State.Start:
			MahoujinObj.transform.Rotate (new Vector3 (0, 0, -1), _direction);
			if (Time.time > gatshaTime) {
				MahoujinObj.transform.Rotate (new Vector3 (0, 0, -1), _direction);
				GatshaAnim.SetBool ("gatshaStart", true);
				if (Time.time > gatshaTime + 2f) {
					mahoujinEffect += Time.deltaTime / 1.2f;
					GetItemImage.color = new Color (1, 1, 1, mahoujinEffect);
				}
				if (_direction > 0) {
					_direction -= Time.deltaTime * 4f;
					GetItemObj.GetComponent<RectTransform> ().localPosition = new Vector3 (0, 50f + _direction, 0);
				} else {
					_direction = 0;
					RarityObj.SetActive (true);
					for (int i = 1; i < 4; i++) {
						StarObj.Add(CanvasClone.transform.FindChild ("Rarity/Hoshi" + i.ToString () + "Flame/Hoshi" + i.ToString()).gameObject);
					}
					StarObj [0].SetActive (false);
					StarObj [1].SetActive (false);
					StarObj [2].SetActive (false);
					currentState = State.End;
				}
			} else {
				mahoujinEffect = 0;
				_direction += Time.deltaTime * 8f;
			}
			break;
		case State.End:
			GetTextObj.SetActive (true);
			switch (RarityLevel[level]) {
				case 1:
				StarObj[0].SetActive(true);
				break;
				case 2:
				StarObj[0].SetActive(true);
				StarObj[1].SetActive(true);
				break;
				case 3:
				StarObj[0].SetActive(true);
				StarObj[1].SetActive(true);
				StarObj[2].SetActive(true);
				break;
			}
			gameManager.SceneTransition(GameManager.Scene.Menu);
			break;
		}
	}


	[SerializeField]
	GameObject Canvas;
    public void OnSceneEnter() {
        //シーンが入ったときの処理
        //必要なオブジェクトの生成などはこの関数内でしてください。
		myScene = Instantiate(Canvas);
    }
    public void OnSceneExit() {
        //シーンから出るときの処理
        //オブジェクトの削除などはこの関数内でしてください。
		Destroy(myScene);
    }


	public IEnumerator Restart(){
		CanvasClone = GameObject.Find("Canvas(Clone)");
		//CanvasオブジェクトのRenderCameraにMainCameraをアタッチ
		CanvasClone.GetComponent<Canvas>().worldCamera = Camera.main;
		MahoujinEffectImage = CanvasClone.transform.FindChild ("MahouJin/MahouJinEffect").GetComponent<Image>();
		YajirushiImage = CanvasClone.transform.FindChild ("MahouJin/Yajirushi").GetComponent<Image> ();
		GetItemImage = CanvasClone.transform.FindChild ("GetEnemyImage").GetComponent<Image> ();
		MahoujinObj = CanvasClone.transform.FindChild ("MahouJin").gameObject;
		MahoujinTextObj = CanvasClone.transform.FindChild ("MahouJin/Text").gameObject;
		GetItemObj = CanvasClone.transform.FindChild ("GetEnemyImage").gameObject;
		GatshaAnim = CanvasClone.transform.FindChild("MahouJin").GetComponent<Animator> ();
		GetTextObj = CanvasClone.transform.FindChild ("GetText").gameObject;
		RarityObj = CanvasClone.transform.FindChild ("Rarity").gameObject;
		GetTextObj.SetActive (false);
		RarityObj.SetActive (false);
		wanko = new string[]{"わんこ青","わんこ赤","わんこ無","わんこ緑"};
		RarityLevel = new int[]{1, 2, 3, 2};
		level = UnityEngine.Random.Range (0, 4);
		Sprite GImage = Resources.Load ("GameScenes/Gatsha/" + wanko[level],typeof(Sprite)) as Sprite;
		GetItemImage.sprite = GImage;
		yield break;
	}

}
