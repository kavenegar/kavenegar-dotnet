// SharpYourJson
// (c) 2013 Felipe Herranz
// SharpYourJson may be freely distributed under the MIT license.

using System;
using System.Collections.Generic;

namespace Kavenegar.Json
{
 /// <summary>
 /// Contains the façade-style class to perform Json operations
 /// </summary>

 public class Parser
 {

	public JsonObject DocumentJson; // The deserialized Json Object will be stored here

	private const char ObjectBegin = '{';
	private const char ObjectEnd = '}';
	private const char ArrayBegin = '[';
	private const char ArrayEnd = ']';
	private const char DoubleQuotes = '"';
	private const char DoublePoint = ':';
	private const char Comma = ',';
	private const char BackSlash = '\u005C';
	private const string NullValue = "null";
	private const string TrueValue = "true";
	private const string FalseValue = "false";

	/// <summary>
	/// Deserialize a JSON document. This method does not perform a syntax checking so It assumes a valid Json input
	/// 
	/// </summary>
	/// <param name='json'>
	/// A string which contains a valid Json array or object
	/// </param>
	public JsonObject Parse(String json)
	{
	 if (json[0] == ArrayBegin)
	 {
		json = json.Substring(1, json.Length - 2);
		JsonArray arrayJson = SerializeArray(json);
		JsonObject o = arrayJson;
		return o;

	 }
	 else if (json[0] == ObjectBegin)
	 {

		return SerializeObject(json);
	 }
	 return null;
	}
	/// <summary>
	/// This method performs deserialization of an object(except array)
	/// </summary>
	/// <returns>
	/// JsonObject object as a deserialized JSON object
	/// </returns>
	/// <param name='json'>
	/// A string which contains a valid Json object
	/// </param>
	private JsonObject SerializeObject(String json)
	{
	 json = json.Replace(@"\", "");
	 JsonObject document = new JsonObject();
	 int n = 1;
	 int lengthJson = json.Length;
	 String keyString = "";

	 while (n <= lengthJson - 1)
	 {
		if (json[n] == DoubleQuotes && json[n - 1] != DoublePoint) // (key-value Pair) key Name
		{
		 int secondDoubleQuotes = FindNextQuote(json, n + 1);
		 keyString = json.Substring(n + 1, (secondDoubleQuotes - (n + 1)));
		 n = secondDoubleQuotes + 1;

		}
		else if (json[n] == DoubleQuotes && json[n - 1] == DoublePoint) //  (key-value Pair) value Name (if string)
		{
		 if (json[n + 1] != DoubleQuotes)
		 {
			int secondDoublesQuotes = FindNextQuote(json, n + 1);
			String text = json.Substring(n + 1, (secondDoublesQuotes - (n + 1)));
			JsonString stringValue = new JsonString(text);
			JsonObject o = stringValue;
			document.AddJsonValue(keyString, o);
			n = secondDoublesQuotes + 1;
		 }
		 else
		 {
			JsonObject o = new JsonString("");
			document.AddJsonValue(keyString, o);
		 }

		}
		else if (json[n] == '-' || json[n] == '0' || json[n] == '1' || json[n] == '2' || json[n] == '3' ||
		 json[n] == '4' || json[n] == '5' || json[n] == '6' || json[n] == '7' || json[n] == '8' ||
		 json[n] == '9') // (key-value Pair) value (if number) 
		{
		 char[] arrayEndings = { ObjectEnd, Comma };
		 int nextComma = json.IndexOfAny(arrayEndings, n);
		 String stringNumber = json.Substring(n, nextComma - n);
		 Double valueNumber = Convert.ToDouble(stringNumber);
		 float floatNumber = (float)valueNumber;
		 JsonNumber number = new JsonNumber(floatNumber);
		 JsonObject o = number;
		 document.AddJsonValue(keyString, o);
		 n = nextComma + 1;

		}
		else if (json[n] == ArrayBegin) //(key-value Pair) value (if array)
		{
		 if (json[n + 1] != ArrayEnd)
		 {
			String subJson = json.Substring(n, json.Length - n);
			int arrayClose = CloseBracketArray(subJson);
			String arrayUnknown = json.Substring(n + 1, arrayClose - 2);
			JsonArray arrayObjects = SerializeArray(arrayUnknown);
			JsonObject o = arrayObjects;
			document.AddJsonValue(keyString, o);
			n = n + arrayClose;
		 }
		 else
		 {
			if (!string.IsNullOrEmpty(keyString))
			{
			 JsonArray arrayTempEmpty = new JsonArray { Array = null };
			 JsonObject emptyArray = arrayTempEmpty;
			 document.AddJsonValue(keyString, emptyArray);
			 keyString = "";

			}
			else
			{
			 n++;
			}
		 }

		}
		else if (json[n] == ObjectBegin) // (key-value Pair) value (if object)
		{
		 if (json[n + 1] != ObjectEnd)
		 {
			String subJson = json.Substring(n, json.Length - n);
			int objectClose = CloseBracketObject(subJson);
			String objectUnknown = json.Substring(n, objectClose);
			var o = SerializeObject(objectUnknown);
			document.AddJsonValue(keyString, o);
			n = n + objectClose + 1;
		 }
		 else
		 {
			JsonObject o = new JsonObject { Values = null };
			document.AddJsonValue(keyString, o);
		 }

		}
		else if (String.Compare(SafeSubString(json, n, 4), NullValue, StringComparison.Ordinal) == 0) // (key-value Pair) value (if NULL) 
		{
		 JsonObject o = new JsonNullable();
		 document.AddJsonValue(keyString, o);
		 n = n + 5;
		}
		else if (String.Compare(SafeSubString(json, n, 4), TrueValue, StringComparison.Ordinal) == 0) // (key-value Pair) value (if TRUE)  
		{
		 JsonObject o = new JsonBoolean(true);
		 document.AddJsonValue(keyString, o);
		 n = n + 5;
		}
		else if (String.Compare(SafeSubString(json, n, 5), FalseValue, StringComparison.Ordinal) == 0) // (key-value Pair) value (if FALSE) 
		{
		 JsonObject o = new JsonBoolean(false);
		 document.AddJsonValue(keyString, o);
		 n = n + 6;
		}
		else
		{
		 n++;
		}

	 }

	 return document;
	}

	/// <summary>
	/// Search where is the ending of an object
	/// </summary>
	/// <returns>
	/// the index of the '}' which closes an object
	/// </returns>
	/// <param name='json'>
	/// A valid json string ({........)
	/// </param>

	private int CloseBracketObject(String json)
	{
	 int countObjectBegin = 0;
	 int countObjectEnd = 0;
	 int n = 0;

	 do
	 {
		if (json[n] == ObjectBegin)
		{
		 countObjectBegin++;

		}
		else if (json[n] == ObjectEnd)
		{
		 countObjectEnd++;
		}

		n++;

	 } while (countObjectBegin != countObjectEnd);

	 return n;
	}

	/// <summary>
	/// Search where is the ending of an array
	/// </summary>
	/// <returns>
	/// he index of the ']' which closes an object
	/// </returns>
	/// <param name='json'>
	/// A valid Json string ([.....)
	/// </param>

	private int CloseBracketArray(String json)
	{
	 int countArrayBegin = 0;
	 int countArrayEnd = 0;
	 int n = 0;

	 do
	 {
		if (json[n] == ArrayBegin)
		{
		 countArrayBegin++;

		}
		else if (json[n] == ArrayEnd)
		{
		 countArrayEnd++;
		}

		n++;

	 } while (countArrayBegin != countArrayEnd);

	 return n;
	}

	/// <summary>
	/// Deserialize a Json Array into an object JsonArray
	/// </summary>
	/// <returns>
	/// JsonArray object as a deserialized JSON array
	/// </returns>
	/// <param name='array'>
	/// valid JSON array except the brackets
	/// </param>

	private JsonArray SerializeArray(String array)
	{
	 JsonArray arrayObject = new JsonArray();
	 var elements = SplitElements(array);

	 foreach (String item in elements)
	 {

		if (item[0] == DoubleQuotes)
		{
		 String withoutQuotes = item.Trim(DoubleQuotes);
		 JsonObject o = new JsonString(withoutQuotes);
		 arrayObject.AddElementToArray(o);

		}
		else if (item[0] == ObjectBegin)
		{
		 JsonObject o = SerializeObject(item);
		 arrayObject.AddElementToArray(o);

		}
		else if (item[0] == ArrayBegin)
		{
		 String itemArray = item.Substring(1, item.Length - 2);
		 JsonArray secondaryArray = SerializeArray(itemArray);
		 JsonObject o = secondaryArray;
		 arrayObject.AddElementToArray(o);

		}
		else if (item[0] == '-' || item[0] == '0' || item[0] == '1' || item[0] == '2' || item[0] == '3' ||
		 item[0] == '4' || item[0] == '5' || item[0] == '6' || item[0] == '7' || item[0] == '8' || item[0] == '9')
		{
		 Double doubleValue = Convert.ToDouble(item);
		 float floatValue = (float)doubleValue;
		 JsonObject o = new JsonNumber(floatValue);
		 arrayObject.AddElementToArray(o);
		}
		else if (String.Compare(SafeSubString(item, 0, 4), TrueValue, StringComparison.Ordinal) == 0)
		{
		 JsonObject o = new JsonBoolean(true);
		 arrayObject.AddElementToArray(o);
		}
		else if (String.Compare(SafeSubString(item, 0, 5), FalseValue, StringComparison.Ordinal) == 0)
		{
		 JsonObject o = new JsonBoolean(false);
		 arrayObject.AddElementToArray(o);
		}
		else if (String.Compare(SafeSubString(item, 0, 4), NullValue, StringComparison.Ordinal) == 0)
		{
		 JsonObject o = new JsonNullable();
		 arrayObject.AddElementToArray(o);
		}

	 }

	 return arrayObject;

	}
	/// <summary>
	/// Just a safe subString operation
	/// </summary>
	/// <returns>
	/// A subString of the string input parameter text
	/// </returns>
	/// <param name='text'>
	/// A string
	/// </param>
	/// <param name='start'>
	/// index of starting
	/// </param>
	/// <param name='length'>
	/// Length of the subString
	/// </param>

	private String SafeSubString(String text, int start, int length)
	{
	 var safeString = start + length < text.Length ? text.Substring(start, length) : text.Substring(start, text.Length - start);

	 return safeString;
	}

	/// <summary>
	/// Finds the next '"' to close a String field
	/// </summary>
	/// <returns>
	/// The next ' " '
	/// </returns>
	/// <param name='text'>
	/// A valid JSON string
	/// </param>
	/// <param name='index'>
	/// Index of starting
	/// </param>

	private int FindNextQuote(String text, int index)
	{
	 int nextQuote = text.IndexOf(DoubleQuotes, index);
	 while (text[nextQuote - 1] == BackSlash)
	 {
		nextQuote = text.IndexOf(DoubleQuotes, nextQuote + 1);
	 }

	 return nextQuote;

	}

	/// <summary>
	/// Splits the elements of an Array
	/// </summary>
	/// <returns>
	/// The elements in an array of Strings
	/// </returns>
	/// <param name='arrayText'>
	/// 
	/// </param>

	private String[] SplitElements(String arrayText)
	{
	 int n = 0;
	 int doubleQuotesCounter = 0;
	 int objectBeginCounter = 0;
	 int objectEndCounter = 0;
	 int arrayBeginCounter = 0;
	 int arrayEndCounter = 0;
	 int previousCommaIndex = 0;
	 Boolean oneElement = true;
	 List<String> textSplit = new List<String>();

	 while (n <= arrayText.Length - 1)
	 {
		if (arrayText[n] == DoubleQuotes && arrayText[n - 1] != BackSlash)
		{
		 doubleQuotesCounter++;
		 n++;
		}
		else if (arrayText[n] == ObjectBegin)
		{
		 objectBeginCounter++;
		 n++;

		}
		else if (arrayText[n] == ObjectEnd)
		{
		 objectEndCounter++;
		 n++;

		}
		else if (arrayText[n] == ArrayBegin)
		{
		 arrayBeginCounter++;
		 n++;

		}
		else if (arrayText[n] == ArrayEnd)
		{
		 arrayEndCounter++;
		 n++;

		}
		else if (arrayText[n] == Comma && doubleQuotesCounter % 2 == 0 && objectBeginCounter == objectEndCounter
		 && arrayBeginCounter == arrayEndCounter)
		{
		 textSplit.Add(arrayText.Substring(previousCommaIndex, (n - previousCommaIndex)));
		 previousCommaIndex = n + 1;
		 n++;
		 oneElement = false;

		}
		else
		{
		 n++;
		}
	 }

	 textSplit.Add(oneElement
		 ? arrayText
		 : arrayText.Substring(previousCommaIndex, (arrayText.Length) - previousCommaIndex));

	 String[] textSplitArray = textSplit.ToArray();
	 return textSplitArray;
	}

 }
}



