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
	
	public StatValuePoints Points;

	public Bar EXP;

	public Bar HP;
	public Bar MP;
	public Bar FP;

	public CharacteristicWeight Weight;

	public Stat Strength;
	public Stat Dexterity;
	public Stat Intelligence;
	public Stat Vitality;

	public Stat Health;
	public Stat Move;
	public Stat Speed;
	public Stat Will;
	public Stat Perception;
	public Stat Fatigue;

	public StatCharacteristicDodge Dodge;


	public EntityDamages damages;


	//public StatCharacteristic StepsCellular;

	public EntityStats(StatsData statsData)
	{
		this.data = statsData;

		InitializeStats();
	}

	#region Behavior
	public void ResetStats()
	{
		//Strength;
		//Dexterity.ResetStat(data.statsPrimary.dexterity);
		//Intelligence.ResetStat(data.statsPrimary.intelligence);
		//Vitality.ResetStat(data.statsPrimary.vitality);

		//Health.ResetStat(data.statsSecondary.healthPoints);
		//Move.ResetStat(data.statsSecondary.move);
		//Speed.ResetStat(data.statsSecondary.speed);
		//Will.ResetStat(data.statsSecondary.will);
		//Perception.ResetStat(data.statsSecondary.perception);
		//Fatigue.ResetStat(data.statsSecondary.fatiguePoints);

		//Points.StatValue = totalPoints;
	}
	public void RndStats()
	{

	}

	public void IncreaseStrength()
	{
		Strength.IncreaseStat(Points);
	}
	public void DecreaseStrength()
	{
		Strength.DecreaseStat(Points);
	}

	public void IncreaseDexterity()
	{
		Dexterity.IncreaseStat(Points);

	}
	public void DecreaseDexterity()
	{
		Dexterity.DecreaseStat(Points);
	}

	public void IncreaseIntelligence()
	{
		Intelligence.IncreaseStat(Points);
	}
	public void DecreaseIntelligence()
	{
		Intelligence.DecreaseStat(Points);
	}

	public void IncreaseVitality()
	{
		Vitality.IncreaseStat(Points);
	}
	public void DecreaseVitality()
	{
		Vitality.DecreaseStat(Points);
	}

	public void IncreaseHealth()
	{
		Health.IncreaseStat(Points);
	}
	public void DecreaseHealth()
	{
		Health.DecreaseStat(Points);
	}

	public void IncreaseMove()
	{
		Move.IncreaseStat(Points);
	}
	public void DecreaseMove()
	{
		Move.DecreaseStat(Points);
	}

	public void IncreaseSpeed()
	{
		Speed.IncreaseStat(Points);
	}
	public void DecreaseSpeed()
	{
		Speed.DecreaseStat(Points);
	}

	public void IncreaseWill()
	{
		Will.IncreaseStat(Points);
	}
	public void DecreaseWill()
	{
		Will.DecreaseStat(Points);
	}

	public void IncreasePerception()
	{
		Perception.IncreaseStat(Points);
	}
	public void DecreasePerception()
	{
		Perception.DecreaseStat(Points);
	}

	public void IncreaseFatigue()
	{
		Fatigue.IncreaseStat(Points);
	}
	public void DecreaseFatigue()
	{
		Fatigue.DecreaseStat(Points);
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


		Points = new StatValuePoints(totalPoints);

		Strength = new StatCharacteristicStrength(this, data.statsPrimary.strength);
		Dexterity = new StatCharacteristicDexterity(this, data.statsPrimary.dexterity);
		Intelligence = new StatCharacteristicIntelligence(this, data.statsPrimary.intelligence);
		Vitality = new StatCharacteristicVitality(this, data.statsPrimary.vitality);

		Health = new StatCharacteristicHealth(this);
		Speed = new StatCharacteristicSpeed(this);
		Move = new StatCharacteristicMove(this);
		Will = new StatCharacteristicWill(this);
		Perception = new StatCharacteristicPerception(this);
		Fatigue = new StatCharacteristicFatigue(this);


		Weight = new CharacteristicWeight(data.currentWeight, Strength);

		Dodge = new StatCharacteristicDodge(this);

		EXP = new BarPoints(5, 10);

		HP = new BarPoints(Health);
		MP = new BarPoints(Will);
		FP = new BarPoints(Fatigue);
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

		statsAll.Add(Dodge);

		barsAll = new List<Bar>();
		barsAll.Add(EXP);
		barsAll.Add(HP);
		barsAll.Add(MP);
		barsAll.Add(FP);
	}
}

public class EntityDamages
{
	public DiceRoll damageThrust;
	public DiceRoll damageSwing;

	public EntityDamages()
	{
	
	}

	private string DamageThrust(float ST)
	{
		if(ST <= 1) return "1d-6";
		if(ST <= 2) return "1d-6";
		if(ST <= 3) return "1d-5";
		if(ST <= 4) return "1d-5";
		if(ST <= 5) return "1d-4";
		if(ST <= 6) return "1d-4";
		if(ST <= 7) return "1d-3";
		if(ST <= 8) return "1d-3";
		if(ST <= 9) return "1d-2";
		if(ST <= 10) return "1d-2";
		if(ST <= 11) return "1d-1";
		if(ST <= 12) return "1d-1";
		if(ST <= 13) return "1d";
		if(ST <= 14) return "1d";
		if(ST <= 15) return "1d+1";
		if(ST <= 16) return "1d+1";
		if(ST <= 17) return "1d+2";
		if(ST <= 18) return "1d+2";
		if(ST <= 19) return "2d-1";
		if(ST <= 20) return "2d-1";
		if(ST <= 21) return "2d";
		if(ST <= 22) return "2d";
		if(ST <= 23) return "2d+1";
		if(ST <= 24) return "2d+1";
		if(ST <= 25) return "2d+2";
		if(ST <= 26) return "2d+2";
		if(ST <= 27) return "3d-1";
		if(ST <= 28) return "3d-1";
		if(ST <= 29) return "3d";
		if(ST <= 30) return "3d";
		if(ST <= 31) return "3d+1";
		if(ST <= 32) return "3d+1";
		if(ST <= 33) return "3d+2";
		if(ST <= 34) return "3d+2";
		if(ST <= 35) return "4d-1";
		if(ST <= 36) return "4d-1";
		if(ST <= 37) return "4d";
		if(ST <= 38) return "4d";
		if(ST <= 39) return "4d+1";
		if(ST <= 40) return "4d+1";
		if(ST <= 45) return "5d";
		if(ST <= 50) return "5d+2";
		if(ST <= 55) return "6d";
		if(ST <= 60) return "7d-1";
		if(ST <= 65) return "7d+1";
		if(ST <= 70) return "8d";
		if(ST <= 75) return "8d+2";
		if(ST <= 80) return "9d";
		if(ST <= 85) return "9d+2";
		if(ST <= 90) return "10d";
		if(ST <= 95) return "10d+2";
		if(ST <= 100) return "11d";
		return ((Mathf.Floor((ST - 100) / 10) + 11) + "d");
	}

	private string DamageSwing(float ST)
	{
		if(ST <= 1) return "1d-5";
		if(ST <= 2) return "1d-5";
		if(ST <= 3) return "1d-4";
		if(ST <= 4) return "1d-4";
		if(ST <= 5) return "1d-3";
		if(ST <= 6) return "1d-3";
		if(ST <= 7) return "1d-2";
		if(ST <= 8) return "1d-2";
		if(ST <= 9) return "1d-1";
		if(ST <= 10) return "1d";
		if(ST <= 11) return "1d+1";
		if(ST <= 12) return "1d+2";
		if(ST <= 13) return "2d-1";
		if(ST <= 14) return "2d";
		if(ST <= 15) return "2d+1";
		if(ST <= 16) return "2d+2";
		if(ST <= 17) return "3d-1";
		if(ST <= 18) return "3d";
		if(ST <= 19) return "3d+1";
		if(ST <= 20) return "3d+2";
		if(ST <= 21) return "4d-1";
		if(ST <= 22) return "4d";
		if(ST <= 23) return "4d+1";
		if(ST <= 24) return "4d+2";
		if(ST <= 25) return "5d-1";
		if(ST <= 26) return "5d";
		if(ST <= 27) return "5d+1";
		if(ST <= 28) return "5d+1";
		if(ST <= 29) return "5d+2";
		if(ST <= 30) return "5d+2";
		if(ST <= 31) return "6d-1";
		if(ST <= 32) return "6d-1";
		if(ST <= 33) return "6d";
		if(ST <= 34) return "6d";
		if(ST <= 35) return "6d+1";
		if(ST <= 36) return "6d+1";
		if(ST <= 37) return "6d+2";
		if(ST <= 38) return "6d+2";
		if(ST <= 39) return "7d-1";
		if(ST <= 40) return "7d-1";
		if(ST <= 45) return "7d+1";
		if(ST <= 50) return "8d-1";
		if(ST <= 55) return "8d+1";
		if(ST <= 60) return "9d";
		if(ST <= 65) return "9d+2";
		if(ST <= 70) return "10d";
		if(ST <= 75) return "10d+2";
		if(ST <= 80) return "11d";
		if(ST <= 85) return "11d+2";
		if(ST <= 90) return "12d";
		if(ST <= 95) return "12d+2";
		if(ST <= 100) return "13d";
		return ((Mathf.Floor((ST - 100) / 10) + 13) + "d");
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

	[Button]
	private void Reset()
	{
		statsPrimary.Reset();
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