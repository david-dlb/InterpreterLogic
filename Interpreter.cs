// using System.Collections.Generic;
// using Microsoft.VisualBasic.CompilerServices;
// using System;
// namespace Conqueror.Logic.Language;

// class Interpreter{
//     public Dictionary<string, int> Scope;
//     private Parser parser;
    
//     public Interpreter(Parser parser, Dictionary<string, int> scope) {
//         this.parser = parser;
//         this.Scope = scope;
//     }

//     public AST Visit(AST node) { 
//         if (node is BinOp) { 
//             return VisitBinOp(node);
//         }
//         if (node is Num) { 
//             return VisitNum(node);
//         }
//         if (node is UnaryOp) {
//             return VisitUnaryOP(node);
//         }
//         if (node is Compound) {
//             VisitCompount(node);
//         }
//         if (node is NoOp) {
//             return VisitNoOp(node);
//         }
//         if (node is Var) {
//             //Console.WriteLine(((Var)node).Value);
//             return VisitVar(node);
//         }
//         if (node is Assign) {
//             VisitAssign(node);
//         }
//         if (node is Condition) {
//             VisitCondition(node);
//         }
//         return null;
//     }

//     private AST VisitBinOp(AST node) {
//         BinOp op = (BinOp)node;

//         int l = 0, r = 0;
//         AST vl = Visit(op.Left);
//         AST vr = Visit(op.Right);
//         //Console.WriteLine(vl.ToString() + " " + vr);

//         string sl = vl.ToString();
//         string sr = vr.ToString();
//     //Console.WriteLine(op.Op.Type);
//         switch (op.Op.Type) {
//             case "PLUS":
//                 return Int32.Parse(sl) + Int32.Parse(sr); 

//             case "MINUS": 
//                 return Int32.Parse(sl) - Int32.Parse(sr);

//             case "MUL":
//                 return Int32.Parse(sl) * Int32.Parse(sr);

//             case "DIV": 
//                 if (sr == "0") {
//                     Utils.Error("Division por cero");
//                 } 
//                 return Int32.Parse(sl) / Int32.Parse(sr);
            
//             case "LESS":
//                 return Int32.Parse(sl) < Int32.Parse(sr);
            
//             case "MORE":
//                 return Int32.Parse(sl) > Int32.Parse(sr);
            
//             case "EQUAL":
//                 return Int32.Parse(sl) == Int32.Parse(sr);
            
//             case "AND":
//                 return sl == "True" && sr == "True";

//             case "OR":
//                 return sl == "True" || sr == "True";
//         }
//         return null;
//     }

//     private AST VisitNum(AST node) {
//         Num n = (Num)node;
//         return n.Value;
//     }


//     public AST VisitUnaryOP(AST node) {
//         UnaryOp uop = (UnaryOp)node;
//         string value = Visit(uop.Expr).ToString();
//         return Int32.Parse(value) * (uop.Token.Type == "PLUS" ? 1 : -1);
//     }

//     public void VisitCompount(AST node) {
//         List<AST> children = ((Compound)node).Children;
//         foreach (var item in children) {
//             Visit(item);
//         }
//     }

//     public AST VisitNoOp(AST node) {
//         return null;
//     }

//     public void VisitAssign(AST node) {
//         Assign assign = (Assign)node;
//         string name = ((Var)assign.Left).Value;
//         AST visit = Visit(assign.Right);
//         string res = visit.ToString();
//         int result = Int32.Parse(res);

//         if (Scope.ContainsKey(name)) {
//             Scope[name] = result;
//         } else {
//             Scope.Add(name, result);
//         }
//     }

//     public AST VisitVar(AST node) {
//         string name = ((Var)node).Value;
//         if (!Scope.ContainsKey(name)) {
//             Utils.Error("Uso de variable no creada previamente");
//         }
//         return Scope[name];
//     }

//     public void VisitCondition(AST node) {
//         Condition condition = (Condition)node;
//         if (!(condition.Cond is BinOp)) {
//             Utils.Error("Error se esperaba una operacion binaria de comparacion");
//         }
//         AST mk = VisitBinOp(condition.Cond);
//         if (mk.ToString() == "True") {
//             Visit(condition.Compound);
//         }
//     }

//     public void Interpret() {
//         AST tree = parser.Parse();
//         Visit(tree);
//     }
// }