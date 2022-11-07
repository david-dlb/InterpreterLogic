using System;
using Conqueror.Logic;
namespace Conqueror.Logic.Language;

class Lexer {
    private string text;
    private int pos;
    private char currentChar;
    public Lexer(string text) {
        this.text = text;
        this.pos = 0;
        this.currentChar = text[pos];
    }

    private void Advance() {
        // avanza pos y establece el nuevo currentChar
        pos++;
        if (pos > text.Length - 1) { 
            currentChar = '~'; // indica fin
        } else {
            currentChar = text[pos];
        }
    }

    // para ver el siguiente caracter sin moverme a el
    private char Peek() {
        int peekPos = pos + 1;
        if (peekPos > text.Length - 1) {
            return '~';
        } else {
            return text[peekPos];
        }
    }

    private void SkipWhiteSpace() {
        while (currentChar != '~' && (currentChar == ' ' || currentChar == '\n')) {
            Advance();
        }
    }

    // returna un entero multidigito
    private int Integer() {
        string result = ""; 

        while (currentChar != '~' && IsNum(currentChar)) {
            result += currentChar;  
            Advance();
        }  
        return Int32.Parse(result);
    }

    private Token Id() {
        string result = "";
        while (currentChar != '~' && isAlfNum(currentChar)) {
            result += currentChar;
            Advance();
        }

        switch (result) {
            case "BEGIN":
                return new Token("BEGIN", "BEGIN");

            case "END":
                return new Token("END", "END");

            default:
                return new Token("ID", result);
        }
    }
    
    public Token GetNextToken() {
        //Console.WriteLine(currentChar);
        while (currentChar != '~') {
            if (currentChar == ' ' || currentChar == '\n') {
                SkipWhiteSpace();
                continue;
            }
            if (IsNum(currentChar)) { 
                return new Token("INT", Integer().ToString());
            }
            if (currentChar == '+') { 
                Advance();
                return new Token("PLUS", "+");
            }
            if (currentChar == '-') { 
                Advance();
                return new Token("MINUS", "-");
            }
            if (currentChar == '*') {
                Advance();
                return new Token("MUL", "*");
            }
            if (currentChar == '/') { 
                Advance();
                return new Token("DIV", "/");
            }
            if (currentChar == '(') { 
                Advance();
                return new Token("LPAREN", "(");
            }
            if (currentChar == ')') { 
                Advance();
                return new Token("RPAREN", ")");
            }
            if (isAlpha(currentChar)) {
                return Id();
            }
            if (currentChar == ':' && Peek() == '=') {
                Advance();
                Advance();
                return new Token("ASSIGN", ":=");
            }
            if (currentChar == ';') {
                Advance();
                return new Token("SEMI", ";");
            }
            if (currentChar == '.') {
                Advance();
                return new Token("DOT", ".");
            }
            
            //Console.WriteLine(currentChar);
            Utils.Error("Caracter invalido");
        }     
        return new Token("EOF", "~");
    }

    private bool IsNum(char ch) {
        return (ch > 47 && ch < 58);
    }
    private bool isAlfNum(char ch) {
        return IsNum(ch) || (ch > 64 && ch < 123);
    }
    private bool isAlpha(char ch) {
        return ch > 64 && ch < 123;
    }
}