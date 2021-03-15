using UnityEngine;

public class Character : Entity<CharacterStatistics> 
{
	public CharacterAvatar avatar;

	public CharacterInformation Information => Statistics.information;
	public CharacterModel Model => Statistics.model;


	public override CharacterStatistics Statistics
	{
		get
		{
			if(statistics == null)
			{
				CharacterStatisticsData data = new CharacterStatisticsData();

				SetStatistics(data);
			}
			return statistics;
		}
	}


	private void Start()
	{
		Debug.LogError(Statistics.stats.Dexterity.StatValue);

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