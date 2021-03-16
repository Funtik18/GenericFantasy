using System;

public class EntityStatistics
{
	private string id;
	public string ID { private set => id = value; get => id; }


	public readonly EntityCustomColors colors;

	public readonly EntityStats stats;

	//public readonly EntityAttributes attributes;

	public EntityStatistics(EntityStatisticsData data)
	{
		NewId();

		colors = new EntityCustomColors(data.colorsData);

		stats = new EntityStats(data.statsData);

		//attributes = new EntityAttributes(this, data.attributesData);
	}

	public string NewId()
	{
		ID = Guid.NewGuid().ToString();
		return ID;
	}

	public EntityStatisticsData GetData()
	{
		return new EntityStatisticsData(this);
	}
}
public class CharacterStatistics : EntityStatistics
{
	public CharacterStatistics(CharacterStatisticsData data) : base(data)
	{
	}

	public new CharacterStatisticsData GetData()
	{
		return new CharacterStatisticsData(this);
	}
}