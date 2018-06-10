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
	public Dictionary<String, JsonObject> Values;

	public JsonObject()
	{
	 Values = new Dictionary<String, JsonObject>();
	}

	public void AddJsonValue(String textTag, JsonObject newObject)
	{
	 if (!Values.ContainsKey(textTag))
	 {
		Values.Add(textTag, newObject);
	 }
	}

	public JsonObject GetObject(String key)
	{
	 JsonObject current = Values[key];
	 return current;
	}

	public int ElementsOfDictionary()
	{
	 return Values.Count;
	}


	public Boolean IsJsonString()
	{
	 if (this is JsonString)
	 {
		return true;
	 }
	 return false;
	}

	public Boolean IsJsonNumber()
	{
	 if (this is JsonNumber)
	 {
		return true;
	 }
	 return false;
	}

	public Boolean IsJsonBoolean()
	{
	 if (this is JsonBoolean)
	 {
		return true;
	 }
	 return false;
	}

	public Boolean IsJsonNullable()
	{
	 if (this is JsonNullable)
	 {
		return true;
	 }
	 return false;
	}

	public Boolean IsJsonArray()
	{
	 if (this is JsonArray)
	 {
		return true;
	 }
	 return false;
	}

	public JsonString GetAsString()
	{
	 return (JsonString)this;
	}
	public JsonNumber GetAsNumber()
	{
	 return (JsonNumber)this;
	}
	public JsonArray GetAsArray()
	{
	 return (JsonArray)this;
	}
 }
}

