namespace AgoraChat
{
    public abstract class BaseModel
    {

        internal BaseModel() { }

        internal BaseModel(string jsonString)
        {
            if (jsonString.Length > 0)
            {
                SimpleJSON.JSONNode jn = SimpleJSON.JSON.Parse(jsonString);
                if (jn.IsObject) {
                    FromJsonObject(jn.AsObject);
                }
            }
        }

        internal BaseModel(SimpleJSON.JSONObject jsonObject)
        {
            FromJsonObject(jsonObject);
        }

        internal abstract void FromJsonObject(SimpleJSON.JSONObject jsonObject);
    }

    public interface ToJsonInterface
    {
        SimpleJSON.JSONObject ToJson();
    }
}