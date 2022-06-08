using LiteDB;
using UrlShortenerCase;

Uri uriObject;
Console.WriteLine("Please write an url...");
var url = Console.ReadLine();
bool uriResult = Uri.TryCreate(url, UriKind.Absolute, out uriObject) && (uriObject.Scheme == Uri.UriSchemeHttp || uriObject.Scheme == Uri.UriSchemeHttps);

if (uriResult)
{
    var createdKey = GetKey();

    if (!IfKeyExistAndExpires(createdKey))
    {
        UrlShortener urlShortener = new UrlShortener
        {
            CreatedDate = DateTime.Now,
            Expires = DateTime.Now.AddDays(3),
            Key = createdKey,
            Url = url
        };

        try
        {
            AddUrl(urlShortener);
            Console.WriteLine("URL is added here there are all url records");

        }
        catch (Exception e)
        {

            Console.WriteLine("Url is not addedd due to " + e.Message);

        }


    }
}
else
{
    Console.WriteLine("You wrote an invalid url...");
}
Console.ReadLine();

static string GetKey()
{
    Random rand = new Random();
    const string keyChars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    var stringChars = new char[6];

    for (int i = 0; i < stringChars.Length; i++)
    {
        stringChars[i] = keyChars[rand.Next(keyChars.Length)];
    }
    string createdNewKeys = new String(stringChars);

    return createdNewKeys;
}

static bool IfKeyExistAndExpires(string key)
{
    bool isExist = false;
    using (var db = new LiteDatabase(@"Case.db"))
    {
        var record = db.GetCollection<UrlShortener>("UrlShortener").Exists(P => P.Key == key && DateTime.Now > P.Expires);
        if (record)
        {
            isExist = true;
        }

        return isExist;

    }
}

static void AddUrl(UrlShortener url)
{
    using (var db = new LiteDatabase(@"Case.db"))
    {
        var table = db.GetCollection<UrlShortener>("UrlShortener");
        table.Insert(url);

    }
}
