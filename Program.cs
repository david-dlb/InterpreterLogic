using System.Collections.Generic;
using System;
using Conqueror.Logic;
using Conqueror.Logic.Language;
string effect = @"   
                    CantMyCards= 2; 
                    if (EnemyLife == 2) {
                        EnemyLife = 3;
                        ChangeHands();
                    };  
                    while (EnemyLife > 1) {
                        EnemyLife = EnemyLife - 1;
                    }
                ";

Lexer lexer = new Lexer(effect); 
Parser pr = new Parser(lexer);

Dictionary<string, int> scope;
scope = new Dictionary<string, int>();
scope.Add("MyLife", 30);
scope.Add("EnemyLife", 15);
scope.Add("MyCharms", 5);
scope.Add("EnemyCharms", 5); 

Context ctx = new Context();
ctx.Add(new Token("INT", "MyLife"), 30);
ctx.Add(new Token("INT", "EnemyLife"), 30);
ctx.Add(new Token("INT", "MyCharms"), 5);
ctx.Add(new Token("INT", "EnemyCharms"), 5);
ctx.Add(new Token("CONST", "CantMyCards"), 0);
ctx.Add(new Token("FUNC", "ChangeHands"), 0);

 
Interpreter i = new Interpreter(pr, ctx);
i.Interpret();
ctx.Show();
// foreach (var item in i.Scope)
// {
//     Console.ForegroundColor = ConsoleColor.DarkYellow;
//     Console.Write('[');

//     Console.ForegroundColor = ConsoleColor.Green;
//     Console.Write(item.Key);

//     Console.ForegroundColor = ConsoleColor.DarkYellow;
//     Console.Write("]: ");

//     Console.ForegroundColor = ConsoleColor.DarkBlue;
//     Console.WriteLine(item.Value);
// }