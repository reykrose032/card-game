namespace GameInterpreter;

public enum TokenType
{
    Integer,
    Plus,
    Minus,
    EOF
}

public class Token
{
    public TokenType Type { get; }
    public int Value { get; }

    public Token(TokenType type)
    {
        Type = type;
    }

    public Token(TokenType type, int value)
    {
        Type = type;
        Value = value;
    }
}

public class Interpreter
{
    public string Text { get; }
    private int _position;
    private char _currentChar;
    private Token _currentToken;

    public Interpreter(string text)
    {
        Text = text;
        _position = 0;
        _currentChar = Text[_position];
    }

    public void Advance()
    {
        _position++;

        if (_position > Text.Length - 1)
            _currentChar = char.MinValue; // Represents End Of File
        else
            _currentChar = Text[_position];
            
    }

    public int GetMultiDigitInt()
    {
        var result = "";
        while (_currentChar != char.MinValue && char.IsDigit(_currentChar))
        {
            result += _currentChar;
            Advance();
        }

        return int.Parse(result);
    }

    public Token GetNextToken()
    {
        while (_currentChar != char.MinValue)
        {
            if (_currentChar == ' ')
            {
                Advance();
                continue;
            }

            if (char.IsDigit(_currentChar))
            {
                return new Token(TokenType.Integer, GetMultiDigitInt());
            }

            if (_currentChar == '+')
            {
                Advance();
                return new Token(TokenType.Plus);
            }

            if (_currentChar == '-')
            {
                Advance();
                return new Token(TokenType.Minus);
            }

            throw new Exception("ERROR Parsing input: Invalid character! at position " + _position);
        }

        return new Token(TokenType.EOF);
    }

    public void Eat(TokenType expectedTokenType)
    {
        if (_currentToken.Type == expectedTokenType)
            _currentToken = GetNextToken();
        else
            throw new Exception("Error: Invalid Expression!");
    }

    public int Expression()
    {
        _currentToken = GetNextToken();

        var left = _currentToken;
        Eat(TokenType.Integer);

        var opperation = _currentToken;
        if (opperation.Type == TokenType.Plus)
            Eat(TokenType.Plus);
        else
            Eat(TokenType.Minus);
        
        var right = _currentToken;
        Eat(TokenType.Integer);

        if (opperation.Type == TokenType.Plus)
            return left.Value + right.Value;
        else
            return left.Value - right.Value;
    }

}
