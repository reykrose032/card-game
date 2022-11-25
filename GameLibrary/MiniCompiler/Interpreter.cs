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
        List<Token> propertiesTokens = new List<Token>(); //poseera los tokens Mas generales(ningun subtoken)
        List<Token> actionTokens = new List<Token>(); //lista con los tokens creados "a partir" del Token Action
        Dictionary<string, int> quantumVariables = new Dictionary<string, int>(); //relaciona cada nombre de variable con su valor


        public Interpreter(string input)
        {
            this.input = input;
            Tokenizing(); //creando tokens a partir de la entrada del usuario
            ParseCardPropierties(); //asignandole valores a las propiedades de la carta nueva. Example: newInitialCardName,etc
        }


        public void ActivateClientEffect(Card ownCard, Card enemyCard)
        {
            if (quantumVariables == null)
                FillQuantumDictionary(ownCard, enemyCard);
            else
                UpdateQuantumVariablesStats(ownCard, enemyCard);
            TokenizingActionTokens();
            ParseTokenAction();
            UpdateCardStats(ownCard, enemyCard);
        }


        //tokeniza los "subtokens" q se encuentran en el string del TokenAction
        void TokenizingActionTokens()
        {
            //splitea el token ACTION q es el ultimo de la lista propertiesToken del cual tokenizaaremos aun mas...
            //los tokens siguientes podrian verlo como subtokens del token Action
            splitedInput = propertiesTokens[propertiesTokens.Count - 1].represent.Split(' ', StringSplitOptions.TrimEntries).ToList();

            for (int position = 0; position < splitedInput.Count; position++)
            {
                if (IsNumber(splitedInput[position]))
                    actionTokens.Add(new Token(TokenType.NUMBER, "", int.Parse(splitedInput[position])));
                else if (splitedInput[position] == "+")
                    actionTokens.Add(new Token(TokenType.PLUS, "+"));
                else if (splitedInput[position] == "-")
                    actionTokens.Add(new Token(TokenType.MINUS, "-"));
                else if (splitedInput[position] == "*")
                    actionTokens.Add(new Token(TokenType.MULT, "*"));
                else if (splitedInput[position] == "/")
                    actionTokens.Add(new Token(TokenType.DIV, "/"));
                else if (splitedInput[position] == "=")
                    continue;
                else if (splitedInput[position] == ";")
                    continue;
                else
                    actionTokens.Add(new Token(TokenType.IDENTIFIER, splitedInput[position], quantumVariables[splitedInput[position]]));
            }
        }


        void ParseTokenAction()//examp:card.ATK=card.ATK*2;//se q esta feo,repetitivo,pero estoy priorizando funcionalidad,luego veremos como arreglar esto
        {
            //modificando la estadistica objetiva 
            quantumVariables[actionTokens[0].represent] = CalculateExpresion(actionTokens);
        }


        //esto es para modificar las estadisticas de las cartas 
        public void UpdateCardStats(Card ownCard, Card enemyCard)
        {
            ownCard.Health = quantumVariables["ownCard.Health"];
            ownCard.MaxHealthValue = quantumVariables["ownCard.MaxHealthValue"];
            ownCard.AttackValue = quantumVariables["ownCard.AttackValue"];
            ownCard.MaxAttackValue = quantumVariables["ownCard.MaxAttackValue"];

            enemyCard.Health = quantumVariables["ownCard.Health"];
            enemyCard.MaxHealthValue = quantumVariables["ownCard.MaxHealthValue"];
            enemyCard.AttackValue = quantumVariables["ownCard.AttackValue"];
            enemyCard.MaxAttackValue = quantumVariables["ownCard.MaxAttackValue"];
        }


        //esto es para modificar las estadisticas del diccionario quantumVariables
        public void UpdateQuantumVariablesStats(Card ownCard, Card enemyCard)
        {
            quantumVariables["ownCard.Health"] = ownCard.Health;
            quantumVariables["ownCard.MaxHealthValue"] = ownCard.MaxHealthValue;
            quantumVariables["ownCard.AttackValue"] = ownCard.AttackValue;
            quantumVariables["ownCard.MaxAttackValue"] = ownCard.MaxAttackValue;

            quantumVariables["ownCard.Health"] = enemyCard.Health;
            quantumVariables["ownCard.MaxHealthValue"] = enemyCard.MaxHealthValue;
            quantumVariables["ownCard.AttackValue"] = enemyCard.AttackValue;
            quantumVariables["ownCard.MaxAttackValue"] = enemyCard.MaxAttackValue;
        }


        //tokenizando para guardar tokens en lista propertiesTokens
        void Tokenizing()
        {
            splitedInput = input.Split(' ', StringSplitOptions.TrimEntries).ToList();
            for (int position = 0; position < splitedInput.Count; position++)
            {
                switch (splitedInput[position])
                {
                    case "Name:":
                        propertiesTokens.Add(new Token(TokenType.NAME, AddStringFromPositionToEOF(position + 1)));
                        break;
                    case "Health:":
                        propertiesTokens.Add(new Token(TokenType.HEALTH, "", int.Parse(splitedInput[position + 1])));
                        splitedInput.RemoveAt(position + 1);
                        break;
                    case "ATK:":
                        propertiesTokens.Add(new Token(TokenType.ATK, "", int.Parse(splitedInput[position + 1])));
                        splitedInput.RemoveAt(position + 1);
                        break;
                    case "Specie:":
                        propertiesTokens.Add(new Token(TokenType.SPECIE, GetSpecieFromString(splitedInput[position + 1])));
                        break;
                    case "EffectAction:":
                        propertiesTokens.Add(new Token(TokenType.ACTION, AddStringFromPositionToEOF(position + 1)));
                        break;
                    case ";":
                        propertiesTokens.Add(new Token(TokenType.EOF));
                        break;
                    default:
                        break;
                }
            }
        }


        //asigna a las propiedades de la nueva carta ,por ejemplo newInitialCard.Health,el valor q introducido por el usuario
        void ParseCardPropierties()
        {
            for (int i = 0; i < propertiesTokens.Count; i++)
            {
                switch (propertiesTokens[i].type)
                {
                    case TokenType.NAME:
                        newInitialCardName = propertiesTokens[i].represent;
                        break;
                    case TokenType.HEALTH:
                        newInitialCardHealth = propertiesTokens[i].value;
                        break;
                    case TokenType.ATK:
                        newInitialCardATK = propertiesTokens[i].value;
                        break;
                    case TokenType.SPECIE:
                        newInitialCardSpecie = propertiesTokens[i].specie;
                        break;
                    case TokenType.EOF:
                        continue;
                    default:
                        break;
                }
            }
        }


        public void FillQuantumDictionary(Card ownCard, Card enemyCard)
        {
            quantumVariables.Add("ownCard.Health", ownCard.Health);
            quantumVariables.Add("ownCard.MaxHealth", ownCard.MaxHealthValue);
            quantumVariables.Add("ownCard.AttackValue", ownCard.AttackValue);
            quantumVariables.Add("ownCard.MaxAttackValue", ownCard.MaxAttackValue);

            quantumVariables.Add("enemyCard.Health", enemyCard.Health);
            quantumVariables.Add("ownCard.MaxHealth", enemyCard.MaxHealthValue);
            quantumVariables.Add("enemyCard.AttackValue", enemyCard.AttackValue);
            quantumVariables.Add("enemyCard.MaxAttackValue", enemyCard.MaxAttackValue);
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
                splitedInput.RemoveAt(position);
            }
            return result;
        }


        //calcula la expresion a asignarle a la variable a modificar(modificar metodo para hacerlo modular y reusable,Deberia calcular 
        //simplemente la expresion calculable dada)
        //REQUIERE OPTIMIZACION IMPORTANTE
        int CalculateExpresion(List<Token> actionTokens)
        {
            for (int position = 1; position < actionTokens.Count; position++)
            {
                if (actionTokens[position].type == TokenType.MULT)
                {
                    actionTokens[position].value = actionTokens[position - 1].value * actionTokens[position + 1].value;
                    actionTokens.RemoveAt(position - 1);
                    actionTokens.RemoveAt(position + 1);
                }
                else if (actionTokens[position].type == TokenType.DIV)
                {
                    actionTokens[position].value = actionTokens[position - 1].value / actionTokens[position + 1].value;
                    actionTokens.RemoveAt(position - 1);
                    actionTokens.RemoveAt(position + 1);
                }
            }
            for (int position = 1; position < actionTokens.Count; position++)
            {
                if (actionTokens[position].type == TokenType.PLUS)
                {
                    actionTokens[position].value = actionTokens[position - 1].value + actionTokens[position + 1].value;
                    actionTokens.RemoveAt(position - 1);
                    actionTokens.RemoveAt(position + 1);
                }
                else if (actionTokens[position].type == TokenType.MINUS)
                {
                    actionTokens[position].value = actionTokens[position - 1].value - actionTokens[position + 1].value;
                    actionTokens.RemoveAt(position - 1);
                    actionTokens.RemoveAt(position + 1);
                }
            }
            return actionTokens[1].value;
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