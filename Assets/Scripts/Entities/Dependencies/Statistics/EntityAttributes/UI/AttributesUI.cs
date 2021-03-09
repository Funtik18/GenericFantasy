using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

public class AttributesUI : MonoBehaviour
{
	[SerializeField] private RectTransform highter;

	[AssetList]
	[SerializeField] private AttributeRemoveUI advantagesRemoveUIPrefab;

	[SerializeField] private TMPro.TextMeshProUGUI description;


	[SerializeField] private AttributesAllUI advantagesAllUI;

	private EntityAttributes entityAttributes;

	private List<Attribute> attributes;

	public void Initialization(EntityAttributes entityAttributes)
	{
		this.entityAttributes = entityAttributes;

		advantagesAllUI.onAddAttribute += AddAttribute;
		advantagesAllUI.Initialization();

		entityAttributes.onAttributesChanged += UpdateAttributes;

		attributes = entityAttributes.attributesAll;

		UpdateAttributes();
	}
	private void UpdateAttributes()
	{
		DisposeAllUI();
		for(int i = 0; i < attributes.Count; i++)
		{
			AttributeRemoveUI created = Instantiate(advantagesRemoveUIPrefab, transform);

			created.onRemove += RemoveAttribute;

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

	private void AddAttribute(Attribute attribute)
	{
		entityAttributes.AddAttribute(attribute);
	}
	private void RemoveAttribute(Attribute attribute)
	{
		entityAttributes.RemoveAttribute(attribute);
	}

	private void SetDescription(AttributeUI attribute)
	{
		description.text = attribute.data.description;
	}
}
