namespace ExistForAll.SimpleSettings.Binder
{
	internal class InMemoryBinder : ISectionBinder
	{
		private readonly InMemoryCollection _collection;

		public InMemoryBinder(InMemoryCollection collection)
		{
			_collection = collection;
		}

		public void BindPropertySettings(BindingContext context)
		{
			if (_collection.TryGetValue(context.Section, context.Key, out var value))
			{
				context.SetNewValue(value);
			}
		}
	}
}
