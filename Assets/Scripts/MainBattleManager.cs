using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainBattleManager : MonoBehaviour {
	
	//ゲームの状態
	public enum State {
		Waiting,        //入力待ち中
		Playing,        //入力中
		Calculating,     //計算中
		Animation,		//演出中
		Clear           //ゲームクリア
	}

	//現在のゲームの状態
	public State currentState = State.Waiting;

	//再生させるオブジェクト
	public GameObject mainBattleScene;
	//再生しているシーンオブジェクト
	GameObject myScene;
	//プレイヤー
	PlayerStatus player;
	GameManager gameManager;
	
    //スキルポイントの合計値
    public int skillPoint = 0;
    //１ウェーブの時間
    int waveInterval = 5;
    //ウェーブの経過時間
    public float waveElapedTime = 0;
	//ダメージの合計値
	[SerializeField]
	int totalDamage = 0;
	//ボスキャラ
	BossStatus boss;
	//敵一覧
	public GameObject[] enemyList;

	public bool animationIsPlay = false;

	public GameObject cutInText;
	public GameObject cutInGraphics;

	// Use this for initialization
	void Start()
    {
        InputManager.Instance.state = InputManager.State.Up;
		boss = GameObject.Find("Boss").GetComponent<BossStatus>();
		gameManager = GetComponentInParent<GameManager>();
		player = GameObject.Find("Player").GetComponent<PlayerStatus>();
	}
	GameObject ct;
	bool flg = true;
    // Update is called once per frame
    void Update() {

        switch (currentState) {
            case State.Waiting:
				
            break;
            case State.Playing:
				waveElapedTime += Time.deltaTime;
				if (waveElapedTime > waveInterval) {
					waveElapedTime = 0;
					currentState = State.Calculating;
				}
            break;
            case State.Calculating:
				//攻撃演出
				currentState = State.Waiting;
				Reset();
				foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
					EnemyStatus es = enemy.GetComponent<EnemyStatus>();
					es.attackCount--;
					if (es.attackCount <= 0) {
						EnemyAttack(enemy);
					es.attackCount = es.maxCount;
					}
			} 
			break;
			case State.Animation:
				if (animationIsPlay) {
					Summon();
					return;
				}
			if (boss.HP <= 0) currentState = State.Clear;
			else {
				currentState = State.Waiting;
				Reset();
			}
				break;
			case State.Clear:
				gameManager.SceneTransition(GameManager.Scene.Result);
			break;
        }
    }

	public void OnSceneEnter() {
		//シーンが入ったときの処理
		//必要なオブジェクトの生成などはこの関数内でしてください。
		myScene = (GameObject)Instantiate(mainBattleScene , Vector2.zero , Quaternion.identity);
	}
    public void OnSceneExit() {
		//シーンから出るときの処理
		//オブジェクトの削除などはこの関数内でしてください。
	    Destroy(myScene);
    }
	void Reset() {
		totalDamage = 0;
		waveElapedTime = 0;
		flg = true;
	}
    public int GetDamageValue(GameObject attacker,GameObject another) {
		CharacterStatus attackerStatus = attacker.GetComponent<CharacterStatus>();
		CharacterStatus anotherStatus = another.GetComponent<CharacterStatus>();
		
		#region 相性の決定表
        /////////////////////////////////////////////
        //属性相性の決定表
        //                          →敵の属性
        //        　無　火　水　木
        //      無　Ｎ　Ｎ　Ｎ　Ｎ
        //      火　Ｎ　Ｎ　Ｂ　Ｇ　
        //      水　Ｎ　Ｇ　Ｎ　Ｂ
        //      木　Ｎ　Ｂ　Ｇ　Ｎ
        //
        //  ↓自分の属性   
        //
        //  Ｇ：Good/相性がいい
        //  Ｎ：Normal/普通
        //  Ｂ：Bad/相性が悪い
        ////////////////////////////////////////////
        #endregion
        float[,] damageRateAsType = new float[4 , 4] {
            { 1.1f,1.1f,1.1f,1.1f },
            { 1.1f,1.1f,0.8f,1.2f },
            { 1.1f,1.2f,1.1f,0.8f },
            { 1.1f,0.8f,1.2f,1.1f }
        };
        
		//攻撃力　*　相性　-　防御力
        return (int)(attackerStatus.attack * 
			damageRateAsType[(int)attackerStatus.type,(int)anotherStatus.type])
			- anotherStatus.defense;
    }

	public void AddDamageValue(int damageValue) {
		totalDamage += damageValue;
	}

	public void AttackToBoss() {
		boss.afterHp -= totalDamage;
	}
	GameObject animation;
	void Summon() {
		if (animation == null) {
			animation= (GameObject)Instantiate(cutInGraphics , new Vector2(0 , 0) , Quaternion.identity);
			animation.transform.SetParent(myScene.transform);
			animation.transform.localPosition = new Vector3(0 , 0 , 0);
			animation.transform.localScale = new Vector3(1 , 1 , 1);
		}
	}
	
	void EnemyAttack(GameObject s) {
		player.usingCharacter.GetComponent<PlayCharaStatus>().HP -= GetDamageValue(s , player.usingCharacter);
	}
}
