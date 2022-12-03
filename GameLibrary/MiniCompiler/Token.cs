using GameLibrary;
namespace MiniCompiler
{
    public enum TokenType
    {
        NAME, HEALTH, ATK, SPECIE, IF, HIGHER, MINOR, SAME, AND, OR, ACTION, IDENTIFIER, NUMBER, PLUS, MINUS, MULT, DIV, ASSIGN, END
    }
    public class Token
    {
        public TokenType type;
        public string value = "";
        public Species specie;
        public Token(TokenType type, string repr = "")
        {
            this.type = type;
            this.value = repr;

        }
        public Token(TokenType type, Species specie)
        {
            this.type = type;
            this.specie = specie;
        }
    }
}

