using System.Collections;
using System.Collections.Generic;

using Sirenix.OdinInspector;

using UnityEngine;

public class CharacterCustomization : MonoBehaviour
{
    [OnValueChanged("UpdateLists")]
    [SerializeField] private CharacterGender gender = CharacterGender.Female;

    [SerializeField] private CharacterAvatar avatar;

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

    private bool CheckFacialHair()
    {
        return (gender == CharacterGender.Male || gender == CharacterGender.TransNigga);
    }

    [Button]
    private void ReBuild()
	{
        Clear();

        heads.CheckObjects();
        hairs.CheckObjects();
        eyebrows.CheckObjects();
        facialHair.CheckObjects();
        torso.CheckObjects();

        arms.CheckObjects();

        hips.CheckObjects();
        legs.CheckObjects();

        UpdateLists();
    }
    private void Clear()
	{
        heads.ClearObjects();
        hairs.ClearObjects();
        eyebrows.ClearObjects();
        facialHair.ClearObjects();
        torso.ClearObjects();

        arms.ClearObjects();

        hips.ClearObjects();
        legs.ClearObjects();
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
        legs.legLeft.Left();
        legs.legRight.Left();
    }
    [Button]
    [TabGroup("Hips")]
    private void RightLegs()
	{
        legs.legLeft.Right();
        legs.legRight.Right();
    }

    private void UpdateLists()
	{
        hairs.UpdateList(new Transform[1] { avatar.hairs });

        arms.armLeft.hand.UpdateList(new Transform[1] { avatar.handLeft });
        arms.armRight.hand.UpdateList(new Transform[1] { avatar.handRight });

        if(gender == CharacterGender.Male)
		{
            heads.UpdateList(new Transform[1] { avatar.maleHeads });
            eyebrows.UpdateList(new Transform[1] { avatar.maleEyebrows });
            
            if(isHaveFacialHair)
                facialHair.UpdateList(new Transform[1] { avatar.maleFacialHairs });
            else
                facialHair.CheckObjects();

            torso.UpdateList(new Transform[1] { avatar.maleTorso });

            arms.armLeft.armUpper.UpdateList(new Transform[1] { avatar.maleArmUpperLeft });
            arms.armRight.armUpper.UpdateList(new Transform[1] { avatar.maleArmUpperRight });

            arms.armLeft.armLower.UpdateList(new Transform[1] { avatar.maleArmLowerLeft });
            arms.armRight.armLower.UpdateList(new Transform[1] { avatar.maleArmLowerRight });

            hips.UpdateList(new Transform[1] { avatar.maleHips });

            legs.legLeft.UpdateList(new Transform[1] { avatar.maleLegLeft });
            legs.legRight.UpdateList(new Transform[1] { avatar.maleLegRight });
        }
        else if(gender == CharacterGender.Female)
		{
            heads.UpdateList(new Transform[1] {avatar.femaleHeads });
            eyebrows.UpdateList(new Transform[1] { avatar.femaleEyebrows });

            isHaveFacialHair = false;
            facialHair.CheckObjects();

            torso.UpdateList(new Transform[1] { avatar.femaleTorso });

            arms.armLeft.armUpper.UpdateList(new Transform[1] { avatar.femaleArmUpperLeft });
            arms.armRight.armUpper.UpdateList(new Transform[1] { avatar.femaleArmUpperRight });

            arms.armLeft.armLower.UpdateList(new Transform[1] { avatar.femaleArmLowerLeft });
            arms.armRight.armLower.UpdateList(new Transform[1] { avatar.femaleArmLowerRight });

            hips.UpdateList(new Transform[1] { avatar.femaleHips });

            legs.legLeft.UpdateList(new Transform[1] { avatar.femaleLegLeft });
            legs.legRight.UpdateList(new Transform[1] { avatar.femaleLegRight });
        }
        else
		{
            heads.UpdateList(new Transform[2] { avatar.maleHeads, avatar.femaleHeads });
            eyebrows.UpdateList(new Transform[2] { avatar.maleEyebrows, avatar.femaleEyebrows });

            if(isHaveFacialHair)
                facialHair.UpdateList(new Transform[1] { avatar.maleFacialHairs });
            else
                facialHair.CheckObjects();

            torso.UpdateList(new Transform[2] { avatar.maleTorso, avatar.femaleTorso });

            //armLeft.armUpper.UpdateList(new Transform[2] { avatar.maleArmUpperLeft, avatar.femaleArmUpperLeft });
            //armRight.armUpper.UpdateList(new Transform[2] { avatar.maleArmUpperRight, avatar.femaleArmUpperRight });

            //armLeft.armLower.UpdateList(new Transform[2] { avatar.maleArmLowerLeft, avatar.femaleArmLowerLeft });
            //armRight.armLower.UpdateList(new Transform[2] { avatar.maleArmLowerRight, avatar.femaleArmLowerRight });
		
            hips.UpdateList(new Transform[2] { avatar.maleHips, avatar.femaleHips });

            legs.legLeft.UpdateList(new Transform[2] { avatar.maleLegLeft, avatar.femaleLegLeft });
            legs.legRight.UpdateList(new Transform[2] { avatar.maleLegRight, avatar.femaleLegRight });
        }
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

        public void CheckObjects()
        {
            armLeft.CheckObjects();
            armRight.CheckObjects();
        }

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

        public void CheckObjects()
		{
            armUpper.CheckObjects();
            armLower.CheckObjects();
            hand.CheckObjects();
        }

        public void ClearObjects()
		{
            armUpper.ClearObjects();
            armLower.ClearObjects();
            hand.ClearObjects();
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
        public CharacterPart legLeft;
        [TabGroup("Legs/Split/Arm", "LegRight")]
        [HideLabel]
        public CharacterPart legRight;

        public void CheckObjects()
		{
            legLeft.CheckObjects();
            legRight.CheckObjects();
        }
        public void ClearObjects()
		{
            legLeft.ClearObjects();
            legRight.ClearObjects();
        }
    }

    [System.Serializable]
    public class CharacterPart
	{
        private List<GameObject> objects;

        private int currentIndex = 0;
        private int CurrentIndex
		{
			set
			{
                if(value >= objects.Count)
				{
                    currentIndex = objects.Count - value;
				}else if(value < 0)
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
            DeselectObject();
            CurrentIndex--;
            SelectObject();
        }
        [ButtonGroup]
        public void Right()
        {
            DeselectObject();
            CurrentIndex++;
            SelectObject();
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
            CheckObjects();

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
        public void CheckObjects()
		{
            if(objects != null)
                if(objects.Count > 0)
                    DeselectObject();

            ClearObjects();
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