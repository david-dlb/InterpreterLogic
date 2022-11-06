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

    private void SkipWhiteSpace() {
        while (currentChar != '~' && currentChar == ' ') {
            Advance();
        }
    }

    // returna un entero multidigito
    private int Integer() {
        string result = ""; 

        while (currentChar != '~' && (currentChar > 47 && currentChar < 58)) {
            result += currentChar;  
            Advance();
        }  
        return Int32.Parse(result);
    }
    
    public Token GetNextToken() {
        //Console.WriteLine(currentChar);
        while (currentChar != '~') {
            if (currentChar == ' ') {
                SkipWhiteSpace();
                continue;
            }
            // 47 porque es cero tambien esta incluido
            if (currentChar > 47 && currentChar < 58) { 
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
}