namespace Conqueror.Logic.Language;

class Num {
    public Token Token {
        private set; get;
    }
    public string Value {
        private set; get;
    }

    public Num(Token token) {
        this.Token = token;
        this.Value = token.Value;
    }
}