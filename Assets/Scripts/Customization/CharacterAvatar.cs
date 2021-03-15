using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;
using UnityEngine.Events;

public class CharacterAvatar : MonoBehaviour
{
    [SerializeField] private Material characterMaterial;
    [HideInInspector] private Material currentMaterial;

    public CharacterPersonaPiece persona = new CharacterPersonaPiece();

    [Button]
    private void UpdateLists()
	{
        persona.UpdateLists();
    }

    [Button]
    private void SetMaterailOrigin()
	{
        persona.SetMaterial(characterMaterial);
    }

    public void UpdateCharacter(CharacterStatisticsData data)
	{
        persona.UpdatePersona(data.modelData);
    }

    public void UpdateMaterial()
	{
        currentMaterial = new Material(characterMaterial);

        //currentMaterial.SetColor("_Color_Primary", new Color(0.2431373f, 0.4196079f, 0.6196079f, 1));
        //currentMaterial.SetColor("_Color_Secondary", new Color(0.8196079f, 0.6431373f, 0.2980392f, 1));
        //currentMaterial.SetColor("_Color_Leather_Primary",new Color(0.282353f, 0.2078432f, 0.1647059f, 1));
        //currentMaterial.SetColor("_Color_Metal_Primary", new Color(0.5960785f, 0.6117647f, 0.627451f, 1));
        //currentMaterial.SetColor("_Color_Leather_Secondary", new Color(0.372549f, 0.3294118f, 0.2784314f, 1));
        //currentMaterial.SetColor("_Color_Metal_Dark", new Color(0.1764706f, 0.1960784f, 0.2156863f, 1));
        //currentMaterial.SetColor("_Color_Metal_Secondary", new Color(0.345098f, 0.3764706f, 0.3960785f, 1));
        //currentMaterial.SetColor("_Color_Hair", );
        persona.SetMaterial(currentMaterial);
    }
}

[System.Serializable]
public class CharacterPersonaPiece
{
    [TabGroup("Head")]
    [HideLabel]
    public CharacterHeadPiece headPiece;
    [TabGroup("Body")]
    [HideLabel]
    public CharacterBodyPiece bodyPiece;

    public void UpdateLists()
	{
        headPiece.UpdateLists();
        bodyPiece.UpdateLists();
    }
    public void UpdatePersona(CharacterModelData data)
	{
        headPiece.UpdatePersona(data.head);
        bodyPiece.UpdatePersona(data.body);
    }
    public void SetMaterial(Material material)
	{
        headPiece.SetMaterial(material);
        bodyPiece.SetMaterial(material);
    }

    public CharacterModelData GetData()
	{
        CharacterModelData data = new CharacterModelData()
        {
            head = headPiece.GetData(),
            body = bodyPiece.GetData(),
        };
        return data;
    }
}

[System.Serializable]
public class CharacterHeadPiece
{
    private bool isCustomEars = false;
    public bool IsCustomEars
    {
        set 
        {
            isCustomEars = value;
            earsPiece.CurrentIndex = 0;

            if(value == false)
                earsPiece.DisableAllObjects();
        }
        get => isCustomEars;
    }

    private bool isHaveFacialHairs = false;
    public bool IsHaveFacialHairs
    {
        set
        {
            isHaveFacialHairs = value;
            facialHairsPiece.CurrentIndex = 0;

            if(value == false)
			{
                facialHairsPiece.DisableAllObjects();
            }
        }
        get => isHaveFacialHairs;
    }

    public CharacterPiece headsPiece;
    public CharacterPiece hairsPiece;
    public CharacterPiece eyebrowsPiece;
    public CharacterPiece facialHairsPiece;
    public CharacterPiece earsPiece;

    public void UpdateLists()
    {
        headsPiece.UpdateList();
        hairsPiece.UpdateList();
        eyebrowsPiece.UpdateList();
        facialHairsPiece.UpdateList();
        earsPiece.UpdateList();
    }
    public void UpdatePersona(CharacterModelHeadData data)
	{
        headsPiece.CurrentIndex = data.headIndex;
		hairsPiece.CurrentIndex = data.hairIndex;
		eyebrowsPiece.CurrentIndex = data.eyebrowIndex;

        IsHaveFacialHairs = data.isHaveFacialHair;
        IsCustomEars = data.isCustomEars;

        if(IsHaveFacialHairs)
			facialHairsPiece.CurrentIndex = data.facialhairIndex;
		else
			facialHairsPiece.DisableAllObjects();

		if(IsCustomEars)
			earsPiece.CurrentIndex = data.earsIndex;
		else
			earsPiece.DisableAllObjects();
	}
	public void SetMaterial(Material material)
    {
        headsPiece.SetMaterial(material);
        hairsPiece.SetMaterial(material);
        eyebrowsPiece.SetMaterial(material);
        facialHairsPiece.SetMaterial(material);
        earsPiece.SetMaterial(material);
    }

    public CharacterModelHeadData GetData()
	{
        CharacterModelHeadData data = new CharacterModelHeadData()
        {
            headIndex = headsPiece.CurrentIndex,
            hairIndex = hairsPiece.CurrentIndex,
            eyebrowIndex = eyebrowsPiece.CurrentIndex,
            isHaveFacialHair = IsHaveFacialHairs,
            facialhairIndex = facialHairsPiece.CurrentIndex,
            isCustomEars = IsCustomEars,
            earsIndex = earsPiece.CurrentIndex,
        };

        return data;
    }
}

[System.Serializable]
public class CharacterBodyPiece
{
    [TabGroup("Torso")]
    [HideLabel]
    public CharacterTorsoPiece torsoPiece;
    [TabGroup("Hips")]
    [HideLabel]
    public CharacterHipsPiece hipsPiece;

    public void UpdateLists()
    {
        torsoPiece.UpdateLists();
        hipsPiece.UpdateLists();
    }
    public void UpdatePersona(CharacterModelBodyData data)
    {
        torsoPiece.UpdatePersona(data.torso);
        hipsPiece.UpdatePersona(data.hips);
    }
    public void SetMaterial(Material material)
    {
        torsoPiece.SetMaterial(material);
        hipsPiece.SetMaterial(material);
    }

    public CharacterModelBodyData GetData()
	{
        CharacterModelBodyData data = new CharacterModelBodyData()
        {
            torso = torsoPiece.GetData(), 
            hips = hipsPiece.GetData(),
        };

        return data;
    }
}

[System.Serializable]
public class CharacterTorsoPiece
{
    public CharacterPiece torsoPiece;

    [TabGroup("LeftArm")]
    [HideLabel]
    public CharacterArmPiece armLeftPiece;
    [TabGroup("RightArm")]
    [HideLabel]
    public CharacterArmPiece armRightPiece;

    public void UpdateLists()
    {
        torsoPiece.UpdateList();

        armLeftPiece.UpdateLists();
        armRightPiece.UpdateLists();
    }
    public void UpdatePersona(CharacterModelTorsoData data)
	{
        torsoPiece.EnableObjectByIndex(data.torsoIndex);

        armLeftPiece.UpdatePersona(data.leftArm);
        armRightPiece.UpdatePersona(data.rightArm);
    }
    public void SetMaterial(Material material)
    {
        torsoPiece.SetMaterial(material);
        armLeftPiece.SetMaterial(material);
        armRightPiece.SetMaterial(material);
    }

    public CharacterModelTorsoData GetData()
    {
        CharacterModelTorsoData data = new CharacterModelTorsoData()
        {
            torsoIndex = torsoPiece.CurrentIndex,
            leftArm= armLeftPiece.GetData(),
            rightArm = armRightPiece.GetData(),
        };

        return data;
    }
}
[System.Serializable]
public class CharacterArmPiece
{
    public CharacterPiece armUpperPiece;
    public CharacterPiece armLowerPiece;
    public CharacterPiece handPiece;

    public CharacterPiece sholderAttachmentPiece;
    public CharacterPiece elbowAttachmentPiece;

    public void UpdateLists()
    {
        armUpperPiece.UpdateList();
        armLowerPiece.UpdateList();
        handPiece.UpdateList();

        sholderAttachmentPiece.UpdateList();
        elbowAttachmentPiece.UpdateList();
    }
    public void UpdatePersona(CharacterModelArmData data)
	{
        armUpperPiece.EnableObjectByIndex(data.armUpperIndex);
        armLowerPiece.EnableObjectByIndex(data.armLowerIndex);
        handPiece.EnableObjectByIndex(data.handIndex);

        if(data.sholderAttachmentIndex == -1)
            sholderAttachmentPiece.DisableAllObjects();
		else
            sholderAttachmentPiece.EnableObjectByIndex(data.sholderAttachmentIndex);

        if(data.elbowAttachmentIndex == -1)
            elbowAttachmentPiece.DisableAllObjects();
		else
            elbowAttachmentPiece.EnableObjectByIndex(data.elbowAttachmentIndex);
    }
    public void SetMaterial(Material material)
    {
        armUpperPiece.SetMaterial(material);
        armLowerPiece.SetMaterial(material);
        handPiece.SetMaterial(material);

        sholderAttachmentPiece.SetMaterial(material);
        elbowAttachmentPiece.SetMaterial(material);
    }

    public CharacterModelArmData GetData()
    {
        CharacterModelArmData data = new CharacterModelArmData()
        {
            armUpperIndex = armUpperPiece.CurrentIndex,
            armLowerIndex = armLowerPiece.CurrentIndex,
            handIndex = handPiece.CurrentIndex,

            sholderAttachmentIndex = sholderAttachmentPiece.CurrentIndex,
            elbowAttachmentIndex = elbowAttachmentPiece.CurrentIndex,
        };

        return data;
    }
}

[System.Serializable]
public class CharacterHipsPiece
{
    public CharacterPiece hipsPiece;
    public CharacterLegPiece legLeftPiece;
    public CharacterLegPiece legRightPiece;

    public void UpdateLists()
    {
        hipsPiece.UpdateList();
        legLeftPiece.UpdateLists();
        legRightPiece.UpdateLists();
    }
    public void UpdatePersona(CharacterModelHipsData data)
	{
        hipsPiece.EnableObjectByIndex(data.hipsIndex);
        legLeftPiece.UpdatePersona(data.leftLeg);
        legRightPiece.UpdatePersona(data.rightLeg);
    }
    public void SetMaterial(Material material)
    {
        hipsPiece.SetMaterial(material);
        legLeftPiece.SetMaterial(material);
        legRightPiece.SetMaterial(material);
    }

    public CharacterModelHipsData GetData()
    {
        CharacterModelHipsData data = new CharacterModelHipsData()
        {
            hipsIndex = hipsPiece.CurrentIndex,
            leftLeg = legLeftPiece.GetData(),
            rightLeg = legRightPiece.GetData(),
        };

        return data;
    }
}
[System.Serializable]
public class CharacterLegPiece
{
    public CharacterPiece legPiece;
    public CharacterPiece kneeAttachementPiece;

    public void UpdateLists()
    {
        legPiece.UpdateList();
        kneeAttachementPiece.UpdateList();
    }
    public void UpdatePersona(CharacterModelLegData data)
	{
        legPiece.EnableObjectByIndex(data.legIndex);

        if(data.kneeAttachementIndex == -1)
            kneeAttachementPiece.DisableAllObjects();
		else
            kneeAttachementPiece.EnableObjectByIndex(data.kneeAttachementIndex);
    }
    public void SetMaterial(Material material)
    {
        legPiece.SetMaterial(material);
        kneeAttachementPiece.SetMaterial(material);
    }

    public CharacterModelLegData GetData()
    {
        CharacterModelLegData data = new CharacterModelLegData()
        {
            legIndex = legPiece.CurrentIndex,
            kneeAttachementIndex = kneeAttachementPiece.CurrentIndex,
        };

        return data;
    }
}

[System.Serializable]
public class CharacterPiece
{
    [SerializeField] private Transform root;

    public int currentIndex = -1;
    public int CurrentIndex
    {
        set
        {
            if(rootPices.Count == 0) return;

            if(value < 0) value += rootPices.Count;

            if(currentIndex != value)
            {
                currentIndex = value % rootPices.Count;

                EnableObjectByIndex(currentIndex);
            }
        }
        get => currentIndex;
    }

    public List<GameObject> rootPices = new List<GameObject>();

    public UnityAction<int> onIndexChanged;

    public List<GameObject> GetGameObjects()
    {
        List<GameObject> gameObjects = new List<GameObject>();
        if(root == null) return gameObjects;

        for(int i = 0; i < root.childCount; i++)
        {
            GameObject go = root.GetChild(i).gameObject;
            gameObjects.Add(go);
        }
        return gameObjects;
    }

    public void EnableObjectByIndex(int index)
	{
        if(rootPices.Count > 0)
		{
            if(index >= 0 && index < rootPices.Count)
			{
                DisableAllObjects();
                currentIndex = index;
                rootPices[currentIndex].SetActive(true);
			}
            onIndexChanged?.Invoke(currentIndex);
        }
    }
    public void DisableAllObjects()
	{
		for(int i = 0; i < rootPices.Count; i++)
		{
            rootPices[i].SetActive(false);
		}
        currentIndex = -1;
        onIndexChanged?.Invoke(currentIndex);
    }

    public void SetMaterial(Material material)
    {
        if(currentIndex >= 0)
        {
            rootPices[currentIndex].GetComponent<SkinnedMeshRenderer>().material = material;
        }
    }
    public void UpdateList()
    {
        rootPices.Clear();
        rootPices.AddRange(GetGameObjects());
    }

    [ButtonGroup]
    private void Left()
	{
        CurrentIndex--;
    }
    [ButtonGroup]
    private void Right()
	{
        CurrentIndex++;
    }
    [ButtonGroup]
    private void DisableAll()
	{
        DisableAllObjects();
    }
}