using System.Collections.Generic;
using Microsoft.VisualBasic.CompilerServices;
using System;
namespace Conqueror.Logic.Language;

class Interpreter : NodeVisitor{
    public Dictionary<string, int> Scope;
    private Parser parser;
    
    public Interpreter(Parser parser) {
        this.parser = parser;
    }

    public Object Visit(Object node) { 
        if (node is BinOp) { 
            return VisitBinOp(node);
        }
        if (node is Num) { 
            return VisitNum(node);
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
        return null;
    }

    private Object VisitBinOp(Object node) {
        BinOp op = (BinOp)node;

        int l = 0, r = 0;
        Object vl = Visit(op.Left);
        Object vr = Visit(op.Right);
        //Console.WriteLine(vl.ToString() + " " + vr);

        string sl = vl.ToString();
        string sr = vr.ToString();

        switch (op.Op.Type) {
            case "PLUS":
                return Int32.Parse(sl) + Int32.Parse(sr); 

            case "MINUS": 
                return Int32.Parse(sl) - Int32.Parse(sr);

            case "MUL":
                return Int32.Parse(sl) * Int32.Parse(sr);

            case "DIV": 
                if (sr == "0") {
                    Utils.Error("Division por cero");
                } 
                return Int32.Parse(sl) / Int32.Parse(sr);
        }
        return null;
    }

    private Object VisitNum(Object node) {
        Num n = (Num)node;
        return n.Value;
    }


    public Object VisitUnaryOP(Object node) {
        UnaryOp uop = (UnaryOp)node;
        string value = Visit(uop.Expr).ToString();
        return Int32.Parse(value) * (uop.Token.Type == "PLUS" ? 1 : -1);
    }

    public void VisitCompount(Object node) {
        List<Object> children = ((Compound)node).Children;
        foreach (var item in children) {
            Visit(item);
        }
    }

    public Object VisitNoOp(Object node) {
        return null;
    }

    public void VisitAssign(Object node) {
        Assign assign = (Assign)node;
        string name = ((Var)assign.Left).Value;
        Object visit = Visit(assign.Right);
        string res = visit.ToString();
        int result = Int32.Parse(res);

        if (Scope.ContainsKey(name)) {
            Scope[name] = result;
        } else {
            Scope.Add(name, result);
        }
    }

    public Object VisitVar(Object node) {
        string name = ((Var)node).Value;
        if (!Scope.ContainsKey(name)) {
            Utils.Error("Uso de variable no creada previamente");
        }
        return Scope[name];
    }

    public void Interpret() {
        Scope = new Dictionary<string, int>();
        Object tree = parser.Parse();
        Visit(tree);
    }
}