using System.Text;

namespace ExistAll.Settings.Binder
{
	internal class InMemoryBinder : ISectionBinder
	{
		private readonly InMemoryCollection _collection;

		public InMemoryBinder(InMemoryCollection collection)
		{
			_collection = collection;
		}

		public string GetValue(SettingsBindingContext bindingContext)
		{
			return _collection.GetValue(bindingContext.Section, bindingContext.Key);
		}
	}
}
