public abstract class Bind
{
	public abstract string StringValue { get; }
	public abstract float Value { get; }
}

public class CharacteristicBind : Bind
{
	public Attribute attribute;
	public CharacteristicModifier characteristic;

	private float value;
	public override float Value { get => value; }
	public override string StringValue { get; }

	public CharacteristicBind(Attribute attribute, CharacteristicModifier characteristic, float value)
	{
		this.attribute = attribute;
		this.characteristic = characteristic;
		this.value = value;
	}
}