using UnityEngine;

public static class ItemFactory
{
    public static Item GenerateItemFromLocation(Location location)
    {
        Item item = new Item()
        {
            level = Random.Range(0, location.level + 1)
        };

        int skillPoints = item.level * 2;

        if (location.isRisky)
        {
            skillPoints += Mathf.FloorToInt(item.level * 0.5f);
        }

        while (skillPoints > 0)
        {
            int type = Random.Range(0, 3);

            if (type == 0 && location.hasMagick)
            {
                item.magick++;
                skillPoints--;
            }
            else if (type == 1 && location.hasStrength)
            {
                item.strength++;
                skillPoints--;
            }
            else if (type == 2 && location.hasLore)
            {
                item.strength++;
                skillPoints--;
            }
        }

        if (item.lore > item.strength && item.lore >= item.magick)
        {
            item.type = ItemType.Tome;
        }
        else if (item.magick > item.strength && item.magick > item.lore)
        {
            item.type = ItemType.Prayer;
        }
        else
        {
            item.type = ItemType.Artefact;
        }

        item.name = RandomNameGenerator.GetRandomItemName(item);

        if (location.isRisky)
        {
            item.name += " +";
        }

        return item;
    }
}
