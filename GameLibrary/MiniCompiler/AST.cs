using MiniCompiler;



interface IExpr
{
    public int Evaluate();
}
abstract class BinaryExpr : IExpr
{
    public IExpr left;
    public IExpr right;
    public BinaryExpr(IExpr left, IExpr right)
    {
        this.left = left;
        this.right = right;
    }
    public abstract int Evaluate();
}

class Plus : BinaryExpr
{
    public Plus(IExpr left, IExpr right) : base(left, right) { }
    public override int Evaluate()
    {
        return left.Evaluate() + right.Evaluate();
    }
}

class Minus : BinaryExpr
{
    public Minus(IExpr left, IExpr right) : base(left, right) { }
    public override int Evaluate()
    {
        return left.Evaluate() - right.Evaluate();
    }
}

class Mult : BinaryExpr
{
    public Mult(IExpr left, IExpr right) : base(left, right) { }
    public override int Evaluate()
    {
        return left.Evaluate() * right.Evaluate();
    }
}

class Div : BinaryExpr
{
    public Div(IExpr left, IExpr right) : base(left, right) { }
    public override int Evaluate()
    {
        if (right.Evaluate() == 0)
            throw new Exception("Division by 0");

        return left.Evaluate() / right.Evaluate();
    }
}

class Number : IExpr
{
    public int value;
    public Number(int value)
    {
        this.value = value;
    }
    public int Evaluate()
    {
        return value;
    }
}

class Identifier : IExpr
{
    string key;
    public Identifier(string key) => this.key = key;

    public int Evaluate() => Interpreter.cardsStatsDic[key];
}

class Higher : BinaryExpr
{
    public Higher(IExpr left, IExpr right) : base(left, right) { }

    public override int Evaluate()
    {
        if (left.Evaluate() > right.Evaluate())
            return 1;
        else
            return 0;
    }
}

class Minor : BinaryExpr
{
    public Minor(IExpr left, IExpr right) : base(left, right) { }

    public override int Evaluate()
    {
        if (left.Evaluate() < right.Evaluate())
            return 1;
        else
            return 0;
    }
}

class Same : BinaryExpr
{
    public Same(IExpr left, IExpr right) : base(left, right) { }

    public override int Evaluate()
    {
        if (left.Evaluate() == right.Evaluate())
            return 1;
        else
            return 0;
    }
}

class AND : BinaryExpr
{
    public AND(IExpr left, IExpr right) : base(left, right) { }

    public override int Evaluate()
    {
        if (left.Evaluate() == 1 && right.Evaluate() == 1)
            return 1;
        else
            return 0;
    }
}

class OR : BinaryExpr
{
    public OR(IExpr left, IExpr right) : base(left, right) { }

    public override int Evaluate()
    {
        if (left.Evaluate() == 1 || right.Evaluate() == 1)
            return 1;
        else
            return 0;
    }
}


interface Iinstruction
{
    void Execute();
}

class IF : Iinstruction
{
    IExpr condition;
    List<Iinstruction> ifInstructions = new List<Iinstruction>();

    public IF(IExpr condition, List<Iinstruction> ifInstructions)
    {
        this.condition = condition;
        this.ifInstructions = ifInstructions;
    }
    public void Execute()
    {
        if (condition.Evaluate() == 1)
        {
            for (int position = 0; position < ifInstructions.Count; position++)
            {
                ifInstructions[position].Execute();
            }
        }
    }
}

class Assignment : Iinstruction
{
    Token leftIdentifier;
    IExpr rightExpr;

    public Assignment(Token left, IExpr right)
    {
        leftIdentifier = left;
        rightExpr = right;
    }
    public void Execute()
    {
        Interpreter.cardsStatsDic[leftIdentifier.value] = rightExpr.Evaluate();
    }
}

class Action : Iinstruction //incomplete
{
    Token stringAction;

    public Action(Token stringAction)
    {
        this.stringAction = stringAction;
    }
    public void Execute()
    {
        Interpreter.gameActions[stringAction.value]();
    }
}
class GodTree : Iinstruction
{
    public List<Iinstruction> instructions = new List<Iinstruction>();

    public void Execute()
    {
        for (int position = 0; position < instructions.Count; position++)
        {
            instructions[position].Execute();
        }
    }
}