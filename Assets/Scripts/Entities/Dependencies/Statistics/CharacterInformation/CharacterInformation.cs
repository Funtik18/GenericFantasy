public class CharacterInformation
{
    private CharacterInformationData data;

    public string firstName;
    public string secondName;
    public string nickName;
    public CharacterGenders gender;
    public CharacterRaces race;

    public CharacterInformation(CharacterInformationData infoData)
	{
        this.data = infoData;
    }

	public CharacterInformationData GetCurrentData()
	{
        CharacterInformationData infoData = new CharacterInformationData()
        {
            firstName = firstName,
            secondName = secondName,
            nickName = nickName,
            gender = gender,
            race = race,
        };
        return infoData;
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