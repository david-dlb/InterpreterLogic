using System;
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
    public int Value {
        private set; get;
    }

    public Num(string value) { 
        this.Value = Int32.Parse(value);
    }
    public Num(int value) { 
        this.Value = value;
    }
}
class Bool : AST{ 
    public bool Value {
        private set; get;
    }

    public Bool(bool value) { 
        this.Value = value;
    }
}

class Function : AST { 
    public string Name {
        get; private set;
    }
    public Function(string name) {
        this.Name = name;
    }
}
 