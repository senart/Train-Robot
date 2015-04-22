using UnityEngine;
using System.Collections;

public class ModuleStats : Stats {

	public int startingSpeed;
	public int startingDamage;
	public int startingHP;
	public int startingProtection;

	public string moduleName, moduleDescription;
	public int damage;
	public int protection;
	public float HP;

	void Awake()
	{
		HP = startingHP;
	}

	public void UpdateBonusStats(int speedBonus, int damageBonus, int protectionBonus, int HPBonus)
	{
		speed = startingSpeed + speedBonus;
		damage = startingDamage + damageBonus;
		protection = startingProtection + protectionBonus;
		if (HP >= startingHP) HP = startingHP + HPBonus;
	}
}
