namespace ExistAll.Settings
{
	public interface ISectionBinder
	{
		bool TryGetValue(SettingsBindingContext bindingContext, out string value);
	}
}