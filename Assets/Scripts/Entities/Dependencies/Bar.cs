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

	public Bar()
	{
		//this.isRound = isRound;

		//MaxValue = maxValue;
		//CurrentValue = currentValue;
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

	public BarPoints(Stat stat)
	{
		characteristic = stat;
		characteristic.onValueChanged += StatChanged;
	}
	public BarPoints(float cur, float max) { }

	private void StatChanged()
	{
		maxValue = characteristic.StatValue;
		CurrentValue = currenValue;
	}
}