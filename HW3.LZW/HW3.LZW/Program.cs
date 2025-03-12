using System.Text;
using HW3.LZW;

byte[] input = Encoding.UTF8.GetBytes("lolololololollalalalabracadabra");

// Сжатие
var compressed = LZW.Compress(input);
Console.WriteLine("Compressed: " + string.Join(", ", compressed));

// Распаковка
byte[] decompressed = LZW.Decompress(compressed);
string result = Encoding.UTF8.GetString(decompressed);
Console.WriteLine("Decompressed: " + result);