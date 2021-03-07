using UnityEngine;

[CreateAssetMenu(menuName = "RPG/Attributes/Advantages/Strong", fileName = "AdvantageStrong")]
public class AdvantageStrong : Advantage
{
	public override void Enable(EntityStatistics statistics)
	{
		statistics.stats.Strength.AddBind(this, 3);
	}
	public override void Disabe(EntityStatistics statistics)
	{
		statistics.stats.Strength.RemoveBind(this);
	}
}