using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AttributeUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMPro.TextMeshProUGUI attributeName;
    [SerializeField] protected TMPro.TextMeshProUGUI AttributeCost;
    [SerializeField] private Button buttonDescription;

    protected Attribute attribute;
    [HideInInspector] public InformationData data;

    public UnityAction<AttributeUI> onShowDescription;

    protected virtual void Awake()
	{
        buttonDescription.onClick.AddListener(Click);
    }

    public void SetAttribute(Attribute attribute)
    {
        this.attribute = attribute;

        data = attribute.data.information;

        UpdateAttribute();
    }

    protected virtual void UpdateAttribute()
    {
        icon.sprite = data.icon;
        attributeName.text = "<color=white" + ">" + data.name + "</color>";
        AttributeCost.text = "[" + (attribute is DisAdvantage? -attribute.baseCost : attribute.baseCost) + "]";
    }

    private void Click()
    {
        onShowDescription?.Invoke(this);
    }
}