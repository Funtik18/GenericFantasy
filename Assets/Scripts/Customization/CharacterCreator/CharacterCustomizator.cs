using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections.Generic;

using Sirenix.OdinInspector;

public class CharacterCustomizator : MonoBehaviour
{
	[HideInInspector] public Character character;

	[AssetList]
	[SerializeField] private Character characterMaleBase;
	[AssetList]
	[SerializeField] private Character characterFemaleBase;

	[SerializeField] private CharacterStand stand;

	public UnityAction onCharacterCreated;

	private CharacterAvatar Avatar => character.avatar;
	private CharacterInformation Information => character.Information;

	#region Variables
	[SerializeField] private CanvasGroup canvasGroup;

	[Title("Main")]
	[SerializeField] private InputFieldRandomString firstName;
	[SerializeField] private InputFieldRandomString secondName;

	[SerializeField] private InputFieldRandomString nickName;

	[SerializeField] private DropdownChooseRace race;
	[SerializeField] private DropdownChooseGender gender;
	
	[Title("Head")]
	[SerializeField] private CarouselList headCarousel;
	[SerializeField] private CarouselList earsCarousel;
	[SerializeField] private CarouselList hairsCarousel;
	[SerializeField] private CarouselList eyebrowsCarousel;
	[SerializeField] private Toggle isFacialhairs;
	[SerializeField] private CarouselList facialHairsCarousel;

	[Title("Torso")]
	[SerializeField] private CarouselList torsoCarousel;
	[Title("LArm")]
	[SerializeField] private CarouselList lUpperCarousel;
	[SerializeField] private CarouselList lLowerCarousel;
	[SerializeField] private CarouselList lHandCarousel;
	[SerializeField] private CarouselList lSholderCarousel;
	[SerializeField] private CarouselList lElbowCarousel;
	[Title("RArm")]
	[SerializeField] private CarouselList rUpperCarousel;
	[SerializeField] private CarouselList rLowerCarousel;
	[SerializeField] private CarouselList rHandCarousel;
	[SerializeField] private CarouselList rSholderCarousel;
	[SerializeField] private CarouselList rElbowCarousel;

	[Title("Hips")]
	[SerializeField] private CarouselList hipsCarousel;
	[Title("LLeg")]
	[SerializeField] private CarouselList lLegCarousel;
	[SerializeField] private CarouselList lKneeCarousel;
	[Title("RLeg")]
	[SerializeField] private CarouselList rLegCarousel;
	[SerializeField] private CarouselList rKneeCarousel;
	#endregion


	private void Awake()
	{
		FindObjectOfType<ButtonCreateNewCharacter>().onClicked = delegate { CreateNewFemaleCharacter(); SetCharacter(character); };

		#region Main
		firstName.onValueChanged = FirstNameChanged;
		secondName.onValueChanged = SecondNameChanged;
		nickName.onValueChanged = NickNameChanged;

		race.onEnumChanged = RaceChanged;
		gender.onEnumChanged = GenderChanged;
		#endregion

		isFacialhairs.onValueChanged.AddListener(IsFacialHairsChanged);

		firstName.Initialization(new List<string>() { "Iron", "Mege" });
		secondName.Initialization(new List<string>() { "DE", "asd" });
		nickName.Initialization(new List<string>() { "qweq1", "Qwea" });
	}

	#region Create Character
	public void LoadCharacter(CharacterStatisticsData data)
	{
		if(character == null)
		{
			if(data.informationData.gender == CharacterGenders.Female)
				CreateFemaleCharacter();
			else
				CreateMaleCharacter();
		}
		else
		{
			if(Information.data.gender != data.informationData.gender)
			{
				if(data.informationData.gender == CharacterGenders.Female)
					CreateFemaleCharacter();
				else
					CreateMaleCharacter();
			}
		}


		character.SetStatistics(data);
		SetCharacter(character);
	}

	public void CreateFemaleCharacter()
	{
		CharacterRaces races = CharacterRaces.Human;
		if(character != null)
		{
			races = Information.data.race;
		}

		CreateNewFemaleCharacter();
		Information.data.race = races; 
	}
	public void CreateMaleCharacter()
	{
		CharacterRaces races = CharacterRaces.Human;
		if(character != null)
		{
			races = Information.data.race;
		}

		CreateNewMaleCharacter();
		Information.data.race = races;
	}

	//Болванка
	private void CreateNewFemaleCharacter()
	{
		Debug.LogError(characterFemaleBase.avatar.persona.bodyPiece.torsoPiece.armLeftPiece.sholderAttachmentPiece.CurrentIndex);

		character = stand.ReplaceCharacter(characterFemaleBase);
		Debug.LogError(character.avatar.persona.bodyPiece.torsoPiece.armLeftPiece.sholderAttachmentPiece.CurrentIndex);

		Information.data.gender = CharacterGenders.Female;

		onCharacterCreated?.Invoke();
	}
	//Болванка
	private void CreateNewMaleCharacter()
	{
		character = stand.ReplaceCharacter(characterMaleBase);
		Information.data.gender = CharacterGenders.Male;

		onCharacterCreated?.Invoke();
	}
	#endregion

	public void SetCharacter(Character character)
	{
		canvasGroup.interactable = character != null;

		if(character)
			Setup();
	}

	private void Setup()
	{
		firstName.SetField(Information.data.firstName);
		secondName.SetField(Information.data.secondName);
		nickName.SetField(Information.data.nickName);

		race.SetEnumOption(Information.data.race);
		gender.SetEnumOption(Information.data.gender);

		SetupObjects();
	}

	private void SetupObjects()
	{
		CharacterHeadPiece headPiece = Avatar.persona.headPiece;
		CharacterBodyPiece bodyPiece = Avatar.persona.bodyPiece;
		CharacterTorsoPiece torsoPiece = bodyPiece.torsoPiece;
		CharacterHipsPiece hipsPiece = bodyPiece.hipsPiece;

		#region Head
		headCarousel.SetCarousel(headPiece.headsPiece);
		eyebrowsCarousel.SetCarousel(headPiece.eyebrowsPiece);

		earsCarousel.SetCarousel(headPiece.earsPiece);
		earsCarousel.IsEnable = Information.data.race == CharacterRaces.Elf;

		hairsCarousel.SetCarousel(headPiece.hairsPiece);

		//Facial
		facialHairsCarousel.SetCarousel(headPiece.facialHairsPiece);
		
		isFacialhairs.interactable = Information.data.gender == CharacterGenders.Male;
		isFacialhairs.isOn = headPiece.IsHaveFacialHairs && isFacialhairs.interactable;

		if(isFacialhairs.isOn == false)
			facialHairsCarousel.IsEnable = false;
		#endregion

		#region Torso
		torsoCarousel.SetCarousel(torsoPiece.torsoPiece);

		lUpperCarousel.SetCarousel(torsoPiece.armLeftPiece.armUpperPiece);
		lLowerCarousel.SetCarousel(torsoPiece.armLeftPiece.armLowerPiece);
		lHandCarousel.SetCarousel(torsoPiece.armLeftPiece.handPiece);

		rUpperCarousel.SetCarousel(torsoPiece.armRightPiece.armUpperPiece);
		rLowerCarousel.SetCarousel(torsoPiece.armRightPiece.armLowerPiece);
		rHandCarousel.SetCarousel(torsoPiece.armRightPiece.handPiece);

		lSholderCarousel.SetCarousel(torsoPiece.armLeftPiece.sholderAttachmentPiece, true);
		lElbowCarousel.SetCarousel(torsoPiece.armLeftPiece.elbowAttachmentPiece, true);

		rSholderCarousel.SetCarousel(torsoPiece.armRightPiece.sholderAttachmentPiece, true);
		rElbowCarousel.SetCarousel(torsoPiece.armRightPiece.elbowAttachmentPiece, true);
		#endregion

		#region Hips
		hipsCarousel.SetCarousel(hipsPiece.hipsPiece);

		lLegCarousel.SetCarousel(hipsPiece.legLeftPiece.legPiece);
		rLegCarousel.SetCarousel(hipsPiece.legRightPiece.legPiece);

		lKneeCarousel.SetCarousel(hipsPiece.legLeftPiece.kneeAttachementPiece, true);
		rKneeCarousel.SetCarousel(hipsPiece.legRightPiece.kneeAttachementPiece, true);
		#endregion
	}

	#region Main
	private void FirstNameChanged(string value)
	{
		Information.data.firstName = value;
	}
	private void SecondNameChanged(string value)
	{
		Information.data.secondName = value;
	}
	private void NickNameChanged(string value)
	{
		Information.data.nickName = value;
	}

	private void RaceChanged(CharacterRaces race)
	{
		earsCarousel.GetComponent<CanvasGroup>().interactable = earsCarousel.IsEnable = race == CharacterRaces.Elf;

		if(race != Information.data.race)
		{
			Information.data.race = race;
			Avatar.persona.headPiece.IsCustomEars = race != CharacterRaces.Human;
		}
	}
	private void GenderChanged(CharacterGenders gender)
	{
		if(gender != Information.data.gender)
		{
			if(gender == CharacterGenders.Female)
				CreateNewFemaleCharacter();
			else
				CreateNewMaleCharacter();

			SetCharacter(character);
		}
	}
	#endregion

	private void IsFacialHairsChanged(bool trigger)
	{
		if(Avatar.persona.headPiece.IsHaveFacialHairs != trigger)
			facialHairsCarousel.IsEnable = Avatar.persona.headPiece.IsHaveFacialHairs = trigger;
	}

	public CharacterStatisticsData GetData()
	{
		return character.GetData();
	}
}