using UnityEngine;
using UnityEngine.UI;

public class BarUI : MonoBehaviour
{
    [SerializeField] private Image currentAmount;
	[SerializeField] private TMPro.TextMeshProUGUI text;

	protected Bar bar;

	public float FillAmount
	{
		set => currentAmount.fillAmount = value;
		get => currentAmount.fillAmount;
	}

    public void SetBar(Bar bar)
	{
		this.bar = bar;
		this.bar.onBarChanged += UpdateBar;

		UpdateBar();
	}
	public void UpdateBar()
	{
		FillAmount = bar.CurrentValue / bar.MaxValue;
		text.text = bar.CurrentValue + " / " + bar.MaxValue;
	}
}