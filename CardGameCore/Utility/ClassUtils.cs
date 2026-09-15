using CardGameCore.Framework.Cards;

namespace CardGameCore.Utility;

public static class ClassUtils
{
    public static void Foo()
    {
        var type = typeof(ICard);
        
        IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => type.IsAssignableFrom(p) && p is { IsClass: true, IsAbstract: false });
        
        
    } 
}