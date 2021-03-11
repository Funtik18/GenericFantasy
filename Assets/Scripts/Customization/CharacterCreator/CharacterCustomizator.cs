using System.Collections;
using System.Collections.Generic;

using Sirenix.OdinInspector;

using UnityEngine;
using UnityEngine.UI;

public class CharacterCustomizator : MonoBehaviour
{
	private Character character;
	private CharacterAvatar Avatar => character.avatar;
	private CharacterInformation Information => character.Statistics.information;
	private CharacterModel Model => character.Statistics.model;

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

	public void SetCharacter(Character character)
	{
		this.character = character;
		canvasGroup.interactable = this.character != null;

		if(this.character)
			Setup();
	}
	public void SetCharacterData(CharacterStatisticsData data)
	{
		if(character == null)
		{
			Debug.LogError("ERROR", this);
		}
		else
		{
			Debug.LogError("I'm Here");
			character.SetStatistics(data);
		}
	}


	private void Setup()
	{
		firstName.onValueChanged = FirstNameChanged;
		secondName.onValueChanged = SecondNameChanged;
		nickName.onValueChanged = NickNameChanged;

		race.onEnumChanged = RaceChanged;
		gender.onEnumChanged = GenderChanged;

		#region Head
		headCarousel.onIndexChanged = HeadChanged;
		earsCarousel.onIndexChanged = EarsChanged;
		hairsCarousel.onIndexChanged = HairsChanged;
		eyebrowsCarousel.onIndexChanged = EyeBrowsChanged;
		isFacialhairs.onValueChanged.AddListener(IsFacialHairsChanged);
		facialHairsCarousel.onIndexChanged = FacialHairsChanged;
		#endregion

		#region Torso
		torsoCarousel.onIndexChanged = TorsoChanged;
		//larm
		lUpperCarousel.onIndexChanged = LUpperChanged;
		lLowerCarousel.onIndexChanged = LLowerChanged;
		lHandCarousel.onIndexChanged = LHandChanged;
		lArmUseExtra.onValueChanged.AddListener(LArmUseExtraChanged);
		lSholderCarousel.onIndexChanged = LSholderChanged;
		lElbowCarousel.onIndexChanged = LElbowChanged;
		//rarm
		rUpperCarousel.onIndexChanged = RUpperChanged;
		rLowerCarousel.onIndexChanged = RLowerChanged;
		rHandCarousel.onIndexChanged = RHandChanged;
		rArmUseExtra.onValueChanged.AddListener(RArmUseExtraChanged);
		rSholderCarousel.onIndexChanged = RSholderChanged;
		rElbowCarousel.onIndexChanged = RElbowChanged;
		#endregion

		#region Hips
		hipsCarousel.onIndexChanged = HipsChanged;
		//lleg
		lLegCarousel.onIndexChanged = LLegChanged;
		lLegUseExtra.onValueChanged.AddListener(LLegUseExtraChanged);
		lKneeCarousel.onIndexChanged = LKneeChanged;
		//rleg
		rLegCarousel.onIndexChanged = RLegChanged;
		rLegUseExtra.onValueChanged.AddListener(RLegUseExtraChanged);
		rKneeCarousel.onIndexChanged = RKneeChanged;
		#endregion

		firstName.Initialization(new List<string>() { "Iron", "Mege" });
		firstName.SetField(Information.firstName);

		secondName.Initialization(new List<string>() { "DE", "asd" });
		firstName.SetField(Information.secondName);

		nickName.Initialization(new List<string>() { "qweq1", "Qwea" });
		firstName.SetField(Information.nickName);

		race.SetEnumOption(Information.race);

		gender.SetEnumOption(CharacterGenders.Female);///problem
		gender.SetEnumOption(Information.gender);
	}

	private void SetupObjects()
	{
		#region Head
		if(Information.gender == CharacterGenders.Female)
		{
			headCarousel.SetCarousel(GetGameObjects(Avatar.femaleHeads));
			eyebrowsCarousel.SetCarousel(GetGameObjects(Avatar.femaleEyebrows));
		}
		else
		{
			headCarousel.SetCarousel(GetGameObjects(Avatar.maleHeads));
			eyebrowsCarousel.SetCarousel(GetGameObjects(Avatar.maleEyebrows));
		}

		earsCarousel.SetCarousel(GetGameObjects(Avatar.ears), Information.race == CharacterRaces.Elf);
		hairsCarousel.SetCarousel(GetGameObjects(Avatar.hairs));

		facialHairsCarousel.SetCarousel(GetGameObjects(Avatar.maleFacialHairs), Information.gender == CharacterGenders.Male && isFacialhairs.isOn);
		#endregion

		#region Torso
		if(Information.gender == CharacterGenders.Female)
		{
			torsoCarousel.SetCarousel(GetGameObjects(Avatar.femaleTorso));

			lUpperCarousel.SetCarousel(GetGameObjects(Avatar.femaleArmUpperLeft));
			lLowerCarousel.SetCarousel(GetGameObjects(Avatar.femaleArmLowerLeft));

			rUpperCarousel.SetCarousel(GetGameObjects(Avatar.femaleArmUpperRight));
			rLowerCarousel.SetCarousel(GetGameObjects(Avatar.femaleArmLowerRight));
		}

		else
		{
			torsoCarousel.SetCarousel(GetGameObjects(Avatar.maleTorso));

			lUpperCarousel.SetCarousel(GetGameObjects(Avatar.maleArmUpperLeft));
			lLowerCarousel.SetCarousel(GetGameObjects(Avatar.maleArmLowerLeft));

			rUpperCarousel.SetCarousel(GetGameObjects(Avatar.maleArmUpperRight));
			rLowerCarousel.SetCarousel(GetGameObjects(Avatar.maleArmLowerRight));
		}
		
		lHandCarousel.SetCarousel(GetGameObjects(Avatar.handLeft));
		lSholderCarousel.SetCarousel(GetGameObjects(Avatar.sholderAttachmentLeft), lArmUseExtra.isOn);
		lElbowCarousel.SetCarousel(GetGameObjects(Avatar.elbowAttachmentLeft), lArmUseExtra.isOn);


		rHandCarousel.SetCarousel(GetGameObjects(Avatar.handRight));
		rSholderCarousel.SetCarousel(GetGameObjects(Avatar.sholderAttachmentRight), rArmUseExtra.isOn);
		rElbowCarousel.SetCarousel(GetGameObjects(Avatar.elbowAttachmentRight), rArmUseExtra.isOn);
		#endregion

		#region Hips
		if(Information.gender == CharacterGenders.Female)
		{
			hipsCarousel.SetCarousel(GetGameObjects(Avatar.femaleHips));
			lLegCarousel.SetCarousel(GetGameObjects(Avatar.femaleLegLeft));
			rLegCarousel.SetCarousel(GetGameObjects(Avatar.femaleLegRight));
		}
		else
		{
			hipsCarousel.SetCarousel(GetGameObjects(Avatar.maleHips));
			lLegCarousel.SetCarousel(GetGameObjects(Avatar.maleLegLeft));
			rLegCarousel.SetCarousel(GetGameObjects(Avatar.maleLegRight));
		}

		lKneeCarousel.SetCarousel(GetGameObjects(Avatar.kneeAttachementLeft), lLegUseExtra.isOn);

		rKneeCarousel.SetCarousel(GetGameObjects(Avatar.kneeAttachementRight), rLegUseExtra.isOn);
		#endregion
	}
	private List<GameObject> GetGameObjects(Transform root)
	{
		List<GameObject> gameObjects = new List<GameObject>();
		for(int i = 0; i < root.childCount; i++)
		{
			GameObject go = root.GetChild(i).gameObject;
			gameObjects.Add(go);
		}
		return gameObjects;
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
		Information.race = race;

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
		Information.gender = gender;

		if(gender == CharacterGenders.Female)
			isFacialhairs.isOn = isFacialhairs.interactable = false;
		else
			isFacialhairs.interactable = true;

		SetupObjects();
	}
	#endregion

	#region Head
	private void HeadChanged(int index)
	{
		Model.data.head.headIndex = index;
	}
	private void EarsChanged(int index)
	{
		Model.data.head.earsIndex = index;
	}
	private void HairsChanged(int index)
	{
		Model.data.head.hairIndex = index;
	}
	private void EyeBrowsChanged(int index)
	{
		Model.data.head.eyebrowIndex = index;
	}
	private void IsFacialHairsChanged(bool trigger)
	{
		Model.data.head.isHaveFacialHair = trigger;

		if(Information.gender == CharacterGenders.Male && trigger)
		{
			facialHairsCarousel.Enable();
		}
		else
		{
			facialHairsCarousel.Disable();
		}
	}
	private void FacialHairsChanged(int index)
	{
		Model.data.head.facialhairIndex = index;
	}
	#endregion

	#region Torso
	private void TorsoChanged(int index)
	{
		Model.data.torso.torsoIndex = index;
	}

	private void LUpperChanged(int index)
	{
		Model.data.torso.leftArm.armUpperIndex = index;
	}
	private void LLowerChanged(int index)
	{
		Model.data.torso.leftArm.armLowerIndex = index;
	}
	private void LHandChanged(int index)
	{
		Model.data.torso.leftArm.handIndex = index;
	}
	
	private void LArmUseExtraChanged(bool trigger)
	{
		Model.data.torso.leftArm.useExtra = trigger;
		if(trigger)
		{
			lSholderCarousel.Enable();
			lElbowCarousel.Enable();
		}
		else
		{
			lSholderCarousel.Disable();
			lElbowCarousel.Disable();
		}
	}

	private void LSholderChanged(int index)
	{
		Model.data.torso.leftArm.sholderAttachmentIndex = index;
	}
	private void LElbowChanged(int index)
	{
		Model.data.torso.leftArm.elbowAttachmentIndex = index;
	}


	private void RUpperChanged(int index)
	{
		Model.data.torso.rightArm.armUpperIndex = index;
	}
	private void RLowerChanged(int index)
	{
		Model.data.torso.rightArm.armLowerIndex = index;
	}
	private void RHandChanged(int index)
	{
		Model.data.torso.rightArm.handIndex = index;
	}

	private void RArmUseExtraChanged(bool trigger)
	{
		Model.data.torso.rightArm.useExtra = trigger;
		if(trigger)
		{
			rSholderCarousel.Enable();
			rElbowCarousel.Enable();
		}
		else
		{
			rSholderCarousel.Disable();
			rElbowCarousel.Disable();
		}
	}

	private void RSholderChanged(int index)
	{
		Model.data.torso.rightArm.sholderAttachmentIndex = index;
	}
	private void RElbowChanged(int index)
	{
		Model.data.torso.rightArm.elbowAttachmentIndex = index;
	}
	#endregion

	#region Hips
	private void HipsChanged(int index)
	{
		Model.data.hips.hipsIndex = index;
	}

	private void LLegChanged(int index)
	{
		Model.data.hips.leftLeg.legIndex = index;
	}
	private void LLegUseExtraChanged(bool trigger)
	{
		Model.data.hips.leftLeg.useExtra = trigger;
		if(trigger)
		{
			lKneeCarousel.Enable();
		}
		else
		{
			lKneeCarousel.Disable();
		}
	}
	private void LKneeChanged(int index)
	{
		Model.data.hips.leftLeg.legIndex = index;
	}

	private void RLegChanged(int index)
	{
		Model.data.hips.rightLeg.legIndex = index;
	}
	private void RLegUseExtraChanged(bool trigger)
	{
		Model.data.hips.rightLeg.useExtra = trigger;
		if(trigger)
		{
			rKneeCarousel.Enable();
		}
		else
		{
			rKneeCarousel.Disable();
		}
	}
	private void RKneeChanged(int index)
	{
		Model.data.hips.rightLeg.legIndex = index;
	}
	#endregion

	public CharacterStatisticsData GetData()
	{
		return character.GetData();
	}
}