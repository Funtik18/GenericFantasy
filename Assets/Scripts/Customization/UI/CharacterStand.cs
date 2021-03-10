using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStand : MonoBehaviour
{
    private Character character;

    public void ReplaceCharacter(Character character)
	{
		this.character = character;
		DisposeAllUI();
		if(this.character != null)
		{
			Instantiate(character, transform);
		}
	}
	private void DisposeAllUI()
	{
		foreach(Transform child in transform)
		{
			Destroy(child.gameObject);
		}
	}
}