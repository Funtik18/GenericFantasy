using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;
using System.Linq;

public class AttributesAllUI : MonoBehaviour
{
	[SerializeField] private AttributesScriptableData data;
	[Space]
	[SerializeField] private RectTransform highter;
	[AssetList]
	[SerializeField] private AttributeAddUI attributeAddUIPrefab;

	[SerializeField] private TMPro.TextMeshProUGUI description;

	public UnityAction<Attribute> onAddAttribute;

	private List<Attribute> attributes;

	public void Initialization()
	{
		attributes = data.attributesAll.ToList();

		UpdateAttributes();
	}
	private void UpdateAttributes()
	{
		DisposeAllUI();
		for(int i = 0; i < attributes.Count; i++)
		{
			AttributeAddUI created = Instantiate(attributeAddUIPrefab, transform);
			created.onAdd += onAddAttribute;
			created.onShowDescription += SetDescription;
			created.SetAttribute(attributes[i]);
		}
		highter.SetAsLastSibling();
	}
	private void DisposeAllUI()
	{
		foreach(Transform child in transform)
		{
			if(child != highter.transform)
			{
				Destroy(child.gameObject);
			}
		}
	}

	private void SetDescription(AttributeUI attribute)
	{
		description.text = attribute.data.description;
	}
}