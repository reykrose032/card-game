namespace Utils;
public static class InputValidation
{
    public static bool IsDigit(ConsoleKeyInfo userInput)
    {
        if (!char.IsDigit(userInput.KeyChar))
        {
            Console.WriteLine("Key is not a digit.");
            return false;
        }

        return true;
    }
    public static bool IsIndexOutOfBounds(int cardIndex, int upperBound)
    {
        if (cardIndex >= upperBound)
        {
            System.Console.WriteLine("Not a card in there.");
            return false;
        }

        return true;
    }
}