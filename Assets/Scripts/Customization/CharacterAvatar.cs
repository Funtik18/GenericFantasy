using System.Collections;
using System.Collections.Generic;

using Sirenix.OdinInspector;

using UnityEngine;

public class CharacterAvatar : MonoBehaviour
{
    public Material characterMaterial;

    [Title("Head")]
    public Transform maleHeads;
    public Transform femaleHeads;
    [Space]
    public Transform hairs;
    [Space]
    public Transform maleEyebrows;
    public Transform femaleEyebrows;
    [Space]
    public Transform maleFacialHairs;
    [Space]
    public Transform ears;

    [Space]
    [Title("Torso")]
    public Transform maleTorso;
    public Transform femaleTorso;

    [Space]
    [Title("Arms")]
    public Transform maleArmUpperLeft;
    public Transform femaleArmUpperLeft; 
    public Transform maleArmUpperRight;
    public Transform femaleArmUpperRight;

    public Transform maleArmLowerLeft;
    public Transform femaleArmLowerLeft;
    public Transform maleArmLowerRight;
    public Transform femaleArmLowerRight;

    public Transform handLeft;
    public Transform handRight;

    [Space]
    [Title("Hips")]
    public Transform maleHips;
    public Transform femaleHips;

    [Space]
    [Title("Legs")]
    public Transform maleLegLeft;
    public Transform femaleLegLeft;
    public Transform maleLegRight;
    public Transform femaleLegRight;

    [Title("Extra")]
    public Transform sholderAttachmentLeft;
    public Transform sholderAttachmentRight;
    public Transform elbowAttachmentLeft;
    public Transform elbowAttachmentRight;

    public Transform kneeAttachementLeft;
    public Transform kneeAttachementRight;
}