using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "RPG/Data/AdvatantagesData")]
public class AdvatantagesScriptableData : ScriptableObject
{
	[AssetList(AutoPopulate = true)]
	public Advantage[] advantagesAll;
}