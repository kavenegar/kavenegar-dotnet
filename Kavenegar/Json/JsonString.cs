using System;

namespace Kavenegar.Json
{
    public class JsonString : JsonObject
    {
        public string Text { get; set; }

        public JsonString(string text)
        {
            Text = text;
        }

        public JsonObject UpCast()
        {
            JsonObject objectJ = this;
            return objectJ;
        }

    }
}
