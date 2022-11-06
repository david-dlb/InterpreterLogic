using Microsoft.VisualBasic.CompilerServices;
using System;
namespace Conqueror.Logic.Language;

class Interpreter : NodeVisitor{
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
                break;
            case "MINUS": 
                return Int32.Parse(sl) - Int32.Parse(sr);
                break;
            case "MULT":
                return Int32.Parse(sl) * Int32.Parse(sr);
                break;
            case "DIV": 
                if (sr == "0") {
                    Utils.Error("Division por cero");
                } 
                return Int32.Parse(sl) / Int32.Parse(sr);
                break;
        }
        return null;
    }

    private Object VisitNum(Object node) {
        Num n = (Num)node;
        return n.Value;
    }


    public Object VisitUnaryOP(Object node) {
        UnaryOp uop = (UnaryOp)node;
        if (uop.Token.Type == "PLUS") {
            Object value = Visit(uop.Expr);
            int s = Int32.Parse(value.ToString()) * 1;
            return s;
        } else {
            string value = Visit(uop.Expr).ToString();
            int s = Int32.Parse(value) * (-1); 
            return s;
        }
    }

    public Object Interpret() {
        Object tree = parser.Parse();
        
        return Visit(tree);
    }
}