using GameLibrary;
namespace MiniCompiler
{
    public enum TokenType
    {
        NAME, HEALTH, ATK, SPECIE, EOF
    }
    public class Token
    {
        public TokenType type;
        public string represent = "";
        public int value;
        public Species specie;
        public Token(TokenType type, string repr = "", int value = 0)
        {
            this.type = type;
            this.represent = repr;
            this.value = value;
        }
        public Token(TokenType type, Species specie)
        {
            this.type = type;
            this.specie = specie;
        }
    }
}

