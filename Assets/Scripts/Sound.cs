using UnityEngine;
using System.Collections;
/// <summary>
/// 作成者：森田　勝
/// 日付：H28/09/10
/// Soundを簡単に再生させるためのクラス
/// 
/// Sound se = new Sound(必要引数)
/// 
/// 上記のようにインスタンス化させてあげることで
/// シーン上にSoundを生成して、再生してくれます。
/// ここではSoundObjectという空のGameObjectを生成し、
/// それに音をアタッチさせるような仕組みになっています。
/// </summary>
public class Sound : MonoBehaviour {
    

    //AudioSourceが埋め込まれる空のゲームオブジェクト
    private GameObject soundObject;
    //AudioSourceコンポーネント
    public AudioSource audioSource;
	//BGMかどうか
	public bool isBgm {
		set { audioSource.loop = value; }
		get { return audioSource.loop; }
	}

    public Sound() {
        //SoundObjectの生成
        Init();
    }
    public Sound(Vector3 pos) {
        //SoundObjectを指定座標に生成
        Init();
        soundObject.transform.position = pos;
    }
    public Sound(Vector3 pos,string pass) {
        //SoundObjectを指定座標に生成し、同時にSEも設定します
        Init();
        soundObject.transform.position = pos;
        SetSound(pass);
    }
    void Init() {
        //soundObjectを生成します。
        soundObject = new GameObject("SoundObject");
        audioSource = soundObject.AddComponent<AudioSource>();
        soundObject.AddComponent<SoundDestroyer>();
    }
    public void SetSound(string pass) {
        //再生させるSEを設定する
        audioSource.clip = Resources.Load<AudioClip>("Audio" + pass);
    }

    public void SetParent(GameObject parent) {
        //SoundObjectの親を設定します
        soundObject.transform.parent = parent.transform;
    }
    public void SetPosition(Vector3 position) {
        //SoundObjectの座標を設定します
        soundObject.transform.position = position;
    }
    public Vector3 GetPosition() {
        //SoundObjectの座標を取得します
        return soundObject.transform.position;
    }
    
}
