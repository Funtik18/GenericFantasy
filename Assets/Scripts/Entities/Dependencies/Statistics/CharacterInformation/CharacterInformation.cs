using UnityEngine;

public class CharacterInformation
{
    public CharacterInformationData data;

    public CharacterInformation(CharacterInformationData infoData)
	{
        data = infoData;
    }
}
[System.Serializable]
public struct CharacterInformationData
{
    public string firstName;
    public string secondName;
    public string nickName;
    public CharacterGenders gender;
    public CharacterRaces race;
}
public enum CharacterGenders { Male, Female }
public enum CharacterRaces { Human, Elf }