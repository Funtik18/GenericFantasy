using System;

public class EntityStatistics
{
	public readonly string id;

	public readonly EntityStats stats;

	public readonly EntityAdvantages advantages;

	public EntityStatistics(EntityStatisticsData data)
	{
		id = Guid.NewGuid().ToString();

		stats = new EntityStats(data.statsData);

		advantages = new EntityAdvantages(this, data.advantagesData);
	}
}