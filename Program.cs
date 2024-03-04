using System;
using System.Diagnostics.Metrics;
using System.Text;

namespace VigenereKey;


class VigenereKey
{
    static void Main(string[] args)
    {
        Console.WriteLine(Encrypt("Computer", "hello"));
    }

    public static string Encrypt(string plainText, string key)
    {
        StringBuilder encryptedText = new StringBuilder();
        plainText = plainText.ToUpper();
        key = key.ToUpper();
        char[,] vigenereTable = generateViegenereTable();
        string keyStream = generateKeyStream(key, plainText);
        // Generate The EncryptedText
        for (int i = 0; i < plainText.Length; i++)
        {
            encryptedText.Append(getEncryptChar(vigenereTable, plainText[i], keyStream[i]));
        }
        return encryptedText.ToString();
    }

    public static string Decrypt(string cipherText, string key)
    {
        StringBuilder decryptedText = new StringBuilder();

        cipherText = cipherText.ToUpper();
        key = key.ToUpper();
        char[,] vigenereTable = generateViegenereTable();
        string keyStream = generateKeyStream(key, cipherText);

        for (int i = 0; i < cipherText.Length; i++)
        {
            decryptedText.Append(getDecryptChar(vigenereTable, cipherText[i], keyStream[i]));
        }

        return decryptedText.ToString();
    }
    public static char getDecryptChar(char[,] vigenereTable, char cipherChar, char keyChar)
    {
        int keyPos = keyChar - 'A';
        int cipherPos = 0;
        for (int i = 0;i < 26; i++)
        {
            if (vigenereTable[keyPos,i] == cipherChar)
            {
                Console.WriteLine(vigenereTable[keyPos,i]);
                cipherPos = i;
            }
        }
        return vigenereTable[keyPos,0];
    }

    public static char getEncryptChar(char [,] vigenereTable,char planChar,char keyChar)
    {
        int xPos = planChar - 'A';
        int yPos = keyChar - 'A';
        return vigenereTable[yPos,xPos];
    }
    public static string generateKeyStream(string key,string plainText)
    {
        StringBuilder  keyStream = new StringBuilder();
        keyStream.Append(key);
        int count = 0;
        while (keyStream.Length < plainText.Length)
        {
            keyStream.Append(plainText[count % key.Length]);
            count++;
        }
        return keyStream.ToString();
    }
    
    public static char[,] generateViegenereTable()
    {
        char[,] vigenereTable = new char[26, 26];

        // Fill the table with the letters of the alphabet
        for (int i = 0; i < 26; i++)
        {
            for (int j = 0; j < 26; j++)
            {
                // Calculate the letter for each position in the table
                vigenereTable[i, j] = (char)('A' + (i + j) % 26);
            }
        }

        return vigenereTable;
    }
   
}