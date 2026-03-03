using System.Globalization;

string? input = Console.ReadLine();

if (string.IsNullOrWhiteSpace(input))
{
    return;
}

string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

if (parts.Length < 2)
{
    return;
}

double first = double.Parse(parts[0], CultureInfo.InvariantCulture);
double second = double.Parse(parts[1], CultureInfo.InvariantCulture);
double sum = first + second;

Console.WriteLine($"Sum = {sum.ToString(CultureInfo.InvariantCulture)}");
