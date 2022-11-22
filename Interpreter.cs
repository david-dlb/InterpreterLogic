using System.Collections.Generic;
using Microsoft.VisualBasic.CompilerServices;
using System;
namespace Conqueror.Logic.Language;

class Interpreter{
    public Dictionary<string, int> Scope;
    private Parser parser;
    
    public Interpreter(Parser parser, Dictionary<string, int> scope) {
        this.parser = parser;
        this.Scope = scope;
    }

    public AST Visit(AST node) { 
        if (node is BinOp) { 
            return VisitBinOp(node);
        }
        if (node is Num) {  
            return VisitNum(node);
        }
        if (node is Bool) {
            return VisitBool(node);
        }
        if (node is UnaryOp) {
            return VisitUnaryOP(node);
        }
        if (node is Compound) {
            VisitCompount(node);
        }
        if (node is NoOp) {
            return VisitNoOp(node);
        }
        if (node is Var) {
            //Console.WriteLine(((Var)node).Value);
            return VisitVar(node);
        }
        if (node is Assign) {
            VisitAssign(node);
        }
        if (node is Condition) {
            VisitCondition(node);
        }
        if (node is While) {
            VisitWhile(node);
        }
        if (node is Function) {
            VisitFunction(node);
        }
        return null;
    }

    private AST VisitBinOp(AST node) {
        BinOp op = (BinOp)node;

        int l = 0, r = 0;
        AST sl = Visit(op.Left);
        AST sr = Visit(op.Right);
        //Console.WriteLine(vl.ToString() + " " + vr);
        if (!((sl is Num) && (sr is Num)) && !((sl is Bool) && (sr is Bool))) {
            Console.WriteLine(sl + " " + sr);
            Utils.Error("Tipos de datos incorrectos en la expresion");
        }

        if (sr is Num) {
            int vl = ((Num)sl).Value;
            int vr = ((Num)sr).Value;
            switch (op.Op.Type) {
                case "PLUS":
                    return new Num(vl + vr); 

                case "MINUS": 
                    return new Num(vl - vr);

                case "MUL":
                    return new Num(vl * vr);

                case "DIV": 
                    if (vr == 0) {
                        Utils.Error("Division por cero");
                    } 
                    return new Num(vl / vr);
                case "LESS":
                    return new Bool (vl < vr);
            
                case "MORE":
                    return new Bool (vl > vr);
                
                case "EQUAL": 
                    return new Bool (vl == vr); 
            }
        }
        if (sr is Bool) {
            bool vl = ((Bool)sl).Value;
            bool vr = ((Bool)sr).Value;
            switch (op.Op.Type) {
                
                case "AND":
                    return new Bool(vl == true && vr == true);

                case "OR":
                    return new Bool(vl == true || vr == true);
            }
        }
        
    //Console.WriteLine(op.Op.Type);
        
        return null;
    }

    private Num VisitNum(AST node) {
        Num n = (Num)node;
        return n;
    }
    private Bool VisitBool(AST node) {
        Bool n = (Bool)node;
        return n;
    }

    public AST VisitUnaryOP(AST node) {
        UnaryOp uop = (UnaryOp)node;
        string value = Visit(uop.Expr).ToString();
        return new Num(Int32.Parse(value) * (uop.Token.Type == "PLUS" ? 1 : -1));
    }

    public void VisitCompount(AST node) {
        List<AST> children = ((Compound)node).Children;
        foreach (var item in children) {
            Visit(item);
        }
    }

    public AST VisitNoOp(AST node) {
        return null;
    }

    public void VisitAssign(AST node) {
        Assign assign = (Assign)node;
        string name = ((Var)assign.Left).Value;
        AST visit = Visit(assign.Right);
        Num result = (Num)visit; 

        // expresiones tipo EnemyLife= ;
        if (result == null) {
            Utils.Error("Sintaxis incorrecta, falta valor a la variable");
        }

        if (Scope.ContainsKey(name)) {
            Scope[name] = result.Value;
        } else {
            Scope.Add(name, result.Value);
        }
    }

    public AST VisitVar(AST node) {
        string name = ((Var)node).Value;
        if (!Scope.ContainsKey(name)) {
            Utils.Error("Uso de variable no creada previamente");
        }
        return new Num(Scope[name]);
    }

    public void VisitWhile(AST node) {
        While condition = (While)node;
        if (!(condition.Cond is BinOp)) {
            Utils.Error("Error se esperaba una operacion binaria de comparacion");
        }
        bool folow = true;
        int calls = 0;

        while (folow) {
            calls++;
            if (calls > 100000) {
                Utils.Error("Ciclo infinito");
            }
            AST mk = VisitBinOp(condition.Cond);
            Bool cond = (Bool)mk;
            if (cond.Value == true) {
                Visit(condition.Compound);
            } else {
                folow = false;
            }
        }
        
    }

    public void VisitCondition(AST node) {
        Condition condition = (Condition)node;
        if (!(condition.Cond is BinOp)) {
            Utils.Error("Error se esperaba una operacion binaria de comparacion");
        }
        AST mk = VisitBinOp(condition.Cond);
        Bool cond = (Bool)mk;
        if (cond.Value == true) {
            Visit(condition.Compound);
        }
    }
    public void VisitFunction(AST node) {
        Function func = (Function)node;
        string name = func.Name;
        if (Scope.ContainsKey(name)) {
            Scope[name] ++;
        } else {
            Scope.Add(name, 1);
        }
    }

    public void Interpret() {
        AST tree = parser.Parse();
        Visit(tree);
    }
}