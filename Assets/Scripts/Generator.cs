using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Generator : MonoBehaviour {

	MainBattleManager mBattleManager;
	GameObject emergedEnemy;
	float elapsedTime = 0;

	// Use this for initialization
	void Start () {
		mBattleManager = transform.root.GetComponent<MainBattleManager>();
		emergedEnemy = NewEnemy();
	}
	
	// Update is called once per frame
	void Update () {
		if (emergedEnemy == null) {
			if(mBattleManager.currentState == MainBattleManager.State.Waiting) {
				//emergedEnemy = NewEnemy();
			}
		}
	}
	
	GameObject NewEnemy() {
		GameObject enemy;
		enemy = (GameObject)Instantiate(mBattleManager.enemyList[Random.Range(0 , 4)] ,transform.position, Quaternion.identity);
		//修正・補整
		enemy.transform.parent = transform;
		enemy.transform.localScale = new Vector3(1 , 1 , 1);
		EnemyStatus status = enemy.GetComponent<EnemyStatus>();
		status.attack = Random.Range(30 , 50);
		status.attackCount = status.maxCount = Random.Range(2 , 5);
		status.defense = Random.Range(10 , 30);
		return enemy;
	}
}
