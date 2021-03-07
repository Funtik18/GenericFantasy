using UnityEngine;

using Sirenix.OdinInspector;

public abstract class Attribute : ScriptableObject
{
    [Required]
    [HideLabel]
    [SerializeField]
    public InformationScriptableData data;

    public AttributeType type;

    [Min(0)]
    public int baseCost;

    public abstract void Enable(EntityStatistics statistics);
    public abstract void Disabe(EntityStatistics statistics);
}
[System.Flags]
public enum AttributeType
{
    None,
    Сommon = 1 << 1,
    Mental = 1 << 2,
    Physical = 1 << 3,
    Social = 1 << 4,
    SuperDuper = 1 << 5,
    Exotic = 1 << 6,
    All = Сommon | Mental | Physical | Social | SuperDuper | Exotic,
}

public enum AttributeLaunchType
{
    Automatically,//Означает, что после получения атрибутта его эффекты срабатывают мгновенно.
    Mechanically,//Означает, что для запуска аттрибута сначала требуется выкинуть (3d6+-mod) с определёной проверкой.
}

//[System.Serializable]
//public class Improvement
//{


//}
//[System.Serializable]
//public class Limitation
//{

//}