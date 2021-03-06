using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AdvantageUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMPro.TextMeshProUGUI advantageName;
    [SerializeField] protected TMPro.TextMeshProUGUI advantageCost;
    [SerializeField] private Button buttonDescription;

    protected Advantage advantage;
    protected Advantage.AdvantageProperties advantageProps;
    [HideInInspector] public InformationData data;

    public UnityAction<AdvantageUI> onShowDescription;

    protected virtual void Awake()
	{
        buttonDescription.onClick.AddListener(Click);
    }

    public void SetAdvantage(Advantage advantage)
    {
        this.advantage = advantage;
        advantageProps = advantage.properties;
        data = advantage.data.information;

        UpdateAdvantage();
    }

    protected virtual void UpdateAdvantage()
    {
        icon.sprite = data.icon;
        advantageName.text = "<color=" + advantageProps.AdvantageType + ">" + data.name + "</color>";
        advantageCost.text = "[" + advantageProps.cost + "]";
    }

    private void Click()
    {
        onShowDescription?.Invoke(this);
    }
}
