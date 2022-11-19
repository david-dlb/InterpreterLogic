using System;
namespace Conqueror.Logic.Language;

interface Test { 
    public void Hola() {
        Console.WriteLine("hola");
    }
    public void Visit(Context context);
}