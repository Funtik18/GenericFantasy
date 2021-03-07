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

	[TabGroup("Entity Attributes")]
	[HideLabel]
	public AttributesData attributesData;

	public EntityStatisticsData(EntityStatisticsData data)
	{
		//копии данных
		statsData = new StatsData();
		statsData = data.statsData;

		attributesData = new AttributesData();
		attributesData = data.attributesData;
	}

	public EntityStatisticsData(EntityStatistics statistics)
	{
		//statsData = statistics.stats.GetCurrentData();
		//abilitiesData = statistics.abilities.GetCurrentData();
	}
}