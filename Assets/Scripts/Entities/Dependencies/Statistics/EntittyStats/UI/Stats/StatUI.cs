using UnityEngine;

public class StatUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI statName;
    [SerializeField] private TMPro.TextMeshProUGUI statValue;

    private InformationData data;

    protected Characteristic stat;

    public void SetStat(InformationData statData, Characteristic stat)
    {
        data = statData;

        this.stat = stat;

        this.stat.onValueChanged += UpdateStat;

        UpdateStat();
    }

    private void UpdateStat()
    {
        statName.text = data.name;
        statValue.text = stat.CurrentStringValue;
    }
}