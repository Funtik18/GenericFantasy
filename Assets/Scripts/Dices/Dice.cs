using UnityEngine;

public class Dice
{
	public readonly DiceType diceType = DiceType.Cube;
	public int MaxDiceValue
	{
		get => (int)diceType;
	}

	//success rolls, reaction rolls и damage rolls
	public Dice(DiceType diceType)
	{
		this.diceType = diceType;
	}

	public void Roll()
	{
		Debug.LogError(Random.Range(0, MaxDiceValue));
	}

	public void RollSuccess()
	{

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