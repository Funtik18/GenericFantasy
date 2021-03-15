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
	private CharacterInformationData Information => character.Information;

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

		#region Actions
		firstName.onValueChanged = FirstNameChanged;
		secondName.onValueChanged = SecondNameChanged;
		nickName.onValueChanged = NickNameChanged;

		race.onEnumChanged = RaceChanged;
		gender.onEnumChanged = GenderChanged;
		
		isFacialhairs.onValueChanged.AddListener(IsFacialHairsChanged);
		#endregion


		firstName.Initialization(new List<string>() { "Iron", "Mege" });
		secondName.Initialization(new List<string>() { "DE", "asd" });
		nickName.Initialization(new List<string>() { "qweq1", "Qwea" });
	}

	#region Create Character
	public void LoadCharacter(CharacterData data)
	{
		if(character == null)
		{
			if(data.information.gender == CharacterGenders.Female)
				CreateNewFemaleCharacter();
			else
				CreateNewMaleCharacter();
		}
		else
		{
			if(Information.gender != data.information.gender)
			{
				if(data.information.gender == CharacterGenders.Female)
					CreateNewFemaleCharacter();
				else
					CreateNewMaleCharacter();
			}
		}

		character.SetCharacter(data);//update avatar from model
		SetCharacter(character);
	}

	//Болванка
	private void CreateNewFemaleCharacter()
	{
		character = stand.ReplaceCharacter(characterFemaleBase);

		onCharacterCreated?.Invoke();
	}
	//Болванка
	private void CreateNewMaleCharacter()
	{
		character = stand.ReplaceCharacter(characterMaleBase);

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
		firstName.SetField(Information.firstName);
		secondName.SetField(Information.secondName);
		nickName.SetField(Information.nickName);

		race.SetEnumOption(Information.race);
		gender.SetEnumOption(Information.gender);

		SetupObjects();
	}

	private void SetupObjects()
	{
		//берём все кусочки модели
		CharacterHeadPiece headPiece = Avatar.persona.headPiece;
		CharacterBodyPiece bodyPiece = Avatar.persona.bodyPiece;
		CharacterTorsoPiece torsoPiece = bodyPiece.torsoPiece;
		CharacterHipsPiece hipsPiece = bodyPiece.hipsPiece;

		#region Head
		headCarousel.SetCarousel(headPiece.headsPiece);
		eyebrowsCarousel.SetCarousel(headPiece.eyebrowsPiece);

		earsCarousel.SetCarousel(headPiece.earsPiece);
		earsCarousel.IsEnable = Information.race == CharacterRaces.Elf;

		hairsCarousel.SetCarousel(headPiece.hairsPiece);

		//Facial
		facialHairsCarousel.SetCarousel(headPiece.facialHairsPiece, true);
		
		isFacialhairs.interactable = Information.gender == CharacterGenders.Male;
		isFacialhairs.isOn = headPiece.facialHairsPiece.CurrentIndex != -1 && isFacialhairs.interactable;

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
		Information.firstName = value;
	}
	private void SecondNameChanged(string value)
	{
		Information.secondName = value;
	}
	private void NickNameChanged(string value)
	{
		Information.nickName = value;
	}

	private void RaceChanged(CharacterRaces race)
	{
		earsCarousel.GetComponent<CanvasGroup>().interactable = earsCarousel.IsEnable = race == CharacterRaces.Elf;

		if(race != Information.race)
		{
			Information.race = race;
			Avatar.persona.headPiece.earsPiece.CurrentIndex = race == CharacterRaces.Human? -1 : Avatar.persona.headPiece.earsPiece.CurrentIndex;
		}
	}
	private void GenderChanged(CharacterGenders gender)
	{
		if(gender != Information.gender)
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
		if(trigger)
		{
		}
		else
		{
			Avatar.persona.headPiece.facialHairsPiece.DisableAll();
		}
		//if(Avatar.persona.headPiece.IsHaveFacialHairs != trigger)
		//	facialHairsCarousel.IsEnable = Avatar.persona.headPiece.IsHaveFacialHairs = trigger;
	}
}