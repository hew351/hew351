﻿using UnityEngine;
using System.Collections;

// This class holds the various stats that will be used by the player and enemies in our game
// Each object will have their own version of a stat collection
public class StatCollectionClass : MonoBehaviour {
	
	public float health;

	public float initialHealth;
	
	public float mana;

	public float initialMana;
	
	//connect to player's normal attack damage, uesd for item.
	public float damage;

	public float baseMeleeDamage;

	public float baseRangedDamage;

	public float baseMagicDamage;
	
	//connect to player's ablity to decrease damage from enemy, linked with item.
	public float defend;

	public float baseDefense;
	
	public int strength;
	
	public int intellect;

	public int agility;
	
	public int xp;
	
	public int playerLevel;
	
	//add bool for item get and item equip for item manager
	public bool itemSword;
	
	public bool SwordEquip;
	
	public bool itemArmor;
	
	public bool ArmorEquip;
	
	public bool itemBow;
	
	public bool BowEquip;
	
	//add end
	
	public bool EnergyBallUnlocked;
	
	public float EnergyBalldamage;
	
	public bool FireBreathUnlocked;
	
	public bool SunStrikeUnlocked;

    public int playerDirection;
}

