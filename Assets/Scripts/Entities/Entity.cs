using UnityEngine;
public abstract class Entity : MonoBehaviour { }
public abstract class Entity<STATISTICS> : Entity where STATISTICS : EntityStatistics
{
	protected STATISTICS statistics;
	public abstract STATISTICS Statistics { get; }

	protected virtual void Awake()
	{
		if(Statistics == null) { }
	}
}