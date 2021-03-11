using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterUI : MonoBehaviour
{
    public Entity<EntityStatistics> entity;
	[Space]
	public StatsUI statsUI;
	public BarsUI barsUI;
	public AttributesUI advantagesUI;

	private void Awake()
	{
		statsUI.Initialization(entity.Statistics.stats);
		barsUI.Initialization(entity.Statistics.stats);

		advantagesUI.Initialization(entity.Statistics.attributes);
	}
}