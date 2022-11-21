using System;
namespace Conqueror.Logic.Language;

class Assign : AST{
    public Var Left {
        get; private set;
    }
    public Token Token {
        get; private set;
    }
    public AST Right {
        get; private set;
    }

    public Assign(Var left, Token token, AST right) {
        this.Left = left;
        this.Token = token;
        this.Right = right;
    }
}