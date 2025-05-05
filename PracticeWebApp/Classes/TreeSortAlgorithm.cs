using PracticeWebApp.Services.Interfaces;

namespace PracticeWebApp.Classes
{
    public class TreeSortAlgorithm : ISortAlgorithm
    {
        private class Node
        {
            public char Data { get; set; }
            public int Count { get; set; }  
            public Node Left { get; set; }
            public Node Right { get; set; }

            public Node(char data)
            {
                Data = data;
                Count = 1; 
                Left = null;
                Right = null;
            }
        }

        private Node root;

        public string Sort(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            root = null; 

            
            foreach (char c in input)
            {
                Insert(c);
            }

            
            List<char> sortedChars = new List<char>();
            InOrderTraversal(root, sortedChars);

            return new string(sortedChars.ToArray());
        }

        private void Insert(char data)
        {
            root = InsertRecursive(root, data);
        }

        private Node InsertRecursive(Node root, char data)
        {
            if (root == null)
            {
                return new Node(data);
            }

            if (GetSortValue(data) < GetSortValue(root.Data))
            {
                root.Left = InsertRecursive(root.Left, data);
            }
            else if (GetSortValue(data) > GetSortValue(root.Data))
            {
                root.Right = InsertRecursive(root.Right, data);
            }
            else
            {
                
                root.Count++;
            }

            return root;
        }

        private void InOrderTraversal(Node root, List<char> sortedChars)
        {
            if (root != null)
            {
                InOrderTraversal(root.Left, sortedChars);
                for (int i = 0; i < root.Count; i++)  
                {
                    sortedChars.Add(root.Data);
                }
                InOrderTraversal(root.Right, sortedChars);
            }
        }

        private int GetSortValue(char c)
        {
            return char.ToUpper(c) - 'A'; 
        }

    }
}
