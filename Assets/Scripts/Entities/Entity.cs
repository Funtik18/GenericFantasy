using System;
using UnityEngine;

using Sirenix.OdinInspector;

public abstract class Entity : MonoBehaviour
{
	[ReadOnly]
	[SerializeField] private string id;
	public string Id
	{
		get
		{
			if(id == "")
			{
				id = Guid.NewGuid().ToString();
			}
			return id;
		}
	}

	//[ShowInInspector]//for debug
	//[NonSerialized]
	protected EntityStatistics statistics;
	public virtual EntityStatistics Statistics { get; }

	protected virtual void Awake()
	{
		if(Id == null) { }
	}
}
public class Entity<DATA> : Entity
	where DATA : EntityScriptableData
{
	[Required]
	[SerializeField] protected DATA data;

	protected EntityStatisticsData statisticsData;
	protected virtual EntityStatisticsData StatisticsData
	{
		get
		{
			if(statisticsData == null)
			{
				statisticsData = new EntityStatisticsData(out statistics, data.data);
			}
			return statisticsData;
		}
	}

	public override EntityStatistics Statistics
	{
		get
		{
			if(statistics == null)
			{
				if(StatisticsData == null) { }
			}
			return statistics;
		}
	}

	protected override void Awake()
	{
		base.Awake();

		if(Statistics == null) { }
	}
}