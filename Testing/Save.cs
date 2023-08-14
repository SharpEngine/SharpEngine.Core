using SharpEngine.Core.Data.Save;

namespace Testing;

public record Save(int a, string b)
{
    public static Save FromSave(ISave save, string prefix)
    {
        return new Save(
            save.GetObjectAs("a", 0),
            save.GetObjectAs("b", "test")
        );
    }
}