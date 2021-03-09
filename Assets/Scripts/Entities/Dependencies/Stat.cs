using UnityEngine;

public abstract class Stat : CharacteristicModifier
{
	//StatValue = StatBaseValue + StatModifieredValue + StatPointValue


	private float statBaseValue;
	public virtual float StatBaseValue
	{
		protected set
		{
			statBaseValue = value;

			UpdateChraracteristic();
		}
		get => statBaseValue;
	}

	public virtual float StatModifieredValue { get => GetModifierValue(); }


	private float statPointValue;
	public virtual float StatPointValue
	{
		protected set
		{
			statPointValue = value;

			UpdateChraracteristic();
		}
		get => statPointValue;
	}

	public float StatBasePointValue { get => StatBaseValue + StatPointValue; }

	/// <summary>
	/// Значение стата целиком.
	/// </summary>
	public override float StatValue
	{
		protected set { }
		get => isRound == true ? (int)(StatBaseValue + StatModifieredValue + StatPointValue) : StatBaseValue + StatModifieredValue + StatPointValue;
	}
	public override string CurrentStringValue { get => StatValue + "(" + StatBaseValue + "+" + StatModifieredValue + "+" + StatPointValue + ")"; }

	public Stat(EntityStats stats, float initValue = 0, bool isRound = true) : base(initValue, isRound)
	{
		this.isRound = isRound;
		StatBaseValue = initValue;
	}

	public virtual void IncreaseStat(StatValuePoints points)
	{
		StatPointValue++;
	}
	public virtual void DecreaseStat(StatValuePoints points)
	{
		StatPointValue--;
	}
}

#region StatsImplementation
public class StatCharacteristicStrength : Stat
{
	public StatCharacteristicStrength(EntityStats stats, float initValue) : base(stats, initValue) { }

	public override void IncreaseStat(StatValuePoints points)
	{
		if(points.StatValue - 10 >= 0)
		{
			base.IncreaseStat(points);
			points.SetValue(-10);
		}
	}
	public override void DecreaseStat(StatValuePoints points)
	{
		if(StatBasePointValue - 1 >= 5)
		{
			base.DecreaseStat(points);
			points.SetValue(10);
		}
	}
}
public class StatCharacteristicDexterity : Stat
{
	public StatCharacteristicDexterity(EntityStats stats, float initValue) : base(stats, initValue) { }

	public override void IncreaseStat(StatValuePoints points)
	{
		if(points.StatValue - 20 >= 0)
		{
			base.IncreaseStat(points);
			points.SetValue(-20);
		}
	}
	public override void DecreaseStat(StatValuePoints points)
	{
		if(StatBasePointValue - 1 >= 5)
		{
			base.DecreaseStat(points);
			points.SetValue(20);
		}
	}
}
public class StatCharacteristicIntelligence : Stat
{
	public StatCharacteristicIntelligence(EntityStats stats, float initValue) : base(stats, initValue) { }

	public override void IncreaseStat(StatValuePoints points)
	{
		if(points.StatValue - 20 >= 0)
		{
			base.IncreaseStat(points);
			points.SetValue(-20);
		}
	}
	public override void DecreaseStat(StatValuePoints points)
	{
		if(StatBasePointValue - 1 >= 5)
		{
			base.DecreaseStat(points);
			points.SetValue(20);
		}
	}
}
public class StatCharacteristicVitality : Stat
{
	public StatCharacteristicVitality(EntityStats stats, float initValue) : base(stats, initValue) { }

	public override void IncreaseStat(StatValuePoints points)
	{
		if(points.StatValue - 10 >= 0)
		{
			base.IncreaseStat(points);
			points.SetValue(-10);
		}
	}
	public override void DecreaseStat(StatValuePoints points)
	{
		if(StatBasePointValue - 1 >= 5)
		{
			base.DecreaseStat(points);
			points.SetValue(10);
		}
	}
}

public class StatCharacteristicHealth : Stat
{
	private CharacteristicValue strength;

	public override float StatBaseValue
	{
		get => strength.StatValue;
	}

	public StatCharacteristicHealth(EntityStats stats) : base(stats)
	{
		strength = stats.Strength;
		strength.onValueChanged += UpdateChraracteristic;
	}

	public override void IncreaseStat(StatValuePoints points)
	{
		if(points.StatValue - 2 >= 0)
		{
			if((StatValue + 1) <= (strength.StatValue * 0.3f) + strength.StatValue)
			{
				base.IncreaseStat(points);
				points.SetValue(-2);
			}
		}
	}
	public override void DecreaseStat(StatValuePoints points)
	{
		if(StatBasePointValue - 1 >= 0)
		{
			if((strength.StatValue * 0.7f) <= StatValue - 1)
			{
				base.DecreaseStat(points);
				points.SetValue(2);
			}
		}
	}
}
public class StatCharacteristicMove : Stat
{
	private CharacteristicValue speed;

	public override float StatBaseValue
	{
		get => speed.StatValue;
	}

	public StatCharacteristicMove(EntityStats stats) : base(stats)
	{
		speed = stats.Speed;
		speed.onValueChanged += UpdateChraracteristic;
	}

	public override void IncreaseStat(StatValuePoints points)
	{
		if(points.StatValue - 5 >= 0)
		{
			base.IncreaseStat(points);
			points.SetValue(-5);
		}
	}
	public override void DecreaseStat(StatValuePoints points)
	{
		if(StatBasePointValue - 1 >= 0)
		{
			base.DecreaseStat(points);
			points.SetValue(5);
		}
	}
}
public class StatCharacteristicSpeed : Stat
{
	private CharacteristicValue dexterity;
	private CharacteristicValue vitality;

	public override float StatBaseValue
	{
		get => (dexterity.StatValue + vitality.StatValue) / 4;
	}

	public StatCharacteristicSpeed(EntityStats stats) : base(stats, isRound: false)
	{
		dexterity = stats.Dexterity;
		vitality = stats.Vitality;

		dexterity.onValueChanged += UpdateChraracteristic;
		vitality.onValueChanged += UpdateChraracteristic;
	}

	public override void IncreaseStat(StatValuePoints points)
	{
		if(points.StatValue - 5 >= 0)
		{
			if(StatBasePointValue + 0.25f <= 7)
			{
				StatPointValue += 0.25f;
				points.SetValue(-5);
			}
		}
	}
	public override void DecreaseStat(StatValuePoints points)
	{
		if(StatBasePointValue - 0.25f >= 3)
		{
			StatPointValue -= 0.25f;
			points.SetValue(5);
		}
	}
}
public class StatCharacteristicWill : Stat
{
	private CharacteristicValue intelligence;

	public override float StatBaseValue
	{
		get => intelligence.StatValue;
	}

	public StatCharacteristicWill(EntityStats stats) : base(stats)
	{
		intelligence = stats.Intelligence;
		intelligence.onValueChanged += UpdateChraracteristic;
	}

	public override void IncreaseStat(StatValuePoints points)
	{
		if(points.StatValue - 5 >= 0)
		{
			if(StatBasePointValue + 1 <= 20)
			{
				base.IncreaseStat(points);
				points.SetValue(-5);
			}
		}
	}
	public override void DecreaseStat(StatValuePoints points)
	{
		if(4 <= StatBasePointValue - 1)
		{
			base.DecreaseStat(points);
			points.SetValue(5);
		}
	}
}
public class StatCharacteristicPerception : Stat
{
	private CharacteristicValue intelligence;

	public override float StatBaseValue
	{
		get => intelligence.StatValue;
	}

	public StatCharacteristicPerception(EntityStats stats) : base(stats)
	{
		intelligence = stats.Intelligence;
		intelligence.onValueChanged += UpdateChraracteristic;
	}

	public override void IncreaseStat(StatValuePoints points)
	{
		if(points.StatValue - 5 >= 0)
		{
			if(StatBasePointValue + 1 <= 20)
			{
				base.IncreaseStat(points);
				points.SetValue(-5);
			}
		}
	}
	public override void DecreaseStat(StatValuePoints points)
	{
		if(4 <= StatBasePointValue - 1)
		{
			base.DecreaseStat(points);
			points.SetValue(5);
		}
	}
}
public class StatCharacteristicFatigue : Stat
{
	private CharacteristicValue vitality;

	public override float StatBaseValue
	{
		get => vitality.StatValue;
	}

	public StatCharacteristicFatigue(EntityStats stats) : base(stats)
	{
		vitality = stats.Vitality;
		vitality.onValueChanged += UpdateChraracteristic;
	}

	public override void IncreaseStat(StatValuePoints points)
	{
		if(points.StatValue - 3 >= 0)
		{
			if((StatValue + 1) <= (vitality.StatValue * 0.3f) + vitality.StatValue)
			{
				base.IncreaseStat(points);
				points.SetValue(-3);
			}
		}
	}
	public override void DecreaseStat(StatValuePoints points)
	{
		if((StatBasePointValue - 1) >= 0)
		{
			if((vitality.StatValue * 0.7f) <= (StatValue - 1))
			{
				base.DecreaseStat(points);
				points.SetValue(3);
			}
		}
	}
}


public class StatCharacteristicFear: Stat
{
	public StatCharacteristicFear(EntityStats stats) : base(stats)
	{
	}
}
public class StatCharacteristicTouch : Stat
{
	public StatCharacteristicTouch(EntityStats stats) : base(stats)
	{
	}
}
public class StatCharacteristicTaste : Stat
{
	public StatCharacteristicTaste(EntityStats stats) : base(stats)
	{
	}
}
public class StatCharacteristicSmell : Stat
{
	public StatCharacteristicSmell(EntityStats stats) : base(stats)
	{
	}
}
public class StatCharacteristicHear : Stat
{
	public StatCharacteristicHear(EntityStats stats) : base(stats)
	{
	}
}
public class StatCharacteristicVision : Stat
{
	public StatCharacteristicVision(EntityStats stats) : base(stats)
	{
	}
}


public class StatCharacteristicDamageThrust : Characteristic
{
	private CharacteristicValue strength;

	private DiceFormule diceFormule;
	public DiceFormule DiceFormule
	{
		get => diceFormule;
	}

	public int Roll => DiceFormule.Roll();

	public override string CurrentStringValue => DiceFormule.ToString();

	public StatCharacteristicDamageThrust(EntityStats stats)
	{
		strength = stats.Strength;
		strength.onValueChanged += UpdateChraracteristic;
	}

	protected override void UpdateChraracteristic()
	{
		UpdateFormule();
		base.UpdateChraracteristic();
	}

	private void UpdateFormule()
	{
		float st = strength.StatValue;

		if(st <= 2) diceFormule = new DiceFormule(1, DiceType.Cube, -6);
		else if(st <= 4) diceFormule = new DiceFormule(1, DiceType.Cube, -5);
		else if(st <= 6) diceFormule = new DiceFormule(1, DiceType.Cube, -4);
		else if(st <= 8) diceFormule = new DiceFormule(1, DiceType.Cube, -3);
		else if(st <= 10) diceFormule = new DiceFormule(1, DiceType.Cube, -2);
		else if(st <= 12) diceFormule = new DiceFormule(1, DiceType.Cube, -1);
		else if(st <= 14) diceFormule = new DiceFormule(1, DiceType.Cube, 0);
		else if(st <= 16) diceFormule = new DiceFormule(1, DiceType.Cube, 1);
		else if(st <= 18) diceFormule = new DiceFormule(1, DiceType.Cube, 2);
		else if(st <= 20) diceFormule = new DiceFormule(2, DiceType.Cube, -1);
		else if(st <= 22) diceFormule = new DiceFormule(2, DiceType.Cube, 0);
		else if(st <= 24) diceFormule = new DiceFormule(2, DiceType.Cube, 1);
		else if(st <= 26) diceFormule = new DiceFormule(2, DiceType.Cube, 2);
		else if(st <= 28) diceFormule = new DiceFormule(3, DiceType.Cube, -1);
		else if(st <= 30) diceFormule = new DiceFormule(3, DiceType.Cube, 0);
		else if(st <= 32) diceFormule = new DiceFormule(3, DiceType.Cube, 1);
		else if(st <= 34) diceFormule = new DiceFormule(3, DiceType.Cube, 2);
		else if(st <= 36) diceFormule = new DiceFormule(4, DiceType.Cube, -1);
		else if(st <= 38) diceFormule = new DiceFormule(4, DiceType.Cube, 0);
		else if(st <= 40) diceFormule = new DiceFormule(4, DiceType.Cube, 1);
		else if(st <= 45) diceFormule = new DiceFormule(5, DiceType.Cube, 0);
		else if(st <= 50) diceFormule = new DiceFormule(5, DiceType.Cube, 2);
		else if(st <= 55) diceFormule = new DiceFormule(6, DiceType.Cube, 0);
		else if(st <= 60) diceFormule = new DiceFormule(7, DiceType.Cube, -1);
		else if(st <= 65) diceFormule = new DiceFormule(7, DiceType.Cube, 1);
		else if(st <= 70) diceFormule = new DiceFormule(8, DiceType.Cube, 0);
		else if(st <= 75) diceFormule = new DiceFormule(8, DiceType.Cube, 2);
		else if(st <= 80) diceFormule = new DiceFormule(9, DiceType.Cube, 0);
		else if(st <= 85) diceFormule = new DiceFormule(9, DiceType.Cube, 2);
		else if(st <= 90) diceFormule = new DiceFormule(10, DiceType.Cube, 0);
		else if(st <= 95) diceFormule = new DiceFormule(10, DiceType.Cube, 2);
		else if(st <= 100) diceFormule = new DiceFormule(11, DiceType.Cube, 0);
		else diceFormule = new DiceFormule((int)((Mathf.Floor(st - 100) / 10) + 11), DiceType.Cube, 0);
	}

	//private string DamageThrust(float ST)
	//{
	//	if(ST <= 1) return "1d-6";
	//	if(ST <= 2) return "1d-6";
	//	if(ST <= 3) return "1d-5";
	//	if(ST <= 4) return "1d-5";
	//	if(ST <= 5) return "1d-4";
	//	if(ST <= 6) return "1d-4";
	//	if(ST <= 7) return "1d-3";
	//	if(ST <= 8) return "1d-3";
	//	if(ST <= 9) return "1d-2";
	//	if(ST <= 10) return "1d-2";
	//	if(ST <= 11) return "1d-1";
	//	if(ST <= 12) return "1d-1";
	//	if(ST <= 13) return "1d";
	//	if(ST <= 14) return "1d";
	//	if(ST <= 15) return "1d+1";
	//	if(ST <= 16) return "1d+1";
	//	if(ST <= 17) return "1d+2";
	//	if(ST <= 18) return "1d+2";
	//	if(ST <= 19) return "2d-1";
	//	if(ST <= 20) return "2d-1";
	//	if(ST <= 21) return "2d";
	//	if(ST <= 22) return "2d";
	//	if(ST <= 23) return "2d+1";
	//	if(ST <= 24) return "2d+1";
	//	if(ST <= 25) return "2d+2";
	//	if(ST <= 26) return "2d+2";
	//	if(ST <= 27) return "3d-1";
	//	if(ST <= 28) return "3d-1";
	//	if(ST <= 29) return "3d";
	//	if(ST <= 30) return "3d";
	//	if(ST <= 31) return "3d+1";
	//	if(ST <= 32) return "3d+1";
	//	if(ST <= 33) return "3d+2";
	//	if(ST <= 34) return "3d+2";
	//	if(ST <= 35) return "4d-1";
	//	if(ST <= 36) return "4d-1";
	//	if(ST <= 37) return "4d";
	//	if(ST <= 38) return "4d";
	//	if(ST <= 39) return "4d+1";
	//	if(ST <= 40) return "4d+1";
	//	if(ST <= 45) return "5d";
	//	if(ST <= 50) return "5d+2";
	//	if(ST <= 55) return "6d";
	//	if(ST <= 60) return "7d-1";
	//	if(ST <= 65) return "7d+1";
	//	if(ST <= 70) return "8d";
	//	if(ST <= 75) return "8d+2";
	//	if(ST <= 80) return "9d";
	//	if(ST <= 85) return "9d+2";
	//	if(ST <= 90) return "10d";
	//	if(ST <= 95) return "10d+2";
	//	if(ST <= 100) return "11d";
	//	return ((Mathf.Floor((ST - 100) / 10) + 11) + "d");
	//}
}
public class StatCharacteristicDamageSwing : Stat
{
	public StatCharacteristicDamageSwing(EntityStats stats) : base(stats)
	{
	}
}
public class StatCharacteristicDamageFist : Stat
{
	public StatCharacteristicDamageFist(EntityStats stats) : base(stats)
	{
	}
}
public class StatCharacteristicDamageKick : Stat
{
	public StatCharacteristicDamageKick(EntityStats stats) : base(stats)
	{
	}
}
public class StatCharacteristicDodge : Stat
{
	private CharacteristicValue speed;
	private CharacteristicWeight weight;

	public override float StatBaseValue
	{
		get => Mathf.Max(1, speed.StatValue + 3 + weight.Penalty);
	}

	public StatCharacteristicDodge(EntityStats stats) : base(stats)
	{
		speed = stats.Speed;
		weight = stats.Weight;

		speed.onValueChanged += UpdateChraracteristic;
		weight.onValueChanged += UpdateChraracteristic;
	}
}
#endregion