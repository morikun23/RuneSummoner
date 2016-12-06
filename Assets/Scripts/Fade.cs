using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Fade : MonoBehaviour {

	//public 
	float fade_time;

	Image image;
	Color color;

	bool is_fade_in = true;
	bool is_fade_out = false;

	float fade_counter;

	// Use this for initialization
	void Start () {
		image = GetComponent<Image>();
		color = image.color;

		color.a = 1.0f;
		image.color = color;
	}
	
	// Update is called once per frame
	void Update () {
		if(is_fade_in)
		{
			color.a = -fade_counter * (1.0f / fade_time);
			image.color = color;

			fade_counter += Time.deltaTime;

			if(fade_counter >= 0.0f)
			{
				is_fade_in = false;
			}
		}

		if(is_fade_out)
		{
			color.a = 1.0f + fade_counter * (1.0f / fade_time);
			image.color = color;

			fade_counter += Time.deltaTime;
		}
	}

	public void SetFadeTime(float fade_time)
	{
		this.fade_time = fade_time;
		fade_counter = -fade_time;
	}

	public void FadeOut()
	{
		is_fade_out = true;
		fade_counter = -fade_time;
	}
}
