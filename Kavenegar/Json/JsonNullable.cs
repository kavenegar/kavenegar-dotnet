using System;

namespace Kavenegar.Json
{
    public class JsonNullable : JsonObject
    {
        public string Nullable { get; set; }

        public JsonNullable()
        {
            Nullable = "Null";
        }

        public JsonObject UpCast()
        {
            JsonObject objectJ = this;
            return objectJ;
        }
    }
}
