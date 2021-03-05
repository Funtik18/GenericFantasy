using UnityEngine;

public class BarUI : MonoBehaviour
{
	[SerializeField] private TMPro.TextMeshProUGUI text;

	protected Bar bar;

	public void SetBar(Bar bar)
	{
		this.bar = bar;
		this.bar.onBarChanged += UpdateBar;

		UpdateBar();
	}
	public virtual void UpdateBar()
	{
		text.text = bar.CurrentValue + " / " + bar.MaxValue;
	}
}
