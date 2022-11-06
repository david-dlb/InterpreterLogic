namespace Conqueror.Logic.Language;

// MINUS: -
// PLUS: +
// MULT: *
// DIV: /
// BEGIN: comienzo de una declaración compuesta
// END: final de la declaración compuesta
// DOT: .
// ID: identificador
// ASSING: :=
// SEMI: ;
// LPAREN, RPAREN: ()
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