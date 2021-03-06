using UnityEngine;

[CreateAssetMenu(menuName ="RPG/Advantages/Careful")]
public class AdvantageCareful : Advantage
{
	public override void EnableAdvantage(EntityStatistics statistics)
	{
		base.EnableAdvantage(statistics);

		EntityStats stats = statistics.stats;

		binds.Add(new CharacteristicBind(this, stats.Dexterity, 3).AddBin());
	}
	public override string Value
	{
		get => base.Value + " brua";
	}
}