using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStand : MonoBehaviour
{
    private Character character;

    public Character ReplaceCharacter(Character charact)
	{
		DisposeAll();

		if(charact != null)
		{
			character = Instantiate(charact, transform);
		}
		return this.character;
	}
	private void DisposeAll()
	{
		foreach(Transform child in transform)
		{
			Destroy(child.gameObject);
		}
	}
}