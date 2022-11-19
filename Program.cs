using System.Collections.Generic;
using System;
using Conqueror.Logic;
using Conqueror.Logic.Language;
string effect = @"BEGIN   
                    EnemyLife:= EnemyLif(); 
                END.";

Lexer lexer = new Lexer(effect); 
Parser pr = new Parser(lexer);

// Dictionary<string, int> scope;
// scope = new Dictionary<string, int>();
// scope.Add("MyLife", 30);
// scope.Add("EnemyLife", 15);
// scope.Add("MyCharms", 5);
// scope.Add("EnemyCharms", 5); 

// Console.WriteLine("MyLife = " + i.Scope["MyLife"]);
// Console.WriteLine("MyCharms = " + i.Scope["MyCharms"]);
// Console.WriteLine("EnemyLife = " + i.Scope["EnemyLife"]);
// Console.WriteLine("EnemyCharms = " + i.Scope["EnemyCharms"]);

//Lexer l = new Lexer("BEGIN a := 2; END.");

// problema en instruciones asi a := (4*2) + (5/2); c := 4   d:= 3


Test t = new Imp();
t.Visit(new Context());