namespace Interpreter;

public enum TokenType
{
    Integer,
    Plus,
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

    public Interpreter(string text)
    {
        Text = text;
        _position = 0;
    }

    public Token GetNextToken()
    {
        if (_position > Text.Length - 1)
            return new Token(TokenType.EOF);
        
        var currentChar = Text.ElementAt(_position);
        if (char.IsDigit(currentChar))
        {
            _position++;
            return new Token(TokenType.Integer);
        }

        if (currentChar == '+')
        {
            _position++;
            return new Token(TokenType.Plus);
        }

        throw new Exception("Error parsing input");
    }

}
