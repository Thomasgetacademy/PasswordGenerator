using static System.Int32;

namespace Passordgenerator
{
    internal class Program
    {
        static readonly Random Random = new Random();
        static void Main(string[] args)
        {
            var runProgram = true;
            while (runProgram)
            {
                if (args.Length != 2 || !OnlyNumbersCheck(args[0]) || !OnlyCharactersCheck(args[1]))
                {
                    ShowHelpText();
                    runProgram = false;
                }
                else
                {
                    string argLength = args[0];
                    int length;
                    TryParse(argLength, out length);

                    string pattern = args[1];
                    pattern = pattern.PadRight(length, 'l').Substring(0, length);
                    string passwordBeforeRandom = "";
                    foreach (var character in pattern)
                    {
                        passwordBeforeRandom += character == 'l' ? WriteRandomLowerCaseLetter('a', 'z') :
                                                character == 'L' ? WriteRandomUpperCaseLetter('A', 'Z') :
                                                character == 'd' ? WriteRandomDigit() :
                                                character == 's' ? WriteRandomSpecialCharacter() :
                                                "";
                    }
                    char[] passwordArray = passwordBeforeRandom.ToCharArray();
                    int n = passwordArray.Length;
                    while (n > 1)
                    {
                        n--;
                        int k = Random.Next(n + 1);
                        (passwordArray[k], passwordArray[n]) = (passwordArray[n], passwordArray[k]);
                    }
                    string password = new string(passwordArray);
                    
                    Console.WriteLine(password);
                }
                Console.ReadLine();
            }
        }

        private static bool OnlyNumbersCheck(string numberArg)
        {
            foreach (var number in numberArg)
            {
                if (!char.IsDigit(number))
                {
                    return false;
                }
            }
            return true;
        }
        private static bool OnlyCharactersCheck(string numberArg)
        {
            char[] filter = { 'l', 'L', 'd', 'D', 's', 'S' };
            foreach (var letter in numberArg)
            {
                if (!filter.Contains(letter))
                {
                    return false;
                }
            }
            return true;
        }

        private static char WriteRandomLowerCaseLetter(char min, char max)
        {
            return (char)Random.Next(min, max);
        }
        private static char WriteRandomUpperCaseLetter(char min, char max)
        {
            return (char)Random.Next(min, max);
        }
        private static int WriteRandomDigit()
        {
            return Random.Next(0, 9);
        }
        private static char WriteRandomSpecialCharacter()
        {
            string specialChars = "*^`?=)(/&¤%";
            int index = Random.Next(0, specialChars.Length);
            return specialChars[index];
        }

        /* HelpFunctions */
        private static void ShowHelpText()
        {
            Console.WriteLine(
                "  Options:\r\n  - l = lower case letter\r\n  - L = upper case letter\r\n  - d = digit\r\n  - s = special character (!\"#¤%&/(){}[]\r\nExample: PasswordGenerator 14 lLssdd\r\n         Min. 1 lower case\r\n         Min. 1 upper case\r\n         Min. 2 special characters\r\n         Min. 2 digits");
        }
    }
}