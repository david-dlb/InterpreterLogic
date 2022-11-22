using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
namespace Conqueror.Logic.Language;

class Context {
    private Dictionary<Token, int> scope;
    public Context() {
        scope = new Dictionary<Token, int>();
    }
    public void Show() {
        foreach (var item in scope) {
            Console.WriteLine(item.Key.Value + " " + item.Value);
        }
    }
    public void Add(Token token, int value) {
        if (ContainsId(token.Value) == true) {
            Utils.Error("Variable previamente declarada");
        }
        scope.Add(token, value);
    }
    public bool ContainsId(string id) {
        foreach (var item in scope) {
            if (item.Key.Value == id) {
                return true;
            }  
        }
        return false;
    }
    public int? GetValue(string id) {
        foreach (var item in scope) {
            if (item.Key.Value == id) {
                return item.Value;
            }  
        }
        Utils.Error("Intento de acceder a una variable no declarada");
        return null;
    }public string? GetType(string id) {
        foreach (var item in scope) {
            if (item.Key.Value == id) {
                return item.Key.Type;
            }  
        }
        Utils.Error("Intento de acceder a una variable no declarada");
        return null;
    }
    public void Update(string id, int value) {
        if (!ContainsId(id) == true) {
            Utils.Error("Intento de acceder a una variable no declarada");
        }
        foreach (var item in scope) {
            if (item.Key.Value == id) {
                scope[item.Key] = value;
                return;
            }  
        }
    }
}