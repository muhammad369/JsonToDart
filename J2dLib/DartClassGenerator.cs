using Selim.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonToDart
{
    public class DartClassGenerator
    {
        public static Queue<JsonObject> innerObjects = new Queue<JsonObject>();


        public string generateDartClass(JsonObject jsonObject, string className)
        {
            innerObjects.Clear();
            jsonObject.setClassName(className);
            var sb = new StringBuilder();
            //
            jsonObject.createDartClass(sb);
            //
            while(innerObjects.Count > 0)
            {
                var obj = innerObjects.Dequeue();
                obj.createDartClass(sb);
            }
            //
            return sb.ToString();
        }

    }
}
