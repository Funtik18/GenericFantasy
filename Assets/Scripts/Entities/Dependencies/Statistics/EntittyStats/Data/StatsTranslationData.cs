using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;


[CreateAssetMenu(menuName = "RPG/Data/StatsTranslation")]
public class StatsTranslationData : ScriptableObject
{
	public List<StatsLanguage> translations = new List<StatsLanguage>();


	[System.Serializable]
	public struct StatsLanguage
	{
		public Languages languege;
		public InformationData[] stats;
	}
}

