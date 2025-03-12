using HW3.LZW;

string input = "lololollolololalalaljdkcjsd";
var compressed = LZW.Compress(input);
Console.WriteLine("Compressed: " + string.Join(", ", compressed));

string decompressed = LZW.Decompress(compressed);
Console.WriteLine("Decompressed: " + decompressed);