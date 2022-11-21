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

    public AST Parse() {
        AST node = Program();
        if (currentToken.Type != "EOF") {
            //Console.WriteLine(currentToken.Value);
            Utils.Error("sintaxis incorrecta");
        }
        return node;
    }

    public void Eat(string tokenType) {
        // chequea si es el token esperado
        //Console.WriteLine(currentToken.Type + " " + tokenType);
        if (currentToken.Type == tokenType) {
            currentToken = lexer.GetNextToken();
        } else {
            Console.WriteLine("tokenType: " + tokenType + "  current " + currentToken.Type);
            Utils.Error("Sintaxis incorrecta");
        }
    }

    public AST Factor() {
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
                return new Num(token.Value);
                
            case "LPAREN":
                Eat("LPAREN");
                AST node1 = Expr();
                Eat("RPAREN"); 
                return node1;
            
            case "ID":
                AST node2 = Variable();
                return node2;
        }
        return null;
    }

    public AST Term() {
        AST node = Factor();
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

    public AST Expr() {
        /*
         expr   : term ((PLUS | MINUS) term)*
        term   : factor ((MUL | DIV) factor)*
        factor : INTEGER | LPAREN expr RPAREN 
        */
        //Console.WriteLine(currentToken.Value);
        AST node = Term();
        
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

    public AST Program() {
        // program: compoundStatament DOT
        List<AST> nodes = StatementList();
        Compound root = new Compound();
        //Console.WriteLine(nodes[0]);
        foreach (var item in nodes) {
            root.Children.Add(item);
        }
        return root;
    }

    public Compound CompoundStatement() {
        // compoundStatament: BEGIN statamentList END
        Eat("BEGIN");
        List<AST> nodes = StatementList();
        Eat("END");

        Compound root = new Compound();
        foreach (var item in nodes) {
            root.Children.Add(item);
        }
        return root;
    }

    public List<AST> StatementList() {
        /*
            statementList: statement
                         | statement SEMI statementList
        */
        AST node = Statement();
        List<AST> results = new List<AST>();
        results.Add(node);  
        // si pongo por ejempo { a:= 3 } falta el ;
        if (currentToken.Type != "SEMI" && !(node is NoOp)) {
            Utils.Error("Esperado ; al final de la instruccion");
        }
        while (currentToken.Type == "SEMI") {
            Eat("SEMI");
            bool mk = false;
            if (currentToken.Type != "END") {
                mk = true;
            }
            results.Add(Statement());
            // para validar que al final de la instruccion se puso ;
            if (mk == true && currentToken.Type == "END") {
                Utils.Error("Esperado ; al final de la instruccion");
            }
        } 
        return results;
    }

    public AST Statement() {
        /*
            statement: componentStatement
                     | assignmentSatement
                     | empty
        */
        switch (currentToken.Type) {
            case "BEGIN":
                return CompoundStatement();

            case "IF":
                return Conditional();

            case "WHILE":
                return Repetition();

            case "ID":
                return AssignmentSatement();
            
            case "FUNCTION":
                return Function();
            default:
                return Empty();
        }
    }

    public AST AssignmentSatement() {
        Var left = (Var)Variable();
        Token token = new Token(currentToken);
        Eat("ASSIGN");
        AST right = Expr();
        AST node = new Assign(left, token, right);
        return node;
    }

    public Var Variable() {
        //variable: ID
        Var node = new Var(currentToken);
        //Console.WriteLine(((Var)node).Token.Type); 
        Eat("ID");
        return node;
    }

    public AST Repetition() {
        Eat("WHILE"); 
        Eat("LPAREN");
        AST comparer = Logic();
        Eat("RPAREN");
        Compound componentStatement = CompoundStatement(); 
        return new While(comparer, componentStatement);
    }

    public AST Conditional() {
        Eat("IF");
        Eat("LPAREN");
        AST comparer = Logic();
        Eat("RPAREN");
        Compound componentStatement = CompoundStatement(); 
        return new Condition(comparer, componentStatement);
    }

    public AST Logic() {
        AST node = Comparer();
        while (currentToken.Type == "AND" || currentToken.Type == "OR") {
            Token token = new Token(currentToken);
            Eat(token.Type);
            node = new BinOp(node, token, Comparer());
        }
        return node;
    }

    public AST Comparer() {
        AST node = Expr();
        AST expr2 = null;
        Token token = new Token(currentToken);

        if (currentToken.Type == "LESS" || currentToken.Type == "MORE" || currentToken.Type == "EQUAL") {
            Eat(currentToken.Type); 
            expr2 = Expr();
        } else {
            Utils.Error("Falta un operador de condicion valido");
        }
        return new BinOp(node, token, expr2);
    }

    public Function Function() {
        string name = currentToken.Value;
        Eat("FUNCTION");
        Eat("LPAREN");
        Eat("RPAREN");  
        return new Function(name);
    }

    public AST Empty() {
        // una linea vacia
        return new NoOp();
    }
}