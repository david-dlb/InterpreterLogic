using System;
namespace Conqueror.Logic.Language;

class Condition : AST{
    public AST Cond {
        get; private set;
    }
    public Compound Compound {
        get; private set;
    }

    public Condition(AST cond, Compound compound) {
        this.Cond = cond;
        this.Compound = compound;
    }
}