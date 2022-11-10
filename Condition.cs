using System;
namespace Conqueror.Logic.Language;

class Condition {
    public Object Compound {
        get; private set;
    }
    public Object Cond {
        get; private set;
    }

    public Condition(Object compound, Object cond) {
        this.Compound = compound;
        this.Cond = cond;
    }
}