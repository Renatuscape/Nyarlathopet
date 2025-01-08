using System.Threading.Tasks;

public static class Player
{
    public static PlayerData Data { get; private set; }

    public static void CreateNewGameData()
    {
        Data = new()
        {
            cultName = RandomNameGenerator.GetRandomCultName(),
            level = 1,
            funds = 100,
            cultLeader = CultistFactory.GetCultist(1),
            cultMembers = new(),
            inventory = new(),
        };

        Data.cultLeader.sanity += 10;
    }

    public static void SetPlayerData(PlayerData data)
    {
        Data = data;
    }
}
