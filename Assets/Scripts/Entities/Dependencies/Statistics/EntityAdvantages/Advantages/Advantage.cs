using UnityEngine;

using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;

public abstract class Advantage : ScriptableObject, IModifier
{
    [Required]
    [HideLabel]
    [SerializeField]
    public InformationScriptableData data;

    [HideLabel]
    [TabGroup("Properties")]
    public AdvantageProperties properties;

    public Dictionary<string, List<CharacteristicBind>> whoContainAdvantage = new Dictionary<string, List<CharacteristicBind>>();

    public List<CharacteristicBind> binds;

	public virtual string Value { get => "Advantage <color=" + properties.AdvantageType + ">cost</color> " + properties.cost; }

	public virtual void EnableAdvantage(EntityStatistics targetStatistics)
	{
		if(whoContainAdvantage.ContainsKey(targetStatistics.id))
		{
            Debug.LogError("AdvantageError with " + this.name);
		}
		else
		{
            binds = new List<CharacteristicBind>();
            whoContainAdvantage.Add(targetStatistics.id, binds);
        }
    }
    public virtual void DisableAdvantage(EntityStatistics targetStatistics)
    {
        string id = targetStatistics.id;

        Bind[] binds = whoContainAdvantage[id].ToArray();

		foreach(CharacteristicBind bind in binds)
		{
            bind.characteristic.RemoveRangeBind(binds.ToList());
        }

        whoContainAdvantage.Remove(id);
    }

	[System.Serializable]
    public struct AdvantageProperties
	{
        [ColorPalette("Advantages")]
        public Color advantageType;
        public int cost;
        
        public string AdvantageType
		{
			get => "#" + ColorUtility.ToHtmlStringRGB(advantageType);
		}
    }
}