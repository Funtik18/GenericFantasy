public class EntityStatistics
{
	public readonly EntityStats stats;

	public EntityStatistics(EntityStatisticsData data)
	{
		stats = new EntityStats(data.statsData);
	}
}