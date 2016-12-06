using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadManager : MonoBehaviour {

	GameManager gameManager;
	public GameObject loadScene;
	GameObject myScene;

	//RateオブジェクトのTextコンポーネント
	Text rateText;

	//経過時間
	float elapsedTime = 0f;
	//空き時間
	float emptyTime = 3f;
	//ロードが完了するまでにかかる時間
	float loadTime = 2.0f;
	//現在何パーセントまでロードができているか
	float rate = 0;
	bool atSecond = false;
	// Use this for initialization
	void Start() {
		
	}

	// Update is called once per frame
	void Update() {

		//経過時間を加算
		elapsedTime += Time.deltaTime;

		//演出用にロードの進捗率を加算
		rate += Time.deltaTime * 2.5f;

		if (elapsedTime > emptyTime) {
			//空き時間が終わったら勢いよく加算
			//加算
			rate += Time.deltaTime * (100 / loadTime);
			//100%以上の数値にならないように補整
			if (rate >= 100)	rate = 100;
		}

		//実際に表示
		rateText.text = (int)rate + "%";

		//100%以上ならシーン遷移
		if (rate >= 100) {
			if (atSecond)
				gameManager.SceneTransition(GameManager.Scene.MainBattle);
			else
				gameManager.SceneTransition(GameManager.Scene.Menu);
		}
	}

	void Reset() {
		rate = 0;
		elapsedTime = 0;
		gameManager = GetComponentInParent<GameManager>();
		rateText = myScene.transform.Find("Rate").GetComponent<Text>();
	}
	public void OnSceneEnter() {
		//シーンが入ったときの処理
		//必要なオブジェクトの生成などはこの関数内でしてください。
		Debug.Log("enter");
		myScene = (GameObject)Instantiate(loadScene , Vector2.zero , Quaternion.identity);
		Reset();
	}
	public void OnSceneExit() {
		//シーンから出るときの処理
		//オブジェクトの削除などはこの関数内でしてください。
		Debug.Log("exit");
		atSecond = !atSecond;
		Destroy(myScene);
	}
}
