using GameLibrary;
using GameLibrary.Objects;
namespace MiniCompiler
{
    public interface IInterpreter
    {
        public void EatUserCode(string input);
        public Card BuildCard(Player owner);
        public IEffect BuildEffect();
    }
    public class Interpreter : IInterpreter
    {
        string userInput = "";
        List<string> splitedUserInput = new List<string>();
        List<Token> tokenList = new List<Token>();
        ClientEffectInstructions userCardEffect = new ClientEffectInstructions();

        public string newInitialCardName = ""; public int newInitialCardATK; public int newInitialCardHealth; public Species newInitialCardSpecie;

        public void EatUserCode(string input)
        {
            Tokenizing();
            ParseCardProperties();
            ParseCardEffect(userCardEffect.instructions);
        }

        public Card BuildCard(Player owner) => new Card(newInitialCardName, newInitialCardATK, newInitialCardHealth, newInitialCardSpecie, owner);
        public IEffect BuildEffect() => new ClientEffect(userCardEffect);


        private void Tokenizing()
        {
            splitedUserInput = userInput.Split(' ', StringSplitOptions.TrimEntries).ToList();
            for (int position = 0; position < splitedUserInput.Count; position++)
            {
                switch (splitedUserInput[position])
                {
                    case "Name:":
                        tokenList.Add(new Token(TokenType.NAME, UtilsForInterpreter.AddStringFromPositionToEOF(splitedUserInput, position + 1)));
                        break;
                    case "InitialHealth:":
                        tokenList.Add(new Token(TokenType.HEALTH, splitedUserInput[position + 1]));
                        break;
                    case "InitialATK:":
                        tokenList.Add(new Token(TokenType.ATK, splitedUserInput[position + 1]));
                        break;
                    case "InitialSpecie:":
                        tokenList.Add(new Token(TokenType.SPECIE, UtilsForInterpreter.GetSpecieFromString(splitedUserInput[position + 1])));
                        break;
                    case "IF:":
                        tokenList.Add(new Token(TokenType.IF));
                        break;
                    case "EndIF":
                        tokenList.Add(new Token(TokenType.END));
                        break;
                    case "+":
                        tokenList.Add(new Token(TokenType.PLUS, "+"));
                        break;
                    case "-":
                        tokenList.Add(new Token(TokenType.MULT, "-"));
                        break;
                    case "*":
                        tokenList.Add(new Token(TokenType.MINUS, "*"));
                        break;
                    case "/":
                        tokenList.Add(new Token(TokenType.DIV, "/"));
                        break;
                    case ";":
                        tokenList.Add(new Token(TokenType.END));
                        break;
                    default:
                        break;
                }
                if (UtilsForInterpreter.IsNumber(splitedUserInput[position]))
                    tokenList.Add(new Token(TokenType.NUMBER, splitedUserInput[position]));
                else if (splitedUserInput[position].Contains("Card"))
                    tokenList.Add(new Token(TokenType.IDENTIFIER, splitedUserInput[position]));
                else if (splitedUserInput[position].Contains("()"))
                    tokenList.Add(new Token(TokenType.ACTION, splitedUserInput[position]));
            }
        }
        private void ParseCardProperties(int InitialPosition = 0)
        {
            for (int position = InitialPosition; position < tokenList.Count; position++)
            {
                if (tokenList[position].type == TokenType.NAME)
                    newInitialCardName = tokenList[position].value;

                else if (tokenList[position].type == TokenType.HEALTH)
                    newInitialCardHealth = int.Parse(tokenList[position].value);

                else if (tokenList[position].type == TokenType.ATK)
                    newInitialCardATK = int.Parse(tokenList[position].value);

                else if (tokenList[position].type == TokenType.SPECIE)
                    newInitialCardSpecie = tokenList[position].specie;
            }
        }
        private void ParseCardEffect(List<Iinstruction> instructionsList, int InitialPosition = 0)
        {
            for (int position = InitialPosition; position < tokenList.Count; position++)
            {
                if (tokenList[position].type == TokenType.IDENTIFIER)
                    instructionsList.Add(new Assignment(tokenList[position], ParseExpr(position + 1)));

                else if (tokenList[position].type == TokenType.IF)
                {
                    List<Iinstruction> newIfInstructionList = new List<Iinstruction>();
                    ParseCardEffect(newIfInstructionList, position);
                    instructionsList.Add(new IF(ParseExpr(position), newIfInstructionList));
                }
                else if (tokenList[position].type == TokenType.ACTION)
                    instructionsList.Add(new Action(tokenList[position]));
            }
        }

        private IExpr ParseExpr(int startOfExpr)
        {
            Stack<IExpr> stack = new Stack<IExpr>();
            for (int position = startOfExpr; ;)
            {
                if (tokenList[position].type == TokenType.END)
                {
                    tokenList.RemoveAt(position); //lo remuevo ya q ParseGod se detiene cuando encuentra un token tipo END,y para evitar q itere por esto de nuevo es necesario eliminar cada token Parseado
                    break;
                }
                if (tokenList[position].type == TokenType.NUMBER)
                {
                    stack.Push(new Number(int.Parse(tokenList[position].value)));
                    tokenList.RemoveAt(position);
                }
                else if (tokenList[position].type == TokenType.IDENTIFIER)
                {
                    stack.Push(new Identifier(tokenList[position].value));
                    tokenList.RemoveAt(position);
                }
                else if (stack.Count >= 2)
                {
                    var right = stack.Pop(); //el orden importa
                    var left = stack.Pop();

                    if (tokenList[position].type == TokenType.PLUS)
                    {
                        stack.Push(new Plus(left, right));
                        tokenList.RemoveAt(position);
                    }
                    else if (tokenList[position].type == TokenType.MINUS)
                    {
                        stack.Push(new Minus(left, right));
                        tokenList.RemoveAt(position);
                    }
                    else if (tokenList[position].type == TokenType.MULT)
                    {
                        stack.Push(new Mult(left, right));
                        tokenList.RemoveAt(position);
                    }
                    else if (tokenList[position].type == TokenType.DIV)
                    {
                        stack.Push(new Div(left, right));
                        tokenList.RemoveAt(position);
                    }
                    else if (tokenList[position].type == TokenType.HIGHER)
                    {
                        stack.Push(new Higher(left, right));
                        tokenList.RemoveAt(position);
                    }
                    else if (tokenList[position].type == TokenType.MINOR)
                    {
                        stack.Push(new Minor(left, right));
                        tokenList.RemoveAt(position);
                    }
                    else if (tokenList[position].type == TokenType.SAME)
                    {
                        stack.Push(new Same(left, right));
                        tokenList.RemoveAt(position);
                    }
                    else if (tokenList[position].type == TokenType.AND)
                    {
                        stack.Push(new AND(left, right));
                        tokenList.RemoveAt(position);
                    }
                    else if (tokenList[position].type == TokenType.OR)
                    {
                        stack.Push(new OR(left, right));
                        tokenList.RemoveAt(position);
                    }
                }
            }
            return stack.Pop();
        }

    }
}