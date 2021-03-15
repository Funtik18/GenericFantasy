using UnityEngine;

public class Character : Entity<CharacterStatistics> 
{
	public CharacterAvatar avatar;

	public CharacterData data;

    [Header("Basic attributes")]
    public float myStrength;
    public float myDexterity;
    public float myIntelligence;
    public float myHealth;
    [Header("Second attributes")]
    public float myHP;
    public float myMove;
    public float mySpeed;
    public float myWill;
    public float myPerception;
    public float myFatiguePoints;
    public float myDodge;

	public CharacterInformationData Information => data.information;

    public override CharacterStatistics Statistics
	{
		get
		{
			if(statistics == null)
			{

                statistics = new CharacterStatistics(data.statistics);

                SetStats();

			}
			return statistics;
		}
	}

    void SetStats()
    {
        myStrength = statistics.stats.Strength.StatValue;
        myDexterity = statistics.stats.Dexterity.StatValue;
        myIntelligence = statistics.stats.Intelligence.StatValue;
        myHealth = statistics.stats.Vitality.StatValue;


        myHP = statistics.stats.Health.StatValue;
        myMove = statistics.stats.Move.StatValue;
        mySpeed = statistics.stats.Speed.StatValue;
        myWill = statistics.stats.Will.StatValue;
        myPerception = statistics.stats.Perception.StatValue;
        myFatiguePoints = statistics.stats.Fatigue.StatValue;

        myDodge=statistics.stats.Dodge.StatValue;
    }

	public void Save()
	{
		avatar.SaveModel();
		SaveLoaderManager.SaveCharacter(data, Statistics.ID);
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