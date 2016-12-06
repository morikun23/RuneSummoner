using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	GameManager gameManager;

    public GameObject menuScene;

    GameObject myScene;

    // Use this for initialization
    void Start () {
		gameManager = GetComponentInParent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnSceneEnter() {
        //シーンが入ったときの処理
        //必要なオブジェクトの生成などはこの関数内でしてください。
        myScene = (GameObject)Instantiate(menuScene, Vector2.zero, Quaternion.identity);
	}
    public void OnSceneExit() {
        //シーンから出るときの処理
        //オブジェクトの削除などはこの関数内でしてください。
        Destroy(myScene);
    }
}
