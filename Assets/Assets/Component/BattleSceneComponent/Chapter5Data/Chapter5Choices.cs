public static class Chapter5Choices
{
    public static readonly string[,] Easy = new string[,]
    {
        { "(x+1)(x-35)", "(x-9)(x+4)", "(x+3)(x-7)", "(x+7)(x-5)" },
        { "m=2, 6", "m=3, 4", "m=-3, - 4", "m=-12, - 1" },
        { "x=6, 2", "x=5, 1", "x=-12, - 1", "x=-3, - 2" },
        { "Group terms", "Get the LCM", "Factor out GCFs", "Factor out common factor" },
        { "(n+13)(n+4)", "(n+2)(n+26)", "(n+1)(n+52)", "Not factorable" }
    };

    public static readonly string[,] Average = new string[,]
    {
        { "x=1,2", "x=2,-<sup>1</sup>/<sub>3</sub>", "x=-2, -1", "x=-<sup>1</sup>/<sub>3</sub>, -2" },
        { "(y+5)(y-3)", "(5y+1)(y-3)", "(y-15)(y+4)", "(5y-1)(y+14)" },
        { "c=11, 5", "c=22,3", "c=-11, 2", "c=13,-2" },
        { "(2x+3)(x+4)=0", "(x+3)(2x+4)=0", "(2x+4)(x+3)=0", "(2x+12)(x+1)=0" },
        { "z=3,6", "z=9,-3", "z=-4,-2", "z=-8,-3" }
    };

    public static readonly string[,] Hard = new string[,]
    {
        { "(x+2)(x+3)", "(x+2)(x²+3)", "(x+3)(x²+2)", "(x²+3)(x+2)" },
        { "(x²+3)(2x+5)", "(2x+3)(x²+5)", "(2x²+3)(x+5)", "(x²+3)(2x+5)" },
        { "(a-3)(4a²+1)", "(2a-3)(2a²+1)", "(2a²-3)(2a+1)", "Cannot be factored using grouping" },
        { "(2ab-5)(2b+3)", "(2ab+5)(2b-3)", "(2ab+5)(2b+3)", "(2ab+3)(2b+5)" },
        { "(3x+5y)(2x-2y)", "(3x-2y)(2x+5y)", "(2x-5y)(3x+2y)", "(2x-y)(3x+10y)" }
    };
}