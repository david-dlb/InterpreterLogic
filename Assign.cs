using System;
namespace Conqueror.Logic.Language;

class Assign : AST{
    public Object Left {
        get; private set;
    }
    public Token Token {
        get; private set;
    }
    public Object Right {
        get; private set;
    }

    public Assign(Object left, Token token, Object right) {
        this.Left = left;
        this.Token = token;
        this.Right = right;
    }
}