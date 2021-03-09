using System.Linq;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

using Sirenix.OdinInspector;

public class EntityAttributes
{
	private EntityStatistics statistics;

	public List<Attribute> attributesAll = new List<Attribute>();
	public List<Advantage> advantagesAll = new List<Advantage>();
	public List<DisAdvantage> disAdvantagesAll = new List<DisAdvantage>();

	public UnityAction onAttributesChanged;

    public EntityAttributes(EntityStatistics statistics, AttributesData data)
	{
		this.statistics = statistics;

		List<Attribute> attributes = data.attributes.ToList();
		
		for(int i = 0; i < attributes.Count; i++)
		{
			AddAttribute(attributes[i]);
		}
    }
    public bool AddAttribute(Attribute attribute)
	{
		if(!attributesAll.Contains(attribute))//not work
		{
			Add(attribute);

			attribute.Enable(statistics);

			onAttributesChanged?.Invoke();
			return true;
		}
		return false;
	}



	public bool RemoveAttribute(Attribute attribute)
	{
		if(attributesAll.Contains(attribute))
		{
			Remove(attribute);

			attribute.Disabe(statistics);

			onAttributesChanged?.Invoke();
			return true;
		}
		return false;
	}

	private void Add(Attribute attribute)
	{
		attributesAll.Add(attribute);

		if(attribute is Advantage advantage)
			advantagesAll.Add(advantage);
		if(attribute is DisAdvantage disAdvantage)
			disAdvantagesAll.Add(disAdvantage);
	}
	private void Remove(Attribute attribute)
	{
		attributesAll.Remove(attribute);

		if(attribute is Advantage advantage)
			advantagesAll.Remove(advantage);
		if(attribute is DisAdvantage disAdvantage)
			disAdvantagesAll.Remove(disAdvantage);
	}


	public AttributesData GetCurrentData()
	{
		AttributesData attributes = new AttributesData()
		{
			attributes = attributesAll.ToArray(),
		};
		return attributes;
	}
}
[System.Serializable]
public struct AttributesData
{
	[AssetList]
    public Attribute[] attributes;
}