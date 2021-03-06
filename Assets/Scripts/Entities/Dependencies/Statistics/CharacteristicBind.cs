public abstract class Bind
{
	public abstract string StringValue { get; }
	public abstract float Value { get; }
}

public class CharacteristicBind : Bind
{
	public IModifier modifier;
	public CharacteristicModifier characteristic;

	private float value;
	public override float Value { get => value; }

	public CharacteristicBind(IModifier modifier, CharacteristicModifier characteristic, float value)
	{
		this.modifier = modifier;
		this.characteristic = characteristic;
		this.value = value;

	}
	public CharacteristicBind AddBin()
	{
		characteristic.AddBind(this);
		return this;
	}

	public override string StringValue
	{
		get => modifier.Value;
	}
}