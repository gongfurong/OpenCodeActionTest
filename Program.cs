using System.Globalization;

try
{
    string? input = Console.ReadLine();

    if (!TryParseAdditionExpression(input, out double left, out double right, out string errorMessage))
    {
        Console.WriteLine(errorMessage);
        return;
    }

    double sum = left + right;
    Console.WriteLine($"Sum = {sum.ToString(CultureInfo.InvariantCulture)}");
}
catch (Exception ex)
{
    Console.WriteLine($"Unexpected error: {ex.Message}");
}

static bool TryParseAdditionExpression(string? input, out double left, out double right, out string errorMessage)
{
    left = 0;
    right = 0;
    errorMessage = string.Empty;

    if (string.IsNullOrWhiteSpace(input))
    {
        errorMessage = "Input cannot be empty. Expected format: <float>+<float>.";
        return false;
    }

    string[] parts = input.Split('+', StringSplitOptions.TrimEntries);
    if (parts.Length != 2)
    {
        errorMessage = "Invalid expression. Expected exactly one '+' in format: <float>+<float>.";
        return false;
    }

    if (string.IsNullOrWhiteSpace(parts[0]) || string.IsNullOrWhiteSpace(parts[1]))
    {
        errorMessage = "Both operands are required in format: <float>+<float>.";
        return false;
    }

    if (!double.TryParse(parts[0], NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out left))
    {
        errorMessage = $"Invalid left operand: '{parts[0]}'.";
        return false;
    }

    if (!double.TryParse(parts[1], NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out right))
    {
        errorMessage = $"Invalid right operand: '{parts[1]}'.";
        return false;
    }

    return true;
}
