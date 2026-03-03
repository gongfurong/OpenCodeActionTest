using System.Globalization;

try
{
    string? input = Console.ReadLine();

    if (!TryParseExpression(input, out double left, out char op, out double right, out string errorMessage))
    {
        Console.WriteLine(errorMessage);
        return;
    }

    if (!TryCalculate(left, op, right, out double result, out errorMessage))
    {
        Console.WriteLine(errorMessage);
        return;
    }

    Console.WriteLine($"Result = {result.ToString(CultureInfo.InvariantCulture)}");
}
catch (Exception ex)
{
    Console.WriteLine($"Unexpected error: {ex.Message}");
}

static bool TryParseExpression(string? input, out double left, out char op, out double right, out string errorMessage)
{
    left = 0;
    op = '\0';
    right = 0;
    errorMessage = string.Empty;

    if (string.IsNullOrWhiteSpace(input))
    {
        errorMessage = "Input cannot be empty. Expected format: <float><op><float>.";
        return false;
    }

    string normalized = string.Concat(input.Where(c => !char.IsWhiteSpace(c)));
    if (normalized.Length < 3)
    {
        errorMessage = "Invalid expression. Expected format: <float><op><float>.";
        return false;
    }

    int operatorIndex = -1;

    for (int i = 0; i < normalized.Length; i++)
    {
        char current = normalized[i];
        if (!IsSupportedOperator(current))
        {
            continue;
        }

        if (i == 0 || i == normalized.Length - 1)
        {
            continue;
        }

        char previous = normalized[i - 1];
        if ((current == '+' || current == '-') && (IsSupportedOperator(previous) || previous == 'e' || previous == 'E'))
        {
            continue;
        }

        if (operatorIndex != -1)
        {
            errorMessage = "Invalid expression. Use exactly one operator: +, -, *, /.";
            return false;
        }

        operatorIndex = i;
        op = current;
    }

    if (operatorIndex == -1)
    {
        errorMessage = "No valid operator found. Use one of: +, -, *, /.";
        return false;
    }

    string leftText = normalized[..operatorIndex];
    string rightText = normalized[(operatorIndex + 1)..];

    if (!double.TryParse(leftText, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out left))
    {
        errorMessage = $"Invalid left operand: '{leftText}'.";
        return false;
    }

    if (!double.TryParse(rightText, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out right))
    {
        errorMessage = $"Invalid right operand: '{rightText}'.";
        return false;
    }

    return true;
}

static bool TryCalculate(double left, char op, double right, out double result, out string errorMessage)
{
    result = 0;
    errorMessage = string.Empty;

    switch (op)
    {
        case '+':
            result = left + right;
            return true;
        case '-':
            result = left - right;
            return true;
        case '*':
            result = left * right;
            return true;
        case '/':
            if (right == 0)
            {
                errorMessage = "Division by zero is not allowed.";
                return false;
            }

            result = left / right;
            return true;
        default:
            errorMessage = $"Unsupported operator: '{op}'.";
            return false;
    }
}

static bool IsSupportedOperator(char c)
{
    return c == '+' || c == '-' || c == '*' || c == '/';
}
