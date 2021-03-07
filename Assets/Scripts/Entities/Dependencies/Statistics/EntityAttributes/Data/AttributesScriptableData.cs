using UnityEngine;

using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "RPG/Data/AttributesScriptableData")]
public class AttributesScriptableData : ScriptableObject
{
	//[ColorPalette("Advantages")]
	//public Color color;

	[AssetList(AutoPopulate = true)]
	public Attribute[] attributesAll;

	[AssetList(AutoPopulate = true)]
	public Advantage[] advantagesAll;

	[AssetList(AutoPopulate = true)]
	public DisAdvantage[] disAdvantagesAll;

	[AssetList(AutoPopulate = true)]
	public Language[] languagesAll;

	[AssetList(AutoPopulate = true)]
	public Perk[] perksAll;

	[AssetList(AutoPopulate = true)]
	public Quirk[] quirksAll;

	[AssetList(AutoPopulate = true)]
	public TieredTalent[] TalentsAll;


	//public string AdvantageType
	//{
	//	get => "#" + ColorUtility.ToHtmlStringRGB(color);
	//}
}