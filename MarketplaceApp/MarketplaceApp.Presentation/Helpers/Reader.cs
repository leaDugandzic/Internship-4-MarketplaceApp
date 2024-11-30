using MarketplaceApp.Domain.Enum;

namespace MarketplaceApp.Presentation.Helpers;

public class Reader
{
    
    public static DateTime ReadDate(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            if (DateTime.TryParseExact(input, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime date))
            {
                return date;
            }
            else
            {
                Console.WriteLine("Neispravan format datuma! Molimo unesite datum u formatu dd.MM.yyyy.");
            }
        }
    }
    public static bool TryReadLine(string message, out string line)
    {
        line = string.Empty;

        Console.WriteLine(message);
        var input = Console.ReadLine();
        var isEmpty = string.IsNullOrWhiteSpace(input);

        if (!isEmpty && input is not null)
            line = input;

        return !isEmpty;
    }
    public static bool TryReadNumber(string message, out int number)
    {
        Console.WriteLine(message);
        return TryReadNumber(out number);
    }
    public static bool TryReadNumber(out int number)
    {
        number = 0;
        var isNumber = int.TryParse(Console.ReadLine(), out var inputNumber);
        if (!isNumber)
            return false;

        number = inputNumber;
        return true;
    }
    
    public static bool DoYouWantToContinue()
    {
        Console.WriteLine("Za povratak na prethodnu stranicu pritisnite y");
        var input = Console.ReadLine();
        if (input == "y")
            return false;
        return true;
    }
    
    public static void ReadInput(out string input)
    {
        input = Console.ReadLine() ?? string.Empty;
    }

    public static string? ReadImeInput()
    {
        Console.WriteLine("Unesite ime: ");
        return Console.ReadLine() ?? string.Empty;
    }
    
    public static string? ReadEmailInput()
    {
        Console.WriteLine("Unesite email: ");
        var input = Console.ReadLine();
        string[] inputSplitByMonkey= input.Split('@');
        if (inputSplitByMonkey.Length !=2)
        { 
            Writer.Error("Email treba sadržavati samo jedan znak @");
            return null;
        }
        return input;
    }
}