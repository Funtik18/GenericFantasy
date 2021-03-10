using System.Collections;
using System.Collections.Generic;

using Sirenix.OdinInspector;

using UnityEngine;

public class ButtonCreateNewCharacter : MonoBehaviour
{
    [AssetList]
    [SerializeField] private Character characterBase;

    [SerializeField] private CharacterStand stand;

    private Character currentCharacter;

    public void CreateNew()
	{
        currentCharacter = stand.ReplaceCharacter(characterBase);
        FindObjectOfType<ButtonSaveNewCharacter>().SetCharacter(currentCharacter);
        FindObjectOfType<CharacterCustomizator>().SetCharacter(currentCharacter);
    }
}
