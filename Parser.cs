using System.Collections.Generic;
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
        // chequea si es el token esperado
        //Console.WriteLine(currentToken.Type + " " + tokenType);
        if (currentToken.Type == tokenType) {
            currentToken = lexer.GetNextToken();
        } else {
            //Console.WriteLine(tokenType + " " + currentToken.Type);
            Utils.Error("sintaxis incorrecta");
        }
    }

    public Object Factor() {
        /*
            factor : PLUS  factor
                   | MINUS factor
                   | INTEGER
                   | LPAREN expr RPAREN
                   | variable
        */
        //Console.WriteLine(currentToken.Value + " " + currentToken.Type);
        Token token = new Token(currentToken);
        
        switch (token.Type) {
            case "PLUS":
                Eat("PLUS");
                return new UnaryOp(token, Factor());

            case "MINUS":
                Eat("MINUS");
                return new UnaryOp(token, Factor());

            case "INT":
                    Eat("INT");
                    return new Num(token);
                
            case "LPAREN":
                Eat("LPAREN");
                Object node1 = Expr();
                Eat("RPAREN"); 
                return node1;
            
            case "ID":
                Object node2 = Variable();
                return node2;
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
            } else {
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
        Object node = Program();
        if (currentToken.Type != "EOF") {
            Utils.Error("sintaxis incorrecta");
        }
        return node;
    }

    public Object Program() {
        // program: compoundStatament DOT
        Object node = CompoundStatement();
        Eat("DOT");
        return node;
    }

    public Object CompoundStatement() {
        // compoundStatament: BEGIN statamentList END
        Eat("BEGIN");
        List<Object> nodes = StatementList();
        Eat("END");

        Compound root = new Compound();
        foreach (var item in nodes) {
            root.Children.Add(item);
        }
        return root;
    }

    public List<Object> StatementList() {
        /*
            statementList: statement
                         | statement SEMI statementList
        */
        Object node = Statement();
        List<Object> results = new List<object>();
        results.Add(node);

        while (currentToken.Type == "SEMI") {
            Eat("SEMI");
            results.Add(Statement());
        }
        return results;
    }

    public Object Statement() {
        /*
            statement: componentStatement
                     | assignmentSatement
                     | empty
        */
        switch (currentToken.Type) {
            case "BEGIN":
                return CompoundStatement();

            case "ID":
                return AssignmentSatement();
            
            default:
                return Empty();
        }
    }

    public Object AssignmentSatement() {
        Object left = Variable();
        Token token = new Token(currentToken);
        Eat("ASSIGN");
        Object right = Expr();
        Object node = new Assign(left, token, right);
        return node;
    }

    public Object Variable() {
        //variable: ID
        Object node = new Var(currentToken);
        //Console.WriteLine(((Var)node).Token.Type);
        Eat("ID");
        return node;
    }

    public Object Empty() {
        // una linea vacia
        return new NoOp();
    }
}