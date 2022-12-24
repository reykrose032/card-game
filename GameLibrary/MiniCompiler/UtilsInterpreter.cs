using GameLibrary.Objects;

namespace MiniCompiler;
public static class UtilsForInterpreter
{
    //dado un string,este devuelve la especie a la q se refiere
    public static Species GetSpecieFromString(List<string> splitedUserInput, int position)
    {
        string subject = splitedUserInput[position];
        if (subject == Species.Angel.ToString())
        { splitedUserInput.RemoveAt(position); return Species.Angel; }
        else if (subject == Species.Basilisk.ToString())
        { splitedUserInput.RemoveAt(position); return Species.Basilisk; }
        else if (subject == Species.Cthulhu.ToString())
        { splitedUserInput.RemoveAt(position); return Species.Cthulhu; }
        else if (subject == Species.Dragon.ToString())
        { splitedUserInput.RemoveAt(position); return Species.Dragon; }
        else if (subject == Species.Siren.ToString())
        { splitedUserInput.RemoveAt(position); return Species.Siren; }
        else if (subject == Species.Worm.ToString())
        { splitedUserInput.RemoveAt(position); return Species.Worm; }
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
            splitedUserInput.RemoveAt(position);
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
