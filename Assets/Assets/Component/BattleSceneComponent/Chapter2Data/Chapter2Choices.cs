public static class Chapter2Choices
{
    public static readonly string[,] Easy = new string[,]
    {
        { "Sum of two squares", "Difference of two squares", "Sum and difference of two terms", "Product of sum and difference of two terms" },
        { "(x²-36)²", "(x-6)²-72", "(x²-36)(x²+36)", "(x²-72)(x²+72)" },
        { "Ab", "A=b only", "A=0 and b=0", "A=b or a=-b" },
        { "x²+16", "x²-16", "x²-2x", "(x-4)²" },
        { "x²+9", "x²-25", "4x²-49", "16x²-1" }
    };

    public static readonly string[,] Average = new string[,]
    {
        { "(x-1)(x+1)", "(4x-1)(4x+1)", "(8x-1)(2x+1)", "(16x-1)(x+1)" },
        { "x=7", "x=3", "x=73", "x=9,-49" },
        { "(3x-2y)(3x-2y)", "(3x+2y)(3x-2y)", "(3x-2y)(-3x-2y)", "(-3x-2y)(-3x-2y)" },
        { "x=2", "x=5", "x=25", "x=52" },
        { "x=73", "x=37", "x=4", "x=97" }
    };

    public static readonly string[,] Hard = new string[,]
    {
        { "(x²-4)(x²+4)", "(x-4)(x+4)(x²+4)", "(x²-4)(x-2)(x+2)", "(x-2)(x+2)(x²+4)" },
        { "(9a³-4b)(9a³+4b)", "(9a³-2b)(9a³+8b)", "(27a²-8b)(3a⁴+2b)", "(81a²-16b)(a⁴+b²)" },
        { "(x-17)(x+17)", "(x-1)(x+1 49)", "(x-7)(x+7)", "(7x-1)(7x+1)" },
        { "x=7", "x=72", "x=494", "x=3.5" },
        { "a=b only", "a=-b only", "a=b or a=-b", "a=0 and b=0" }
    };
}