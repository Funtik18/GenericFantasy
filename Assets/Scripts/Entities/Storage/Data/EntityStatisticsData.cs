using Sirenix.OdinInspector;

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

	public EntityStatisticsData()
	{
		statsData = new StatsData()
		{
			level = 1,
			statsPrimary = new StatsData.StatsPrimary()
			{
				strength = 10,
				dexterity = 10,
				intelligence = 10,
				vitality = 10,
			},
			currents = new StatsData.CurrentValues()
			{
				currentExperience = 0,
				currentWeight = 0,
			}
		};
	}


	/// <summary>
	/// Копирование данных
	/// </summary>
	/// <param name="data"></param>
	public EntityStatisticsData(EntityStatisticsData data)
	{
		colorsData = new ColorsData();
		colorsData = data.colorsData;

		statsData = new StatsData();
		statsData = data.statsData;

		attributesData = new AttributesData();
		attributesData = data.attributesData;
	}

	/// <summary>
	/// Замена данных
	/// </summary>
	/// <param name="statistics"></param>
	public EntityStatisticsData(EntityStatistics statistics)
	{
		//colorsData = statistics.colors.GetCurrentData();
		statsData = statistics.stats.GetCurrentData();
		//attributesData = statistics.attributes.GetCurrentData();
	}
}

[System.Serializable]
public class CharacterStatisticsData : EntityStatisticsData
{
	public CharacterStatisticsData() : base()
	{
	}

	public CharacterStatisticsData(CharacterStatisticsData data) : base(data)
	{
	}

	public CharacterStatisticsData(CharacterStatistics statistics) : base(statistics)
	{
	}
}