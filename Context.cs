using System.Collections.Generic;
namespace Conqueror.Logic.Language;

class Context {
    private Dictionary<string, int> scope;
    public Context() {
        scope = new Dictionary<string, int>();
    }
    public void Add(string id, int value) {
        scope.Add(id, value);
    }
    public bool ContainsId(string id) {
        return scope.ContainsKey(id);
    }
    public int Get(string id) {
        return scope[id];
    }
    public void Update(string name, int value) {
        scope[name] = value;
    }
}