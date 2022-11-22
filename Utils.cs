using System;
namespace Conqueror.Logic;

static class Utils {
    public static string[] Names = { "StillCardEnemy", "ChangeHands" };
    public static void Error(string text) {
        throw new Exception(text);
    }
}