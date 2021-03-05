using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Characteristic
{
	public virtual string CurrentStringValue { get; }

	public UnityAction onValueChanged;
	protected void UpdateChraracteristic()
	{
		onValueChanged?.Invoke();
	}
}

public abstract class CharacteristicModifier : Characteristic, IModifiable
{
	protected List<Modifier> statModifiers;

	public CharacteristicModifier()
	{
		statModifiers = new List<Modifier>();
	}

	public void AddModifier(Modifier addModifier)
	{
		if(!statModifiers.Contains(addModifier))
		{
			statModifiers.Add(addModifier);

			UpdateChraracteristic();
		}
	}
	public void RemoveModifier(Modifier addModifier)
	{
		if(statModifiers.Contains(addModifier))
		{
			statModifiers.Remove(addModifier);

			UpdateChraracteristic();
		}
	}

	protected float GetModifierValue()
	{
		float modifierValue = 0;
		for(int i = 0; i < statModifiers.Count; i++)
		{
			if(statModifiers[i].modifierType == ModifierType.Add)
			{
				modifierValue += statModifiers[i].value;
			}
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
	public abstract T StatCurrentValue { get; }

	public override string CurrentStringValue
	{
		get => StatCurrentValue.ToString();
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
	public override float StatCurrentValue 
	{ get => isRound == true ? (int)(StatBaseValue + StatFormuleValue + StatModifieredValue) : StatBaseValue + StatFormuleValue + StatModifieredValue; }

	public override string CurrentStringValue
	{
		get => StatCurrentValue.ToString() + " (" + StatBaseValue + "+" + StatFormuleValue + "+" + StatModifieredValue + ")";
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

	public override string CurrentStringValue
	{
		get => (isRound ? (int)value : value).ToString();
	}

	private float value;
	public float Value
	{
		set
		{
			this.value = value;
			UpdateChraracteristic();
		}
		get => isRound ? (int)value : value;
	}
	public CharacteristicValue(float initValue, bool isRound = true)
	{
		this.isRound = isRound;
		Value = initValue;
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
	private StatCharacteristic stat;

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


	public BarPoints(float currentValue, StatCharacteristic stat, bool isRound = true) 
		: base(currentValue, stat.StatCurrentValue, isRound)
	{
		this.stat = stat;
		this.stat.onValueChanged += StatChanged;
	}

	private void StatChanged()
	{
		maxValue = stat.StatCurrentValue;
		CurrentValue = currenValue;
	}
}




//public class StatBarEXP : Characteristic
//{
//	public UnityAction onLevelUp;

//	public override string CurrentStringValue { get => CurrentValue + "/" + MaxValue; }

//	private uint currenValue;
//	public uint CurrentValue
//	{
//		set
//		{
//			if(value >= MaxValue)
//			{
//				uint diff = value - MaxValue;

//				onLevelUp?.Invoke();

//				currenValue = diff;
//			}
//			else
//			{
//				currenValue = value;
//			}

//			UpdateChraracteristic();
//		}
//		get => currenValue;
//	}


//	private uint maxValue;
//	public uint MaxValue
//	{
//		set
//		{
//			maxValue = value;
//			UpdateChraracteristic();
//		}
//		get => maxValue;
//	}

//	public StatBarEXP(uint currentExp, uint maxExp)
//	{
//		MaxValue = maxExp;
//		CurrentValue = currentExp;
//	}
//}