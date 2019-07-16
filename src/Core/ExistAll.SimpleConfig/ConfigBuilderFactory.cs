using System;
using System.Collections.Generic;

namespace ExistAll.SimpleConfig
{
    internal class ConfigBuilderFactory : IConfigBuilderFactory
    {
        private int _counter;
        private readonly SortedList<int, ISectionBinder> _binders = new SortedList<int, ISectionBinder>();

        public IEnumerable<ISectionBinder> Binders => _binders.Values;

        public ConfigOptions Options { get; } = new ConfigOptions();

        public void AddSectionBinder(ISectionBinder sectionBinder)
        {
            if (sectionBinder == null) throw new ArgumentNullException(nameof(sectionBinder));
            _binders.Add(_counter++, sectionBinder);
        }
    }
}