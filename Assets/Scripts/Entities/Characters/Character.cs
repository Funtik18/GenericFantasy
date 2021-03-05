using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity<CharacterScriptableData>
{
	public StatsUI statsUI;
	public BarsUI barsUI;

	protected override void Awake()
	{
		base.Awake();

		statsUI.Initialization(Statistics.stats);
		barsUI.Initialization(Statistics.stats);
	}
}