using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LineMaker : MonoBehaviour {
	PlayerStatus player;
	public enum LINE_MAKE_SCENE {
		FREE,       //何もしてない
		MAKEING,    //描いている
		COMPLETION, //完成
	}

	public LINE_MAKE_SCENE _scene = LINE_MAKE_SCENE.FREE;

	private Vector2[] linePositions;

	private const int MAX_LINE_OBJECT = 100;
	public int max {
		get { return MAX_LINE_OBJECT; }
	}
	private int pointNum = 0;
	public int count {
		get { return pointNum; }
	}
	private Vector3 mousePos;

	private LineRenderer lineRenderer;

	Vector2 beforePos;
	[SerializeField]
	public List<GameObject> enemyInLine;
	public bool moveOk;
	// Use this for initialization
	void Start () {
		linePositions = new Vector2[MAX_LINE_OBJECT];
		lineRenderer = GetComponent<LineRenderer>();
		player = GameObject.Find("Player").GetComponent<PlayerStatus>();
		moveOk = false;
	}
	
	// Update is called once per frame
	void Update () {

		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward;
		switch (_scene) {
			case LINE_MAKE_SCENE.FREE:
				Reset();
				if (player.usingCharacter.GetComponent<PlayCharaStatus>().HP <= 0) return;
				//if (!InStage()) return;
				if (InputManager.Instance.state == InputManager.State.Down) {
					beforePos = mousePos;
				
				_scene = LINE_MAKE_SCENE.MAKEING;
				}
				break;
			case LINE_MAKE_SCENE.MAKEING:
				//if (!InStage()) return;

				if(InputManager.Instance.state == InputManager.State.Up) {
					_scene = LINE_MAKE_SCENE.COMPLETION;
				}
			
				if (pointNum < MAX_LINE_OBJECT) {
				
				if (Vector2.Distance(beforePos , mousePos) > 0.2f) {
					//一定距離以上離れたら線を引いているとみなす
					Writing();
					
					beforePos = linePositions[pointNum-1];
					}
				}
				//円が完成したら完成ステートへ
				if(IsCompletion()) _scene = LINE_MAKE_SCENE.COMPLETION;
				break;
			case LINE_MAKE_SCENE.COMPLETION:
				if (IsCompletion()) {
				foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
					if (HitTest(enemy.transform.position)) {
						enemyInLine.Add(enemy);
					}
				}
			if (AttackIsSuccess()) DestoryInLine();
			_scene = LINE_MAKE_SCENE.FREE;
			break;
		}
		
	}

	void Writing() {
		//線を描画します
		Vector3 linePos = mousePos;
		//現在のマウス座標を登録します
		linePositions[pointNum] = linePos;
		//使用中のキャラによって線の色を変えます
		switch (player.usingCharacter.GetComponent<PlayCharaStatus>().type) {
			case Type.Fire:		lineRenderer.material.color = Color.red;	break;
			case Type.Water:	lineRenderer.material.color = Color.blue;	break;
			case Type.Grass:	lineRenderer.material.color = Color.green;	break;
			case Type.Normal:	lineRenderer.material.color = Color.gray;	break;
		}
		//新しく登録されたマウス座標の分、頂点を増やします
		lineRenderer.SetVertexCount(pointNum + 1);
		//新しく登録された頂点に対して線を引く
		lineRenderer.SetPosition(pointNum , linePos);
		pointNum++;
	}

	bool HitTest(Vector2 targetPos) {
		//ポリゴンと点の当たり判定
		//点からステージの右端まで仮想の線を引く（線分AB）
		//ポリゴンの各頂点間を線と捉える（線分CD）
		//最後に各線分ABと線分CDの交差判定をまんべんなく行い、交差数が奇数だと内包、偶数だと外包されていることになる
		Vector2 a = targetPos;
		Vector2 b = targetPos + new Vector2(Screen.width,0);
		int crossCount = 0;
		for (int i = 0; i < pointNum - 2; i++) {
			//各線分CDとの交差判定を行う
			Vector2 c = linePositions[i];
			Vector2 d = linePositions[i + 1];
			if (IsCross(a , b , c , d))		crossCount++;
		}
		//奇数ならtrue、偶数ならfalseを返す
		return crossCount % 2 != 0;
	}

	void Reset() {
		pointNum = 0;
		lineRenderer.SetVertexCount(0);
		enemyInLine.Clear();
	}

	bool IsCompletion() {
		//各線分間が交差しているかを判定し、交差しているのであれば、円が完成
		if (pointNum < 5) return false;	
		for (int i = 0; i < pointNum - 2; i++) {
			Vector2 a = linePositions[pointNum - 1];
			Vector2 b = linePositions[pointNum - 2];
			Vector2 c = linePositions[i];
			Vector2 d = linePositions[i + 1];
			if (IsCross(a,b,c,d)) {
				DeleteLine(i);
				return true;
			}
		}
		return false;
	}

	bool IsCross(Vector2 a,Vector2 b,Vector2 c,Vector2 d) {
		//線分abと線分cdが交差しているかを返します
		float ta = (c.x - d.x) * (a.y - c.y) + (c.y - d.y) * (c.x - a.x);
		float tb = (c.x - d.x) * (b.y - c.y) + (c.y - d.y) * (c.x - b.x);
		float tc = (a.x - b.x) * (c.y - a.y) + (a.y - b.y) * (a.x - c.x);
		float td = (a.x - b.x) * (d.y - a.y) + (a.y - b.y) * (a.x - d.x);
		return tc * td < 0 && ta * tb < 0;
	}

	void DeleteLine(int num) {
		//円に入っていないラインの余分な部分を消去します。
		for(int i = 0; i < num; i++) {
			linePositions[i] = linePositions[num];
			lineRenderer.SetPosition(i , linePositions[i]);
		}
	}

	bool InStage() {
		return mousePos.y >= -3.0f && mousePos.y <= 3.5f;
	}

	bool AttackIsSuccess() {
		bool result = enemyInLine.Count >= player.usingCharacter.GetComponent<PlayCharaStatus>().attackCost;
		if (result) {
			foreach (var enemy in enemyInLine) {
				Destroy(enemy);	
			}
		}
		return result;
	}
	void DestoryInLine() {
		foreach(var enemy in enemyInLine)	Destroy(enemy);
	}
}
