namespace Kavenegar.Json
{
    public class JsonNumber : JsonObject
    {
        public float Number { get; set; }

        public JsonNumber(float number)
        {
            Number = number;
        }

        public JsonObject UpCast()
        {
            JsonObject objectJ = this;
            return objectJ;
        }
    }

}
