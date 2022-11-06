namespace Conqueror.Logic.Language;

// Var: solo contendra letras del abecedario
// OP: +-*/% &|
// BOOL: T y F
// STRUCT: seran sentencias if, while
// LPAREN, RPAREN - ()
// EOF: ~
class Token {
    // tipos de token: VAR, OP, INT, BOOL, LPAREN, RPAREN, STRUCT, EOF
    public string Type {
        private set; get;
    }
    public string Value {
        private set; get;
    }

    public Token(string type, string value) { 
        Type = type;
        Value = value;
    }
    
    public Token(Token token) {
        Type = token.Type;
        Value = token.Value;
    }
}