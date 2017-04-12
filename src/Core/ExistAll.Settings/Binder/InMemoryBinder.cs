namespace ExistAll.Settings.Binder
{
	internal class InMemoryBinder : ISectionBinder
	{
		private readonly InMemoryCollection _collection;

		public InMemoryBinder(InMemoryCollection collection)
		{
			_collection = collection;
		}

		public bool TryGetValue(SettingsBindingContext bindingContext, out string value)
		{
			return _collection.TryGetValue(bindingContext.Section, bindingContext.Key, out value);
		}
	}
}
