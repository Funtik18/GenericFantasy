using UnityEngine;

using Sirenix.OdinInspector;

public abstract class Advantage : MonoBehaviour
{
    [Required]
    [HideLabel]
    public AdvantageData data;
}
[System.Serializable]
public class AdvantageData
{
    [ColorPalette("Advantages")]
    public Color advantageType;
    public int cost;

    [HideLabel]
    public InformationScriptableData information;
}