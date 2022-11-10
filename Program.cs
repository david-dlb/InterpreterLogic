using System;
using Conqueror.Logic;
using Conqueror.Logic.Language;
string effect = @"BEGIN   
                    IF(4 == 4 | 3 == 2) BEGIN 
                        a:= 1;
                    END;
                END.";

Lexer lexer = new Lexer(effect); 
Parser pr = new Parser(lexer);
Interpreter i = new Interpreter(pr);
i.Interpret();

Console.WriteLine("a = " + i.Scope["a"]);

//Lexer l = new Lexer("BEGIN a := 2; END.");

// problema en instruciones asi a := (4*2) + (5/2); c := 4   d:= 3