using UnityEngine;

using Sirenix.OdinInspector;

public abstract class Entity : MonoBehaviour
{
	//[ShowInInspector]//for debug
	//[NonSerialized]
	public virtual EntityStatistics Statistics { get; }
}
public class Entity<DATA> : Entity
	where DATA : EntityScriptableData
{
	[Required]
	[SerializeField] protected DATA data;

	private EntityStatistics statistics;
	public override EntityStatistics Statistics
	{
		get
		{
			if(statistics == null)
			{
				statistics = new EntityStatistics(new EntityStatisticsData(data.data));
			}
			return statistics;
		}
	}

	protected virtual void Awake()
	{
		if(Statistics == null) { }
	}
}