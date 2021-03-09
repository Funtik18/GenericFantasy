using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AttributeAddUI : AttributeUI
{
    [SerializeField] private Button buttonAdd;

    public UnityAction<Attribute> onAdd;

    protected override void Awake()
	{
        base.Awake();
        buttonAdd.onClick.AddListener(Add);
    }
    private void Add()
	{
        onAdd?.Invoke(attribute);
    }

    protected override void UpdateAttribute()
    {
        base.UpdateAttribute();
        //advantageCost.text = "<color=" + advantageProps.AdvantageType + ">" + (advantageProps.cost >= 0 ? "+" + advantageProps.cost : advantageProps.cost.ToString()) + "</color>";
    }
}