<Query Kind="Statements" />

int? x = null;
x = 5;           
if (x != null)           
{
    int y = x.Value;      
    Console.WriteLine (y);
}
int z = x ?? 10;
