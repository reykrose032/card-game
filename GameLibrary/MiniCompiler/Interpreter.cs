using GameLibrary;
namespace MiniCompiler
{
    public class Interpreter
    {
        List<string> splitedInput = new List<string>();
        List<Token> tokens = new List<Token>();
        string input = "";
        public Interpreter(string input)
        {
            this.input = input;
            Tokenizing();
            Parsing();
        }

        public string newCardName = "";
        public int newCardATK;
        public int newCardHealth;
        public Species newCardSpecie;
        void Tokenizing()
        {
            splitedInput = input.Split(' ', StringSplitOptions.TrimEntries).ToList();
            for (int position = 0; position < splitedInput.Count; position++)
            {
                switch (splitedInput[position])
                {
                    case "Name:":
                        tokens.Add(new Token(TokenType.NAME, SubTokenizingString(position + 1)));
                        break;
                    case "Health:":
                        tokens.Add(new Token(TokenType.HEALTH, "", int.Parse(splitedInput[position + 1])));
                        splitedInput.RemoveAt(position + 1);
                        break;
                    case "ATK:":
                        tokens.Add(new Token(TokenType.ATK, "", int.Parse(splitedInput[position + 1])));
                        splitedInput.RemoveAt(position + 1);
                        break;
                    case "Specie:":
                        tokens.Add(new Token(TokenType.SPECIE, GetSpecieFromString(splitedInput[position + 1])));
                        break;
                    case ";":
                        tokens.Add(new Token(TokenType.EOF));
                        break;
                    default:
                        tokens.Add(new Token(TokenType.EOF));
                        break;
                }
            }
        }
        Species GetSpecieFromString(string input)
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
        string SubTokenizingString(int position)
        {
            string result = "";
            while (splitedInput[position] != ";")
            {
                result += splitedInput[position];
                result += " ";
                splitedInput.RemoveAt(position);

            }
            return result;
        }
        void ParseCardPropierties()
        {
            for (int i = 0; i < tokens.Count; i++)
            {
                switch (tokens[i].type)
                {
                    case TokenType.NAME:
                        newCardName = tokens[i].represent;
                        break;
                    case TokenType.HEALTH:
                        newCardHealth = tokens[i].value;
                        break;
                    case TokenType.ATK:
                        newCardATK = tokens[i].value;
                        break;
                    case TokenType.SPECIE:
                        newCardSpecie = tokens[i].specie;
                        break;
                    case TokenType.EOF:
                        continue;
                    default:
                        break;
                }
            }
        }
        void Parsing()
        {
            ParseCardPropierties();
        }
    }
}