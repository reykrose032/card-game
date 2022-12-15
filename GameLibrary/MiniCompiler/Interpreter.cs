using GameLibrary;
using GameLibrary.Objects;


namespace MiniCompiler
{
    public delegate void Actions();

    public class Interpreter //modificar la implementacion grafica de effectos para utilizar y probar el funcionamiento de Effect // introducir condicionales
    {
        public string input = "";

        List<string> splitedInput = new List<string>(); //Poseera string de la entrada del usuario 
        List<Token> tokens = new List<Token>();
        ClientEffectInstructions arbolGod = new ClientEffectInstructions();
        // public static Dictionary<string, int> cardsStatsDic = new Dictionary<string, int>(); //relaciona cada nombre de variable entre ambas cartas con su valor
        // public static Dictionary<string, Actions> gameActions = new Dictionary<string, Actions>();

        public string newInitialCardName = ""; public int newInitialCardATK; public int newInitialCardHealth; public Species newInitialCardSpecie;

        public void EatCode(string input)
        {
            // FillGameStatsDic();
            Tokenizing(); //creando tokens a partir de la entrada del usuario
            ParseCardProperties();
            ParseEffect(arbolGod.instructions);
        }

        public Card buildCard(Player owner)
        {
            return new Card(newInitialCardName, newInitialCardATK, newInitialCardHealth, newInitialCardSpecie, owner);
        }

        public IEffect buildEffect()
        {
            return new ClientEffect(arbolGod);
        }



        // public static void FillGameActionsDic(Card ownCard, Card enemyCard)
        // {
        //     gameActions.Add("Draw()", ownCard.owner.Draw);
        //     gameActions.Add("IncreaseEnergy()", ownCard.owner.IncreaseEnergy);
        //     gameActions.Add("DecreaseEnergy()", ownCard.owner.DecreaseEnergy);
        //     gameActions.Add("enemyDraw()", enemyCard.owner.Draw);
        //     gameActions.Add("enemyIncreaseEnergy()", enemyCard.owner.IncreaseEnergy);
        //     gameActions.Add("enemyDecreaseEnergy()", enemyCard.owner.DecreaseEnergy);
        // }

        // public static void UpdateGameActionsDic(Card ownCard, Card enemyCard)
        // {
        //     gameActions["Draw()"] = ownCard.owner.Draw;
        //     gameActions["IncreaseEnergy()"] = ownCard.owner.IncreaseEnergy;
        //     gameActions["DecreaseEnergy()"] = ownCard.owner.DecreaseEnergy;
        //     gameActions["enemyDraw()"] = enemyCard.owner.Draw;
        //     gameActions["enemyIncreaseEnergy()"] = enemyCard.owner.IncreaseEnergy;
        //     gameActions["enemyDecreaseEnergy()"] = enemyCard.owner.DecreaseEnergy;
        // }

        // public static void FillGameStatsDic()
        // {
        //     cardsStatsDic.Add("ownCard.Health", 0);
        //     cardsStatsDic.Add("ownCard.MaxHealth", 0);
        //     cardsStatsDic.Add("ownCard.AttackValue", 0);
        //     cardsStatsDic.Add("ownCard.MaxAttackValue", 0);

        //     cardsStatsDic.Add("enemyCard.Health", 0);
        //     cardsStatsDic.Add("enemyCard.MaxHealth", 0);
        //     cardsStatsDic.Add("enemyCard.AttackValue", 0);
        //     cardsStatsDic.Add("enemyCard.MaxAttackValue", 0);

        //     cardsStatsDic.Add("NOCInPlayerHand", 0);
        //     cardsStatsDic.Add("NOCInEnemyPlayerHand", 0);
        //     cardsStatsDic.Add("NOCInPlayerField", 0);
        //     cardsStatsDic.Add("NOCInEnemyPlayerField", 0);

        // }


        // public static void UpdateGameStatsDic(Card ownCard, Card enemyCard, Game gameState)
        // {
        //     cardsStatsDic["ownCard.Health"] = ownCard.Health;
        //     cardsStatsDic["ownCard.MaxHealthValue"] = ownCard.MaxHealthValue;
        //     cardsStatsDic["ownCard.AttackValue"] = ownCard.AttackValue;
        //     cardsStatsDic["ownCard.MaxAttackValue"] = ownCard.MaxAttackValue;

        //     cardsStatsDic["ownCard.Health"] = enemyCard.Health;
        //     cardsStatsDic["ownCard.MaxHealthValue"] = enemyCard.MaxHealthValue;
        //     cardsStatsDic["ownCard.AttackValue"] = enemyCard.AttackValue;
        //     cardsStatsDic["ownCard.MaxAttackValue"] = enemyCard.MaxAttackValue;

        //     cardsStatsDic["NOCInPlayerHand"] = ownCard.owner.Hand.Count;
        //     cardsStatsDic["NOCInEnemyPlayerHand"] = enemyCard.owner.Hand.Count;
        //     cardsStatsDic["NOCInPlayerField"] = GetNOCInPlayerField(gameState, ownCard.owner);
        //     cardsStatsDic["NOCInEnemyPlayerField"] = GetNOCInPlayerField(gameState, enemyCard.owner);

        //     cardsStatsDic["PlayerEnergy"] = ownCard.owner.Energy;
        //     cardsStatsDic["EnemyPlayerEnergy"] = enemyCard.owner.Energy;

        //     cardsStatsDic["NOCInPlayerDeck"] = ownCard.owner.Deck.Count;
        //     cardsStatsDic["NOCInEnemyPlayerDeck"] = enemyCard.owner.Deck.Count;

        // }


        // static int GetNOCInPlayerField(Game gameState, Player player)
        // {
        //     return gameState.Board[player].Count;
        // }
        // //esto es para modificar las estadisticas de las cartas 
        // public static void UpdateCardStats(Card ownCard, Card enemyCard)
        // {
        //     ownCard.Health = cardsStatsDic["ownCard.Health"];
        //     ownCard.MaxHealthValue = cardsStatsDic["ownCard.MaxHealthValue"];
        //     ownCard.AttackValue = cardsStatsDic["ownCard.AttackValue"];
        //     ownCard.MaxAttackValue = cardsStatsDic["ownCard.MaxAttackValue"];

        //     enemyCard.Health = cardsStatsDic["ownCard.Health"];
        //     enemyCard.MaxHealthValue = cardsStatsDic["ownCard.MaxHealthValue"];
        //     enemyCard.AttackValue = cardsStatsDic["ownCard.AttackValue"];
        //     enemyCard.MaxAttackValue = cardsStatsDic["ownCard.MaxAttackValue"];
        // }


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
                    case "InitialSpecie:":
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
                else if (splitedInput[position].Contains("()"))
                    tokens.Add(new Token(TokenType.ACTION, splitedInput[position]));
            }
        }
        void ParseCardProperties(int InitialPosition = 0)
        {
            for (int position = InitialPosition; position < tokens.Count; position++)
            {
                if (tokens[position].type == TokenType.NAME)
                    newInitialCardName = tokens[position].value;

                else if (tokens[position].type == TokenType.HEALTH)
                    newInitialCardHealth = int.Parse(tokens[position].value);

                else if (tokens[position].type == TokenType.ATK)
                    newInitialCardATK = int.Parse(tokens[position].value);

                else if (tokens[position].type == TokenType.SPECIE)
                    newInitialCardSpecie = tokens[position].specie;
            }
        }
        void ParseEffect(List<Iinstruction> instructionsList, int InitialPosition = 0)
        {
            for (int position = InitialPosition; position < tokens.Count; position++)
            {
                if (tokens[position].type == TokenType.IDENTIFIER)
                    instructionsList.Add(new Assignment(tokens[position], ParseExpr(position + 1)));

                else if (tokens[position].type == TokenType.IF)
                {
                    List<Iinstruction> newIfInstructionList = new List<Iinstruction>();
                    ParseEffect(newIfInstructionList, position);
                    instructionsList.Add(new IF(ParseExpr(position), newIfInstructionList));
                }
                else if (tokens[position].type == TokenType.ACTION)
                    instructionsList.Add(new Action(tokens[position]));
            }
        }

        IExpr ParseExpr(int startOfExpr)
        {
            Stack<IExpr> stack = new Stack<IExpr>();
            for (int position = startOfExpr; ;)
            {
                if (tokens[position].type == TokenType.END)
                {
                    tokens.RemoveAt(position); //lo remuevo ya q ParseGod se detiene cuando encuentra un token tipo END,y para evitar q itere por esto de nuevo es necesario eliminar cada token Parseado
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