using System;
namespace Conqueror.Logic.Language;

class UnaryOp : AST {
    public Token Token {
        get; private set;
    }
    public Object Expr {
        get; private set;
    }

    public UnaryOp(Token op, Object expr) {
        this.Token = op;
        this.Expr = expr;
    }
    
}