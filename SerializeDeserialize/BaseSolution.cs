using System;
using System.IO;

namespace SerializeDeserialize
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

        public void Serialize(Stream s)
        {
            using (StreamWriter swriter = new StreamWriter(s))
            {
                ListNode currentNode = Head;

                for (int i=0; i< Count; i++)
                {
                    if (currentNode != null)
                    {
                        swriter.Write($"{currentNode.Data.ToString()};");
                        if (currentNode.Random != null)
                        {
                            swriter.Write($"{currentNode.Random.Data.ToString()}");
                        }
                        swriter.WriteLine();
                        currentNode = currentNode.Next;
                    }
                }
                swriter.Flush();
            }
        }
        public void Deserialize(Stream s)
        {
            string line;
            string[] lineData = new string[2];
            Count = 0;

            Head = new ListNode();
            ListNode currentNode = Head;
            ListNode previousNode = null;

            using (StreamReader sreader = new StreamReader(s))
            {
                while ((line = sreader.ReadLine()) != null)
                {
                    lineData[1] = null;
                    Count++;
                    // В начале итерации передаем ссылку предыдущей вершине на текущую
                    if (previousNode != null)
                        previousNode.Next = currentNode;

                    // Заполняем текущую вершину
                    lineData = line.Split(';');
                    currentNode.Data = lineData[0];
                    currentNode.Previous = previousNode;
                    currentNode.Next = new ListNode();

                    // Если у данной вершины есть ссылка на случайную, то заполняем её
                    if (lineData[1] != null)
                    {
                        currentNode.Random = new ListNode();
                        currentNode.Random.Previous = currentNode;
                        currentNode.Random.Data = lineData[1];
                    }

                    previousNode = currentNode;
                    currentNode = currentNode.Next;
                }
                Tail = currentNode;
            }
        }

    }

    class BaseSolution
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



            using (FileStream file = new FileStream("basetest.txt", FileMode.OpenOrCreate))
            {
                sample.Serialize(file);
            }

            using (FileStream file = new FileStream("basetest.txt", FileMode.OpenOrCreate))
            {
                sample.Deserialize(file);
            }

            using (FileStream file = new FileStream("basetest2.txt", FileMode.OpenOrCreate))
            {
                sample.Serialize(file);
            }
            Console.Read();

        }
    }
}
