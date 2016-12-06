using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerLifeGauge : MonoBehaviour {

	private float playerLifeMax;
	private float playerLife;
	private GameObject character;
	public Image image;
	private GameObject player;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update() {
		player = GameObject.Find( "Player" );
		character = player.GetComponent<PlayerStatus>().usingCharacter;
		playerLife = character.GetComponent<CharacterStatus>().HP;
		playerLifeMax = character.GetComponent<CharacterStatus>().maxHp;
		image.fillAmount = playerLife / playerLifeMax;
	}
}
