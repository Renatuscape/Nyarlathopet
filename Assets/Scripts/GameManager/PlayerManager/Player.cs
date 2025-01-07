using System.Threading.Tasks;

public static class Player
{
    public static PlayerData Data { get; private set; }

    public static async Task CreateNewGameDataAsync()
    {
        Data = new()
        {
            cultName = "Curators of the Masks",
            level = 1,
            funds = 100,
            cultLeader = CultistFactory.GetCultist(1),
            cultMembers = new(),
            inventory = new(),
        };

        Data.cultLeader.sanity += 10;

        Report.Write("Player", "Created save data: " + Data.ToString());
        await SaveManager.SaveDataAsync();
    }

    public static void SetPlayerData(PlayerData data)
    {
        Data = data;
    }
}
