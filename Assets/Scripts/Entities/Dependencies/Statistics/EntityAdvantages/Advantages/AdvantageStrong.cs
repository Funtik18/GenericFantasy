﻿using UnityEngine;

[CreateAssetMenu(menuName = "RPG/Advantages/Strong")]
public class AdvantageStrong : Advantage
{
	public override void EnableAdvantage(EntityStatistics statistics)
	{
		base.EnableAdvantage(statistics);

		EntityStats stats = statistics.stats;

		binds.Add(new CharacteristicBind(this, stats.Strength, 3).AddBin());
	}
	public override string Value
	{
		get => base.Value + " brua";
	}
}