using System;
namespace Conqueror.Logic.Language;

class Imp : Test{
    public   void Visit(Context context) {
        Console.WriteLine("imp");
    }
}

class Imp2 : Test{
    public   void Visit(Context context) {
        Console.WriteLine("imp2");
    }
}