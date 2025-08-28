public static class Chapter4Choices
{
    public static readonly string[,] Easy = new string[,]
    {
        { "Binomial", "Monomial", "Polynomial", "Trinomial" },
        { "(x + 2)(x + 3)", "(x - 2)(x - 3)", "(x + 1)(x + 6)", "(x + 3)(x - 2)" },
        { "(x - 2)(x - 5)", "(x + 1)(x + 10)", "(x + 4)(x + 3)", "(x + 5)(x + 2)" },
        { "(x + 1)(x + 9)", "(x + 3)(x + 3)", "(x + 2)(x + 4)", "(x + 9)(x - 1)" },
        { "(x - 5)(x - 5)", "(x - 2)(x - 8)", "(x + 5)(x - 5)", "(x - 6)(x - 4)" }
    };

    public static readonly string[,] Average = new string[,]
    {
        { "x²+4x+5", "x²+3x+7", "x²-2x-15", "x²-5x+6.5" },
        { "3x²+2x+8", "3x²+10x+8", "3x²-10x+8", "3x²+10x-8" },
        { "(x+2)(3x-1)", "(3x+1)(x-2)", "(3x-2)(x+1)", "(3x-1)(x-2)" },
        { "x=1,32", "x=-1,-3", "x=-3,-2", "x=-1,-32" },
        { "x=-6,4", "x=6,-4", "x=2,-12", "x=-2,-12" }
    };

    public static readonly string[,] Hard = new string[,]
    {
        { "(3x+5)(2x-3)", "(2x+5)(3x-3)", "(3x-5)(2x+3)", "(2x-5)(3x+3)" },
        { "x²-7x+10", "x²-5x+6", "x²-2x+2", "x²+6x+8" },
        { "-12", "-6", "6", "9" },
        { "4 and 5", "5 and 8", "2 and 10", "4 and 10" },
        { "4x²-13x+9", "6x²-13x+6", "3x²-13x+12", "2x²-13x+18" }
    };
}