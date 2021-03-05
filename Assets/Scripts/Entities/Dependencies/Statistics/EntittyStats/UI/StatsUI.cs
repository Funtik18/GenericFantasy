using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

public class StatsUI : MonoBehaviour
{
	[Required]
	[SerializeField] private StatsTranslationData data;

	[SerializeField] private RectTransform higther;

	[SerializeField] private List<StatUI> statsUI = new List<StatUI>();

	public void Initialization(EntityStats entityStats)
	{
		List<Characteristic> stats = entityStats.statsAll;

		for(int i = 0; i < statsUI.Count; i++)
		{
			statsUI[i].SetStat(data.translations[0].stats[i], stats[i]);
		}

		higther.SetAsLastSibling();
	}
}
