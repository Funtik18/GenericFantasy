using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AdvantageRemoveUI : AdvantageUI
{
    [SerializeField] private Button buttonRemove;

    public UnityAction<Advantage> onRemove;

    protected override void Awake()
    {
        base.Awake();
        buttonRemove.onClick.AddListener(Remove);
    }
    private void Remove()
    {
        onRemove?.Invoke(advantage);
    }

    protected override void UpdateAdvantage()
    {
        base.UpdateAdvantage();
        advantageCost.text = "<color=" + advantageProps.AdvantageType + ">" + (advantageProps.cost >= 0 ? "+" + advantageProps.cost : advantageProps.cost.ToString()) + "</color>";
    }
}
