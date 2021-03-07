public interface IModifier
{
	string Value { get; }
}
public interface IModifiable
{
	void AddModifier(IModifier addModifier);
	void RemoveModifier(IModifier addModifier);
}
