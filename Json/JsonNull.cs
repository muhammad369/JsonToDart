using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Selim.Json
{
    class JsonNull : JsonValue
    {

       
		public override void toString(StringBuilder sb, int? indents)
		{
			sb.Append("null");
		}
	}
}
