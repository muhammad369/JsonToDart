using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Selim.Json
{
    class JsonNull : JsonValue
    {
        public override string dartTypeName => "Object";

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
			sb.Append("null");
		}
	}
}
