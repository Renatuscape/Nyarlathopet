using System;
using System.Collections.Generic;
using System.Linq;

public static class PetController
{
    public static void FeedPet()
    {
        if (GameplayManager.dummyData.cultMembers.Count < 1)
        {
            AlertSystem.Print(Text.Get("PET-F0"));
        }
        if (GameplayManager.isPetFed)
        {
            AlertSystem.Prompt($"{Text.Get("PET-F1A")} {GameplayManager.dummyData.currentPet.name} {Text.Get("PET-F1B")}", () =>
            {
                PromptChooseVolunteerToFeedPet();
            });
        }
        else
        {
            PromptChooseVolunteerToFeedPet();
        }
    }

    public static void WorshipPet()
    {
        PetWorshipHelper.Initialise();
    }

    public static void CommuneWithPet()
    {
        PetCommuneHelper.Initialise();
    }

    public static void CompleteRitual()
    {
        // Confirmation
            // Execute final summoning (event?)
            // Check if enemies will prevent summoning
            // Add new mask to book of masks
            // Set current pet to null and start new round
    }


    static void PromptChooseVolunteerToFeedPet()
    {
        var members = GameplayManager.dummyData.cultMembers.OrderBy(m => m.sanity).ToList();
        Report.Write("PetController - ChooseVolunteer", "Members found: " + members.Count);

        List<(string, Action)> choices = new();

        for (int i = 0; i < members.Count && i < AlertSystem.MaxOptions; i++)
        {
            var capturedIndex = i;
            Report.Write("PetController - ConfirmVolunteer", "Captured index: " + capturedIndex);
            choices.Add((members[capturedIndex].Print(), () => ExecuteFeeding(members[capturedIndex])));
        }

        Report.Write("PetController - ConfirmVolunteer", "Choices created: " + choices.Count);
        AlertSystem.Choice(Text.Get("PET-F2A"), choices, false);
    }

    static void ExecuteFeeding(Human volunteer)
    {
        GameplayManager.dummyData.cultMembers.Remove(volunteer);
        GameplayManager.dummyData.funds += volunteer.funds;
        GameplayManager.isPetFed = true;

        var changes = StatConverter.ConvertHumanToCreatureStats(volunteer, 0);
        var report = StatConverter.CreateCreatureStatChangeReport(changes, true);

        AlertSystem.Print($"{GameplayManager.dummyData.currentPet.name} {Text.Get("PET-F3")} {volunteer.name}.\n" + report);

        GameplayManager.dummyData.currentPet.ApplyStatChanges(changes);
        SanctuaryManager.instance.UpdateDisplay();
    }
}
