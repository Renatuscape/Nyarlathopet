using System;
using UnityEngine;
using Random = UnityEngine.Random;

public static class RitualSummonHelper
{
    public static void CommenceRitual(CreatureStats ritualState)
    {
        if (FindAndStoreEligiblePet(ritualState))
        {
            AlertSystem.Force($"{Text.Get("RIT-SUCCA")} {GameplayManager.dummyData.currentPet.name} {Text.Get("RIT-SUCCB")}", () =>
            {
                PerformEndRoutine(true);
            });
        }
        else
        {
            AlertSystem.Force(Text.Get("RIT-FAIL"), () =>
            {
                PerformEndRoutine(false);
            });
        }
    }

    static bool FindAndStoreEligiblePet(CreatureStats ritualState)
    {
        // Search repository for the pet that best matches the ritual state
        var foundPet = Repository.GetPetByStats(ritualState);

        if (foundPet == null || foundPet.id == "-1")
        {
            return false;
        }

        // Store copy of pet in dummy data
        GameplayManager.dummyData.currentPet = foundPet;
        GameplayManager.isPetFed = true;

        return true;
    }

    static void ExecutePlayerConsequences(out string report)
    {
        var pet = GameplayManager.dummyData.currentPet;
        var book = GameplayManager.dummyData.bookOfMasks;
        int sanLoss;
        report = "";

        if (book.Contains(pet.id))
        {
            sanLoss = 1;
            report += "You are familiar with this form already, and your mind is braced for its horrors.";
        }
        else
        {
            book.Add(pet.id);
            sanLoss = Mathf.CeilToInt(Random.Range(pet.sanityLoss * 0.5f, pet.sanityLoss));
            report += "Upon witnessing an unfamiliar fragment of Nyarlathotep, you are forever altered.";

            if (sanLoss < 1)
            {
                sanLoss = 1;
            }
        }

        report += "\nThe ritual empowers you.\n";

        int skillPoints = Mathf.CeilToInt(sanLoss * 0.5f);

        if (skillPoints < 1)
        {
            skillPoints = 1;
        }

        int magick = 0;
        int strength = 0;
        int lore = 0;

        while (skillPoints > 0)
        {
            int type = Random.Range(0, 3);

            if (type == 0)
            {
                magick++;
                skillPoints--;
            }
            else if (type == 1)
            {
                strength++;
                skillPoints--;
            }
            else if (type == 2)
            {
                lore++;
                skillPoints--;
            }
        }

        report += $"{Tags.Get("SAN")}-{sanLoss} {Tags.Get("MGK")}+{magick} {Tags.Get("STR")}+{strength} {Tags.Get("LOR")}+{lore}";

        GameplayManager.dummyData.cultLeader.ApplyStatChanges(new()
        {
            sanity = -sanLoss,
            occultism = magick,
            strength = strength,
            lore = lore
        });
    }

    static void ExecuteCultMemberConsequences(out string report)
    {
        var pet = GameplayManager.dummyData.currentPet;

        foreach (var member in GameplayManager.dummyData.cultMembers)
        {
            int sanLoss = Mathf.CeilToInt(Random.Range(pet.sanityLoss * 0.33f, pet.sanityLoss));
            member.ApplyStatChanges(new() { sanity = -sanLoss});
        }

        RitualController.RemoveInstaneCultists(out report);

        if (GameplayManager.dummyData.cultMembers.Count > 0 && string.IsNullOrEmpty(report))
        {
            report = Text.Get("RIT-CULTCONS");
        }
    }

    static void PerformEndRoutine(bool isSuccess)
    {
        Action endRound = () => GameplayManager.SubtractEndeavourPoints(GameplayManager.EndeavourPoints);

        if (isSuccess)
        {
            ExecutePlayerConsequences(out var report);

            AlertSystem.Force(report, () =>
            {
                if (GameplayManager.dummyData.cultMembers.Count > 0)
                {
                    ExecuteCultMemberConsequences(out var memberReport);

                    AlertSystem.Force(memberReport, () =>
                    {
                        endRound.Invoke();
                    });
                }
                else
                {
                    endRound.Invoke();
                }
            });
        }
        else
        {
            endRound.Invoke();
        }
        // Use all EP and force a new round.
    }
}