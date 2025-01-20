// Node on the map. Level denotes when it will be accessible to visit, x and y are map coordinates
using System;
using Newtonsoft.Json;

[System.Serializable]
public class Location: Entity
{
    public int level = 1;           // Player level must be at least this high

    public int x;
    public int y;

    public bool hasMagick;      // Items from here can increase magick
    public bool hasStrength;    // Items from here can increase stength
    public bool hasLore;        // Items from here can increase lore
    public bool hasMoney;       // Money can be gained here
    public bool isRisky;        // A risky location has higher rewards and higher potential costs
    
    [JsonConverter(typeof(LocationTypeConverter))]
    public LocationType type = LocationType.Society;
}

public enum LocationType
{
    Society,
    Natural,
    Landmark,
    Religion,
    Archaeology
}

public class LocationTypeConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(LocationType);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null) return LocationType.Society;

        string enumString = reader.Value?.ToString();
        if (string.IsNullOrEmpty(enumString))
            return LocationType.Society;

        if (Enum.TryParse<LocationType>(enumString, out LocationType result))
            return result;

        return LocationType.Society;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value == null)
        {
            writer.WriteNull();
            return;
        }

        writer.WriteValue(value.ToString());
    }
}