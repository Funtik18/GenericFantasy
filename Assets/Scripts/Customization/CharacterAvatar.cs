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

    public void UpdateCharacter(CharacterStatisticsData data)
	{
        persona.UpdatePersona(data.modelData);

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
    private CharacterModelData modelData;

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
        modelData = data;

        headPiece.UpdatePersona(modelData.head);
        bodyPiece.UpdatePersona(modelData.body);
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
    private CharacterModelHeadData headData;

    public bool IsCustomEars
    {
        set 
        {
            headData.isCustomEars = value;
            earsPiece.CurrentIndex = 0;

            if(value == false)
                earsPiece.DisableAllObjects();
        }
        get => headData.isCustomEars;
    }
    public bool IsHaveFacialHairs
    {
        set
        {
            headData.isHaveFacialHair = value;
            facialHairsPiece.CurrentIndex = 0;

            if(value == false)
                facialHairsPiece.DisableAllObjects();
        }
        get => headData.isHaveFacialHair;
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
        headData = data;

        headsPiece.onIndexChanged += (x) => headData.headIndex = x;
        hairsPiece.onIndexChanged += (x) => headData.hairIndex = x;
        eyebrowsPiece.onIndexChanged += (x) => headData.eyebrowIndex = x;
        facialHairsPiece.onIndexChanged += (x) => headData.facialhairIndex = x;
        earsPiece.onIndexChanged += (x) => headData.earsIndex = x;

        headsPiece.EnableObjectByIndex(headData.headIndex);
		hairsPiece.EnableObjectByIndex(headData.hairIndex);
		eyebrowsPiece.EnableObjectByIndex(headData.eyebrowIndex);

		if(headData.isHaveFacialHair)
			facialHairsPiece.EnableObjectByIndex(headData.facialhairIndex);
		else
			facialHairsPiece.DisableAllObjects();

		if(headData.isCustomEars)
			earsPiece.EnableObjectByIndex(headData.earsIndex);
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
        return headData;
    }
}

[System.Serializable]
public class CharacterBodyPiece
{
    private CharacterModelBodyData bodyData;

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
        bodyData = data;

        torsoPiece.UpdatePersona(bodyData.torso);
        hipsPiece.UpdatePersona(bodyData.hips);
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
    private CharacterModelTorsoData torsoData;

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
    public void UpdatePersona(CharacterModelTorsoData torso)
	{
        torsoData = torso;

        torsoPiece.onIndexChanged += (x) => torsoData.torsoIndex = x;

        torsoPiece.EnableObjectByIndex(torsoData.torsoIndex);

        armLeftPiece.UpdatePersona(torsoData.leftArm);
        armRightPiece.UpdatePersona(torsoData.rightArm);
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
            torsoIndex = torsoData.torsoIndex,
            leftArm= armLeftPiece.GetData(),
            rightArm = armRightPiece.GetData(),
        };

        return data;
    }
}
[System.Serializable]
public class CharacterArmPiece
{
    private CharacterModelArmData armData;

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
    public void UpdatePersona(CharacterModelArmData arm)
	{
        armData = arm;

        armUpperPiece.onIndexChanged += (x) => armData.armUpperIndex = x;
        armLowerPiece.onIndexChanged += (x) => armData.armLowerIndex = x;
        handPiece.onIndexChanged += (x) => armData.handIndex = x;

        sholderAttachmentPiece.onIndexChanged += (x) => armData.sholderAttachmentIndex = x;
        elbowAttachmentPiece.onIndexChanged += (x) => armData.elbowAttachmentIndex = x;


        armUpperPiece.EnableObjectByIndex(armData.armUpperIndex);
        armLowerPiece.EnableObjectByIndex(armData.armLowerIndex);
        handPiece.EnableObjectByIndex(armData.handIndex);

		if(armData.useExtra)
		{
            sholderAttachmentPiece.EnableObjectByIndex(armData.sholderAttachmentIndex);
            elbowAttachmentPiece.EnableObjectByIndex(armData.elbowAttachmentIndex);
		}
		else
		{
            sholderAttachmentPiece.DisableAllObjects();
            elbowAttachmentPiece.DisableAllObjects();
        }
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
        return armData;
    }
}

[System.Serializable]
public class CharacterHipsPiece
{
    private CharacterModelHipsData hipsData;

    public CharacterPiece hipsPiece;
    public CharacterLegPiece legLeftPiece;
    public CharacterLegPiece legRightPiece;

    public void UpdateLists()
    {
        hipsPiece.UpdateList();
        legLeftPiece.UpdateLists();
        legRightPiece.UpdateLists();
    }
    public void UpdatePersona(CharacterModelHipsData hips)
	{
        hipsData = hips;
        hipsPiece.onIndexChanged += (x) => hipsData.hipsIndex = x;

        hipsPiece.EnableObjectByIndex(hipsData.hipsIndex);
        legLeftPiece.UpdatePersona(hipsData.leftLeg);
        legRightPiece.UpdatePersona(hipsData.rightLeg);
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
            hipsIndex = hipsData.hipsIndex,
            leftLeg = legLeftPiece.GetData(),
            rightLeg = legRightPiece.GetData(),
        };

        return data;
    }
}
[System.Serializable]
public class CharacterLegPiece
{
    private CharacterModelLegData legData;

    public CharacterPiece legPiece;
    public CharacterPiece kneeAttachementPiece;

    public void UpdateLists()
    {
        legPiece.UpdateList();
        kneeAttachementPiece.UpdateList();
    }
    public void UpdatePersona(CharacterModelLegData leg)
	{
        legData = leg;

        legPiece.onIndexChanged += (x) => legData.legIndex = x ;
        kneeAttachementPiece.onIndexChanged += (x) => legData.kneeAttachementIndex = x;

        legPiece.EnableObjectByIndex(legData.legIndex);

		if(legData.useExtra)
            kneeAttachementPiece.EnableObjectByIndex(legData.kneeAttachementIndex);
		else
            kneeAttachementPiece.DisableAllObjects();
    }
    public void SetMaterial(Material material)
    {
        legPiece.SetMaterial(material);
        kneeAttachementPiece.SetMaterial(material);
    }

    public CharacterModelLegData GetData()
    {
        return legData;
    }
}

[System.Serializable]
public class CharacterPiece
{
    [SerializeField] private Transform root;

    [ReadOnly][SerializeField] private int currentIndex = -1;
    public int CurrentIndex
    {
		set
		{
            if(value < 0) value += rootPices.Count;

            if(currentIndex != value)
			{
                currentIndex = value % rootPices.Count;

                EnableObjectByIndex(currentIndex);

                onIndexChanged?.Invoke(currentIndex);
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
		}
	}
    public void DisableAllObjects()
	{
		for(int i = 0; i < rootPices.Count; i++)
		{
            rootPices[i].SetActive(false);
		}
        currentIndex = -1;
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
        CurrentIndex = -1;
        currentIndex = -1;
    }
}