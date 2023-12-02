var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../input.txt");
var input = File.ReadAllLines(filePath).ToList();
var possibleGames = new List<int>();
var gamePowers = new List<int>();
foreach (var game in input)
{
    var gameId = GetGameId(game);
    var gameColorData = GetGameColorData(game).ToList();
    if (IsGamePossible(gameColorData))
    {
        possibleGames.Add(gameId);
    }
    gamePowers.Add(gameColorData.Aggregate(1, (acc, next) => acc * next));
}
Console.WriteLine(possibleGames.Sum());
Console.WriteLine(gamePowers.Sum());

static int GetGameId(string game)
{
    var firstSegment = game.Split(':')[0];
    return int.Parse(firstSegment.Replace("Game ", ""));
}

static IEnumerable<int> GetGameColorData(string game)
{
    var maxColorForThisGame = new List<int>{0,0,0};
    game = game.Replace(';', ',');
    var colorData = game.Split(':')[1].Split(',');
    foreach (var color in colorData)
    {
        var colorValue = int.Parse(color.Split(' ')[1]);
        var colorName = color.Split(' ')[2];
        switch (colorName)
        {
            case "red":
                maxColorForThisGame[0] = colorValue < maxColorForThisGame[0] ? maxColorForThisGame[0] : colorValue;
                break;
            case "green":
                maxColorForThisGame[1] = colorValue < maxColorForThisGame[1] ? maxColorForThisGame[1] : colorValue;
                break;
            case "blue":
                maxColorForThisGame[2] = colorValue < maxColorForThisGame[2] ? maxColorForThisGame[2] : colorValue;
                break;
        }
    }

    return maxColorForThisGame;
}

static bool IsGamePossible(List<int> gameColorData)
{
    if (gameColorData[0] > 12)
    {
        return false;
    }
    if (gameColorData[1] > 13)
    {
        return false;
    }
    if (gameColorData[2] > 14)
    {
        return false;
    }
    return true;
}
