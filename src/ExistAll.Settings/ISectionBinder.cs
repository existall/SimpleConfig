namespace ExistAll.Settings
{
	public interface ISectionBinder
	{
		string GetValue(SettingsBindingContext bindingContext);
	}
}