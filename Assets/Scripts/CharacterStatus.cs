using UnityEngine;
using System.Collections;

public abstract class CharacterStatus : MonoBehaviour {

	//名前
	public string name;

	//最大体力
	public int maxHp;

	//体力
	public int HP;

	//属性
	public Type type;

	//攻撃力
	public int attack;

	//防御力
	public int defense;

	//画像
	public Sprite mySprite;
}

public enum Type {
	Normal = 0,
	Fire,
	Grass,
	Water
}