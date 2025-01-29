using System.Collections.Generic;
using System.Linq;

public static class ListHelper
{
    public static string GetCurrentAndTotalPagesString(int index, int objectsTotal, int objectsPerPage)
    {
        Report.Write("ListHelper", $"Getting current page string from index {index} with total objects {objectsTotal} and objects per page {objectsPerPage}");
        int currentPage = (index / objectsPerPage) + 1;
        int totalPages = (objectsTotal + objectsPerPage - 1) / objectsPerPage;
        return currentPage + "/" + totalPages;
    }

    public static void DisplayHumansFromIndex(int index, List<Human> humans, List<HumanUiPrefab> prefabSlots, out bool isListEmpty)
    {
        Report.Write("ListHelper", $"Attempting to display {humans.Count} humans from index {index} in {prefabSlots.Count} slots.");
        isListEmpty = true;

        for (int slotIndex = 0; slotIndex < prefabSlots.Count; slotIndex++)
        {
            int currentItemIndex = index + slotIndex;

            if (currentItemIndex < humans.Count)
            {
                prefabSlots[slotIndex].SetDisplay(humans[currentItemIndex]);
                prefabSlots[slotIndex].gameObject.SetActive(true);
                isListEmpty = false;
            }
            else
            {
                prefabSlots[slotIndex].gameObject.SetActive(false);
            }
        }
    }

    public static void DisplayArtefactsFromIndex(int index, List<Item> items, List<ItemUiPrefab> prefabSlots, out bool isListEmpty)
    {
        var artefacts = items.Where(i => i.type == ItemType.Artefact).ToList();
        isListEmpty = true;

        for (int slotIndex = 0; slotIndex < prefabSlots.Count; slotIndex++)
        {
            int currentItemIndex = index + slotIndex;

            if (currentItemIndex < items.Count)
            {
                prefabSlots[slotIndex].SetDisplay(items[currentItemIndex]);
                prefabSlots[slotIndex].gameObject.SetActive(true);
                isListEmpty = false;
            }
            else
            {
                prefabSlots[slotIndex].gameObject.SetActive(false);
            }
        }
    }

    public static void DisplayItemsFromIndex(int index, List<Item> items, List<ItemUiPrefab> prefabSlots, out bool isListEmpty)
    {
        isListEmpty = true;

        for (int slotIndex = 0; slotIndex < prefabSlots.Count; slotIndex++)
        {
            int currentItemIndex = index + slotIndex;

            if (currentItemIndex < items.Count)
            {
                prefabSlots[slotIndex].SetDisplay(items[currentItemIndex]);
                prefabSlots[slotIndex].gameObject.SetActive(true);
                isListEmpty = false;
            }
            else
            {
                prefabSlots[slotIndex].gameObject.SetActive(false);
            }
        }
    }
}