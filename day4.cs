var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../input.txt");
var input = File.ReadAllLines(filePath);
var cards = new Cards
{
    CardsList = new List<Scratcher>()
};

foreach (var card in input)
{
    var currentScratcher = (Scratcher)card;
    cards.CardsList.Add(currentScratcher);
}
Console.WriteLine(cards.CardsList.Sum(scratcher => scratcher.Points));
foreach (var scratcher in cards.CardsList)
{
    Console.WriteLine("Working on card " + scratcher.GameId);
    Console.WriteLine("Winners: " + scratcher.WinningMatches);
    for (int i = scratcher.GameId; i <= scratcher.GameId + scratcher.WinningMatches; i++)
    {
        if (i == scratcher.GameId)
        {
            continue;
        }
        Console.WriteLine("Moving to card " + i);
        var cardToUpdate = cards.CardsList.First(c => c.GameId == i);
        cardToUpdate.Multiplier += scratcher.Multiplier;
        Console.WriteLine("Increased the multiplier.");
        Console.WriteLine("New multiplier is " + cardToUpdate.Multiplier);
    }
}
Console.WriteLine(cards.CardsList.Sum(scratcher => scratcher.Multiplier));

internal class Cards
{
    public List<Scratcher> CardsList { get; set; }
}

internal class Scratcher
{
    public int GameId { get; set; }
    public List<int> WinningNumbers { get; set; }
    public List<int> ScratcherNumbers { get; set; }
    public int WinningMatches { get; set; }
    public int Points { get; set; }
    public int Multiplier { get; set; }

    public static explicit operator Scratcher(string line)
    {
        var gameId = line.Split(':')[0]
            .Replace("Card  ", "")
            .Replace("Card ", "");
        var winningNumbers = line.Split(':')[1]
            .Trim()
            .Split('|')[0]
            .Split(' ')
            .Where(c => !string.IsNullOrEmpty(c))
            .Select(int.Parse)
            .ToList();
        var scratcherNumbers = line.Split(':')[1]
            .Trim()
            .Split('|')[1]
            .Split(' ')
            .Where(c => !string.IsNullOrEmpty(c))
            .Select(int.Parse)
            .ToList();
        var winningMatches = winningNumbers.Count(num => scratcherNumbers.Contains(num));
        var points = (int)Math.Pow(2, winningMatches - 1);
        return new Scratcher
        {
            GameId = int.Parse(gameId),
            WinningNumbers = winningNumbers,
            ScratcherNumbers = scratcherNumbers,
            WinningMatches = winningMatches,
            Points = points,
            Multiplier = 1
        };
    }
}
