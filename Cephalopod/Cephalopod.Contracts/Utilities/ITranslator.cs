namespace Cephalopod.Client.Contracts;

public interface ITranslator
{
    string this[string name] { get; }
}