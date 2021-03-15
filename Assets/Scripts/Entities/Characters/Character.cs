using UnityEngine;

public class Character : Entity<CharacterStatistics> 
{
	public CharacterAvatar avatar;

	public readonly CharacterData data = new CharacterData();

	public CharacterInformationData Information => data.information;

	public override CharacterStatistics Statistics
	{
		get
		{
			if(statistics == null)
			{
				statistics = new CharacterStatistics(data.statistics);
			}
			return statistics;
		}
	}

	public void SetCharacter(CharacterData data)
	{
		//statistics = new CharacterStatistics(data.statistics);

		//avatar.UpdateCharacter(data);
	}
}
[System.Serializable]
public class CharacterData
{
	public CharacterInformationData information = new CharacterInformationData();
	public CharacterStatisticsData statistics = new CharacterStatisticsData();
	[HideInInspector] public CharacterModelData model = new CharacterModelData();
}
[System.Serializable]
public class CharacterInformationData
{
	public string firstName;
	public string secondName;
	public string nickName;
	public CharacterGenders gender = CharacterGenders.Male;
	public CharacterRaces race = CharacterRaces.Human;
}
public enum CharacterGenders { Male, Female }
public enum CharacterRaces { Human, Elf }