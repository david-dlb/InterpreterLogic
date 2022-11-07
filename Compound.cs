using System.Collections.Generic;
using System;
namespace Conqueror.Logic.Language;

class Compound : AST{
    public List<Object> Children {
        get; private set;
    }

    public Compound() {
        Children = new List<object>();
    }
}