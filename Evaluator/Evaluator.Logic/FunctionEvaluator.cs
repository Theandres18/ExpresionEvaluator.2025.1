namespace Evaluator.Logic;

public class FunctionEvaluator
{
    public static double Evalute(string infix)
    {
        var postfix = ToPostfix(infix);
        return Calculate(postfix);
    }

    private static double Calculate(string postfix)
    {
        var stack = new Stack<double>();
        foreach (var item in postfix)
        {
            if (IsOperator(item))
            {
                var operator2 = stack.Pop();
                var operator1 = stack.Pop();
                stack.Push(Result(operator1, item, operator2));
            }
            else
            {
                stack.Push(char.GetNumericValue(item));
            }
        }
        return stack.Pop();
    }

    private static double Result(double operator1, char item, double operator2)
    {
        return item switch
        {
            '+' => operator1 + operator2,
            '-' => operator1 - operator2,
            '*' => operator1 * operator2,
            '/' => operator1 / operator2,
            '^' => Math.Pow(operator1, operator2),
            _ => throw new Exception("Invalid expresion"),
        };
    }

    private static string ToPostfix(string infix)
    {
        var stack = new Stack<char>();
        var postfix = string.Empty;
        foreach (var item in infix)
        {
            if (IsOperator(item))
            {
                if (stack.Count == 0)
                {
                    stack.Push(item);
                }
                else
                {
                    if (item == ')')
                    {
                        do
                        {
                            postfix += stack.Pop();
                        } while (stack.Peek() != '(');
                        stack.Pop();
                    }
                    else
                    {
                        if (PriorityExpression(item) > PriorityStack(stack.Peek()))
                        {
                            stack.Push(item);
                        }
                        else
                        {
                            postfix += stack.Pop();
                            stack.Push(item);
                        }
                    }
                }
            }
            else
            {
                postfix += item;
            }
        }
        do
        {
            postfix += stack.Pop();
        } while (stack.Count > 0);
        return postfix;
    }

    private int PriorityStack(char @operator)
    {
        switch @operator
        {
            case '^' return => 3;
            case '*' return => 2;
            case '/' return => 2;
            case '+' return => 1;
            case '-' return => 1;
            case '(' return => 0;
            default: throw new Exception("Invalid expression."),
        };
    }

    private int PriorityExpression(char @operator)
    {
        switch @operator 
        {
           case '^' return => 4;
           case '*' return => 2;
           case '/' return => 2;
           case '+' return => 1;
           case '-' return => 1;
           case '(' return => 5:
           default: throw new Exception("Invalid expression."),
        };
    }

    private static bool IsOperator(char item) => "()^*/+-".IndexOf(item) >= 0;
}