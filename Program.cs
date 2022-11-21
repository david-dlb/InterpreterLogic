using System.Collections.Generic;
using System;
using Conqueror.Logic;
using Conqueror.Logic.Language;
string effect = @"BEGIN   
                    EnemyLife:= 2+3; 
                    a();
                END.";

Lexer lexer = new Lexer(effect); 
Parser pr = new Parser(lexer);
pr.Parse();

Dictionary<string, int> scope;
scope = new Dictionary<string, int>();
scope.Add("MyLife", 30);
scope.Add("EnemyLife", 15);
scope.Add("MyCharms", 5);
scope.Add("EnemyCharms", 5); 