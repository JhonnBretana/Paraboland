public static class Chapter3Choices
{
    public static readonly string[,] Easy = new string[,]
    {
        { "Binomial", "Trinomial", "Monomial", "Perfect square trinomial" },
        { "x³ - x + 5 = 0", "x² + 2x + 1 = 0", "x² - 3x + 4 = 0", "x² + 5x + 10 = 0" },
        { "1", "6", "9", "10" },
        { "x = 4", "x = 8", "x = -4", "x = -8" },
        { "x = 2", "x = 3", "x = 4", "x = -1" }
    };

    public static readonly string[,] Average = new string[,]
    {
        { "x² + 4x + 8", "x² - 3x + 10", "x² - 9x + 27", "x² + 10x + 25" },
        { "(a + 3)(a + 3)", "(4a + 3)(a + 3)", "(2a + 3)(2a + 3)", "(2a - 3)(2a - 3)" },
        { "x = 52", "x = -52", "x = 25", "x = -25" },
        { "(3x + 4)²", "(3x - 4)²", "(x - 4)(x - 4)", "(x - 3)(x - 3)" },
        { "x = 3", "x = 13", "x = -3", "x = -13" }
    };

    public static readonly string[,] Hard = new string[,]
    {
        { "x = 5", "x = -5", "x = 5, -5", "No possible root" },
        { "(7m + 4)(7m + 4)", "(7m + 8)(7m - 2)", "(49m + 2)(m + 8)", "Not a perfect square trinomial" },
        { "16x² + 24x + 9", "25a² - 30a + 9", "4m² + 12m + 9", "36x² - 48x + 49" },
        { "x = 54", "x = 52", "x = 254", "x = -52, 52" },
        { "16", "25", "36", "100" }
    };
}