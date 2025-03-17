using NUnit.Framework;
using HW3.LZW;
using System.IO;
using System.Text;

namespace HW3.LZW.Tests
{
    [TestFixture]
    public class LZWTests
    {
        private string _testFilePath;
        private string _compressedFilePath;
        private string _decompressedFilePath;

        [SetUp]
        public void Setup()
        {
            _testFilePath = Path.GetTempFileName();
            _compressedFilePath = Path.GetTempFileName();
            _decompressedFilePath = Path.GetTempFileName();
        }

        [TearDown]
        public void Cleanup()
        {
            if (File.Exists(_testFilePath))
                File.Delete(_testFilePath);
            if (File.Exists(_compressedFilePath))
                File.Delete(_compressedFilePath);
            if (File.Exists(_decompressedFilePath))
                File.Delete(_decompressedFilePath);
        }

        [Test]
        public void TestCompressAndDecompress_SimpleText()
        {
            // Arrange
            string originalText = "Hello, world!";
            File.WriteAllText(_testFilePath, originalText);

            // Act
            LZW.CompressFile(_testFilePath, _compressedFilePath);
            LZW.DecompressFile(_compressedFilePath, _decompressedFilePath);

            // Assert
            string decompressedText = File.ReadAllText(_decompressedFilePath);
            Assert.AreEqual(originalText, decompressedText);
        }

        [Test]
        public void TestCompressAndDecompress_RepeatedCharacters()
        {
            // Arrange
            string originalText = new string('a', 1000);
            byte[] originalBytes = Encoding.UTF8.GetBytes(originalText);
            File.WriteAllBytes(_testFilePath, originalBytes);

            // Act
            LZW.CompressFile(_testFilePath, _compressedFilePath);
            LZW.DecompressFile(_compressedFilePath, _decompressedFilePath);

            // Assert
            byte[] decompressedBytes = File.ReadAllBytes(_decompressedFilePath);
            string decompressedText = Encoding.UTF8.GetString(decompressedBytes);
            Assert.AreEqual(originalText, decompressedText);
        }

        [Test]
        public void TestCompressAndDecompress_LargeFile()
        {
            // Arrange
            string originalText = new string('x', 100000);
            byte[] originalBytes = Encoding.UTF8.GetBytes(originalText);
            File.WriteAllBytes(_testFilePath, originalBytes);

            // Act
            LZW.CompressFile(_testFilePath, _compressedFilePath);
            LZW.DecompressFile(_compressedFilePath, _decompressedFilePath);

            // Assert
            byte[] decompressedBytes = File.ReadAllBytes(_decompressedFilePath);
            string decompressedText = Encoding.UTF8.GetString(decompressedBytes);
            Assert.AreEqual(originalText, decompressedText);
        }
        
        [Test]
        public void TestCompressAndDecompress_EmptyFile()
        {
            // Arrange
            File.WriteAllText(_testFilePath, string.Empty); // Пустой 

            // Act
            LZW.CompressFile(_testFilePath, _compressedFilePath); 
            LZW.DecompressFile(_compressedFilePath, _decompressedFilePath); 

            // Assert
            string decompressedText = File.ReadAllText(_decompressedFilePath);
            Assert.AreEqual(string.Empty, decompressedText);
        }

        [Test]
        public void TestCompress_NonExistentFile()
        {
            string nonExistentFile = "nonexistent.txt";
            
            Assert.Throws<FileNotFoundException>(() => LZW.CompressFile(nonExistentFile, _compressedFilePath));
        }

        [Test]
        public void TestDecompress_NonExistentFile()
        {
            string nonExistentFile = "nonexistent.lzw";

            Assert.Throws<FileNotFoundException>(() => LZW.DecompressFile(nonExistentFile, _decompressedFilePath));
        }
    }
}