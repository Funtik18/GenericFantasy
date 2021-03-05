using UnityEngine;

using Sirenix.OdinInspector;

public abstract class EntityScriptableData : ScriptableObject
{
	[HideLabel]
	public EntityStatisticsData data;
}
[System.Serializable]
public class EntityStatisticsData
{
	[TabGroup("Entity Stats")]
	[HideLabel]
	public StatsData statsData;

	public EntityStatisticsData(out EntityStatistics statistics, EntityStatisticsData data)
	{
		//копии данных
		statsData = new StatsData();
		statsData = data.statsData;

		statistics = new EntityStatistics(data);
	}


	public EntityStatisticsData(EntityStatistics statistics)
	{
		//statsData = statistics.stats.GetCurrentData();
		//abilitiesData = statistics.abilities.GetCurrentData();
	}
}