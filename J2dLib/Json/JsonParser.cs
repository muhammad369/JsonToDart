using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Selim.Json
{
    public class JsonParser
    {
        static string string_pattern = "\"\"|\"(.|\n)*?[^\\\\]\""; //matches also empty string
        static string number_pattern = @"[-+]?[0-9]*\.?[0-9]+([eE][-+]?[0-9]+)?";// @"[0-9]+(\.[0-9]+)?";

        static string pattern;

        int state = 3; //instead of 0 to allow single value or an array
        Stack<JsonValue> stack = new Stack<JsonValue>();
        string tmp_name="";

        static JsonParser()
        {
            pattern ="("+ string_pattern + "|" +number_pattern +"|{|}|\\[|\\]|\\:|,|true|false|null" + ")"; 
        }

        public static JsonValue parse(string jsonString)
        {
            return new JsonParser().Parse(jsonString);
        }

        public JsonValue Parse(string text)
        {
            foreach(Match token in Regex.Matches(text, pattern))
            {
                switch (state)
                {
                    
                    case 1:
                        if ( token.Value == "}")
                        {
                            state = 4;
                            JsonValue jo = stack.Pop();
                            if (stack.Count == 0)
                            {
                                return jo;// (JsonObject)jo;
                            }
                        }
                        else if (Regex.IsMatch(token.Value, string_pattern))
                        {
                            state = 2;
                            tmp_name = token.Value.Trim('"').Replace("\\\"","\"");
                        }
                        else
                        {
                            parsingError();
                        }
                        break;
                    case 2:
                        if (token.Value == ":")
                        {
                            state = 3;
                        }
                        else
                        {
                            parsingError();
                        }
                        break;
                    case 3:
                        if (token.Value == "]")
                        {
                            state = 4;
                            JsonValue jo = stack.Pop();
                            if (stack.Count == 0)
                            {
                                return jo;// (JsonObject)jo;
                            }
                        }
                        else if ( token.Value == "{")
                        {
                            state = 1;
                            JsonObject new_object = new JsonObject();
                            if(stack.Count == 0)
                            {
                                //do nothing
                            }
                            else if (stack.Peek() is JsonObject)
                            {
                                ((JsonObject)stack.Peek()).add(tmp_name, new_object);
                            }
                            else if (stack.Peek() is JsonArray)
                            {
                                ((JsonArray)stack.Peek()).add(new_object);
                            }
                            stack.Push( new_object );
                        }
                        else if ( token.Value == "[")
                        {
                            state = 3;
                            JsonArray new_Array = new JsonArray();
                            if (stack.Count == 0)
                            {
                                //do nothing
                            }
                            else if (stack.Peek() is JsonObject)
                            {
                                ((JsonObject)stack.Peek()).add(tmp_name, new_Array);
                            }
                            else if (stack.Peek() is JsonArray)
                            {
                                ((JsonArray)stack.Peek()).add(new_Array);
                            }
                            stack.Push( new_Array );
                        }
                        else if (Regex.IsMatch(token.Value, string_pattern))
                        {
                            state = 4;
                            if (stack.Count == 0)
                            {
                                stack.Push(new JsonString(token.Value.Trim('"').Replace("\\\"", "\"")));
                            }
                            else if (stack.Peek() is JsonObject)
                            {
                                ((JsonObject)stack.Peek()).add(tmp_name, token.Value.Trim('"').Replace("\\\"", "\""));
                            }
                            else if (stack.Peek() is JsonArray)
                            {
                                ((JsonArray)stack.Peek()).add(token.Value.Trim('"').Replace("\\\"", "\""));
                            }
                        }
                        else if (Regex.IsMatch(token.Value, number_pattern))
                        {
                            state = 4;
                            if (stack.Count == 0)
                            {
                                stack.Push(new JsonNumber(Convert.ToDouble(token.Value)));
                            }
                            else if (stack.Peek() is JsonObject)
                            {
                                ((JsonObject)stack.Peek()).add(tmp_name, Convert.ToDouble(token.Value));
                            }
                            else if (stack.Peek() is JsonArray)
                            {
                                ((JsonArray)stack.Peek()).add( Convert.ToDouble(token.Value));
                            }
                        }
                        else if ( token.Value == "true" || token.Value == "false")
                        {
                            state = 4;
                            if (stack.Count == 0)
                            {
                                stack.Push(new JsonBoolean(token.Value == "true" ? true : false));
                            }
                            else if (stack.Peek() is JsonObject)
                            {
                                ((JsonObject)stack.Peek()).add( tmp_name, token.Value == "true"? true : false );
                            }
                            else if (stack.Peek() is JsonArray)
                            {
                                ((JsonArray)stack.Peek()).add(token.Value == "true" ? true : false);
                            }
                        }
                        else if (token.Value == "null")
                        {
                            state = 4;
                            if (stack.Count == 0)
                            {
                                stack.Push(new JsonNull());
                            }
                            else if (stack.Peek() is JsonObject)
                            {
                                ((JsonObject)stack.Peek()).add(tmp_name, new JsonNull());
                            }
                            else if (stack.Peek() is JsonArray)
                            {
                                ((JsonArray)stack.Peek()).add(new JsonNull());
                            }
                        }
                        else
                        {
                            parsingError();
                        }
                        break;
                    case 4:
                        if (token.Value=="]" || token.Value=="}")
                        {
                            state = 4;
                            JsonValue jo= stack.Pop();
                            if (stack.Count == 0)
                            {
                                return jo;// (JsonObject)jo;
                            }
                        }
                        else if (token.Value == ",")
                        {
                            if (stack.Peek() is JsonObject)
                            {
                                state = 1;
                            }
                            else if (stack.Peek() is JsonArray)
                            {
                                state = 3;
                            }
                            else
                            {
                                parsingError();
                            }
                        }
                        else
                        {
                            parsingError();
                        }
                        
                        break;
                    default:
                        break;
                }
            }//end foreach
            if (stack.Count == 0)
            {
                parsingError();
            }
            return stack.Pop();

        }



        private void parsingError()
        {
            throw new Exception("Json Parsing Error");
        }

        
    }
}
