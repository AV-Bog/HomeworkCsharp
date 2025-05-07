// <copyright file="Program.cs" author="AV-Bog">
// under MIT License
// </copyright>

using System;
using System.IO;
using HW3.LZW;

Console.Write("Введите путь к файлу, который надо сжать или разжать: ");
string filePath = Console.ReadLine() ?? string.Empty;
while (!File.Exists(filePath))
{
    Console.WriteLine("Файл не найден. Введите корректный путь: ");
    filePath = Console.ReadLine() ?? string.Empty;
}

Console.Write("Введите ключ -c, означающий, что файл надо сжать, или -u, означающий, что надо разжать: ");
string mode = Console.ReadLine()?.ToLower() ?? string.Empty;

bool AskYesNo(string question)
{
    Console.Write(question + " [y/n] ");
    while (true)
    {
        string input = Console.ReadLine()?.ToLower() ?? string.Empty;
        if (input == "y")
        {
            return true;
        }

        if (input == "n")
        {
            return false;
        }

        Console.Write("Пожалуйста, введите 'y' или 'n': ");
    }
}

void RunReserchBWT()
{
    string testBwtFile = @"C:\Users\ASUS\RiderProjects\Homeworks\Новая папка\HW2.Trie.exe";
    if (!File.Exists(testBwtFile))
    {
        Console.WriteLine("Файл длля исследования БВТ не найден. Проверьте путь.");
        return;
    }

    string result = BWT.Research(testBwtFile);
    Console.WriteLine(result);
}

string GenerateOutputPath(string inputPath, bool isCompress)
{
    string directory = Path.GetDirectoryName(inputPath) ?? string.Empty;
    string fileName = Path.GetFileNameWithoutExtension(inputPath);
    string extension = Path.GetExtension(inputPath);

    if (isCompress)
    {
        return Path.Combine(directory, $"{fileName}{extension}.zipped");
    }
    else
    {
        if (inputPath.EndsWith(".zipped"))
        {
            return Path.Combine(directory, fileName);
        }

        return Path.Combine(directory, $"{fileName}_decompressed{extension}");
    }
}

switch (mode)
{
    case "-c":
        string compressedPath = GenerateOutputPath(filePath, true);
        Console.WriteLine("Сжатие начато...");

        bool useBwt = AskYesNo("Использовать BWT преобразование?");
        LZW.CompressFile(filePath, compressedPath, useBwt);

        double ratio = CCR.CalculateCompressionRatio(filePath, compressedPath);
        Console.WriteLine($"Сжатие завершено. Коэффициент сжатия: {ratio:F2}");
        break;

    case "-u":
        string decompressedPath = GenerateOutputPath(filePath, false);
        Console.WriteLine("Распаковка начата...");
        LZW.DecompressFile(filePath, decompressedPath);
        Console.WriteLine("Распаковка завершена.");
        break;

    default:
        Console.WriteLine("Неверный режим. Используйте -c для сжатия или -u для распаковки.");
        break;
}

void RunBWTResearch()
{
    Console.Write("Введите путь к файлу для исследования BWT: ");
    string researchFile = Console.ReadLine() ?? string.Empty;

    if (!File.Exists(researchFile))
    {
        Console.WriteLine("Файл не найден.");
        return;
    }

    string originalCompressed = Path.GetTempFileName();
    string bwtCompressed = Path.GetTempFileName();

    try
    {
        // Сжатие без BWT
        LZW.CompressFile(researchFile, originalCompressed, false);
        long originalSize = new FileInfo(originalCompressed).Length;

        // Сжатие с BWT
        LZW.CompressFile(researchFile, bwtCompressed, true);
        long bwtSize = new FileInfo(bwtCompressed).Length;

        Console.WriteLine($"Результаты исследования:\n" +
                          $"Без BWT: {originalSize} байт\n" +
                          $"С BWT: {bwtSize} байт\n" +
                          $"Разница: {originalSize - bwtSize} байт ({(double)originalSize / bwtSize:F2}x)");
    }
    finally
    {
        File.Delete(originalCompressed);
        File.Delete(bwtCompressed);
    }
}