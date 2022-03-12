using System;
using System.IO;

namespace SerializeDeserializeGood
{
    class ListNode
    {
        public void SetNode(ListNode previous, ListNode next, ListNode random, string data)
        {
            Previous = previous;
            Next = next;
            Random = random;
            Data = data;
        }
        public ListNode Previous;
        public ListNode Next;
        public ListNode Random; // произвольный элемент внутри списка
        public string Data;
    }

    class ListRandom
    {
        public ListNode Head;
        public ListNode Tail;
        public int Count;

        public void Serialize(FileStream s)
        {
            using (StreamWriter swriter = new StreamWriter(s))
            {
                swriter.WriteLine(Bypass(this, Head, Count));
                swriter.WriteLine("~reverse~");
                swriter.WriteLine(Bypass(this, Tail, Count));
            }
        }

        public void Deserialize(FileStream s)
        {
        }

        // Рекурсивный обход усложненного списка
        public static string Bypass(ListRandom list, ListNode current, int iterationsLeft)
        {
            if (iterationsLeft == 0)
                return $"({current.Data}, _, _)";

            string leftSubtree;
            string rightSubtree;

            if (current.Next == null)
                leftSubtree = " _, ";
            else
                leftSubtree = $" {Bypass(list, current.Next, iterationsLeft-1)}, ";

            if (current.Random == null)
                rightSubtree = "_)";
            else
                rightSubtree = $"{Bypass(list, current.Random, iterationsLeft-1)})";
            
            return $"({current.Data}," + leftSubtree + rightSubtree;
        }
    }
    class Good
    {
        static void Main(string[] args)
        {
            ListRandom sample = new ListRandom();

            ListNode head = new ListNode();
            ListNode l1 = new ListNode();
            ListNode l2 = new ListNode();
            ListNode tail = new ListNode();
            ListNode r1 = new ListNode();
            ListNode r2 = new ListNode();

            head.SetNode(null, l1, r1, "head");
            l1.SetNode(head, l2, null, "l1");
            l2.SetNode(l1, tail, r2, "l2");
            r1.SetNode(head, null, null, "r1");
            r2.SetNode(l2, null, null, "r2");
            tail.SetNode(l2, null, null, "tail");

            sample.Head = head;
            sample.Tail = tail;
            sample.Count = 6;

            using (FileStream file = new FileStream("goodSol.txt", FileMode.OpenOrCreate))
            {
                sample.Serialize(file);
            }

            Console.Read();
        }
    }
}
