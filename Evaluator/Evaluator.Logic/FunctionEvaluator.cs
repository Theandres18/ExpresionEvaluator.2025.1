namespace Evaluator.Logic;

using System;
using System.Globalization;

public class FunctionEvaluator
{
    public static double Evaluate(string infix)
    {
        var postfix = ToPostfix(infix);
        // Uncomment to debug the postfix notation
        // Console.WriteLine($"Postfix notation: '{postfix}'");
        return Calculate(postfix);
    }

    private static double Calculate(string postfix)
    {
        var stack = new Stack<double>(100);
        var numberBuffer = string.Empty;

        for (int i = 0; i < postfix.Length; i++)
        {
            if (char.IsDigit(postfix[i]) || postfix[i] == '.')
            {
                // Accumulate digits for multi-digit numbers
                numberBuffer += postfix[i];
            }
            else if (postfix[i] == ' ')
            {
                // Space used as separator - push accumulated number if any
                if (!string.IsNullOrEmpty(numberBuffer))
                {
                    stack.Push(double.Parse(numberBuffer, CultureInfo.InvariantCulture));
                    numberBuffer = string.Empty;
                }
            }
            else if (IsOperator(postfix[i]))
            {
                // Make sure any pending number is processed first
                if (!string.IsNullOrEmpty(numberBuffer))
                {
                    stack.Push(double.Parse(numberBuffer, CultureInfo.InvariantCulture));
                    numberBuffer = string.Empty;
                }

                var number2 = stack.Pop();
                var number1 = stack.Pop();
                var result = Calculate(number1, postfix[i], number2);
                stack.Push(result);
            }
        }

        // Push any remaining number
        if (!string.IsNullOrEmpty(numberBuffer))
        {
            stack.Push(double.Parse(numberBuffer, CultureInfo.InvariantCulture));
        }

        return stack.Pop();
    }

    private static double Calculate(double number1, char @operator, double number2)
    {
        switch (@operator)
        {
            case '^': return Math.Pow(number1, number2);
            case '*': return number1 * number2;
            case '/': return number1 / number2;
            case '+': return number1 + number2;
            case '-': return number1 - number2;
            default: throw new Exception("Not a valid operator.");
        }
    }

    private static string ToPostfix(string infix)
    {
        var stack = new Stack<char>(100);
        var postfix = string.Empty;
        var numberBuffer = string.Empty;

        for (int i = 0; i < infix.Length; i++)
        {
            // Skip spaces in the input
            if (infix[i] == ' ')
                continue;

            if (char.IsDigit(infix[i]) || infix[i] == '.')
            {
                // Accumulate digits for multi-digit numbers
                numberBuffer += infix[i];
            }
            else
            {
                // If we were building a number, add it to postfix with space
                if (!string.IsNullOrEmpty(numberBuffer))
                {
                    postfix += numberBuffer + " ";
                    numberBuffer = string.Empty;
                }

                if (IsOperator(infix[i]))
                {
                    if (stack.IsEmpty())
                    {
                        stack.Push(infix[i]);
                    }
                    else
                    {
                        if (infix[i] == ')')
                        {
                            do
                            {
                                postfix += stack.Pop() + " ";  // Add space after operator
                            } while (stack.GetItemInTop() != '(');
                            stack.Pop();
                        }
                        else
                        {
                            if (PriorityInExpression(infix[i]) > PriorityInStack(stack.GetItemInTop()))
                            {
                                stack.Push(infix[i]);
                            }
                            else
                            {
                                postfix += stack.Pop() + " ";  // Add space after operator
                                stack.Push(infix[i]);
                            }
                        }
                    }
                }
            }
        }

        // Add any remaining number
        if (!string.IsNullOrEmpty(numberBuffer))
        {
            postfix += numberBuffer + " ";
        }

        while (!stack.IsEmpty())
        {
            postfix += stack.Pop() + " ";  // Add space after operator
        }

        return postfix;
    }

    private static bool IsOperator(char item)
    {
        return item == '(' || item == ')' || item == '^' || item == '/' || item == '*' || item == '+' || item == '-';
    }

    private static int PriorityInExpression(char @operator)
    {
        switch (@operator)
        {
            case '^': return 4;
            case '*': return 2;
            case '/': return 2;
            case '+': return 1;
            case '-': return 1;
            case '(': return 5;
            default: throw new Exception("Not a valid operator.");
        }
    }

    private static int PriorityInStack(char @operator)
    {
        switch (@operator)
        {
            case '^': return 3;
            case '*': return 2;
            case '/': return 2;
            case '+': return 1;
            case '-': return 1;
            case '(': return 0;
            default: throw new Exception("Not a valid operator.");
        }
    }
}