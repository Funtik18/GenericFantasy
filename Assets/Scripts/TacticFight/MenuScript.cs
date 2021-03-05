using System.Collections;
using UnityEditor;
using UnityEngine;

public class MenuScript 
{
    [MenuItem("Tools/Map/Assign Tile Material")]
    public static void AssignTileMaterial()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        Material material = Resources.Load<Material>("Materials/Tile");
        foreach (var item in tiles)
        {
            item.GetComponent<Renderer>().material = material;
        }
    }
    [MenuItem("Tools/Map/Assign Tile Script")]
    public static void AssignTileScript()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach (var item in tiles)
        {
            item.AddComponent<Tile>();
        }
    }
}
