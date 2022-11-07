namespace Conqueror.Logic.Language;

class Var : AST{
    public Token Token {
        get; private set;
    }
    public string Value {
        get; private set;
    }

    public Var(Token token) {
        this.Token = token;
        this.Value = token.Value;
    }
}