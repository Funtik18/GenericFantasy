using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class EntityAdvantages
{
    public List<Advantage> all;

	public UnityAction onAdvantagesChanged;

    public EntityAdvantages(AdvantagesData data)
	{
		List<Advantage> advantages = data.advantages;
		
		all = new List<Advantage>();

		for(int i = 0; i < advantages.Count; i++)
		{
			AddAdvantage(advantages[i]);
		}
    }
    public void AddAdvantage(Advantage advantage)
	{
		if(!all.Contains(advantage))
		{
			all.Add(advantage);

			onAdvantagesChanged?.Invoke();
		}
	}
    public void RemoveAdvantage(Advantage advantage)
	{
		if(all.Contains(advantage))
		{
			all.Remove(advantage);

			onAdvantagesChanged?.Invoke();
		}
	}
}
[System.Serializable]
public struct AdvantagesData
{
    public List<Advantage> advantages;
}