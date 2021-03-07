using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Sirenix.OdinInspector;

public abstract class DisAdvantage : Attribute
{
	[ShowIf("CheckControl")]
	public bool isNeedControl;
	[ShowIf("CheckDisAdvantage")]
	public SelfControl selfControl;

	private bool CheckControl()
	{
		return (type == AttributeType.Mental || type == AttributeType.Physical);
	}
	private bool CheckDisAdvantage()
	{
		return isNeedControl && CheckControl();
	}
}
public enum SelfControl
{
	//https://gurps.fandom.com/wiki/Self-Control_Number
	//https://gurps-zana4ka.fandom.com/ru/wiki/%D0%9D%D0%B5%D0%B4%D0%BE%D1%81%D1%82%D0%B0%D1%82%D0%BA%D0%B8
	CannotResist,
	ResistQuiteRarely,
	ResistFairlyOften,
	ResistQuiteOften,
	ResistAlmostAllTheTime,
	ResistAllTheTime,
}