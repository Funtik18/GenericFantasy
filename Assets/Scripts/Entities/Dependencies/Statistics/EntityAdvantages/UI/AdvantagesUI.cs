using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

public class AdvantagesUI : MonoBehaviour
{
	[SerializeField] private RectTransform highter;

	[AssetList]
	[SerializeField] private AdvantageRemoveUI advantagesRemoveUIPrefab;

	[SerializeField] private TMPro.TextMeshProUGUI description;


	[SerializeField] private AdvantagesAllUI advantagesAllUI;

	private EntityAdvantages entityAdvantages;

	private List<Advantage> advantages;

	private bool isCustom = false;

	public void Initialization(EntityAdvantages entityAdvantages)
	{
		this.entityAdvantages = entityAdvantages;

		advantagesAllUI.onAddAdvantage += AddAdvantage;
		advantagesAllUI.Initialization();

		entityAdvantages.onAdvantagesChanged += UpdateAdvantages;

		advantages = entityAdvantages.advantagesAll;

		UpdateAdvantages();
	}
	private void UpdateAdvantages()
	{
		DisposeAllUI();
		for(int i = 0; i < advantages.Count; i++)
		{
			AdvantageRemoveUI created = Instantiate(advantagesRemoveUIPrefab, transform);

			created.onRemove += RemoveAdvantage;

			created.onShowDescription += SetDescription;

			created.SetAdvantage(advantages[i]);
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

	private void AddAdvantage(Advantage advantage)
	{
		entityAdvantages.AddAdvantage(advantage);
	}
	private void RemoveAdvantage(Advantage advantage)
	{
		entityAdvantages.RemoveAdvantage(advantage);
	}

	private void SetDescription(AdvantageUI advantage)
	{
		description.text = advantage.data.description;
	}
}
