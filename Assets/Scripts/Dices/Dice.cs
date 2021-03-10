using UnityEngine;

[System.Serializable]
public class Dice
{
	public readonly DiceType diceType;
	public int LastValue { private set; get; }

	public int MaxDiceValue
	{
		get => (int)diceType;
	}

	public Dice(DiceType diceType)
	{
		this.diceType = diceType;
	}

	public int Roll()
	{
		LastValue = Random.Range(1, MaxDiceValue + 1);
		return LastValue;
	}
}

[System.Serializable]
public class DiceFormule
{
	//success rolls, reaction rolls и damage rolls
	[Min(1)]
	public int diceThrows = 1;
	public readonly DiceType diceType = DiceType.Cube;
	public int diceModifier = 0;

	public Dice[] dices;

	public DiceFormule(int diceThrows, DiceType diceType, int diceModifier)
	{
		this.diceThrows = diceThrows;
		this.diceType = diceType;
		this.diceModifier = diceModifier;

		dices = new Dice[this.diceThrows];
		for(int i = 0; i < dices.Length; i++)
		{
			dices[i] = new Dice(this.diceType);
		}
	}

	public int Roll()
	{
		int roll = 0;
		for(int i = 0; i < dices.Length; i++)
		{
			roll += dices[i].Roll();
		}

		roll += diceModifier;

		roll = Mathf.Max(0, roll);

		return roll;
	}

	public override string ToString()
	{
		return diceThrows + ("d" + (int)diceType) + (diceModifier == 0 ? "" : diceModifier > 0 ? "+" + diceModifier : diceModifier.ToString());
	}
}
public enum DiceType : int 
{ 
	Tetrahedron = 4,
	Cube = 6,
	Octahedron = 8,
	Dodecahedron = 12,
	Icosahedron = 20,
}