using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Selim.Json
{
    class JsonBoolean : JsonValue
    {
        public bool value;
        public JsonBoolean(bool value)
        {
            this.value = value;
        }

        public override string dartTypeName => "bool";

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
			sb.Append(value ? "true" : "false");
		}
	}
}
