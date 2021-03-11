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
    public CharacterModelHead head;
    public CharacterModelTorso torso;
    public CharacterModelHips hips;
}
[System.Serializable]
public struct CharacterModelHead
{
    public int headIndex;
    public int earsIndex;
    public int hairIndex;
    public int eyebrowIndex;
    public bool isHaveFacialHair;
    public int facialhairIndex;
}


[System.Serializable]
public struct CharacterModelTorso
{
    public int torsoIndex;

    public CharacterModelArm leftArm;
    public CharacterModelArm rightArm;
}
[System.Serializable]
public struct CharacterModelArm
{
    public int armUpperIndex;
    public int armLowerIndex;
    public int handIndex;

    public bool useExtra;

    public int sholderAttachmentIndex;
    public int elbowAttachmentIndex;
}


[System.Serializable]
public struct CharacterModelHips
{
    public int hipsIndex;

    public CharacterModelLeg leftLeg;
    public CharacterModelLeg rightLeg;
}
[System.Serializable]
public struct CharacterModelLeg
{
    public int legIndex;
    public bool useExtra;
    public int kneeAttachementIndex;
}
#endregion