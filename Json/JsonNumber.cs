using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Selim.Json
{
    class JsonNumber : JsonValue
    {
        public double value;
        public JsonNumber(double value)
        {
            this.value = value;
        }

        public override string dartTypeName => value%1==0? "int" : "double";

        public override string toDartMapAssignmentExpr(string name)
        {
            return $"\t\tdata['{name}'] = this.{name};";
        }

        public override string toDartMapFetchingExpr(string name)
        {
            return $"\t\t{name} = map['{name}'];";
        }

        public override void toString(StringBuilder sb, int? indents)
		{
			sb.Append(value.ToString());
		}
	}
}
