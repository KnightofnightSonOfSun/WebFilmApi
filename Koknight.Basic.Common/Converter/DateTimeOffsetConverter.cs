/*************************************************************************************
 *
 * File name:   DateTimeOffsetConverter.cs
 * Description: 
 * 
 * Version：  V1.0
 * Creator  Eason Huang
 * Create time：  2024/11/17 17:21
 * ======================================
*************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace Koknight.Basic.Common.Converter
{
    public class DateTimeOffsetConverter : JsonConverter<DateTimeOffset>
    {
        private readonly string formatString;
        public DateTimeOffsetConverter()
        {
            formatString = "yyyy/MM/dd HH:mm:ss zzz";
        }

        public DateTimeOffsetConverter(string inFormatString)
        {
            formatString = inFormatString;
        }

        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (DateTimeOffset.TryParse(reader.GetString(), out DateTimeOffset date))
                {
                    return date;
                }
            }
            return reader.GetDateTimeOffset();
        }


        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(formatString));
        }
    }
}
