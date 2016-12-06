using UnityEngine;
using System.Collections;

public class PlayCharaStatus : CharacterStatus {

	//攻撃コスト
	public int attackCost;

	//スキルコスト
	public int skillCost;

	//スキル一覧
	public SkillList mySkill = SkillList.NoDamage;

	
}

public enum SkillList {
	StrongAttack,	//攻撃力ＵＰ
	Heal,			//回復
	WaveStop,		//時間を止めて囲み放題
	NoDamage		//被ダメージを０にする
}
