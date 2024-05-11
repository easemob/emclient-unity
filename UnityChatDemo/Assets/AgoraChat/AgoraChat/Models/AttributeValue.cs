
using AgoraChat.SimpleJSON;
using System;
using System.Collections.Generic;
#if !_WIN32
using UnityEngine.Scripting;
#endif

namespace AgoraChat
{
    [Preserve]
    public class AttributeValue : BaseModel
    {
        [Preserve]
        public AttributeValue() { }
        [Preserve]
        internal AttributeValue(string json) : base(json) { }
        [Preserve]
        internal AttributeValue(JSONObject jo) : base(jo) { }

        private AttributeValueType VType;

        private bool BoolV;
        private int Int32V;
        private long Int64V;
        private float FloatV;
        private double DoubleV;
        private string StringV;
        private string JsonStringV;
        [Obsolete]
        private List<string> StringVecV;


        public static AttributeValue Of(in object value, AttributeValueType type)
        {
            if (type == AttributeValueType.BOOL)
            {
                return Of((bool)value);
            }
            else if (type == AttributeValueType.INT32)
            {
                return Of((int)value);
            }
            else if (type == AttributeValueType.INT64)
            {
                return Of((long)value);
            }
            else if (type == AttributeValueType.FLOAT)
            {
                return Of((float)Convert.ToDouble(value));
            }
            else if (type == AttributeValueType.DOUBLE)
            {
                return Of((double)value);
            }
            else if (type == AttributeValueType.STRING)
            {
                return Of((string)value);
            }
            else if (type == AttributeValueType.JSONSTRING)
            {
                return Of((string)value, AttributeValueType.JSONSTRING);
            }
            else
            {
                return null;
            }
        }

        internal static AttributeValue Of(in bool value)
        {
            var result = new AttributeValue
            {
                VType = AttributeValueType.BOOL,
                BoolV = value
            };
            return result;
        }
        internal static AttributeValue Of(in int value)
        {
            var result = new AttributeValue
            {
                VType = AttributeValueType.INT32,
                Int32V = value
            };
            return result;
        }

        internal static AttributeValue Of(in long value)
        {
            var result = new AttributeValue
            {
                VType = AttributeValueType.INT64,
                Int64V = value
            };
            return result;
        }
        internal static AttributeValue Of(in float value)
        {
            var result = new AttributeValue
            {
                VType = AttributeValueType.FLOAT,
                FloatV = value
            };
            return result;
        }

        internal static AttributeValue Of(in double value)
        {
            var result = new AttributeValue
            {
                VType = AttributeValueType.DOUBLE,
                DoubleV = value
            };
            return result;
        }

        internal static AttributeValue Of(in string value)
        {
            var result = new AttributeValue();
            {
                result.VType = AttributeValueType.STRING;
                result.StringV = value;
            };
            return result;
        }

        internal static AttributeValue Of(in string value, AttributeValueType type)
        {
            var result = new AttributeValue();
            if (AttributeValueType.JSONSTRING == type)
            {
                result.VType = AttributeValueType.JSONSTRING;
                result.JsonStringV = value;
            }
            else
            {
                result.VType = AttributeValueType.STRING;
                result.StringV = value;
            }
            return result;
        }

        internal object GetAttributeValue(AttributeValueType type)
        {
            if (type == AttributeValueType.BOOL)
            {
                return BoolV;
            }
            else if (type == AttributeValueType.INT32)
            {
                return Int32V;
            }
            else if (type == AttributeValueType.INT64)
            {
                return Int64V;
            }
            else if (type == AttributeValueType.FLOAT)
            {
                return FloatV;
            }
            else if (type == AttributeValueType.DOUBLE)
            {
                return DoubleV;
            }
            else if (type == AttributeValueType.STRING)
            {
                return StringV;
            }
            else if (type == AttributeValueType.JSONSTRING)
            {
                return JsonStringV;
            }
            else
            {
                return null;
            }
        }

        internal AttributeValueType GetAttributeValueType()
        {
            return VType;
        }

        internal override JSONObject ToJsonObject()
        {
            JSONObject jo = new JSONObject();

            string _type = "";
            string _value = "";

            switch (VType)
            {
                case AttributeValueType.BOOL:
                    _type = "b";
                    _value = BoolV.ToString();
                    break;
                case AttributeValueType.INT32:
                    _type = "i";
                    _value = Int32V.ToString();
                    break;
                case AttributeValueType.INT64:
                    _type = "l";
                    _value = Int64V.ToString();
                    break;
                case AttributeValueType.FLOAT:
                    _type = "f";
                    _value = FloatV.ToString();
                    break;
                case AttributeValueType.DOUBLE:
                    _type = "d";
                    _value = DoubleV.ToString();
                    break;
                case AttributeValueType.STRING:
                    _type = "str";
                    _value = StringV;
                    break;
                case AttributeValueType.JSONSTRING:
                    _type = "jstr";
                    _value = JsonStringV;
                    break;
                default:
                    break;
            }
            jo.AddWithoutNull("type", _type);
            jo.AddWithoutNull("value", _value);
            return jo;
        }

        internal override void FromJsonObject(JSONObject jo)
        {
            if (null == jo) return;

            string typeString = jo["type"];
            JSONNode jvalue = jo["value"];
            string value = null;
            if ("strv" != typeString && "attr" != typeString)
            {
                value = jvalue.Value;
            }

            switch (typeString)
            {
                case "b":
                    VType = AttributeValueType.BOOL;
                    BoolV = bool.Parse(value);
                    break;
                case "i":
                    VType = AttributeValueType.INT32;
                    Int32V = int.Parse(value);
                    break;
                case "l":
                    VType = AttributeValueType.INT64;
                    Int64V = long.Parse(value);
                    break;
                case "f":
                    VType = AttributeValueType.FLOAT;
                    FloatV = float.Parse(value);
                    break;
                case "d":
                    VType = AttributeValueType.DOUBLE;
                    DoubleV = double.Parse(value);
                    break;
                case "str":
                    VType = AttributeValueType.STRING;
                    StringV = value;
                    break;
                case "jstr":
                    VType = AttributeValueType.JSONSTRING;
                    JsonStringV = value;
                    break;
                default:
                    break;
            }
            return;
        }

        internal static Dictionary<string, AttributeValue> DictFromJsonObject(JSONNode jn)
        {
            Dictionary<string, AttributeValue> ret = new Dictionary<string, AttributeValue>();

            if (null == jn || !jn.IsObject) return ret;

            JSONObject jo = jn.AsObject;
            foreach (string k in jo.Keys)
            {
                ret.Add(k, new AttributeValue(jo[k].AsObject));
            }
            return ret;
        }

        internal static Dictionary<string, AttributeValue> DictFromJson(string jsonString)
        {
            Dictionary<string, AttributeValue> ret = new Dictionary<string, AttributeValue>();

            if (null == jsonString || jsonString.Length <= 2) return ret;

            JSONNode jn = JSON.Parse(jsonString);
            if (null == jn || !jn.IsObject) return ret;

            return DictFromJsonObject(jn);
        }
    }
}
