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

        Data.cultLeader.ApplyStatChanges(new() { sanity = 10});
        Data.cultLeader.origin = "Unknown";
    }

    public static void SetPlayerData(PlayerData data)
    {
        Data = data;
    }

    public static void CreateDummyData()
    {
        Data = new();
        Data.CreateDummyData();

        if (Repository.pets == null) {
            Repository.CreateDummyData();
        }
    }
}
