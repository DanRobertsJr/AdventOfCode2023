var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../input.txt");
var input = File.ReadAllLines(filePath);
var numbers = new List<Number>();
var symbols = new List<Symbol>();

for (var row = 0; row < input.Length; row++)
{
    var currentNumber = new Number();

    for (var column = 0; column < input[row].Length; column++)
    {
        if (input[row][column] == '.')
        {
            continue;
        }

        if (int.TryParse(input[row][column].ToString(), out var digit))
        {
            var number = "";
            number += digit;
            if (number.Length == 1)
            {
                currentNumber.StartCoordinate = (row, column);
            }

            while (column < input[row].Length - 1  && int.TryParse(input[row][column + 1].ToString(), out digit))
            {
                number += digit;
                column++;
            }

            currentNumber.EndCoordinate = (row, column);
            currentNumber.FullNumber = int.Parse(number);
            numbers.Add(currentNumber);
            currentNumber = new Number();
        }
        
        if (!int.TryParse(input[row][column].ToString(), out _))
        {
            symbols.Add(new Symbol
            {
                Character = input[row][column],
                Coordinate = (row, column)
            });    
        }
    }
}

var partOneAnswer = numbers
    .Where(number => symbols.Any(symbol => AreAdjacent(number, symbol)))
    .Sum(number => number.FullNumber);

var partTwoAnswer = symbols
    .Where(symbol => symbol.Character == '*')
    .Select(symbol => numbers.Where(number => AreAdjacent(number, symbol)).ToArray())
    .Where(gears => gears.Length == 2)
    .Sum(gears => gears[0].FullNumber * gears[1].FullNumber);

Console.WriteLine($"Part 1: {partOneAnswer}");
Console.WriteLine($"Part 2: {partTwoAnswer}");

static bool AreAdjacent(Number number, Symbol symbol)
{
    return Math.Abs(symbol.Coordinate.Row - number.StartCoordinate.Row) <= 1 
           && symbol.Coordinate.Column >= number.StartCoordinate.Column - 1 
           && symbol.Coordinate.Column <= number.EndCoordinate.Column + 1;
}

internal struct Number
{
    public int FullNumber { get; set; }
    public (int Row, int Column) StartCoordinate { get; set; }
    public (int Row, int Column) EndCoordinate { get; set; }
}

internal struct Symbol
{
    public char Character { get; set; }
    public (int Row, int Column) Coordinate { get; set; }
}
