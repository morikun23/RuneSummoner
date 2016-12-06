using UnityEngine;
using System.Collections;
/// <summary>
/// 作成者：森田　勝
/// 日付：H28/09/10
/// Soundクラスと併用します。
/// Soundクラスで再生させた音オブジェクトに
/// コンポーネントされ、ここでは消す処理をしています。
/// </summary>
public class SoundDestroyer : MonoBehaviour {

    //AudioSourceコンポーネント
    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        //ループするのであれば、消す処理はここではしません。

        if (!audioSource.loop) {
            if (!audioSource.isPlaying) {
                //再生終了したら消します
                Destroy(this.gameObject);
            }
        }
	}
}
