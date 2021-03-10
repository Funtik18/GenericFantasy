using System;
using System.Collections;
using System.Collections.Generic;

using Sirenix.OdinInspector;

using UnityEngine;

public class EntityCustomColors 
{
    private ColorsData data;

    public Color portraitColor;
    public Color hairColor;
    public Color eyesColor;
    public Color skinColor;
    public Color stubbleColor;
    public Color scarColor;
    public Color artColor;

	public EntityCustomColors(ColorsData colorsData)
	{
		data = colorsData;

        //this.mat = colorsData.material;

        this.portraitColor = data.portraitColor;
        this.hairColor = data.hairColor;
        this.eyesColor = data.eyesColor;
        this.skinColor = data.skinColor;
        this.stubbleColor = data.stubbleColor;
        this.scarColor = data.scarColor;
        this.artColor = data.artColor;

        //UpdateColors();
    }

 //   public void UpdateColors()
	//{
 //       mat.SetColor("_Color_Hair", hairColor);
 //       mat.SetColor("_Color_Eyes", eyesColor);
 //       mat.SetColor("_Color_Skin", skinColor);
 //       mat.SetColor("_Color_Stubble", stubbleColor);
 //       mat.SetColor("_Color_Scar", scarColor);
 //       mat.SetColor("_Color_BodyArt", artColor);
 //   }

	public ColorsData GetCurrentData()
	{
        ColorsData colors = new ColorsData()
        {
            portraitColor = portraitColor,
            hairColor = hairColor,
            eyesColor = eyesColor,
            skinColor = skinColor,
            stubbleColor = stubbleColor,
            scarColor = scarColor,
            artColor = artColor,
        };
        return colors;
    }
}
[System.Serializable]
public struct ColorsData
{
    [ColorPalette("Portrats")]
    public Color portraitColor;

    [ColorPalette("Country")]
    public Color hairColor;

    [ColorPalette("Eyes")]
    public Color eyesColor;

    [ColorPalette("Skins")]
    public Color skinColor;

    [ColorPalette("Stubbles")]
    public Color stubbleColor;

    [ColorPalette("Scars")]
    public Color scarColor;

    [ColorPalette("Underwater")]
    public Color artColor;
}