﻿using System;

namespace TigrSettings.Converters
{
	/// <summary>
	/// Converts raw string setting values to <typeparamref name="TValue"/> type.
	/// </summary>
	/// <typeparam name="TValue">Converted value type.</typeparam>
	public abstract class SettingValueConverterBase<TValue> : ISettingValueConverter
	{
		/// <inheritdoc />
		public bool CanConvert(Type type)
		{
			return type == typeof(TValue);
		}

		/// <inheritdoc />
		public object Convert(string value, Type type)
		{
			return Convert(value);
		}

		/// <summary>
		/// Covnerts string setting value to <typeparamref name="TValue"/> type.
		/// </summary>
		/// <param name="value">Value to convert.</param>
		/// <returns>Converted value.</returns>
		public abstract TValue Convert(string value);
	}
}
