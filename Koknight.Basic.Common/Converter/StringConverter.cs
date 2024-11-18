/*************************************************************************************
 *
 * File name:   StringConverter.cs
 * Description: 
 * 
 * Version：  V1.0
 * Creator  Eason Huang
 * Create time：  2024/11/17 17:40
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
    public class StringConverter : JsonConverter<string?>
    {

        public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? value = reader.GetString();

            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }
            else
            {
                return value;
            }
        }


        public override void Write(Utf8JsonWriter writer, string? value, JsonSerializerOptions options)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                value = null;
                writer.WriteStringValue(value);
            }
            else
            {
                writer.WriteStringValue(value.ToString());
            }
        }
    }
}
