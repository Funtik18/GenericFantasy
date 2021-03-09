using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Characteristic
{
	/// <summary>
	/// Округлять ли число до целого значения при выводе.
	/// </summary>
	protected bool isRound = false;

	public abstract string CurrentStringValue { get; }
	public abstract float StatValue { protected set; get; }

	public UnityAction onValueChanged;
	protected virtual void UpdateChraracteristic()
	{
		onValueChanged?.Invoke();
	}
}

public abstract class CharacteristicModifier : Characteristic
{
	public List<CharacteristicBind> binds;

	public CharacteristicModifier()
	{
		binds = new List<CharacteristicBind>();
	}

	public void AddBind(Attribute attribute, float value)
	{
		if(!ContainAttribute(attribute))
		{
			binds.Add(new CharacteristicBind(attribute, this, value));

			UpdateChraracteristic();
		}
	}
	public void RemoveBind(Attribute attribute)
	{
		CharacteristicBind bind;
		if(ContainAttribute(attribute, out bind))
		{
			binds.Remove(bind);
			UpdateChraracteristic();
		}
	}

	private bool ContainAttribute(Attribute attribute, out CharacteristicBind bind)
	{
		bind = null;
		for(int i = 0; i < binds.Count; i++)
		{
			if(binds[i].attribute == attribute)
			{
				bind = binds[i];
				return true;
			}
		}
		return false;
	}
	private bool ContainAttribute(Attribute attribute)
	{
		for(int i = 0; i < binds.Count; i++)
		{
			if(binds[i].attribute == attribute)
			{
				return true;
			}
		}
		return false;
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

public class CharacteristicValue : Characteristic
{
	protected bool isRound = false;

	public override string CurrentStringValue { get => StatValue.ToString(); }

	private float statValue;
	public override float StatValue
	{
		protected set
		{
			statValue = value;

			UpdateChraracteristic();
		}
		get => (isRound ? (int)statValue : statValue);
	}



	/// <summary>
	/// Характеристика, не модифицируемая.
	/// </summary>
	public CharacteristicValue(float initValue, bool isRound = true)
	{
		this.isRound = isRound;
		StatValue = initValue;
	}
}
public class StatValuePoints : CharacteristicValue
{
	public void SetValue(float value)
	{
		StatValue += value;
	}

	public StatValuePoints(float initValue) : base(initValue, true)
	{
	}
}

public class CharacteristicWeight : CharacteristicValue
{
	//http://mentor.gurps.ru/books/basic_set_4ed_rus.pdf#zoom=80&page=15
	protected Characteristic strength;

	public UnityAction<int> onWeightLevelChanged;

	private bool isPound = true;

	public override string CurrentStringValue { get => StatCurrentValue + "/" + StatValue + (isPound?"lbs":"kg"); }

	public override float StatValue 
	{ 
		get
		{
			float value = (strength.StatValue * strength.StatValue) / 5;
			if(value > 10) value = Mathf.Round(value);
			return value;
		}
	}

	private float statCurrentValue;
	public float StatCurrentValue
	{
		set
		{
			statCurrentValue = Mathf.Max(value, 0);
			
			UpdateChraracteristic();
		}
		get => statCurrentValue;
	}

	private int weightLevel = 0;
	public int WeightLevel
	{
		get => weightLevel;
	}


	/// <summary>
	/// Штраф к Уклонению.
	/// </summary>
	public int Penalty
	{
		get => -WeightLevel;
	}

	public CharacteristicWeight(float currentValue, Characteristic strength) : base(0, true)
	{
		this.strength = strength;
		this.strength.onValueChanged += UpdateChraracteristic;

		onValueChanged += UpdateWeightLevel;

		StatCurrentValue = currentValue;
	}

	private void UpdateWeightLevel()
	{
		int newWeightLevel = CalculateWeightLevel();

		if(WeightLevel != newWeightLevel)
		{
			weightLevel = newWeightLevel;
			onWeightLevelChanged?.Invoke(WeightLevel);
		}
	}

	private int CalculateWeightLevel()
	{
		float maxWeight = StatValue;
		float currentWeight = StatCurrentValue;
		if(currentWeight < maxWeight)
		{
			return 0;
		}
		else if(currentWeight < 2 * maxWeight)
		{
			return 1;
		}
		else if(currentWeight < 3 * maxWeight)
		{
			return 2;
		}
		else if(currentWeight < 6 * maxWeight)
		{
			return 3;
		}
		else
		{
			return 4;
		}
	}
}