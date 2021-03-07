using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AttributeRemoveUI : AttributeUI
{
    [SerializeField] private Button buttonRemove;

    public UnityAction<Attribute> onRemove;

    protected override void Awake()
    {
        base.Awake();
        buttonRemove.onClick.AddListener(Remove);
    }
    private void Remove()
    {
        onRemove?.Invoke(attribute);
    }

    protected override void UpdateAttribute()
    {
        base.UpdateAttribute();
        //advantageCost.text = "<color=" + advantageProps.AdvantageType + ">" + (advantageProps.cost >= 0 ? "+" + advantageProps.cost : advantageProps.cost.ToString()) + "</color>";
    }
}
