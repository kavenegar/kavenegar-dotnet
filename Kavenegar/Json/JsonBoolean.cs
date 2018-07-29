using System;

namespace Kavenegar.Json
{

    public class JsonBoolean : JsonObject
    {
        public bool BooleanValue { get; set; }

        public JsonBoolean(bool booleanValue)
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
