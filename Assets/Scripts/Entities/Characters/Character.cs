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


	public void SetStatistics(CharacterStatisticsData data)
	{
		statistics = new CharacterStatistics(data);
		avatar.UpdateCharacter(data);
	}

	public CharacterStatisticsData GetData()
	{
		CharacterStatisticsData data = Statistics.GetData();
		data.modelData = avatar.persona.GetData();
		
		return data;
	}
}