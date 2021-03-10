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
	[TabGroup("EntityColors")]
	[HideLabel]
	public ColorsData colorsData;

	[TabGroup("Entity Stats")]
	[HideLabel]
	public StatsData statsData;

	[TabGroup("Entity Attributes")]
	[HideLabel]
	public AttributesData attributesData;

	public EntityStatisticsData(EntityStatisticsData data)
	{
		//копии данных
		colorsData = new ColorsData();
		colorsData = data.colorsData;

		statsData = new StatsData();
		statsData = data.statsData;

		attributesData = new AttributesData();
		attributesData = data.attributesData;
	}

	public EntityStatisticsData(EntityStatistics statistics)
	{
		colorsData = statistics.colors.GetCurrentData();
		statsData = statistics.stats.GetCurrentData();
		attributesData = statistics.attributes.GetCurrentData();
	}
}