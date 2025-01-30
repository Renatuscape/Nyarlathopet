using UnityEngine;

public static class ItemFactory
{
    public static Item GenerateItemFromLocation(Location location, int bonusPoints)
    {
        Item item = new Item()
        {
            level = Random.Range(Mathf.FloorToInt(location.level * 0.5f), location.level + 1)
        };

        if (item.level <= 0)
        {
            item.level = 1;
        }

        int skillPoints = (item.level * 2) + bonusPoints;

        if (location.isRisky)
        {
            skillPoints += Mathf.FloorToInt(item.level * 0.5f);
        }

        while (skillPoints > 0)
        {
            int type = Random.Range(0, 3);

            if (type == 0 && location.hasOccultism)
            {
                item.occultism++;
                skillPoints--;
            }
            else if (type == 1 && location.hasStrength)
            {
                item.strength++;
                skillPoints--;
            }
            else if (type == 2 && location.hasLore)
            {
                item.lore++;
                skillPoints--;
            }
        }

        if (item.lore > item.strength && item.lore > item.occultism) // Lore must be more than strength and magic
        {
            item.type = ItemType.Tome;
        }
        else if (item.occultism >= item.strength && item.lore >= item.strength) // Magick and lore must be more than strength
        {
            item.type = ItemType.Prayer;
        }
        else
        {
            item.type = ItemType.Artefact;
        }

        item.name = RandomNameGenerator.GetRandomItemName(item);

        return item;
    }
}
