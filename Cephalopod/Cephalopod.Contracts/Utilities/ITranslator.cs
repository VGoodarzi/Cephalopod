namespace Cephalopod.Contracts.Utilities
{
    public interface ITranslator
    {
        string this[string name] { get; }
    }
}