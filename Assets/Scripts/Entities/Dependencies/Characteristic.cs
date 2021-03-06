using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Characteristic
{
	public virtual string CurrentStringValue { get; }

	public abstract float StatValue { set; get; }

	public UnityAction onValueChanged;
	protected void UpdateChraracteristic()
	{
		onValueChanged?.Invoke();
	}
}

public abstract class CharacteristicModifier : Characteristic
{
	public List<Bind> binds;

	public CharacteristicModifier()
	{
		binds = new List<Bind>();
	}

	public void AddBind(Bind bind)
	{
		if(!binds.Contains(bind))
		{
			binds.Add(bind);
			UpdateChraracteristic();
		}
	}

	public void RemoveRangeBind(List<Bind> binds)
	{
		for(int i = 0; i < binds.Count; i++)
		{
			RemoveBind(binds[i]);
		}
	}
	public void RemoveBind(Bind bind)
	{
		if(binds.Contains(bind))
		{
			binds.Remove(bind);
			UpdateChraracteristic();
		}
	}


	protected float GetModifierValue()
	{
		float modifierValue = 0;
		for(int i = 0; i < binds.Count; i++)
		{
			modifierValue += binds[i].Value;
		}	
		return modifierValue;
	}
}
public abstract class StatCharacteristic<T> : CharacteristicModifier where T : struct
{
	//StatCurrent = StatBaseValue + StatFormuleValue + StatModifierValue

	protected T statBaseValue;
	public abstract T StatBaseValue { get; set; }
	protected T statFormuleValue;
	public abstract T StatFormuleValue { get; set; }
	public abstract T StatModifieredValue { get; }
	
	public override string CurrentStringValue
	{
		get => StatValue.ToString();
	}

	public StatCharacteristic(T initValue)
	{
		StatBaseValue = initValue;
	}
}
public class StatCharacteristic : StatCharacteristic<float>
{
	private bool isRound = false;//округлять ли число до целого значения при выводе.

	public override float StatBaseValue
	{
		set
		{
			statBaseValue = Mathf.Max(0, value);

			UpdateChraracteristic();
		}
		get => statBaseValue;

	}
	public override float StatFormuleValue
	{
		set
		{
			statFormuleValue = value;

			UpdateChraracteristic();
		}
		get => statFormuleValue;
	}
	public override float StatModifieredValue { get => GetModifierValue(); }
	public override float StatValue 
	{
		set { }
		get => isRound == true ? (int)(StatBaseValue + StatFormuleValue + StatModifieredValue) : StatBaseValue + StatFormuleValue + StatModifieredValue; }

	public override string CurrentStringValue
	{
		get => StatValue.ToString() + " (" + StatBaseValue + "+" + StatFormuleValue + "+" + StatModifieredValue + ")";
	}

	public StatCharacteristic(float initValue, bool isRound = true) : base(initValue) { this.isRound = isRound; }

	public void ResetStat(float baseValue)
	{
		StatBaseValue = baseValue;
		StatFormuleValue = 0;
	}
}

public class CharacteristicValue : Characteristic
{
	protected bool isRound = false;

	public override string CurrentStringValue { get => StatValue.ToString(); }

	private float statValue;
	public override float StatValue
	{
		set
		{
			statValue = value;

			UpdateChraracteristic();
		}
		get => (isRound? (int) statValue : statValue);
	}

	public CharacteristicValue(float initValue, bool isRound = true)
	{
		this.isRound = isRound;
		StatValue = initValue;
	}
}

public class CharacteristicWeight : CharacteristicValue
{
	public Characteristic strength;

	public override string CurrentStringValue { get => StatCurrentValue + "/" + StatValue; }
	public override float StatValue { get => (int)(strength.StatValue * strength.StatValue); }//max

	private float statCurrentValue;
	public float StatCurrentValue
	{
		set
		{
			statCurrentValue = Mathf.Clamp(value, 0, StatValue);

			UpdateChraracteristic();
		}
		get => (int)statCurrentValue;
	}

	public CharacteristicWeight(float currentValue, Characteristic strength) : base(strength.StatValue * strength.StatValue, true)
	{
		this.strength = strength;
		this.strength.onValueChanged += UpdateChraracteristic;

		StatCurrentValue = currentValue;
	}
}

public class Bar
{
	protected bool isRound = false;

	public UnityAction onBarChanged;

	protected float currenValue;
	public virtual float CurrentValue
	{
		set
		{
			currenValue = Mathf.Clamp(value, 0, MaxValue);

			UpdateBar();
		}
		get => isRound ? (int)currenValue : currenValue;
	}

	protected float maxValue;
	public virtual float MaxValue
	{
		set
		{
			maxValue = Mathf.Max(0, value);

			UpdateBar();
		}
		get => isRound ? (int)maxValue : maxValue;
	}

	public Bar(float currentValue, float maxValue, bool isRound = true)
	{
		this.isRound = isRound;

		MaxValue = maxValue;
		CurrentValue = currentValue;
	}

	protected virtual void UpdateBar()
	{
		onBarChanged?.Invoke();
	}
}
public class BarPoints : Bar
{
	private Characteristic characteristic;

	public override float CurrentValue
	{
		set
		{
			currenValue = Mathf.Clamp((isRound ? (int)value : value), 0, MaxValue);

			UpdateBar();
		}
		get => base.CurrentValue;
	}
	public override float MaxValue
	{
		get => isRound ? (int)maxValue : maxValue;
	}

	public BarPoints(float currentValue, Characteristic characteristic, bool isRound = true) 
		: base(currentValue, characteristic.StatValue, isRound)
	{
		this.characteristic = characteristic;
		this.characteristic.onValueChanged += StatChanged;
	}

	private void StatChanged()
	{
		maxValue = characteristic.StatValue;
		CurrentValue = currenValue;
	}
}