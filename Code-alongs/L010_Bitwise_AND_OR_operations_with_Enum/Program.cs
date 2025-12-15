int a = 5;
int b = 12;

Console.WriteLine(a.ToString("B8"));
Console.WriteLine(b.ToString("B8"));
Console.WriteLine((a & b).ToString("B8"));

int c = a & b;

Console.WriteLine();
Console.WriteLine(c);
Console.WriteLine();

Option myOption = Option.OptionB;

Console.WriteLine(myOption);
Console.WriteLine((int)myOption);
Console.WriteLine(((int)myOption).ToString("B8"));
Console.WriteLine();

Option myOtherOption = Option.OptionD;
Console.WriteLine(myOtherOption);
Console.WriteLine((int)myOtherOption);
Console.WriteLine(((int)myOtherOption).ToString("B8"));
Console.WriteLine();

Console.WriteLine(((int)myOtherOption | (int)myOption).ToString("B8"));

Console.WriteLine();
Console.WriteLine(myOption | myOtherOption);

[Flags]
enum Option { OptionA = 1, OptionB = 2, OptionC = 4, OptionD = 8 }