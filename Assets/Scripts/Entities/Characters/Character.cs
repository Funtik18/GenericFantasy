public class Character : Entity<CharacterStatistics> 
{
	public CharacterAvatar avatar;

	public override CharacterStatistics Statistics
	{
		get
		{
			if(statistics == null)
			{

				statistics = new CharacterStatistics(new CharacterStatisticsData());

				//statistics = new CharacterStatistics(SaveLoaderManager.LoadPlayerStatistics());//загрузка базы


				//if(SaveLoaderManager.IsFirstTime)
				{
					//statistics = new EntityStatistics(new EntityStatisticsData(data.data));//базовые значения

					//SaveLoaderManager.SavePlayerStatistics(statistics.GetData());//сохраняем

					//SaveLoaderManager.IsFirstTime = false;
				}
				//else
				{
				}
			}
			return statistics;
		}
	}


	public void SetStatistics(CharacterStatisticsData data)
	{
		statistics = new CharacterStatistics(data);
	}

	public CharacterStatisticsData GetData()
	{
		return Statistics.GetData();
	}
}