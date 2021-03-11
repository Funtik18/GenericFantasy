using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsAdapter : MonoBehaviour
{
	[SerializeField] private Entity<EntityStatistics> entity;


	public void Roll()
	{
		DiceFormule d = new DiceFormule(2, DiceType.Cube, -3);
	}

	public void ResetStats()
	{
		entity.Statistics.stats.ResetStats();
	}
	public void RndStats()
	{
		entity.Statistics.stats.RndStats();
	}
	
	public void AddWeight()
	{
		entity.Statistics.stats.Weight.StatCurrentValue += Random.Range(1f, 5f);
	}

	public void RemoveWeight()
	{
		entity.Statistics.stats.Weight.StatCurrentValue -= Random.Range(1f, 5f);
	}

	public void IncreaseStrength()
	{
		entity.Statistics.stats.IncreaseStrength();
	}
	public void DecreaseStrength()
	{
		entity.Statistics.stats.DecreaseStrength();
	}

	public void IncreaseDexterity()
	{
		entity.Statistics.stats.IncreaseDexterity();
	}
	public void DecreaseDexterity()
	{
		entity.Statistics.stats.DecreaseDexterity();
	}

	public void IncreaseIntelligence()
	{
		entity.Statistics.stats.IncreaseIntelligence();
	}
	public void DecreaseIntelligence()
	{
		entity.Statistics.stats.DecreaseIntelligence();
	}

	public void IncreaseVitality()
	{
		entity.Statistics.stats.IncreaseVitality();
	}
	public void DecreaseVitality()
	{
		entity.Statistics.stats.DecreaseVitality();
	}

	public void IncreaseHealth()
	{
		entity.Statistics.stats.IncreaseHealth();
	}
	public void DecreaseHealth()
	{
		entity.Statistics.stats.DecreaseHealth();
	}

	public void IncreaseMove()
	{
		entity.Statistics.stats.IncreaseMove();
	}
	public void DecreaseMove()
	{
		entity.Statistics.stats.DecreaseMove();
	}

	public void IncreaseSpeed()
	{
		entity.Statistics.stats.IncreaseSpeed();
	}
	public void DecreaseSpeed()
	{
		entity.Statistics.stats.DecreaseSpeed();
	}

	public void IncreaseWill()
	{
		entity.Statistics.stats.IncreaseWill();
	}
	public void DecreaseWill()
	{
		entity.Statistics.stats.DecreaseWill();
	}

	public void IncreasePerception()
	{
		entity.Statistics.stats.IncreasePerception();
	}
	public void DecreasePerception()
	{
		entity.Statistics.stats.DecreasePerception();
	}

	public void IncreaseFatigue()
	{
		entity.Statistics.stats.IncreaseFatigue();
	}
	public void DecreaseFatigue()
	{
		entity.Statistics.stats.DecreaseFatigue();
	}
}