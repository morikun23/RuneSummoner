using UnityEngine;
using System.Collections;

public class ResultButton : MonoBehaviour {

    GameManager gm;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void OnClick()
    {
        gm.SceneTransition(GameManager.Scene.Title);
    }

}
