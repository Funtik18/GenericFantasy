using UnityEngine;

[CreateAssetMenu(menuName = "RPG/Attributes/DisAdvantages/Careful", fileName = "DisAdvantageCareful")]
public class DisAdvantageCareful : DisAdvantage
{
	public override void Enable(EntityStatistics statistics)
	{
		statistics.stats.Dexterity.AddBind(this, 3);
	}
	public override void Disabe(EntityStatistics statistics)
	{
		statistics.stats.Dexterity.RemoveBind(this);
	}
}