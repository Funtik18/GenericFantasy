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

	[TabGroup("Entity Advantages")]
	[HideLabel]
	public AdvantagesData advantagesData;

	public EntityStatisticsData(EntityStatisticsData data)
	{
		//копии данных
		statsData = new StatsData();
		statsData = data.statsData;

		advantagesData = new AdvantagesData();
		advantagesData = data.advantagesData;
	}

	public EntityStatisticsData(EntityStatistics statistics)
	{
		//statsData = statistics.stats.GetCurrentData();
		//abilitiesData = statistics.abilities.GetCurrentData();
	}
}