using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuCharaChange : MonoBehaviour {

    private GameObject image1;
    private GameObject image2;
    private GameObject image3;
    private GameObject image4;

    private GameObject nowImage;
    private float time;
    public float fadetime = 5.0f;
    private float count = 5.0f;
    bool isRunning = false;


    // Use this for initialization
    void Start () {
        image1 = GameObject.FindGameObjectWithTag("PlayerImage1");
        image2 = GameObject.FindGameObjectWithTag("PlayerImage2");
        image3 = GameObject.FindGameObjectWithTag("PlayerImage3");
        image4 = GameObject.FindGameObjectWithTag("PlayerImage4");
        // image1.SetActive(false);
        image2.SetActive(false);
        image3.SetActive(false);
        image4.SetActive(false);
        
    }

    // Update is called once per frame
    void Update () {
        /*StartCoroutine(coRoutine());
        count -= Time.deltaTime;
        time -= Time.deltaTime;//時間更新(徐々に減らす)
        float a = time / fadetime;//徐々に0に近づける
        var color = nowImage.GetComponent<Image>().color;
        color.a = -a;
        nowImage.GetComponent<Image>().color = color;
        Debug.Log(a);
        Debug.Log(time);
        if(count < 1.5f)
        {
            time -= Time.deltaTime;//時間更新(徐々に減らす)
            a = time / fadetime;//徐々に0に近づける
            color = nowImage.GetComponent<Image>().color;
            color.a = +a;
            nowImage.GetComponent<Image>().color = color;
        }*/
    }

    /*IEnumerator coRoutine()
    {
        if (isRunning)
            yield break;
        isRunning = true;

        time = fadetime;
        count = 5.0f;
        Change1();
        yield return new WaitForSeconds(5.0f);
        time = fadetime;
        count = 5.0f;
        Change2();
        yield return new WaitForSeconds(5.0f);
        count = 5.0f;
        time = fadetime;
        Change3();
        yield return new WaitForSeconds(5.0f);
        count = 5.0f;
        time = fadetime;
        Change4();
        yield return new WaitForSeconds(5.0f);

        isRunning = false;
    }

    void Change1()
    {
        nowImage = image1;
        image4.SetActive(false);
        image1.SetActive(true);
        
        

    }

    void Change2()
    {
        nowImage = image2;
        image1.SetActive(false);
        image2.SetActive(true);
       

    }

    void Change3()
    {
        nowImage = image3;
        image2.SetActive(false);
        image3.SetActive(true);

    }

    void Change4()
    {
        nowImage = image4;
        image3.SetActive(false);
        image4.SetActive(true);
    }*/
}
