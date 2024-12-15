namespace AdventOfCode.Utility;

public struct VecInt2 : IEquatable<VecInt2>
{
    public int x;
    public int y;
    
    public VecInt2(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public VecInt2()
    {
        this.x = 0;
        this.y = 0;
    }

    public static VecInt2 operator +(VecInt2 a, VecInt2 b) => new(a.x + b.x, a.y + b.y);
    public static VecInt2 operator -(VecInt2 a, VecInt2 b) => a + (-1 * b);
    
    public static VecInt2 operator *(VecInt2 a, int b) => new(a.x * b, a.y * b);
    public static VecInt2 operator *(int b, VecInt2 a) => a * b;
    
    public static VecInt2 operator /(VecInt2 a, int b) => new(a.x / b, a.y / b);

    public static bool operator ==(VecInt2 a, VecInt2 b) => a.x == b.x && a.y == b.y;
    public static bool operator !=(VecInt2 a, VecInt2 b) => a.x != b.x || a.y != b.y;
    
    public bool Equals(VecInt2 other) => x == other.x && y == other.y;
    public override bool Equals(object? obj) => obj is VecInt2 other && Equals(other);

    public override int GetHashCode() => HashCode.Combine(x, y);

    public override string ToString() => $"({x}, {y})";
    
    
    
    public static void OperatorTests()
    {
        VecInt2 a = new VecInt2(1, 2);
        VecInt2 b = new VecInt2(3, 4);
        
        Console.WriteLine($"Addition: {a + b}");
        Console.WriteLine($"Subtraction: {a - b}");
        Console.WriteLine($"Int Mul: {5 * a}, {3 * b}");
        Console.WriteLine($"Equality: {a != b}, {new VecInt2(5, 5) == new VecInt2(2+3, 5)}");
        Console.WriteLine($"Int Div: {b / 3}");
        Console.WriteLine($"x: {a.x}, y: {a.y}");
    }
}