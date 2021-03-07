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

	public Stat(EntityStats stats, float initValue = 0, bool isRound = true)
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
	private Characteristic strength;

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
	private Characteristic speed;

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
	private Characteristic dexterity;
	private Characteristic vitality;

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
	private Characteristic intelligence;

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
	private Characteristic intelligence;

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
	private Characteristic vitality;

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

public class StatCharacteristicDodge : Stat
{
	private Characteristic speed;
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