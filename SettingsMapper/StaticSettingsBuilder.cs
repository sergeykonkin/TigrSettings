﻿using System;
using System.Globalization;

namespace SettingsMapper
{
    /// <summary>
    /// Provides functionality to map raw settings to Static classes.
    /// </summary>
    public class StaticSettingsBuilder : SettingsBuilderBase
    {
        /// <summary>
        /// Initializes a new instance of <see cref="StaticSettingsBuilder"/>.
        /// </summary>
        /// <param name="settingsProvider">Raw string settings provider.</param>
        /// <param name="converters">Set of additional converters.</param>
        public StaticSettingsBuilder(
            ISettingsProvider settingsProvider,
            params ISettingConverter[] converters)
            : base(settingsProvider, CultureInfo.InvariantCulture, converters)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="StaticSettingsBuilder"/>.
        /// </summary>
        /// <param name="settingsProvider">Raw string settings provider.</param>
        /// <param name="formatProvider">Format provider for e.g. numbers and dates.</param>
        /// <param name="converters">Set of additional converters.</param>
        public StaticSettingsBuilder(
            ISettingsProvider settingsProvider,
            IFormatProvider formatProvider,
            params ISettingConverter[] converters)
            : base(settingsProvider, formatProvider, converters)
        {
        }

        /// <summary>
        /// Maps converted settings to Static class properties.
        /// </summary>
        /// <param name="staticType">Static class type.</param>
        public void MapTo(Type staticType)
        {
            if (!staticType.IsStatic())
                throw new ArgumentException("Provided type must be a static class.", nameof(staticType));

            base.Build(staticType);
        }

        /// <inheritdoc />
        internal override IMapper Mapper { get; } = new StaticMapper();

        /// <inheritdoc />
        protected override string TypeDesc => base.TypeDesc + " or nested static class";
    }
}
