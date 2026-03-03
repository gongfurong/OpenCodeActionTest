using System.Globalization;

string? input = Console.ReadLine();

if (string.IsNullOrWhiteSpace(input))
{
    return;
}

if (!TryParseExpression(input, out double left, out char op, out double right))
{
    return;
}

if (op != '+')
{
    return;
}

double sum = left + right;

Console.WriteLine($"Sum = {sum.ToString(CultureInfo.InvariantCulture)}");

static bool TryParseExpression(string rawInput, out double left, out char op, out double right)
{
    left = 0;
    op = '\0';
    right = 0;

    string input = rawInput.Replace(" ", string.Empty);
    int opIndex = input.IndexOfAny(['+', '-', '*', '/']);

    if (opIndex <= 0 || opIndex >= input.Length - 1)
    {
        return false;
    }

    op = input[opIndex];

    string leftText = input[..opIndex];
    string rightText = input[(opIndex + 1)..];

    bool isLeftValid = double.TryParse(leftText, NumberStyles.Float, CultureInfo.InvariantCulture, out left);
    bool isRightValid = double.TryParse(rightText, NumberStyles.Float, CultureInfo.InvariantCulture, out right);

    return isLeftValid && isRightValid;
}
