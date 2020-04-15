namespace Interfaces.Model
{
    public interface IElementInfo
    {
        string Selector { get; }
        string Control { get; }
        string Id { get; }
        string Class { get; }
        string Name { get; }
    }
}
