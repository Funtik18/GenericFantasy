using UnityEngine;
using UnityEngine.UI;

public class BarPointsUI : BarUI
{
	[SerializeField] private Image currentAmount;
	public float FillAmount
	{
		set => currentAmount.fillAmount = value;
		get => currentAmount.fillAmount;
	}

	public override void UpdateBar()
	{
		base.UpdateBar();
		FillAmount = bar.CurrentValue / bar.MaxValue;
	}
}