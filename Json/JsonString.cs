using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Selim.Json
{
    class JsonString : JsonValue
    {
        public string value;
        public JsonString(string value)
        {
            this.value = value;
        }

       
		public override void toString(StringBuilder sb, int? indents)
		{
			sb.AppendFormat("\"{0}\"", value.Replace("\"","\\\""));
		}
	}
}
