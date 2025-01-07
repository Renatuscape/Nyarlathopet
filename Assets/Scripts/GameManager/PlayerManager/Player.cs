public static class Player
{
    public static PlayerData Data { get; private set; }

    public static void CreateNewGameData()
    {
        Data = new();

        Data.cultName = "Curators of the Masks";
        Data.level = 1;
        Data.funds = 100;
        Data.cultLeader = CultistFactory.GetCultist(1);
    }
}
