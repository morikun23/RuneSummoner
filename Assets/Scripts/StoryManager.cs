using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour {

	GameManager gameManager;
	public GameObject storyScene;
	//public GameObject skipSeeker;

	GameObject myScene;
	RectTransform storyLog;

	// Use this for initialization
	void Start()
	{
		gameManager = GetComponentInParent<GameManager>();
		storyLog = myScene.transform.FindChild("Mask/StoryLog").GetComponent<RectTransform>();
	}

	// Update is called once per frame
	void Update()
	{
		float speed = 0.5f;
		if (InputManager.Instance.state == InputManager.State.Down) speed = 1.5f;
		storyLog.position += new Vector3(0 , speed , 0);

		if(storyLog.position.y > 620) {
			gameManager.SceneTransition(GameManager.Scene.Load);
		}
	}

	public void OnSceneEnter() {
		//シーンが入ったときの処理
		//必要なオブジェクトの生成などはこの関数内でしてください。
		myScene = (GameObject)Instantiate(storyScene, Vector2.zero, Quaternion.identity);
	}
    public void OnSceneExit() {
		//シーンから出るときの処理
		//オブジェクトの削除などはこの関数内でしてください。
		Destroy(myScene);
	}
}
