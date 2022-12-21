using GameLibrary;

namespace MiniCompiler;
public static class UtilsForInterpreter
{
    //dado un string,este devuelve la especie a la q se refiere
    public static Species GetSpecieFromString(string input)
    {
        if (input == Species.Angel.ToString())
            return Species.Angel;
        else if (input == Species.Basilisk.ToString())
            return Species.Basilisk;
        else if (input == Species.Cthulhu.ToString())
            return Species.Cthulhu;
        else if (input == Species.Dragon.ToString())
            return Species.Dragon;
        else if (input == Species.Siren.ToString())
            return Species.Siren;
        else if (input == Species.Worm.ToString())
            return Species.Worm;
        throw new Exception();
    }


    //guarda un string desde la posicion "position" hasta el simbolo ";"
    public static string AddStringFromPositionToEOF(List<string> splitedUserInput, int position)
    {
        string result = "";
        while (splitedUserInput[position] != ";")
        {
            result += splitedUserInput[position];
            result += " ";
            position++;
        }
        return result;
    }


    public static bool IsNumber(string expresion)
    {
        foreach (var item in expresion)
        {
            if (!char.IsDigit(item))
                return false;
        }
        return true;
    }

}
