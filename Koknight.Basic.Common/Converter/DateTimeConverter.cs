/*************************************************************************************
 *
 * File name:   DateTimeConverter.cs
 * Description: 
 * 
 * Version：  V1.0
 * Creator  Eason Huang
 * Create time：  2024/11/17 17:20
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
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        private readonly string formatString;
        public DateTimeConverter()
        {
            formatString = "yyyy/MM/dd HH:mm:ss";
        }

        public DateTimeConverter(string inFormatString)
        {
            formatString = inFormatString;
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (DateTime.TryParse(reader.GetString(), out DateTime date))
                {
                    return date;
                }
            }
            return reader.GetDateTime();
        }


        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(formatString));
        }
    }
}
