using System;
using Conqueror.Logic;
using Conqueror.Logic.Language;
string effect = "BEGIN  BEGIN a := (4*2) + (5/2); END;   BEGIN b := --5*a; END;   END.";

Lexer lexer = new Lexer(effect); 
Parser pr = new Parser(lexer);
Interpreter i = new Interpreter(pr);
i.Interpret();

Console.WriteLine("a = " + i.Scope["a"]);
Console.WriteLine("b = " + i.Scope["b"]);

//Lexer l = new Lexer("BEGIN a := 2; END.");