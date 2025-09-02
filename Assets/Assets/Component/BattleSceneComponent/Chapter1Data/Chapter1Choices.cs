public static class Chapter1Choices
{
    public static readonly string[,] Easy = new string[,]
    {
        { "Binomial", "Monomial", "Polynomial", "Trinomial" },
        { "Add all the terms", "Multiply the all terms", "Arrange the term in descending order", "Find the greatest monomial common factor (GCF) of all terms" },
        { "6(x+2)", "2(3x+6)", "3(x+4)", "x(6+12)" },
        { "x = 3 only", "x = -3 only", "x = 0 and x = -3", "x = -1 and x = -3" },
        { "x = 2 only", "x = -2 only", "x = 0 and x = 2", "x = -2 and x = 0" }
    };

    public static readonly string[,] Average = new string[,]
    {
        { "x = 0 and x = 2", "x = 3 and x = 2", "x = 0 and x = -2", "x = -3 and x = 2" },
        { "x = 0 and x = 3", "x = -3 and x = 0", "x = -2 and x = 6", "x = 0 and x = -3" },
        { "7xy(4x + 2)", "14xy(2x + 1)", "14x(2y + 1y)", "7xy(4x² + 2)" },
        { "8a²b²(2 - 1)", "8ab(2a - b)", "4ab(4a - 2b)", "4a(4ab - 2b²)" },
        { "x = 2 and x = 0", "x = 0 and x = 4", "x = 0 and x = -2", "x = -4 and x = -2" }
    };

    public static readonly string[,] Hard = new string[,]
    {
        { "x = 0 and x = -4", "x = 0 and x = -<sup>2</sup>/<sub>3</sub>", "x = -1.5 and x = 0", "x = 0 and x = -1" },
        { "x=0 and x=<sup>4</sup>/<sub>3</sub>", "x=0 and x=<sup>3</sup>/<sub>4</sub>", "x = 0 and x = -4", "x = -3 and x = -4" },
        { "3x²y(2x - y)", "3x²y²(2 - 1)", "3x²y(2x - y²)", "3xy(2x² - y)" },
        { "x = 0 and x = -2", "x = 0 and x = -3", "x = 0 and x = 3", "x = 0 and x = -1/3" },
        { "5mn(2m² + m)", "5mn²(2m² + 1)", "5m²n(2mn + 1)", "10mn(2m² + 1)" }
    };
}