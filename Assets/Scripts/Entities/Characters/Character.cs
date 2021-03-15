using UnityEngine;

public class Character : Entity<CharacterStatistics> 
{
	public CharacterAvatar avatar;

	public CharacterData data;

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


	public void Save()
	{
		avatar.SaveModel();
		SaveLoaderManager.SaveCharacter(data, Statistics.ID);
	}
	public void Load()
	{

	}


	public void SetCharacter(CharacterData characterData)
	{
		data = characterData;

		avatar.LoadModel();
		statistics = new CharacterStatistics(data.statistics);
	}
}
[System.Serializable]
public class CharacterData
{
	public CharacterInformationData information;
	public CharacterStatisticsData statistics;
	public CharacterModelData model;
}
[System.Serializable]
public class CharacterInformationData
{
	public string firstName;
	public string secondName;
	public string nickName;
	public CharacterGenders gender;
	public CharacterRaces race;
}
public enum CharacterGenders { Male, Female }
public enum CharacterRaces { Human, Elf }