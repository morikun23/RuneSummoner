using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour {

    public GameObject resultScene;

    GameObject myScene;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnSceneEnter()
    {
        //シーンが入ったときの処理
        //必要なオブジェクトの生成などはこの関数内でしてください。
        myScene = (GameObject)Instantiate(resultScene, Vector2.zero, Quaternion.identity);
	}
    public void OnSceneExit()
    {
        //シーンから出るときの処理
        //オブジェクトの削除などはこの関数内でしてください。
		Destroy(myScene);
		//簡易的にシーンを再生しなおす
		SceneManager.LoadScene("Main");
    }
}
