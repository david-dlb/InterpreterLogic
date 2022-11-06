using System;
using Conqueror.Logic;
using Conqueror.Logic.Language;
string effect = "++-+---1+++(---+34)";

Lexer lexer = new Lexer(effect); 
Parser pr = new Parser(lexer);
Interpreter i = new Interpreter(pr);
Object result = i.Interpret();

Console.WriteLine(result);