using UnityEngine;
using UnityEngine.Events;

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

	public Bar(bool isRound = true)
	{
		this.isRound = isRound;
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
			currenValue = Mathf.Clamp(value, 0, MaxValue);

			UpdateBar();
		}
		get => base.CurrentValue;
	}
	public override float MaxValue
	{
		get => characteristic.StatValue;
	}

	public BarPoints(float currentValue, Stat stat)
	{
		characteristic = stat;
		characteristic.onValueChanged += StatChanged;

		CurrentValue = currentValue;

		UpdateBar();
	}

	private void StatChanged()
	{
		maxValue = characteristic.StatValue;
		CurrentValue = currenValue;
	}
}

public class BarWeightPoints : Bar
{
	CharacteristicWeight weight;

	public BarWeightPoints(float currentWeight, CharacteristicWeight weight) : base(false)
	{
		this.weight = weight;
		this.weight = weight;
	}
}


public class BarExPoints : Bar
{
	public BarExPoints(float cur, float max)
	{
	}
}