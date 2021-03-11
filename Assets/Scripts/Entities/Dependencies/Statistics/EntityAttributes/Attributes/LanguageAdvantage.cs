using UnityEngine;

[CreateAssetMenu(menuName = "RPG/Attributes/Languages", fileName = "Language")]
public abstract class LanguageAdvantage : Advantage
{
	public Linguistic linguistic;
}
public enum Linguistic
{
	None,
	Broken,
	Accented,
	Native,
}