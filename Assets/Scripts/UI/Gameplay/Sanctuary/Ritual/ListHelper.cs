using System.Collections.Generic;

public static class ListHelper
{
    public static string GetCurrentAndTotalPagesString(int index, int objectsTotal, int objectsPerPage)
    {
        Report.Write("ListHelper", "Attempting to display humans from index.");
        int currentPage = (index / objectsPerPage) + 1;
        int totalPages = (objectsTotal + objectsPerPage - 1) / objectsPerPage;
        return currentPage + "/" + totalPages;
    }

    public static void DisplayHumansFromIndex(int index, List<Human> humans, List<HumanUiPrefab> prefabSlots)
    {
        Report.Write("ListHelper", $"Attempting to display {humans.Count} humans from index {index} in {prefabSlots.Count} slots.");
        for (int slotIndex = 0; slotIndex < prefabSlots.Count; slotIndex++)
        {
            int currentItemIndex = index + slotIndex;

            if (currentItemIndex < humans.Count)
            {
                prefabSlots[slotIndex].SetDisplay(humans[currentItemIndex]);
                prefabSlots[slotIndex].gameObject.SetActive(true);
            }
            else
            {
                prefabSlots[slotIndex].gameObject.SetActive(false);
            }
        }
    }

    public static void DisplayItemsFromIndex(int index, List<Item> items, List<ItemUiPrefab> prefabSlots)
    {
        for (int slotIndex = 0; slotIndex < prefabSlots.Count; slotIndex++)
        {
            int currentItemIndex = index + slotIndex;

            if (currentItemIndex < items.Count)
            {
                prefabSlots[slotIndex].SetDisplay(items[currentItemIndex]);
                prefabSlots[slotIndex].gameObject.SetActive(true);
            }
            else
            {
                prefabSlots[slotIndex].gameObject.SetActive(false);
            }
        }
    }
}