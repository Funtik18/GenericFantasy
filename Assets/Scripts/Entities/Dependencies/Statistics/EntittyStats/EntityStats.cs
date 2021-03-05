using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

public class EntityStats
{
	private StatsData data;

	private readonly int totalPoints = 100;

	public List<Characteristic> statsAll;
	public List<Bar> barsAll;
	public Dictionary<STATS, IModifiable> statsModifiable;

	public CharacteristicValue Level;
	
	public CharacteristicValue Points;

	public Bar EXP;

	public Bar WEIGHT;
	
	public Bar HP;
	public Bar MP;
	public Bar FP;

	public CharacteristicWeight Weight;

	public StatCharacteristic Strength;
	public StatCharacteristic Dexterity;
	public StatCharacteristic Intelligence;
	public StatCharacteristic Vitality;

	public StatCharacteristic Health;
	public StatCharacteristic Move;
	public StatCharacteristic Speed;
	public StatCharacteristic Will;
	public StatCharacteristic Perception;
	public StatCharacteristic Fatigue;
	

	public EntityStats(StatsData statsData)
	{
		this.data = statsData;

		InitializeStats();
	}

	#region Behavior
	public void ResetStats()
	{
		Strength.ResetStat(data.statsPrimary.strength);
		Dexterity.ResetStat(data.statsPrimary.dexterity);
		Intelligence.ResetStat(data.statsPrimary.intelligence);
		Vitality.ResetStat(data.statsPrimary.vitality);

		Health.ResetStat(data.statsSecondary.healthPoints);
		Move.ResetStat(data.statsSecondary.move);
		Speed.ResetStat(data.statsSecondary.speed);
		Will.ResetStat(data.statsSecondary.will);
		Perception.ResetStat(data.statsSecondary.perception);
		Fatigue.ResetStat(data.statsSecondary.fatiguePoints);

		Points.StatValue = totalPoints;
	}
	public void RndStats()
	{

	}

	public void IncreaseStrength()
	{
		if(Points.StatValue - 10 >= 0)
		{
			Points.StatValue -= 10;


			Strength.StatBaseValue += 1;

			Health.StatFormuleValue += 1;
			//грузоподъёмность = Strength*Strength
		}
	}
	public void DecreaseStrength()
	{
		if(Strength.StatBaseValue - 1 >= 5)
		{
			Points.StatValue += 10;

			Strength.StatBaseValue -= 1;

			Health.StatFormuleValue -= 1;
		}
	}

	public void IncreaseDexterity()
	{
		if(Points.StatValue - 20 >= 0)
		{
			Points.StatValue -= 20;


			Dexterity.StatBaseValue += 1;

			Speed.StatFormuleValue += 0.25f;
			Move.StatFormuleValue += 0.25f;
		}
	}
	public void DecreaseDexterity()
	{
		if(Dexterity.StatBaseValue - 1 >= 5)
		{
			Points.StatValue += 20;


			Dexterity.StatBaseValue -= 1;

			Speed.StatFormuleValue -= 0.25f;
			Move.StatFormuleValue -= 0.25f;
		}
	}

	public void IncreaseIntelligence()
	{
		if(Points.StatValue - 20 >= 0)
		{
			Points.StatValue -= 20;


			Intelligence.StatBaseValue += 1;

			Will.StatFormuleValue += 1;
			Perception.StatFormuleValue += 1;
		}
	}
	public void DecreaseIntelligence()
	{
		if(Intelligence.StatBaseValue - 1 >= 5)
		{
			Points.StatValue += 20;


			Intelligence.StatBaseValue -= 1;

			Will.StatFormuleValue -= 1;
			Perception.StatFormuleValue -= 1;
		}
	}

	public void IncreaseVitality()
	{
		if(Points.StatValue - 20 >= 0)
		{
			Points.StatValue -= 10;


			Vitality.StatBaseValue += 1;

			Move.StatFormuleValue += 0.25f;
			Speed.StatFormuleValue += 0.25f;
			Fatigue.StatFormuleValue += 1;
		}
	}
	public void DecreaseVitality()
	{
		if(Vitality.StatBaseValue - 1 >= 5)
		{
			Points.StatValue += 10;


			Vitality.StatBaseValue -= 1;

			Move.StatFormuleValue -= 0.25f;
			Speed.StatFormuleValue -= 0.25f;
			Fatigue.StatFormuleValue -= 1;
		}
	}

	public void IncreaseHealth()
	{
		if(Points.StatValue - 2 >= 0)
		{
			if((Health.StatValue + 1) <= (Strength.StatValue * 0.3f) + Strength.StatValue)
			{
				Points.StatValue -= 2;

				Health.StatBaseValue += 1;
			}
		}
	}
	public void DecreaseHealth()
	{
		if(Health.StatBaseValue - 1 >= 0)
		{
			if((Strength.StatValue * 0.7f) <= Health.StatValue - 1)
			{
				Points.StatValue += 2;

				Health.StatBaseValue -= 1;
			}
		}
	}

	public void IncreaseMove()
	{
		if(Points.StatValue - 5 >= 0)
		{
			Points.StatValue -= 5;

			Move.StatBaseValue += 1;
		}
	}
	public void DecreaseMove()
	{
		if(Move.StatBaseValue - 1 >= 0)
		{
			Points.StatValue += 5;

			Move.StatBaseValue -= 1;
		}
	}

	public void IncreaseSpeed()
	{
		if(Points.StatValue - 5 >= 0)
		{
			if(Speed.StatBaseValue + 0.25f <= 7)
			{
				Points.StatValue -= 5;

				Speed.StatBaseValue += 0.25f;

				Move.StatFormuleValue += 0.25f;
			}
		}
	}
	public void DecreaseSpeed()
	{
		if(Speed.StatBaseValue - 0.25f >= 0)
		{
			if(3 <= Speed.StatBaseValue - 0.25f)
			{
				Points.StatValue += 5;

				Speed.StatBaseValue -= 0.25f;

				Move.StatFormuleValue -= 0.25f;
			}
		}
	}

	public void IncreaseWill()
	{
		if(Points.StatValue - 5 >= 0)
		{
			if(Will.StatBaseValue + 1 <= 20)
			{
				Points.StatValue -= 5;


				Will.StatBaseValue += 1;
			}
		}
	}
	public void DecreaseWill()
	{
		if(Will.StatBaseValue - 1 >= 0)
		{
			if(4 <= Will.StatBaseValue - 1)
			{
				Points.StatValue += 5;


				Will.StatBaseValue -= 1;
			}
		}
	}

	public void IncreasePerception()
	{
		if(Points.StatValue - 5 >= 0)
		{
			if(Perception.StatBaseValue + 1 <= 20)
			{
				Points.StatValue -= 5;


				Perception.StatBaseValue += 1;
			}
		}
	}
	public void DecreasePerception()
	{
		if(Perception.StatBaseValue - 1 >= 0)
		{
			if(4 <= Perception.StatBaseValue - 1)
			{
				Points.StatValue += 5;


				Perception.StatBaseValue -= 1;
			}
		}
	}

	public void IncreaseFatigue()
	{
		if(Points.StatValue - 3 >= 0)
		{
			if((Fatigue.StatValue + 1) <= (Vitality.StatValue * 0.3f) + Vitality.StatValue)
			{
				Points.StatValue -= 3;


				Fatigue.StatBaseValue += 1;
			}
		}
	}
	public void DecreaseFatigue()
	{
		if(Fatigue.StatBaseValue - 1 >= 0)
		{
			if((Vitality.StatValue * 0.7f) <= (Fatigue.StatValue - 1))
			{
				Points.StatValue += 3;


				Fatigue.StatBaseValue -= 1;
			}
		}
	}

	//	EXP.MaxValue = (uint)(500 * ((Level.Value + 1) * (Level.Value + 1)) - (500 * (Level.Value + 1)));
	#endregion

	private void InitializeStats()
	{
		CreateFields();

		SetupAllStats();
	}
	private void CreateFields()
	{
		Level = new CharacteristicValue(data.level);

		EXP = new Bar(5, 10);

		Points = new CharacteristicValue(totalPoints);

		Strength = new StatCharacteristic(data.statsPrimary.strength);
		Dexterity = new StatCharacteristic(data.statsPrimary.dexterity);
		Intelligence = new StatCharacteristic(data.statsPrimary.intelligence);
		Vitality = new StatCharacteristic(data.statsPrimary.vitality);

		Health = new StatCharacteristic(data.statsSecondary.healthPoints);
		Move = new StatCharacteristic(data.statsSecondary.move);
		Speed = new StatCharacteristic(data.statsSecondary.speed, false);
		Will = new StatCharacteristic(data.statsSecondary.will);
		Perception = new StatCharacteristic(data.statsSecondary.perception);
		Fatigue = new StatCharacteristic(data.statsSecondary.fatiguePoints);

		Weight = new CharacteristicWeight(data.currentWeight, Strength);

		HP = new BarPoints(data.statsSecondary.healthCurrentPoints, Health);
		MP = new BarPoints(data.statsSecondary.willCurrentPoints, Will);
		FP = new BarPoints(data.statsSecondary.fatigueCurrentPoints, Fatigue);
	}
	private void SetupAllStats()
	{
		statsAll = new List<Characteristic>();

		statsAll.Add(Level);

		statsAll.Add(Points);

		statsAll.Add(Weight);

		statsAll.Add(Strength);
		statsAll.Add(Dexterity);
		statsAll.Add(Intelligence);
		statsAll.Add(Vitality);

		statsAll.Add(Health);
		statsAll.Add(Move);
		statsAll.Add(Speed);
		statsAll.Add(Will);
		statsAll.Add(Perception);
		statsAll.Add(Fatigue);

		barsAll = new List<Bar>();
		barsAll.Add(EXP);
		barsAll.Add(HP);
		barsAll.Add(MP);
		barsAll.Add(FP);
	}
}

[System.Serializable]
public struct StatsData
{
	[MinValue(1)] public int level;
	[Space]
	[ReadOnly] public int experienceCurrentPoints;

	[ReadOnly] public int currentWeight;

	[TabGroup("StatsPrimary")]
	[HideLabel]
	public StatsPrimary statsPrimary;

	[TabGroup("StatsSecondary")]
	[HideLabel]
	public StatsSecondary statsSecondary;

	[Button]
	private void Reset()
	{
		statsPrimary.Reset();
		statsSecondary.Reset();
	}

	[System.Serializable]
	public struct StatsPrimary
	{
		[Space]
		[MinValue(0)] public int strength;
		[MinValue(0)] public int dexterity;
		[MinValue(0)] public int intelligence;
		[MinValue(0)] public int vitality;

		public void Reset()
		{
			strength = 10;
			dexterity = 10;
			intelligence = 10;
			vitality = 10;
		}
	}
	[System.Serializable]
	public struct StatsSecondary
	{
		[MinValue(0)] public int healthPoints;
		[ReadOnly] public int healthCurrentPoints;

		[MinValue(0)] public int move;
		[ReadOnly] [MinValue(0)] public float speed;

		[MinValue(0)] public int will;
		[ReadOnly] public int willCurrentPoints;

		[MinValue(0)] public int perception;

		[MinValue(0)] public int fatiguePoints;
		[ReadOnly] public int fatigueCurrentPoints;

		public void Reset()
		{
			healthPoints = 10;
			healthCurrentPoints = healthPoints;

			move = 5;
			speed = 5f;

			will = 10;
			willCurrentPoints = will;

			perception = 10;

			fatiguePoints = 10;
			fatigueCurrentPoints = fatiguePoints;
		}

		[Button]
		private void SpeedUp()
		{
			speed += 0.25f;
		}
		[Button]
		private void SpeedDown()
		{
			speed -= 0.25f;
		}
	}
}
[EnumPaging]
public enum STATS
{
	POINTS,
	STRENGTH,
	DEXTERITY,
	INTELLIGENCE,
	VITALITY,

	HP,
	MOVE,
	SPEED,
	WILL,
	PERCEPTION,
	FP,
}