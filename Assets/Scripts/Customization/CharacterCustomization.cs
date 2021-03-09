using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

public class CharacterCustomization : MonoBehaviour
{

    [SerializeField] private CharacterAvatar avatar;


    [OnValueChanged("ReBuild")]
    [SerializeField] private CharacterGender gender = CharacterGender.Female;
    [OnValueChanged("ReBuild")]
    [SerializeField] private CharacterRace race = CharacterRace.Human;

	#region Colors
	[TabGroup("Colors")]
    [ColorPalette("Country")]
    [OnValueChanged("UpdateColor")]
    [SerializeField] private Color hairColor;

    [TabGroup("Colors")]
    [ColorPalette("Eyes")]
    [OnValueChanged("UpdateColor")]
    [SerializeField] private Color eyesColor;

    [TabGroup("Colors")]
    [ColorPalette("Skins")]
    [OnValueChanged("UpdateColor")]
    [SerializeField] private Color skinColor;

    [TabGroup("Colors")]
    [ColorPalette("Stubbles")]
    [OnValueChanged("UpdateColor")]
    [SerializeField] private Color stubbleColor;

    [TabGroup("Colors")]
    [ColorPalette("Scars")]
    [OnValueChanged("UpdateColor")]
    [SerializeField] private Color scarColor;

    [TabGroup("Colors")]
    [ColorPalette("Underwater")]
    [OnValueChanged("UpdateColor")]
    [SerializeField] private Color artColor;
	#endregion

	[HideLabel]
    [TabGroup("Head")]
    [Title("Base")]
    [SerializeField] private CharacterPart heads;

	[HideLabel]
	[TabGroup("Head")]
    [Title("Hairs")]
    [SerializeField] private CharacterPart hairs;

	[HideLabel]
	[TabGroup("Head")]
    [Title("Eyebrows")]
    [SerializeField] private CharacterPart eyebrows;

    [HideLabel]
    [TabGroup("Head")]
    [Title("Ears")]
    [ShowIf("race", CharacterRace.Elf)]
    [SerializeField] private CharacterPart ears;


    [ShowIf("CheckFacialHair")]
    [TabGroup("Head")]
    [Title("FacialHair")]
    [OnValueChanged("UpdateLists")] 
    [SerializeField] private bool isHaveFacialHair = false;
    [ShowIf("isHaveFacialHair")]
    [HideLabel]
    [TabGroup("Head")]
    [SerializeField] private CharacterPart facialHair;


    [TabGroup("Torso")]
    [Title("Base")]
    [HideLabel]
    [SerializeField] private CharacterPart torso;

    [TabGroup("Torso")]
    [SerializeField] private CharacterArms arms;


    [TabGroup("Hips")]
    [Title("Base")]
    [HideLabel]
    [SerializeField] private CharacterPart hips;

    [TabGroup("Hips")]
    [SerializeField] private CharacterLegs legs;


    [Button]
    public void ReBuild()
	{
        heads.ClearObjects();
        hairs.ClearObjects();
        eyebrows.ClearObjects();
        facialHair.ClearObjects();
        torso.ClearObjects();

        arms.ClearObjects();

        hips.ClearObjects();
        legs.ClearObjects();

        UpdateLists();
    }

    private void UpdateLists()
	{
        hairs.UpdateList(new Transform[1] { avatar.hairs });

        arms.armLeft.hand.UpdateList(new Transform[1] { avatar.handLeft });
        arms.armRight.hand.UpdateList(new Transform[1] { avatar.handRight });

		#region extra

		if(arms.armLeft.useExtra)
		{
            arms.armLeft.sholderAttachment.UpdateList(new Transform[1] { avatar.sholderAttachmentLeft });
            arms.armLeft.elbowAttachment.UpdateList(new Transform[1] { avatar.elbowAttachmentLeft });
		}
		else
		{
            arms.armLeft.sholderAttachment.ClearObjects();
            arms.armLeft.elbowAttachment.ClearObjects();
        }

		if(arms.armRight.useExtra)
		{
            arms.armRight.sholderAttachment.UpdateList(new Transform[1] { avatar.sholderAttachmentRight });
            arms.armRight.elbowAttachment.UpdateList(new Transform[1] { avatar.elbowAttachmentRight });
		}
		else
		{
            arms.armRight.sholderAttachment.ClearObjects();
            arms.armRight.elbowAttachment.ClearObjects();
        }


		if(legs.legLeft.useExtra)
		{
            legs.legLeft.kneeAttachement.UpdateList(new Transform[1] { avatar.kneeAttachementLeft });
        }
        else
		{
            legs.legLeft.kneeAttachement.ClearObjects();
        }
        if(legs.legRight.useExtra)
        {
            legs.legRight.kneeAttachement.UpdateList(new Transform[1] { avatar.kneeAttachementRight });
        }
        else
        {
            legs.legRight.kneeAttachement.ClearObjects();
        }
		#endregion

		if(race == CharacterRace.Elf)
        {
            ears.UpdateList(new Transform[1] { avatar.ears });
		}
		else
		{
            ears.ClearObjects();
		}

        if(gender == CharacterGender.Male)
		{
            heads.UpdateList(new Transform[1] { avatar.maleHeads });
            eyebrows.UpdateList(new Transform[1] { avatar.maleEyebrows });
            
            if(isHaveFacialHair)
                facialHair.UpdateList(new Transform[1] { avatar.maleFacialHairs });
            else
                facialHair.ClearObjects();

            torso.UpdateList(new Transform[1] { avatar.maleTorso });

            arms.armLeft.armUpper.UpdateList(new Transform[1] { avatar.maleArmUpperLeft });
            arms.armRight.armUpper.UpdateList(new Transform[1] { avatar.maleArmUpperRight });

            arms.armLeft.armLower.UpdateList(new Transform[1] { avatar.maleArmLowerLeft });
            arms.armRight.armLower.UpdateList(new Transform[1] { avatar.maleArmLowerRight });

            hips.UpdateList(new Transform[1] { avatar.maleHips });

            legs.legLeft.leg.UpdateList(new Transform[1] { avatar.maleLegLeft });
            legs.legRight.leg.UpdateList(new Transform[1] { avatar.maleLegRight });
        }
        else if(gender == CharacterGender.Female)
		{
            heads.UpdateList(new Transform[1] {avatar.femaleHeads });
            eyebrows.UpdateList(new Transform[1] { avatar.femaleEyebrows });

            isHaveFacialHair = false;
            facialHair.ClearObjects();

            torso.UpdateList(new Transform[1] { avatar.femaleTorso });

            arms.armLeft.armUpper.UpdateList(new Transform[1] { avatar.femaleArmUpperLeft });
            arms.armRight.armUpper.UpdateList(new Transform[1] { avatar.femaleArmUpperRight });

            arms.armLeft.armLower.UpdateList(new Transform[1] { avatar.femaleArmLowerLeft });
            arms.armRight.armLower.UpdateList(new Transform[1] { avatar.femaleArmLowerRight });

            hips.UpdateList(new Transform[1] { avatar.femaleHips });

            legs.legLeft.leg.UpdateList(new Transform[1] { avatar.femaleLegLeft });
            legs.legRight.leg.UpdateList(new Transform[1] { avatar.femaleLegRight });
        }
        else
		{
            heads.UpdateList(new Transform[2] { avatar.maleHeads, avatar.femaleHeads });
            eyebrows.UpdateList(new Transform[2] { avatar.maleEyebrows, avatar.femaleEyebrows });

            if(isHaveFacialHair)
                facialHair.UpdateList(new Transform[1] { avatar.maleFacialHairs });
            else
                facialHair.ClearObjects();

            torso.UpdateList(new Transform[2] { avatar.maleTorso, avatar.femaleTorso });

			arms.armLeft.armUpper.UpdateList(new Transform[2] { avatar.maleArmUpperLeft, avatar.femaleArmUpperLeft });
            arms.armRight.armUpper.UpdateList(new Transform[2] { avatar.maleArmUpperRight, avatar.femaleArmUpperRight });

            arms.armLeft.armLower.UpdateList(new Transform[2] { avatar.maleArmLowerLeft, avatar.femaleArmLowerLeft });
            arms.armRight.armLower.UpdateList(new Transform[2] { avatar.maleArmLowerRight, avatar.femaleArmLowerRight });

			hips.UpdateList(new Transform[2] { avatar.maleHips, avatar.femaleHips });

            legs.legLeft.leg.UpdateList(new Transform[2] { avatar.maleLegLeft, avatar.femaleLegLeft });
            legs.legRight.leg.UpdateList(new Transform[2] { avatar.maleLegRight, avatar.femaleLegRight });
        }
    }

    private void UpdateColor()
	{
        Material mat = avatar.characterMaterial;
        mat.SetColor("_Color_Hair", hairColor);
        mat.SetColor("_Color_Eyes", eyesColor);
        mat.SetColor("_Color_Skin", skinColor);
        mat.SetColor("_Color_Stubble", stubbleColor);
        mat.SetColor("_Color_Scar", scarColor);
        mat.SetColor("_Color_BodyArt", artColor);
    }

    private bool CheckFacialHair()
    {
        return (gender == CharacterGender.Male || gender == CharacterGender.TransNigga) && race != CharacterRace.Elf;
    }

    [Button]
    [TabGroup("Torso")]
    private void LeftArms()
    {
        arms.armLeft.Left();
        arms.armRight.Left();
    }
    [Button]
    [TabGroup("Torso")]
    private void RightArms()
    {
        arms.armLeft.Right();
        arms.armRight.Right();
    }

    [Button]
    [TabGroup("Hips")]
    private void LeftLegs()
    {
        legs.legLeft.leg.Left();
        legs.legRight.leg.Left();
    }
    [Button]
    [TabGroup("Hips")]
    private void RightLegs()
    {
        legs.legLeft.leg.Right();
        legs.legRight.leg.Right();
    }

    [System.Serializable]
    public class CharacterArms
	{
        [TitleGroup("Arms")]
        [HorizontalGroup("Arms/Split", Width = 0.5f)]
        [TabGroup("Arms/Split/Arms", "ArmLeft")]
        [HideLabel]
        public CharacterArm armLeft;
        [TabGroup("Arms/Split/Arm", "ArmRight")]
        [HideLabel]
        public CharacterArm armRight;

        public void ClearObjects()
		{
            armLeft.ClearObjects();
            armRight.ClearObjects();
        }
    }

    [System.Serializable]
    public class CharacterArm
	{
        public CharacterPart armUpper;
        [Space]
        public CharacterPart armLower;
        [Space]
        public CharacterPart hand;

        [OnValueChanged("UPD")]
        public bool useExtra = false;

        [Title("Extra")]
        [ShowIf("useExtra")]
        public CharacterPart sholderAttachment;
        [ShowIf("useExtra")]
        public CharacterPart elbowAttachment;


        private void UPD()
		{
            FindObjectOfType<CharacterCustomization>().ReBuild();
        }

        public void ClearObjects()
		{
            armUpper.ClearObjects();
            armLower.ClearObjects();
            hand.ClearObjects();

            sholderAttachment.ClearObjects();
            elbowAttachment.ClearObjects();
        }

        [ButtonGroup]
        public void Left()
        {
            armUpper.Left();
            armLower.Left();
            hand.Left();
        }
        [ButtonGroup]
        public void Right()
        {
            armUpper.Right();
            armLower.Right();
            hand.Right();
        }
    }

    [System.Serializable]
    public class CharacterLegs
	{

        [TitleGroup("Legs")]
        [HorizontalGroup("Legs/Split", Width = 0.5f)]
        [TabGroup("Legs/Split/Arms", "LegLeft")]
        [HideLabel]
        public CharacterLeg legLeft;
        [TabGroup("Legs/Split/Arm", "LegRight")]
        [HideLabel]
        public CharacterLeg legRight;

        public void ClearObjects()
		{
            legLeft.ClearObjects();
            legRight.ClearObjects();
        }
    }
    [System.Serializable]
    public class CharacterLeg
	{
        [OnValueChanged("UPD")]
        public bool useExtra = false;

        public CharacterPart leg;

        [ShowIf("useExtra")]
        public CharacterPart kneeAttachement;

        private void UPD()
		{
            FindObjectOfType<CharacterCustomization>().ReBuild();
		}

        public void ClearObjects()
		{
            leg.ClearObjects();
            kneeAttachement.ClearObjects();
        }
    }

    [System.Serializable]
    public class CharacterPart
	{
        private List<GameObject> objects = new List<GameObject>();

        private int currentIndex = 0;
        private int CurrentIndex
		{
			set {
			
                if(value >= objects.Count)
				{
                    currentIndex = objects.Count - value;
				}
                else if(value < 0)
				{
                    currentIndex = value + objects.Count;
				}
				else
				{
                    currentIndex = value;
                }
            }
            get => currentIndex;
		}

        [ButtonGroup]
        public void Left()
        {
            if(objects.Count > 0)
			{
                DeselectObject();
                CurrentIndex--;
                SelectObject();
            }
        }
        [ButtonGroup]
        public void Right()
        {
            if(objects.Count > 0)
			{
                DeselectObject();
                CurrentIndex++;
                SelectObject();
            }
        }

        private void SelectObject()
		{
            objects[CurrentIndex].SetActive(true);
        }
        private void DeselectObject()
        {
            objects[CurrentIndex].SetActive(false);
        }

        public void UpdateList(Transform[] roots)
        {
            for(int i = 0; i < roots.Length; i++)
			{
                for(int j = 0; j < roots[i].childCount; j++)
                {
                    GameObject go = roots[i].GetChild(j).gameObject;
                    objects.Add(go);
                }
            }
            SelectObject();
        }

        public void ClearObjects()
		{
            CurrentIndex = 0;

            if(objects.Count > 0)
			{
				for(int i = 0; i < objects.Count; i++)
				{
                    objects[i].SetActive(false);
				}
			}

            objects.Clear();
        }
    }
}
public enum CharacterGender { Male, Female, TransNigga }
public enum CharacterRace { Human, Elf }