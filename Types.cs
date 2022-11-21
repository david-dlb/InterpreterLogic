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


class Num : AST{
    public Token Token {
        private set; get;
    }
    public int Value {
        private set; get;
    }

    public Num(Token token) {
        this.Token = token;
        this.Value = Int32.Parse(token.Value);
    }
}
class Bool : AST{
    public Token Token {
        private set; get;
    }
    public string Value {
        private set; get;
    }

    public Bool(Token token) {
        this.Token = token;
        this.Value = token.Value;
        
    }
}