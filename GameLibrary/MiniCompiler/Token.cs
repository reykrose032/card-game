using GameLibrary;
using GameLibrary.Objects;
namespace MiniCompiler
{
    public enum TokenType
    {
        NAME, HEALTH, ATK, SPECIE, PLAYER, IF, HIGHER, MINOR, SAME, AND, OR, ACTION, IDENTIFIER, NUMBER, PLUS, MINUS, MULT, DIV, ASSIGN, END
    }
    public class Token
    {
        public TokenType type;
        public string value = "";
        public Species specie;
        public Player player;
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
        public Token(TokenType type, Player player)
        {
            this.type = type;
            this.player = player;
        }
    }
}

