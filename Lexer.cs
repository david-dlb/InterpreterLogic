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
        while (currentChar != '~' && currentChar == ' ') {
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
        return new Token(result,result);
    }
    
    public Token GetNextToken() {
        //Console.WriteLine(currentChar);
        while (currentChar != '~') {
            if (currentChar == ' ') {
                SkipWhiteSpace();
                continue;
            }
            // 47 porque es cero tambien esta incluido
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
                return new Token("MULT", "*");
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
}