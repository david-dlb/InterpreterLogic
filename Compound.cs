using System.Collections.Generic;
using System;
namespace Conqueror.Logic.Language;

class Compound : AST{
    public List<AST> Children {
        get; private set;
    }

    public Compound() {
        Children = new List<AST>();
    }
}