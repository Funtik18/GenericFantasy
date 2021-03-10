using System.Collections;
using System.Collections.Generic;

using Sirenix.OdinInspector;

using UnityEngine;
using UnityEngine.Events;

public class CharacterSelection : MonoBehaviour
{
	[SerializeField] private PlayerChief playerChief;

	[SerializeField] private Transform content;
	[SerializeField] private Transform highter;

	[SerializeField] private List<Character> characters = new List<Character>();

	[AssetList]
	[SerializeField] private CharacterAddUI characterAddPrefab;

	[Space]
	[SerializeField] private CharacterStand stand;

	public UnityAction onCharacterSelected;

	private List<CharacterAddUI> charactersAddUI = new List<CharacterAddUI>();

	private void Awake()
	{
		playerChief.quartetUI.onCharacterSelected = CharacterSelectedQuartet;
		playerChief.quartetUI.onCharacterRemoved = CharacterQuartetRemoved;
		UpdateCharacterList();
	}

	public void UpdateCharacterList()
	{
		DisposeAll();
		for(int i = 0; i < characters.Count; i++)
		{
			CharacterAddUI characterAdd = Instantiate(characterAddPrefab, content);

			characterAdd.onSelected += CharacterSelected;
			characterAdd.onRequit += CharacterRequit;

			characterAdd.SetCharacter(characters[i]);

			charactersAddUI.Add(characterAdd);
		}
		highter.SetAsLastSibling();
	}

	private void CharacterSelected(CharacterAddUI characterAdd)
	{
		for(int i = 0; i < charactersAddUI.Count; i++)
		{
			if(charactersAddUI[i] != characterAdd)
			{
				charactersAddUI[i].Deselect();
			}
		}

		stand.ReplaceCharacter(characterAdd.character);

		onCharacterSelected?.Invoke();
	}
	private void CharacterRequit(CharacterAddUI characterAdd)
	{
		if(playerChief.quartet.AddCharacter(characterAdd.character))
		{
			characterAdd.Disable();
		}
	}
	private void CharacterQuartetRemoved(Character character)
	{
		for(int i = 0; i < charactersAddUI.Count; i++)
		{
			if(charactersAddUI[i].character == character)
			{
				charactersAddUI[i].Enable();
				break;
			}
		}
	}
	private void CharacterSelectedQuartet()
	{
		for(int i = 0; i < charactersAddUI.Count; i++)
		{
			charactersAddUI[i].Deselect();
		}
	}


	public void DisposeAll()
	{
		foreach(Transform child in content)
		{
			if(child != highter)
			{
				Destroy(child.gameObject);
			}
		}
	}
}