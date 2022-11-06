namespace Conqueror.Logic.Language;

// clase para guardar operadores binarios en los nodos
class BinOp : AST{
    public Object Left {
        get; private set;
    } 
    public Token Op {
        get; private set;
    }
    public Object Right {
        get; private set;
    }

    public BinOp(Object left, Token op, Object right) {
        this.Left = left;
        this.Op = op;
        this.Right = right;
    }
}