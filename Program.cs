using System;
using Conqueror.Logic;
using Conqueror.Logic.Language;
string effect = @"BEGIN   
                    a := (4*2) + (5/2); 
                    c:=4; e3:=133; 
                    e:= 34*4;
                END.";

Lexer lexer = new Lexer(effect); 
Parser pr = new Parser(lexer);
Interpreter i = new Interpreter(pr);
i.Interpret();

Console.WriteLine("a = " + i.Scope["a"]);
Console.WriteLine("a = " + i.Scope["c"]);

//Lexer l = new Lexer("BEGIN a := 2; END.");

// problema en instruciones asi a := (4*2) + (5/2); c := 4   d:= 3