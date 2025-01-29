using System;
using System.Collections.Generic;
using System.Linq;

public static class PetController
{
    public static void FeedPet()
    {
        if (GameplayManager.dummyData.cultMembers.Count < 1)
        {
            AlertSystem.Print(Repository.GetText("PET-F0"));
        }
        if (GameplayManager.isPetFed)
        {
            AlertSystem.Prompt($"{Repository.GetText("PET-F1A")} {GameplayManager.dummyData.currentPet.name} {Repository.GetText("PET-F1B")}", () =>
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
        // Options - how would you like to commune with your pet?
        // Commune in private - Increase stats or level, reducing specified pet stat (increases rage), requires tome
        // Volunteer a cultist - Reduce a specified pet stat, increasing cultist stat (increases rage), requires tome
        // Everyone will commune - spend a tome to increase everyone's sanity a little, increasing rage, requires tome
    }

    public static void FinaliseSummoning()
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
        AlertSystem.Choice(Repository.GetText("PET-F2A"), choices, false);
    }

    static void ExecuteFeeding(Human volunteer)
    {
        GameplayManager.dummyData.cultMembers.Remove(volunteer);
        GameplayManager.dummyData.funds += volunteer.funds;
        GameplayManager.isPetFed = true;

        var changes = StatConverter.ConvertHumanToCreatureStats(volunteer, 0);
        var report = StatConverter.CreateCreatureStatChangeReport(changes, true);

        AlertSystem.Print($"{GameplayManager.dummyData.currentPet.name} {Repository.GetText("PET-F3")} {volunteer.name}.\n" + report);

        GameplayManager.dummyData.currentPet.ApplyStatChanges(changes);
        SanctuaryManager.instance.UpdateDisplay();
    }
}
