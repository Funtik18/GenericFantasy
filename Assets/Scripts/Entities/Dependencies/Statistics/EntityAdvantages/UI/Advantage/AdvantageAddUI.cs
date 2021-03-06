using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AdvantageAddUI : AdvantageUI
{
    [SerializeField] private Button buttonAdd;

    public UnityAction<Advantage> onAdd;

    protected override void Awake()
	{
        base.Awake();
        buttonAdd.onClick.AddListener(Add);
    }
    private void Add()
	{
        onAdd?.Invoke(advantage);
    }

    protected override void UpdateAdvantage()
    {
        base.UpdateAdvantage();
        advantageCost.text = "<color=" + advantageProps.AdvantageType + ">" + (advantageProps.cost >= 0 ? "+" + advantageProps.cost : advantageProps.cost.ToString()) + "</color>";
    }
}