namespace Cephalopod.Client.Contracts;

public class FakeTranslator : ITranslator
{

    private readonly Dictionary<string, string> _fa = [];

    public FakeTranslator()
    {
        _fa.Add("Home","داشبورد");
        _fa.Add("Counter","شمارنده");
        _fa.Add("Weather", "آب و هوا");
        _fa.Add("Brand", "برند");
        _fa.Add("Name", "عنوان");
        _fa.Add("Status", "وضعیت");
        _fa.Add("ImageUrl", "تصویر");
        _fa.Add("Description", "توضیحات");
        _fa.Add("Inactive", "غیرفعال");
        _fa.Add("Active", "فعال");
        _fa.Add("Badoomeh", "بادومه");
    }

    public string this[string name] => _fa.GetValueOrDefault(name) ?? name;
}