using System;
using Microsoft.VisualBasic.CompilerServices;
namespace Conqueror.Logic.Language;

class Parser {
    private Lexer lexer;
    private Token currentToken;

    public Parser(Lexer lexer) {
        this.lexer = lexer;
        this.currentToken = lexer.GetNextToken();
    }

    public void Eat(string tokenType) {
        //Console.WriteLine(currentToken.Type + " " + tokenType);
        if (currentToken.Type == tokenType) {
            currentToken = lexer.GetNextToken();
        } else {
            Utils.Error("sintaxis incorrecta");
        }
    }

    public Object Factor() {
        //Console.WriteLine(currentToken.Value + " " + currentToken.Type);
        Token token = new Token(currentToken);
        
        if (token.Type == "PLUS") {
            Eat("PLUS");
            return new UnaryOp(token, Factor());
        } else {
            if (token.Type == "MINUS") {
                Eat("MINUS");
                return new UnaryOp(token, Factor());
            } else {
                if (token.Type == "INT") {
                    Eat("INT");
                    return new Num(token);
                } else {
                    if (token.Type == "LPAREN") {
                        Eat("LPAREN");
                        Object node = Expr();
                        Eat("RPAREN"); 

                        return node;
                    }
                }
            }
        }
        return null;
    }

    public Object Term() {
        Object node = Factor();

        if (node is Num) {
            Num n = (Num)node;
            //Console.WriteLine(n.Value);
        }
        while (currentToken.Type == "MUL" || currentToken.Type == "DIV") {
            Token token = new Token(currentToken);
            //Console.WriteLine("hhoa");
            if (token.Type == "MUL") {
                Eat("MUL");
            }
            else {
                Eat("DIV");
            }
            node = new BinOp(node, token, Factor());
        }
        if (node is Num){
        Num n = (Num)node;
        //Console.WriteLine(n.Value + " " + currentToken.Value);
        }
        return node;
    }

    public Object Expr() {
        /*
         expr   : term ((PLUS | MINUS) term)*
        term   : factor ((MUL | DIV) factor)*
        factor : INTEGER | LPAREN expr RPAREN 
        */
        Object node = Term();
        
        while (currentToken.Type == "MINUS" || currentToken.Type == "PLUS") {
            Token token = new Token(currentToken);
            if (token.Type == "PLUS") {
                Eat("PLUS");
            } else {
                Eat("MINUS");
            }
            node = new BinOp(node, token, Term());
        //Console.WriteLine(((BinOp)node));
        } 
        return node;
    }

    public Object Parse() {
        Object res = Expr(); 
        /* 
        BinOp b1 = (BinOp)res;
        Num r = (Num)(b1.Right);
        BinOp b2 = (BinOp)b1.Left;
        Num l1 = (Num)(b2.Left);
        Num l2 = (Num)(b2.Right); */

        //Console.WriteLine(b2.Op.Value);
        return res;
    }
}