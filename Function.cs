namespace Conqueror.Logic.Language;

class Function : AST { 
    private string name;
    public Function(string name) {
        this.name = name;
    }
}



public enum FunctionNames {
    #region Funtions

    StillCardEnemy,
    ChangeHands, 

    #endregion
 
}