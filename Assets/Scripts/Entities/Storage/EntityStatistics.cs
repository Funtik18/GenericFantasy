using System;

public class EntityStatistics
{
	public readonly string id;


	public readonly EntityCustomColors colors;

	public readonly EntityStats stats;

	public readonly EntityAttributes attributes;

	public EntityStatistics(EntityStatisticsData data)
	{
		id = Guid.NewGuid().ToString();

		//colors = new EntityCustomColors(data.colorsData);

		//stats = new EntityStats(data.statsData);

		//attributes = new EntityAttributes(this, data.attributesData);
	}

	public EntityStatisticsData GetData()
	{
		return new EntityStatisticsData(this);
	}
}
public class CharacterStatistics : EntityStatistics
{
	public readonly CharacterInformation information;

	public readonly CharacterModel model;

	public CharacterStatistics(CharacterStatisticsData data) : base(data)
	{
		information = new CharacterInformation(data.informationData);
		model = new CharacterModel(data.modelData);
	}

	public new CharacterStatisticsData GetData()
	{
		return new CharacterStatisticsData(this);
	}
}