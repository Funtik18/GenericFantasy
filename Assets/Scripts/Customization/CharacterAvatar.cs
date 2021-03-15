using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;
using UnityEngine.Events;

public class CharacterAvatar : MonoBehaviour
{
    [SerializeField] private Character character;


    [SerializeField] private Material characterMaterial;
    [HideInInspector] private Material currentMaterial;
   

    public CharacterPersonaPiece persona;

    public void LoadModel()
    {
        persona.UpdatePersona(character.data.model);
    }
    public void SaveModel()
    {
        character.data.model = persona.GetData();
    }

    [Button]
    private void SetMaterailOrigin()
    {
        persona.SetMaterial(characterMaterial);
    }

    [Button]
    private void UpdateLists()
	{
        persona.UpdateLists();
    }
    [GUIColor(0.85f,0.15f,0.15f)]
    [Button]
    private void UpdateData()
	{
        SaveModel();
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


#region Persona
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
        facialHairsPiece.CurrentIndex = data.facialhairIndex;
        earsPiece.CurrentIndex = data.earsIndex;
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
            facialhairIndex = facialHairsPiece.CurrentIndex,
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
        torsoPiece.CurrentIndex = data.torsoIndex;

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
        armUpperPiece.CurrentIndex = data.armUpperIndex;
        armLowerPiece.CurrentIndex = data.armLowerIndex;
        handPiece.CurrentIndex = data.handIndex;


        sholderAttachmentPiece.CurrentIndex = data.sholderAttachmentIndex;
        elbowAttachmentPiece.CurrentIndex = data.elbowAttachmentIndex;
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
        hipsPiece.CurrentIndex = data.hipsIndex;
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
        legPiece.CurrentIndex = data.legIndex;
        kneeAttachementPiece.CurrentIndex = data.kneeAttachementIndex;
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

    [ReadOnly] [SerializeField] private int currentIndex;
    public int CurrentIndex
    {
        set
        {
            if(rootPices.Count == 0) return;

            if(value >= -1)
            {
                if(value > -1)
                    currentIndex = value % rootPices.Count;
                else
                    currentIndex = -1;
            
                onIndexChanged?.Invoke(currentIndex);
            }
            else
			{
                value += rootPices.Count;
                currentIndex = value % rootPices.Count + 1;

                onIndexChanged?.Invoke(currentIndex);
            }
            
            EnableObjectByIndex(currentIndex);
        }
        get => currentIndex;
    }

    [ListDrawerSettings(ShowIndexLabels = true)]
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

    private void EnableObjectByIndex(int index)
	{
        if(rootPices.Count > 0)
		{
            if(index == -1 || (index >=0 && index < rootPices.Count))
            {
                DisableAllObjects();
            }
            if(index >= 0 && index < rootPices.Count)
			{
                rootPices[index].SetActive(true);
            }
        }
    }
    private void DisableAllObjects()
	{
		for(int i = 0; i < rootPices.Count; i++)
		{
            rootPices[i].SetActive(false);
		}
    }

    [ButtonGroup]
    public void Left()
	{
        CurrentIndex--;
    }
    [ButtonGroup]
    public void Right()
	{
        CurrentIndex++;
    }
    [ButtonGroup]
    public void DisableAll()
	{
        CurrentIndex = -1;
    }
}
#endregion

#region CharacterParts
[System.Serializable]
public class CharacterModelData
{
    public CharacterModelHeadData head;
    public CharacterModelBodyData body;
}
[System.Serializable]
public class CharacterModelHeadData
{
    public int headIndex = 0;
    public int earsIndex = -1;
    public int hairIndex = 0;
    public int eyebrowIndex = 0;
    public int facialhairIndex = -1;
}
[System.Serializable]
public class CharacterModelBodyData
{
    public CharacterModelTorsoData torso;
    public CharacterModelHipsData hips;
}
[System.Serializable]
public class CharacterModelTorsoData
{
    public int torsoIndex = 0;

    public CharacterModelArmData leftArm;
    public CharacterModelArmData rightArm;
}
[System.Serializable]
public class CharacterModelArmData
{
    public int armUpperIndex = 0;
    public int armLowerIndex = 0;
    public int handIndex = 0;

    public int sholderAttachmentIndex = -1;
    public int elbowAttachmentIndex = -1;
}
[System.Serializable]
public class CharacterModelHipsData
{
    public int hipsIndex = 0;

    public CharacterModelLegData leftLeg;
    public CharacterModelLegData rightLeg;
}
[System.Serializable]
public class CharacterModelLegData
{
    public int legIndex = 0;
    public int kneeAttachementIndex = -1;
}
#endregion