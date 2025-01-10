using System;

public static class DateCalculator
{
    private static readonly DateTime StartDate = new DateTime(1920, 1, 1);

    public static string GetGameDate(int round)
    {
        // Add months to starting date
        DateTime calculatedDate = StartDate.AddMonths(round);

        // Format as "MMMyyy" to get "Jan1920" style output
        return calculatedDate.ToString("MMMyyyy");
    }

    // Alternative version if you want month and year separately
    public static (string month, int year) GetGameDateComponents(int round)
    {
        DateTime calculatedDate = StartDate.AddMonths(round);
        return (
            month: calculatedDate.ToString("MMM"),
            year: calculatedDate.Year
        );
    }
}