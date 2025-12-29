using System.IO;
using System.Text;

class Program
{
    static void Main()
    {
        string filePath = "可参考的往数据库里添加的数据.sql";
        string content = File.ReadAllText(filePath, Encoding.Default);
        File.WriteAllText(filePath, content, Encoding.UTF8);
        System.Console.WriteLine("File converted to UTF-8 encoding successfully.");
    }
}