using System;
using System.Text;

namespace ReverseString
{
    public static class ReverseString
    {
        /// <summary>
        /// Unsafe :)
        /// если вы хотите играть в действительно опасную игру, то это, безусловно, самый быстрый способ есть (около четыре раза быстрее, чем метод Array.Reverse). Это на месте обратного помощью указателей.
        /// </summary>
        /// <see cref="http://habrahabr.ru/post/58333/"/>
        /// <remarks>
        /// НЕПРАВИЛЬНО с символами юникода
        /// вместе с интернированием строк это может привести к печальным последствиями для приложения. меняет константы
        /// 
        ///  To set this compiler option in the Visual Studio development environment
        /// 1.Open the project's Properties page.
        /// 2.Click the Build property page.
        /// 3.Select the Allow Unsafe Code check box.
        /// </remarks>
        public static unsafe void ReverseFastest(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return;

            fixed (char* pText = text)
            {
                char* pBegin = pText;
                char* pEnd = pText + text.Length - 1;

                while (pBegin < pEnd)
                {
                    char temp = *pBegin;

                    *pBegin++ = *pEnd;
                    *pEnd-- = temp;
                }
            }
        }



        /// <summary>
        /// Unsafe 2
        /// </summary>
        public static unsafe string ReverseUnsafeCopy(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            //если надо менять не оригинал интернированый, а вернуть копию строки
            String copy = String.Copy(text);

            fixed (char* buf = copy)
            {
                char* pBegin = buf;
                char* pEnd = buf + copy.Length - 1;

                while (pBegin < pEnd)
                {
                    char tmp = *pBegin;

                    *pBegin = *pEnd;
                    *pEnd = tmp;

                    pBegin++; pEnd--;
                }
            }

            return copy;
        }



        /// <summary>
        /// Оптималный Array.Reverse
        /// </summary>
        public static string ReverseUsingArrayClass(string text)
        {
            if (text == null)
                return null;

            char[] charArray = text.ToCharArray();
            Array.Reverse(charArray);

            return new string(charArray);
        }



        /// <summary>
        /// Перестановка в массиве символов посредством XOR
        /// </summary>
        public static string ReverseUsingXor(string text)
        {
            char[] charArray = text.ToCharArray();

            var length = text.Length - 1;
            for (var i = 0; i < length; i++, length--)
            {
                charArray[i] ^= charArray[length];
                charArray[length] ^= charArray[i];
                charArray[i] ^= charArray[length];
            }

            return new string(charArray);
        }



        /// <summary>
        /// Копирование в массив символов. Наподобие ReverseSB
        /// </summary>
        public static string ReverseUsingCharacterBuffer(string text)
        {
            var charArray = new char[text.Length];

            var inputStrLength = text.Length - 1;
            for (var idx = 0; idx <= inputStrLength; idx++)
                charArray[idx] = text[inputStrLength - idx];

            return new string(charArray);
        }



        /// <summary>
        /// Через StringBuilder
        /// </summary>
        /// <see cref="http://stackoverflow.com/questions/228038/best-way-to-reverse-a-string"/>
        public static string ReverseSb(string text)
        {
            var builder = new StringBuilder(text.Length);

            for (var i = text.Length - 1; i >= 0; i--)
                builder.Append(text[i]); // !

            return builder.ToString();
        }



        // Есть еще относительно красивый и короткий способ с LINQ, но он не выдерживает никакой критики в плане производительности — работает в 3-3.5 раза медленнее
        // метода на базе StringBuilder. Виной тому прокачивание данных через IEnumerable и виртуальный вызов на каждую итерацию. Для желающих, ниже приведена реализация:
        // var s2 = s.Reverse();   или      return string.Concat(Enumerable.Reverse(s));

    }
}
