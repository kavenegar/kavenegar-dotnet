using System;
using System.Collections.Generic;

namespace Kavenegar.Json
{

    public class JsonArray : JsonObject
    {
        public List<JsonObject> Array { get; set; }
        private List<JsonObject>.Enumerator _e;

        public JsonArray()
        {
            Array = new List<JsonObject>();
        }

        public void AddElementToArray(JsonObject arrayElement)
        {
            Array.Add(arrayElement);
        }

        public JsonObject UpCast()
        {
            JsonObject objectJ = this;
            return objectJ;
        }

        public void AddList(List<JsonObject> lista)
        {
            Array = lista;
        }

        public bool NextObject(out JsonObject o)
        {

            JsonObject outObject;
            _e = Array.GetEnumerator();

            if (_e.MoveNext())
            {
                outObject = _e.Current;
                o = outObject;
                return true;
            }
            outObject = new JsonObject();
            o = outObject;
            return false;
        }

        public int Count
        {
            get { return Array.Count; }
        }

    }

}
