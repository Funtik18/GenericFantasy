using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarsUI : MonoBehaviour
{
	[SerializeField] private List<BarPointsUI> barsUI = new List<BarPointsUI>();

	public void Initialization(EntityStats entityStats)
	{
		List<Bar> bars = entityStats.barsAll;

		for(int i = 0; i < barsUI.Count; i++)
		{
			barsUI[i].SetBar(bars[i]);
		}
	}
}
