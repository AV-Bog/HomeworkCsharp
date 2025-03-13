using System.Text;
using System.IO;
using HW3.LZW;

Console.WriteLine("Введите путь к файлу, который надо сжать или разжать: ");
string filePath = Console.ReadLine();
while (!File.Exists(filePath))
{
    Console.WriteLine("Нормально введи ПУТЬ ДО ФАЙЛА");
    filePath = Console.ReadLine();
}

Console.WriteLine("Введите ключ -c, означающий, что файл надо сжать, или -u, означающий, что надо разжать: ");
string userInput = Console.ReadLine();
if (userInput == "-c")
{
    string outputFilePath = GenerateOutputFilePath(filePath, 1);
    LZW.CompressFile(filePath, outputFilePath);
}
if (userInput == "-u")
{
    string outputFilePath = GenerateOutputFilePath(filePath, 0);
    LZW.CompressFile(filePath, outputFilePath);
}

string GenerateOutputFilePath(string filePath, int key)
{
    string directory = Path.GetDirectoryName(filePath);
    string fileName = Path.GetFileNameWithoutExtension(filePath);

    if (key == 1)
    {
        return Path.Combine(directory, $"{fileName}.zipped");
    }
    else
    {
        return Path.Combine(directory, $"{fileName}");
    }
} 
