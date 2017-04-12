namespace ExistAll.SimpleConfig
{
	public interface ISectionBinder
	{
		bool TryGetValue(ConfigBindingContext bindingContext, out string value);
	}
}