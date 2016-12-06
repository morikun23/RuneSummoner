using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// 作成者：森田　勝
/// 日付：H28/09/15
/// 今回は、１つのシーン内で管理させるため
/// 以下のようになっています。
/// TitleManagerなどシーンマネージャーを
/// GameManager傘下に配置し、
/// 現在再生しているシーンによって
/// Activeを切り替えています。
/// </summary>
public class GameManager : MonoBehaviour {

	/// <summary>
	/// Singleton
	/// </summary>
	private static GameManager instance = null;
	public static GameManager Instance {
		get {
			if (instance == null) {
				var obj = new GameObject("GameManager");
				instance = obj.AddComponent<GameManager>();
			}
			return instance;
		}
	}

	//これが全てのシーン（画面）です
	public enum Scene {
        Title,              //タイトルシーン
        Story,              //ストーリーシーン
        Load,               //ロードシーン
        Menu,               //メニュー選択シーン
        Edit,               //編集シーン
        Gatsha,             //ガチャシーン
        QuestChoice,        //クエスト選択シーン
        MainBattle,         //メイン戦闘シーン
        Result              //リザルトシーン
    }

    //現在、再生しているシーン
    public Scene currentScene = Scene.Title;

	// Use this for initialization
	void Start () {

    }
	
    
    public void SceneTransition(Scene next) {
		SceneManager.LoadScene(next.ToString());	
    }
}
