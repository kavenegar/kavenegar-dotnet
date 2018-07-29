using System;
using System.Collections.Generic;

namespace Kavenegar.Json
{
    /// <summary>
    /// JsonObject is the base class.
    /// JsonString,JsonNumber,JsonBoolean,JsonNullable and JsonArray inherits from JsonObject.
    /// A JsonArray object may contain objects of the base class
    /// </summary>

    public class JsonObject
    {
        public Dictionary<string, JsonObject> Values;

        public JsonObject()
        {
            Values = new Dictionary<string, JsonObject>();
        }

        public void AddJsonValue(string textTag, JsonObject newObject)
        {
            if (!Values.ContainsKey(textTag)) Values.Add(textTag, newObject);
        }

        public JsonObject GetObject(string key) => Values[key];

        public int ElementsOfDictionary() => Values.Count;

        public bool IsJsonString() => this is JsonString;

        public bool IsJsonNumber() => this is JsonNumber;

        public bool IsJsonBoolean() => this is JsonBoolean;

        public bool IsJsonNullable() => this is JsonNullable;

        public bool IsJsonArray() => this is JsonArray;

        public JsonString GetAsString() => (JsonString)this;

        public JsonNumber GetAsNumber() => (JsonNumber)this;

        public JsonArray GetAsArray() => (JsonArray)this;
    }
}

