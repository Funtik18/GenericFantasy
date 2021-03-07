using UnityEngine;

[CreateAssetMenu(menuName = "RPG/Attributes/Languages", fileName = "Language")]
public abstract class Language : Advantage
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