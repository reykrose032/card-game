using GameLibrary;
using GameLibrary.Objects;

namespace MiniCompiler
{

    public class Interpreter //modificar la implementacion grafica de effectos para utilizar y probar el funcionamiento de Effect // introducir condicionales
    {
        string input = "";
        public string newInitialCardName = "";
        public int newInitialCardATK;
        public int newInitialCardHealth;
        public Species newInitialCardSpecie;
        List<string> splitedInput = new List<string>(); //Poseera string de la entrada del usuario 
        List<Token> tokens = new List<Token>();
        GodTree arbolGod = new GodTree();
        public static Dictionary<string, int> cardsStatsDic = new Dictionary<string, int>(); //relaciona cada nombre de variable con su valor

        public Interpreter(string input)
        {
            this.input = input;
            Tokenizing(); //creando tokens a partir de la entrada del usuario
            ParseGod(arbolGod.instructions, 0);
        }
        public void ExecuteCode()
        {
            arbolGod.Execute();
        }


        public void FillQuantumDictionary(Card ownCard, Card enemyCard)
        {
            cardsStatsDic.Add("ownCard.Health", ownCard.Health);
            cardsStatsDic.Add("ownCard.MaxHealth", ownCard.MaxHealthValue);
            cardsStatsDic.Add("ownCard.AttackValue", ownCard.AttackValue);
            cardsStatsDic.Add("ownCard.MaxAttackValue", ownCard.MaxAttackValue);

            cardsStatsDic.Add("enemyCard.Health", enemyCard.Health);
            cardsStatsDic.Add("ownCard.MaxHealth", enemyCard.MaxHealthValue);
            cardsStatsDic.Add("enemyCard.AttackValue", enemyCard.AttackValue);
            cardsStatsDic.Add("enemyCard.MaxAttackValue", enemyCard.MaxAttackValue);
        }

        //esto es para modificar las estadisticas de las cartas 
        public void UpdateCardStats(Card ownCard, Card enemyCard)
        {
            ownCard.Health = cardsStatsDic["ownCard.Health"];
            ownCard.MaxHealthValue = cardsStatsDic["ownCard.MaxHealthValue"];
            ownCard.AttackValue = cardsStatsDic["ownCard.AttackValue"];
            ownCard.MaxAttackValue = cardsStatsDic["ownCard.MaxAttackValue"];

            enemyCard.Health = cardsStatsDic["ownCard.Health"];
            enemyCard.MaxHealthValue = cardsStatsDic["ownCard.MaxHealthValue"];
            enemyCard.AttackValue = cardsStatsDic["ownCard.AttackValue"];
            enemyCard.MaxAttackValue = cardsStatsDic["ownCard.MaxAttackValue"];
        }


        //esto es para modificar las estadisticas del diccionario quantumVariables
        public void UpdateCardStatsDic(Card ownCard, Card enemyCard)
        {
            cardsStatsDic["ownCard.Health"] = ownCard.Health;
            cardsStatsDic["ownCard.MaxHealthValue"] = ownCard.MaxHealthValue;
            cardsStatsDic["ownCard.AttackValue"] = ownCard.AttackValue;
            cardsStatsDic["ownCard.MaxAttackValue"] = ownCard.MaxAttackValue;

            cardsStatsDic["ownCard.Health"] = enemyCard.Health;
            cardsStatsDic["ownCard.MaxHealthValue"] = enemyCard.MaxHealthValue;
            cardsStatsDic["ownCard.AttackValue"] = enemyCard.AttackValue;
            cardsStatsDic["ownCard.MaxAttackValue"] = enemyCard.MaxAttackValue;
        }


        //tokenizando para guardar tokens en lista Tokens
        void Tokenizing()
        {
            splitedInput = input.Split(' ', StringSplitOptions.TrimEntries).ToList();
            for (int position = 0; position < splitedInput.Count; position++)
            {
                switch (splitedInput[position])
                {
                    case "Name:":
                        tokens.Add(new Token(TokenType.NAME, AddStringFromPositionToEOF(position + 1)));
                        break;
                    case "InitialHealth:":
                        tokens.Add(new Token(TokenType.HEALTH, splitedInput[position + 1]));
                        break;
                    case "InitialATK:":
                        tokens.Add(new Token(TokenType.ATK, splitedInput[position + 1]));
                        break;
                    case "Specie:":
                        tokens.Add(new Token(TokenType.SPECIE, GetSpecieFromString(splitedInput[position + 1])));
                        break;
                    case "IF:":
                        tokens.Add(new Token(TokenType.IF));
                        break;
                    case "EndIF":
                        tokens.Add(new Token(TokenType.END));
                        break;
                    case "+":
                        tokens.Add(new Token(TokenType.PLUS, "+"));
                        break;
                    case "-":
                        tokens.Add(new Token(TokenType.MULT, "-"));
                        break;
                    case "*":
                        tokens.Add(new Token(TokenType.MINUS, "*"));
                        break;
                    case "/":
                        tokens.Add(new Token(TokenType.DIV, "/"));
                        break;
                    case ";":
                        tokens.Add(new Token(TokenType.END));
                        break;
                    default:
                        break;
                }
                if (IsNumber(splitedInput[position]))
                    tokens.Add(new Token(TokenType.NUMBER, splitedInput[position]));
                else if (splitedInput[position].Contains("Card"))
                    tokens.Add(new Token(TokenType.IDENTIFIER, splitedInput[position]));
            }
        }

        void ParseGod(List<Iinstruction> instructionsList, int InitialPosition)
        {
            for (int position = InitialPosition; position < tokens.Count; position++)
            {
                if (tokens[position].type == TokenType.IDENTIFIER)
                    instructionsList.Add(new Assignment(tokens[position], ParseExpr(position)));
                else if (tokens[position].type == TokenType.IF)
                {
                    List<Iinstruction> newIfInstructionList = new List<Iinstruction>();
                    ParseGod(newIfInstructionList, position);
                    instructionsList.Add(new IF(ParseExpr(position), newIfInstructionList));
                }
                else if (tokens[position].type == TokenType.END)
                    break;
            }
        }

        IExpr ParseExpr(int startOfExpr)
        {
            Stack<IExpr> stack = new Stack<IExpr>();
            for (int position = startOfExpr; ; position++)
            {
                if (tokens[position].type == TokenType.END)
                {
                    tokens.RemoveAt(position); //lo remuevo ya q ParseGod se detiene cuando encuentra un token tipo END
                    break;
                }
                if (tokens[position].type == TokenType.NUMBER)
                {
                    stack.Push(new Number(int.Parse(tokens[position].value)));
                    tokens.RemoveAt(position);
                }
                else if (tokens[position].type == TokenType.IDENTIFIER)
                {
                    stack.Push(new Identifier(tokens[position].value));
                    tokens.RemoveAt(position);
                }
                else if (stack.Count >= 2)
                {
                    var right = stack.Pop(); //el orden importa
                    var left = stack.Pop();

                    if (tokens[position].type == TokenType.PLUS)
                    {
                        stack.Push(new Plus(left, right));
                        tokens.RemoveAt(position);
                    }
                    else if (tokens[position].type == TokenType.MINUS)
                    {
                        stack.Push(new Minus(left, right));
                        tokens.RemoveAt(position);
                    }
                    else if (tokens[position].type == TokenType.MULT)
                    {
                        stack.Push(new Mult(left, right));
                        tokens.RemoveAt(position);
                    }
                    else if (tokens[position].type == TokenType.DIV)
                    {
                        stack.Push(new Div(left, right));
                        tokens.RemoveAt(position);
                    }
                    else if (tokens[position].type == TokenType.HIGHER)
                    {
                        stack.Push(new Higher(left, right));
                        tokens.RemoveAt(position);
                    }
                    else if (tokens[position].type == TokenType.MINOR)
                    {
                        stack.Push(new Minor(left, right));
                        tokens.RemoveAt(position);
                    }
                    else if (tokens[position].type == TokenType.SAME)
                    {
                        stack.Push(new Same(left, right));
                        tokens.RemoveAt(position);
                    }
                    else if (tokens[position].type == TokenType.AND)
                    {
                        stack.Push(new AND(left, right));
                        tokens.RemoveAt(position);
                    }
                    else if (tokens[position].type == TokenType.OR)
                    {
                        stack.Push(new OR(left, right));
                        tokens.RemoveAt(position);
                    }
                }
            }
            return stack.Pop();
        }

        //dado un string,este devuelve la especie a la q se refiere
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


        //guarda un string desde la posicion "position" hasta el simbolo ";"
        string AddStringFromPositionToEOF(int position)
        {
            string result = "";
            while (splitedInput[position] != ";")
            {
                result += splitedInput[position];
                result += " ";
                position++;
            }
            return result;
        }


        bool IsNumber(string expresion)
        {
            foreach (var item in expresion)
            {
                if (!char.IsDigit(item))
                    return false;
            }
            return true;
        }

    }
}