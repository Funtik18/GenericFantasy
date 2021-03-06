using System.Linq;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

public class AdvantagesAllUI : MonoBehaviour
{
	[SerializeField] private AdvatantagesScriptableData data;
	[Space]
	[SerializeField] private RectTransform highter;
	[AssetList]
	[SerializeField] private AdvantageAddUI advantagesUIPrefab;

	[SerializeField] private TMPro.TextMeshProUGUI description;

	public UnityAction<Advantage> onAddAdvantage;

	private List<Advantage> advantages;

	public void Initialization()
	{
		advantages = data.advantagesAll.ToList();

		UpdateAdvantages();
	}
	private void UpdateAdvantages()
	{
		DisposeAllUI();
		for(int i = 0; i < advantages.Count; i++)
		{
			AdvantageAddUI created = Instantiate(advantagesUIPrefab, transform);
			created.onAdd += onAddAdvantage;
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

	private void SetDescription(AdvantageUI advantage)
	{
		description.text = advantage.data.description;
	}
}