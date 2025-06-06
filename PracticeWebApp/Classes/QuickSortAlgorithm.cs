using PracticeWebApp.Services.Interfaces;

namespace PracticeWebApp.Classes
{
    public class QuickSortAlgorithm : ISortAlgorithm
    {
        public string Sort(string input)
        {

            if (string.IsNullOrEmpty(input))
            {
                return input; // Обработка пустой или null строки
            }
            char[] charArray = input.ToCharArray();
            QuickSortAlphabeticalByCharArray(charArray, 0, charArray.Length - 1);
            return new string(charArray);

            
        }
        private static void QuickSortAlphabeticalByCharArray(char[] arr, int left, int right)
        {
            if (left < right)
            {
                int pivotIndex = Partition(arr, left, right);
                QuickSortAlphabeticalByCharArray(arr, left, pivotIndex - 1);
                QuickSortAlphabeticalByCharArray(arr, pivotIndex + 1, right);
            }
        }

        private static int Partition(char[] arr, int left, int right)
        {
            char pivot = arr[right];
            int i = left - 1;

            for (int j = left; j < right; j++)
            {
                if (GetAlphabeticalIndex(arr[j]) <= GetAlphabeticalIndex(pivot))
                {
                    i++;
                    Swap(arr, i, j);
                }
            }

            Swap(arr, i + 1, right);
            return i + 1;
        }

        private static void Swap(char[] arr, int a, int b)
        {
            char temp = arr[a];
            arr[a] = arr[b];
            arr[b] = temp;
        }

        private static int GetAlphabeticalIndex(char c)
        {
            c = char.ToUpper(c);

            if (!char.IsLetter(c))
            {
                return -1; // Обработка не-буквенных символов (можно изменить по желанию)
            }

            return c - 'A' + 1;
        }

       
    }
}
