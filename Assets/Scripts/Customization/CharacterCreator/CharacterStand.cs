using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStand : MonoBehaviour
{
    private Character character;

    public Character ReplaceCharacter(Character character)
	{
		DisposeAllUI();
		if(character != null)
		{
			this.character = Instantiate(character, transform);
		}

		return this.character;
	}
	private void DisposeAllUI()
	{
		foreach(Transform child in transform)
		{
			Destroy(child.gameObject);
		}
	}
}