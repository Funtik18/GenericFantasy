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

	private CharacterStatisticsData defaultCharacter;


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
	[SerializeField] private Toggle lArmUseExtra;
	[SerializeField] private CarouselList lSholderCarousel;
	[SerializeField] private CarouselList lElbowCarousel;
	[Title("RArm")]
	[SerializeField] private CarouselList rUpperCarousel;
	[SerializeField] private CarouselList rLowerCarousel;
	[SerializeField] private CarouselList rHandCarousel;
	[SerializeField] private Toggle rArmUseExtra;
	[SerializeField] private CarouselList rSholderCarousel;
	[SerializeField] private CarouselList rElbowCarousel;

	[Title("Hips")]
	[SerializeField] private CarouselList hipsCarousel;
	[Title("LLeg")]
	[SerializeField] private CarouselList lLegCarousel;
	[SerializeField] private Toggle lLegUseExtra;
	[SerializeField] private CarouselList lKneeCarousel;
	[Title("RLeg")]
	[SerializeField] private CarouselList rLegCarousel;
	[SerializeField] private Toggle rLegUseExtra;
	[SerializeField] private CarouselList rKneeCarousel;
	#endregion


	private void Awake()
	{
		FindObjectOfType<ButtonCreateNewCharacter>().onClicked = CreateFemaleCharacter;

		#region Main
		firstName.onValueChanged = FirstNameChanged;
		secondName.onValueChanged = SecondNameChanged;
		nickName.onValueChanged = NickNameChanged;

		race.onEnumChanged = RaceChanged;
		gender.onEnumChanged = GenderChanged;
		#endregion

		isFacialhairs.onValueChanged.AddListener(IsFacialHairsChanged);

		lArmUseExtra.onValueChanged.AddListener(LArmUseExtraChanged);
		rArmUseExtra.onValueChanged.AddListener(RArmUseExtraChanged);

		lLegUseExtra.onValueChanged.AddListener(LLegUseExtraChanged);
		rLegUseExtra.onValueChanged.AddListener(RLegUseExtraChanged);

		firstName.Initialization(new List<string>() { "Iron", "Mege" });
		secondName.Initialization(new List<string>() { "DE", "asd" });
		nickName.Initialization(new List<string>() { "qweq1", "Qwea" });
	}


	/// <summary>
	/// Создание персонажа из базовой болванки
	/// </summary>
	public void CreateFemaleCharacter()
	{
		CreateNewFemaleCharacter();
		Information.data.gender = CharacterGenders.Female;
		SetCharacter(character);
	}

	public void CreateMaleCharacter()
	{
		CharacterRaces races = CharacterRaces.Human;
		if(character != null)
		{
			races = Information.data.race;
		}


		CreateNewMaleCharacter();
		Information.data.gender = CharacterGenders.Male;
		Information.data.race = races;
		SetCharacter(character);
	}

	public void LoadCharacter(CharacterStatisticsData data)
	{
		if(character == null)
		{
			if(data.informationData.gender == CharacterGenders.Female)
				CreateFemaleCharacter();
			else if(data.informationData.gender == CharacterGenders.Male)
				CreateMaleCharacter();
		}

		character.SetStatistics(data);
		SetCharacter(character);
	}

	private void CreateNewFemaleCharacter()
	{
		isFacialhairs.isOn = isFacialhairs.interactable = false;

		character = stand.ReplaceCharacter(characterFemaleBase);

		onCharacterCreated?.Invoke();
	}
	private void CreateNewMaleCharacter()
	{
		isFacialhairs.interactable = true;

		character = stand.ReplaceCharacter(characterMaleBase);

		onCharacterCreated?.Invoke();
	}


	public void SetCharacter(Character character)
	{
		canvasGroup.interactable = character != null;

		if(character)
			Setup();
	}

	private void Setup()
	{
		Debug.LogError(Information.data.race);


		firstName.SetField(Information.data.firstName);
		secondName.SetField(Information.data.secondName);
		nickName.SetField(Information.data.nickName);

		gender.SetEnumOption(Information.data.gender);
		race.SetEnumOption(Information.data.race);


		SetupObjects();
	}

	private void SetupObjects()
	{
		#region Head
		headCarousel.SetCarousel(Avatar.persona.headPiece.headsPiece);
		eyebrowsCarousel.SetCarousel(Avatar.persona.headPiece.eyebrowsPiece);

		earsCarousel.SetCarousel(Avatar.persona.headPiece.earsPiece, Information.data.race == CharacterRaces.Elf);
		hairsCarousel.SetCarousel(Avatar.persona.headPiece.hairsPiece);

		facialHairsCarousel.SetCarousel(Avatar.persona.headPiece.facialHairsPiece, Information.data.gender == CharacterGenders.Male && isFacialhairs.isOn);
		#endregion

		#region Torso
		torsoCarousel.SetCarousel(Avatar.persona.bodyPiece.torsoPiece.torsoPiece);

		lUpperCarousel.SetCarousel(Avatar.persona.bodyPiece.torsoPiece.armLeftPiece.armUpperPiece);
		lLowerCarousel.SetCarousel(Avatar.persona.bodyPiece.torsoPiece.armLeftPiece.armLowerPiece);

		rUpperCarousel.SetCarousel(Avatar.persona.bodyPiece.torsoPiece.armRightPiece.armUpperPiece);
		rLowerCarousel.SetCarousel(Avatar.persona.bodyPiece.torsoPiece.armRightPiece.armLowerPiece);

		lHandCarousel.SetCarousel(Avatar.persona.bodyPiece.torsoPiece.armLeftPiece.handPiece);
		lSholderCarousel.SetCarousel(Avatar.persona.bodyPiece.torsoPiece.armLeftPiece.sholderAttachmentPiece, lArmUseExtra.isOn);
		lElbowCarousel.SetCarousel(Avatar.persona.bodyPiece.torsoPiece.armLeftPiece.elbowAttachmentPiece, lArmUseExtra.isOn);


		rHandCarousel.SetCarousel(Avatar.persona.bodyPiece.torsoPiece.armRightPiece.handPiece);
		rSholderCarousel.SetCarousel(Avatar.persona.bodyPiece.torsoPiece.armRightPiece.sholderAttachmentPiece, rArmUseExtra.isOn);
		rElbowCarousel.SetCarousel(Avatar.persona.bodyPiece.torsoPiece.armRightPiece.elbowAttachmentPiece, rArmUseExtra.isOn);
		#endregion

		#region Hips
		hipsCarousel.SetCarousel(Avatar.persona.bodyPiece.hipsPiece.hipsPiece);

		lLegCarousel.SetCarousel(Avatar.persona.bodyPiece.hipsPiece.legLeftPiece.legPiece);
		lKneeCarousel.SetCarousel(Avatar.persona.bodyPiece.hipsPiece.legLeftPiece.kneeAttachementPiece, lLegUseExtra.isOn);


		rLegCarousel.SetCarousel(Avatar.persona.bodyPiece.hipsPiece.legRightPiece.legPiece);
		rKneeCarousel.SetCarousel(Avatar.persona.bodyPiece.hipsPiece.legRightPiece.kneeAttachementPiece, rLegUseExtra.isOn);
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
		Information.data.race = race;

		Avatar.persona.headPiece.IsCustomEars = Information.data.race != CharacterRaces.Human;

		if(race == CharacterRaces.Elf)
		{
			earsCarousel.Enable();
			earsCarousel.GetComponent<CanvasGroup>().interactable = true;
		}
		else
		{
			earsCarousel.Disable();
			earsCarousel.GetComponent<CanvasGroup>().interactable = false;
		}
	}
	private void GenderChanged(CharacterGenders gender)
	{
		if(gender == CharacterGenders.Female)
			CreateFemaleCharacter();
		else
			CreateMaleCharacter();
	}
	#endregion

	//ДОДЕЛАТЬ

	private void IsFacialHairsChanged(bool trigger)
	{
		Avatar.persona.headPiece.IsHaveFacialHairs =( Information.data.gender == CharacterGenders.Male && trigger);
		if(Information.data.gender == CharacterGenders.Male && trigger)
			facialHairsCarousel.Enable();
		else
			facialHairsCarousel.Disable();
	}
	private void LArmUseExtraChanged(bool trigger)
	{
		//Model.data.body.torso.leftArm.useExtra = trigger;
		//if(trigger)
		//{
		//	lSholderCarousel.Enable();
		//	lElbowCarousel.Enable();
		//}
		//else
		//{
		//	lSholderCarousel.Disable();
		//	lElbowCarousel.Disable();
		//}
	}
	private void RArmUseExtraChanged(bool trigger)
	{
		//Model.data.body.torso.rightArm.useExtra = trigger;
		//if(trigger)
		//{
		//	rSholderCarousel.Enable();
		//	rElbowCarousel.Enable();
		//}
		//else
		//{
		//	rSholderCarousel.Disable();
		//	rElbowCarousel.Disable();
		//}
	}

	private void LLegUseExtraChanged(bool trigger)
	{
		//Model.data.body.hips.leftLeg.useExtra = trigger;
		//if(trigger)
		//{
		//	lKneeCarousel.Enable();
		//}
		//else
		//{
		//	lKneeCarousel.Disable();
		//}
	}
	private void RLegUseExtraChanged(bool trigger)
	{
		//Model.data.body.hips.rightLeg.useExtra = trigger;
		//if(trigger)
		//{
		//	rKneeCarousel.Enable();
		//}
		//else
		//{
		//	rKneeCarousel.Disable();
		//}
	}

	public CharacterStatisticsData GetData()
	{
		return character.GetData();
	}
}