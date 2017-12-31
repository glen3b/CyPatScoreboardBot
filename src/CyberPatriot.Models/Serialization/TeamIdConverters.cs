﻿using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace CyberPatriot.Models.Serialization
{
    public class TeamIdJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var name = (TeamId)value;
            writer.WriteValue(value?.ToString());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            return TeamId.Parse((string)reader.Value);
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(TeamId).IsAssignableFrom(objectType);
        }
    }

    public class TeamIdTypeConverter : TypeConverter
    {
        public const int DefaultSeason = 10;

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type source)
        {
            // numerics 
            return source == typeof(string) || source == typeof(int) || source == typeof(short) || source == typeof(TeamId);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type dest)
        {
            return dest == typeof(string) || dest == typeof(TeamId);
        }

        public override object ConvertFrom(ITypeDescriptorContext context,
                           System.Globalization.CultureInfo culture, object value)
        {
            // assumes we're performing a supported conversion: string parse or team # => TeamId; or teamId => string

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            switch (value)
            {
                case string s:
                    if (TeamId.TryParse(s, out TeamId val))
                    {
                        return val;
                    }
                    else if (s.Length == 4 && int.TryParse(s, out int teamNumber) && teamNumber >= 0)
                    {
                        return new TeamId(DefaultSeason, teamNumber);
                    }
                    break;
                case short tempShort:
                    // ctor does bounds checking
                    return new TeamId(DefaultSeason, tempShort);
                case int i:
                    // ctor does bounds checking
                    return new TeamId(DefaultSeason, i);
                case TeamId t:
                    return t.ToString();
            }

            throw new NotSupportedException("The specified conversion is not supported.");
        }
        public override object ConvertTo(ITypeDescriptorContext context,
                         System.Globalization.CultureInfo culture,
                         object value, Type destinationType)
        {
            switch (value)
            {
                case short tempShort:
                    if (destinationType == typeof(TeamId))
                    {
                        // ctor does bounds checking
                        return new TeamId(DefaultSeason, tempShort);
                    }
                    break;
                case int i:
                    if (destinationType == typeof(TeamId))
                    {
                        // ctor does bounds checking
                        return new TeamId(DefaultSeason, i);
                    }
                    break;
                case string s:
                    if (destinationType == typeof(string))
                    {
                        return s;
                    }
                    else if (destinationType == typeof(TeamId))
                    {
                        return TeamId.Parse(s);
                    }
                    break;
                case TeamId t:
                    if (destinationType == typeof(string))
                    {
                        return t.ToString();
                    }
                    else if (destinationType == typeof(TeamId))
                    {
                        return t;
                    }
                    break;
            }

            throw new NotSupportedException("The specified conversion is not supported.");
        }
    }
}