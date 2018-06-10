using System;

namespace Kavenegar.Json
{

 public class JsonBoolean : JsonObject
 {
	public Boolean BooleanValue { get; set; }

	public JsonBoolean(Boolean booleanValue)
	{
	 BooleanValue = booleanValue;
	}

	public JsonObject UpCast()
	{
	 JsonObject objectJ = this;
	 return objectJ;
	}
 }
}
