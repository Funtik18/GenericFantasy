public class CharacterModel
{
	public CharacterModelData data;

    public CharacterModel(CharacterModelData modelData)
	{
		this.data = modelData;
	}
}
#region CharacterParts
[System.Serializable]
public struct CharacterModelData
{
    public CharacterModelHeadData head;
    public CharacterModelBodyData body;
}
[System.Serializable]
public struct CharacterModelHeadData
{
    public int headIndex;
    public bool isCustomEars;
    public int earsIndex;
    public int hairIndex;
    public int eyebrowIndex;
    public bool isHaveFacialHair;
    public int facialhairIndex;
}

[System.Serializable]
public struct CharacterModelBodyData
{
    public CharacterModelTorsoData torso;
    public CharacterModelHipsData hips;
}


[System.Serializable]
public struct CharacterModelTorsoData
{
    public int torsoIndex;

    public CharacterModelArmData leftArm;
    public CharacterModelArmData rightArm;
}
[System.Serializable]
public struct CharacterModelArmData
{
    public int armUpperIndex;
    public int armLowerIndex;
    public int handIndex;

    public int sholderAttachmentIndex;
    public int elbowAttachmentIndex;
}


[System.Serializable]
public struct CharacterModelHipsData
{
    public int hipsIndex;

    public CharacterModelLegData leftLeg;
    public CharacterModelLegData rightLeg;
}
[System.Serializable]
public struct CharacterModelLegData
{
    public int legIndex;

    public int kneeAttachementIndex;
}
#endregion