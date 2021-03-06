using System.Collections.Generic;
using UnityEngine.Events;

using Sirenix.OdinInspector;
using UnityEngine;

public class EntityAdvantages
{
	private EntityStatistics statistics;

	public List<Advantage> advantagesAll = new List<Advantage>();

	public UnityAction onAdvantagesChanged;

    public EntityAdvantages(EntityStatistics statistics, AdvantagesData data)
	{
		this.statistics = statistics;

		List<Advantage> advantages = data.advantages;
		
		for(int i = 0; i < advantages.Count; i++)
		{
			AddAdvantage(advantages[i]);
		}
    }
    public bool AddAdvantage(Advantage advantage)
	{
		if(!advantagesAll.Contains(advantage))//not work
		{
			advantagesAll.Add(advantage);

			advantage.EnableAdvantage(statistics);

			onAdvantagesChanged?.Invoke();
			return true;
		}
		return false;
	}
    public bool RemoveAdvantage(Advantage advantage)
	{
		if(advantagesAll.Contains(advantage))
		{
			advantagesAll.Remove(advantage);

			advantage.DisableAdvantage(statistics);

			onAdvantagesChanged?.Invoke();
			return true;
		}
		return false;
	}
}
[System.Serializable]
public struct AdvantagesData
{
	[AssetList]
    public List<Advantage> advantages;
}