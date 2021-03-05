using UnityEngine;

using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "RPG/Data/BaseInformationData", fileName = "InformationData")]
public class InformationScriptableData : ScriptableObject
{
	[HideLabel]
	public InformationData information;
}
[System.Serializable]
public class InformationData
{
	[TitleGroup("Information")]
	public string name;

	[Required]
	[AssetsOnly]
	[PreviewField(Alignment = ObjectFieldAlignment.Left)]
	public Sprite icon;

	[TabGroup("GroupStrings", "Description")]
	[HideLabel] [Multiline(5)] public string description;
	[TabGroup("GroupStrings", "Anotation")]
	[HideLabel] [Multiline(3)] public string annotation;
}