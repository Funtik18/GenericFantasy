using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Quartet
{
	[HideInInspector] public readonly int maxCharacters = 4;

	public List<CharacterData> characters = new List<CharacterData>();

	public UnityAction onQuartetChanged;

	//+инит сразу нескольких 

	public bool AddCharacter(Character character)
	{
		if(characters.Count < 4)
		{
			if(!Contains(character))
			{
				characters.Add(character.GetData());
				onQuartetChanged?.Invoke();
				return true;
			}
		}
		return false;
	}
	public bool RemoveCharacter(Character character)
	{
		int index;
		if(Contains(character, out index))
		{
			characters.Remove(characters[index]);
			onQuartetChanged?.Invoke();
			return true;
		}
		return false;
	}

	private bool Contains(Character character)
	{
		for(int i = 0; i < characters.Count; i++)
		{
			if(characters[i].character == character) 
				return true;
		}
		return false;
	}
	private bool Contains(Character character, out int index)
	{
		index = -1;
		for(int i = 0; i < characters.Count; i++)
		{
			if(characters[i].character == character)
			{
				index = i;
				return true;
			}
		}
		return false;
	}

	public Character GetCharacter(int index)
	{
		if(index >= 0 && index < characters.Count)
		{
			return characters[index].character;
		}
		return null;
	}
}