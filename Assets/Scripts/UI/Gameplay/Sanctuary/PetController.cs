using System.Linq;
using UnityEngine;

public static class PetController
{
    public static void FeedPet()
    {
        if (GameplayManager.dummyData.cultMembers.Count < 1)
        {
            AlertSystem.Print("You have no cult members to sacrifice.");
        }
        if (GameplayManager.isPetFed)
        {
            AlertSystem.Prompt($"You already fed your {GameplayManager.dummyData.currentPet.name} this month. Feed again?", () =>
            {
                ConfirmVolunteer();
            });
        }
        else
        {
            ConfirmVolunteer();
        }
    }

    static void ConfirmVolunteer()
    {
        var volunteer = FindSacrifice();

        AlertSystem.Prompt($"{volunteer.name} has volunteered.\n{volunteer.GetStatPrintOut(true, false)}\n\nProceed?", () =>
        {
            ExecuteFeeding(volunteer);
        });
    }

    static void ExecuteFeeding(Human volunteer)
    {
        GameplayManager.dummyData.cultMembers.Remove(volunteer);
        GameplayManager.dummyData.funds += volunteer.funds;
        GameplayManager.isPetFed = true;

        var changes = StatConverter.ConvertHumanToCreatureStats(volunteer, 0);
        var report = StatConverter.CreateCreatureStatChangeReport(changes, true);

        AlertSystem.Print($"{GameplayManager.dummyData.currentPet.name} consumed {volunteer.name}.\n" + report);

        GameplayManager.dummyData.currentPet.ApplyStatChanges(changes);
        SanctuaryManager.instance.UpdateDisplay();
    }

    static Human FindSacrifice()
    {
        var cultists = GameplayManager.dummyData.cultMembers.OrderBy(c => c.sanity).ToList();
        Human volunteer;

        if (cultists.Count > 3)
        {
            volunteer = cultists[Random.Range(0, 3)]; 
        }
        else
        {
            volunteer = cultists[0];
        }

        return volunteer;
    }
}
