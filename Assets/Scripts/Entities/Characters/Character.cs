using UnityEngine;

public class Character : Entity<CharacterStatistics> 
{
	public CharacterAvatar avatar;

	public CharacterInformation Information => Statistics.information;
	public CharacterModel Model => Statistics.model;

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

    public override CharacterStatistics Statistics
	{
		get
		{
			if(statistics == null)
			{
				CharacterStatisticsData data = new CharacterStatisticsData();

				SetStatistics(data);
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

	public void SetStatistics(CharacterStatisticsData data)
	{
		statistics = new CharacterStatistics(data);
		avatar.UpdateCharacter(data);
	}

	public CharacterStatisticsData GetData()
	{
		Debug.LogError(avatar.persona.bodyPiece.torsoPiece.armRightPiece.sholderAttachmentPiece.currentIndex);
		CharacterStatisticsData data = Statistics.GetData();
		data.modelData = avatar.persona.GetData();
		Debug.LogError(data.modelData.body.torso.rightArm.sholderAttachmentIndex);

		return data;
	}
}

[System.Serializable]
public struct test
{
	public int b;
}