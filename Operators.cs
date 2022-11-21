using System;
namespace Conqueror.Logic.Language;

class UnaryOp : AST {
    public Token Token {
        get; private set;
    }
    public AST Expr {
        get; private set;
    }

    public UnaryOp(Token op, AST expr) {
        this.Token = op;
        this.Expr = expr;
    }
    
}
class NoOp : AST{

}


// clase para guardar operadores binarios en los nodos
class BinOp : AST{
    public AST Left {
        get; private set;
    } 
    public Token Op {
        get; private set;
    }
    public AST Right {
        get; private set;
    }

    public BinOp(AST left, Token op, AST right) {
        this.Left = left;
        this.Op = op;
        this.Right = right;
    }
}