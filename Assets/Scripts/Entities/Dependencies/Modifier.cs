using Sirenix.OdinInspector;

[System.Serializable]
public class Modifier
{
	[HideLabel]
	public STATS targetKey = STATS.STRENGTH;
	[HideLabel]
	public ModifierType modifierType = ModifierType.Add;
	public float value;
}
[EnumToggleButtons]
public enum ModifierType
{
	Add,
	Multiply
}