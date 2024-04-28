using System;
using System.IO;
using System.Collections.Generic;
using System.Numerics;
using System.Drawing;

namespace _22_3_6
{
    public class City
    {
        public string Name;
        public int x;
        public int y;
        public int con;

        public City(string n, int x, int y)
        {
            this.Name = n;
            this.x = x;
            this.y = y;
        }
        public City(int c)
        {
            this.con = c;
        }
    }

    public class Graph
    {
        private class Node
        {
            private int[,] array;
            public City[] ar_cities;
            // public int[,] ar_long;
            private int size = 0;
            //public string Name;
            //public int x;
            //public int y;

            public int this[int i, int j]
            {
                get
                {
                    return array[i, j];
                }
                set
                {
                    array[i, j] = value;
                    //size++;
                }
            }

            public int Size
            {
                get
                {
                    return size;
                    //  return array.GetLength(0);
                }
                set
                {
                    size = value;
                }
            }
            private bool[] nov;
            public void NovSet()
            {
                for (int i = 0; i < Size; i++) //foreach??????
                {
                    nov[i] = true;
                }
            }
            public void NovSet2()
            {
                for (int i = 0; i < Size; i++) //foreach??????
                {
                    nov[i] = false;
                }
            }

            public Node(int[,] a, City[] cities)
            {
                array = a;
                size = a.GetLength(0);
                nov = new bool[a.GetLength(0)];
                ar_cities = cities;
                //this.x = x;
                //this.y = y;
                //this.Name = name;
            }



            public void Dfs(int v)
            {
                Console.Write("{0} ", v);
                nov[v] = false;
                for (int u = 0; u < Size; u++)
                {

                    if (array[v, u] != 0 && nov[u])
                    {
                        Dfs(u);
                    }
                }
            }







            public bool Hamilton(int current, ref Stack<int> Path)
            {
                Path.Push(current);
                if (Path.Count == Size)
                {
                    return true;
                }
                nov[current] = true;
                for (int i = 0; i < Size; i++)
                {
                    if (array[current, i] != 0 && !nov[i])
                    {
                        if (Hamilton(i, ref Path))
                        {
                            return true;
                        }
                    }
                }
                nov[current] = false;
                Path.Pop();
                return false;
            }




            //���������� ��������� ������ ����� � ������

            public void Bfs(int v)
            {

                Queue<int> q = new Queue<int>();

                q.Enqueue(v);
                nov[v] = false;
                while (q.Count != 0)
                {
                    v = q.Dequeue(); //��������� ������� �� �������
                    Console.Write("{0} ", v); //������������� ��
                    for (int u = 0; u < Size; u++) //������� ��� �������
                    {
                        if (array[v, u] != 0 && nov[u]) // ������� � ������ � ��� �� �������������
                        {
                            q.Enqueue(u); //�������� �� � �������
                            nov[u] = false; //� �������� ��� �������������
                        }
                    }
                }
            }
            //���������� ��������� ��������
            public long[] Dijkstr(int v, out int[] p)
            {
                nov[v] = false; // �������� ������� v ��� �������������
                                //������� ������� �
                int[,] c = new int[Size, Size];
                for (int i = 0; i < Size; i++)
                {
                    for (int u = 0; u < Size; u++)
                    {
                        if (array[i, u] == 0 || i == u)
                        {
                            c[i, u] = int.MaxValue;
                        }
                        else
                        {
                            c[i, u] = array[i, u];
                        }

                    }
                }
                //������� ������� d � p
                long[] d = new long[Size];
                p = new int[Size];
                for (int u = 0; u < Size; u++)
                {
                    if (u != v)
                    {
                        d[u] = c[v, u];
                        p[u] = v;
                    }
                }
                for (int i = 0; i < Size - 1; i++) // �� ������ ���� �����
                {
                    // �������� �� ��������� V\S ����� ������� w, ��� D[w] ����������
                    long min = int.MaxValue;
                    int w = 0;
                    for (int u = 0; u < Size; u++)
                    {
                        if (nov[u] && min > d[u])
                        {
                            min = d[u];
                            w = u;
                        }
                    }
                    nov[w] = false; //�������� w � ��������� S
                                    //��� ������ ������� �� ��������� V\S ���������� ���������� ���� ��
                                    // ��������� �� ���� �������
                    for (int u = 0; u < Size; u++)
                    {
                        long distance = d[w] + c[w, u];
                        if (nov[u] && d[u] > distance)
                        {
                            d[u] = distance;
                            p[u] = w;
                        }
                    }
                }
              
                return d; //� �������� ���������� ���������� ������ ���������� ����� ���
            } //��������� ���������
              //�������������� ���� �� ������� a �� ������� b ��� ��������� ��������
            public long[] Dijkstr_Special(int v, out int[] p)
            {
                nov[v] = false; // �������� ������� v ��� �������������
                                //������� ������� �
                int[,] c = new int[Size, Size];
                Matrix_of_Distances(ref c);
                for (int i = 0; i < Size; i++)
                {
                    for (int u = 0; u < Size; u++)
                    {
                        if (array[i, u] == 0 || i == u)
                        {
                            c[i, u] = int.MaxValue;
                        }


                    }
                }
                //������� ������� d � p
                long[] d = new long[Size];
                p = new int[Size];
                for (int u = 0; u < Size; u++)
                {
                    if (u != v)
                    {
                        d[u] = c[v, u];
                        p[u] = v;
                    }
                }
                for (int i = 0; i < Size - 1; i++) // �� ������ ���� �����
                {
                    // �������� �� ��������� V\S ����� ������� w, ��� D[w] ����������
                    long min = int.MaxValue;
                    int w = 0;
                    for (int u = 0; u < Size; u++)
                    {
                        if (nov[u] && min > d[u])
                        {
                            min = d[u];
                            w = u;
                        }
                    }
                    nov[w] = false; //�������� w � ��������� S
                                    //��� ������ ������� �� ��������� V\S ���������� ���������� ���� ��
                                    // ��������� �� ���� �������
                    for (int u = 0; u < Size; u++)
                    {
                        long distance = d[w] + c[w, u];
                        if (nov[u] && d[u] > distance)
                        {
                            d[u] = distance;
                            p[u] = w;
                        }
                    }
                }
               

                for (int i = 0; i < d.Length; i++)
                {
                    if (d[i] == int.MaxValue)
                    {
                        d[i] = 0;
                    }
                }
                return d; //� �������� ���������� ���������� ������ ���������� ����� ���
            } //��������� ���������
              //�������������� ���� �� ������� a �� ������� b ��� ��������� ��������
            public void WayDijkstr(int a, int b, int[] p, ref Stack<int> items)
            {
                items.Push(b); //�������� ������� b � ����
                if (a == p[b]) //���� ���������� ��� ������� b �������� ������� �, ��
                {
                    items.Push(a); //�������� � � ���� � ��������� �������������� ����
                }
                else //����� ����� ���������� �������� ��� ���� ��� ������ ����
                { //�� ������� � �� �������, �������������� ������� b
                    WayDijkstr(a, p[b], p, ref items);

                }
            }

            //���������� ��������� ������
            public long[,] Floyd(out int[,] p)
            {
                int i, j, k;
                //������� ������� � � �
                long[,] a = new long[Size, Size];
                p = new int[Size, Size];
                for (i = 0; i < Size; i++)
                {
                    for (j = 0; j < Size; j++)
                    {
                        if (i == j)
                        {
                            a[i, j] = 0;
                        }
                        else
                        {
                            if (array[i, j] == 0)
                            {
                                a[i, j] = int.MaxValue;
                            }
                            else
                            {
                                a[i, j] = array[i, j];
                            }
                        }
                        p[i, j] = -1;
                    }
                }
                //������������ ����� ���������� �����
                for (k = 0; k < Size; k++)
                {
                    for (i = 0; i < Size; i++)
                    {
                        for (j = 0; j < Size; j++)
                        {
                            long distance = a[i, k] + a[k, j];
                            if (a[i, j] > distance)
                            {
                                a[i, j] = distance;
                                p[i, j] = k;
                            }
                        }
                    }
                }
                return a;//� �������� ���������� ���������� ������ ���������� ����� �����
            } //����� ������ ������
              //�������������� ���� �� ������� a �� ������� � ��� ��������� ������
            public void WayFloyd(int a, int b, int[,] p, ref Queue<int> items)

            {
                int k = p[a, b];
                //���� k<> -1, �� ���� ������� ����� ��� �� ���� ������ � � b, � �������� �����
                //������� k, �������
                if (k != -1)
                {
                    // ���������� ��������������� ���� ����� ��������� � � k
                    WayFloyd(a, k, p, ref items);
                    items.Enqueue(k); //�������� ������� � � �������
                                      // ���������� ��������������� ���� ����� ��������� k � b
                    WayFloyd(k, b, p, ref items);
                }
            }

            public void Delete_Vertex(ref int[,] Array, ref int size, int del_ver)
            {
                if (del_ver > size || del_ver < 0)
                {
                    Console.WriteLine("Incorrect value");
                }
                else
                {
                    for (int i = del_ver; i < size - 1; i++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            Array[i, j] = Array[i + 1, j];
                        }
                    }

                    for (int j = del_ver; j < size - 1; j++)
                    {
                        for (int i = 0; i < size; i++)
                        {
                            Array[i, j] = Array[i, j + 1];
                        }
                    }
                    size--;
                }

            }

            public void Matrix_of_Distances(ref int[,] matrix)
            {
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (i != j && array[i, j] != 0)
                        {
                            matrix[i, j] = Convert.ToInt32(Math.Round(Math.Sqrt((ar_cities[i].x - ar_cities[j].x) * (ar_cities[i].x - ar_cities[j].x) +
                                (ar_cities[i].y - ar_cities[j].y) * (ar_cities[i].y - ar_cities[j].y))));



                        }
                        else
                        {
                            matrix[i, j] = 0;
                        }
                    }
                }
            }

           
            public bool Search_Way_To_Build(int[,] matrix, int N, int A, int B)
            {
                Matrix_of_Distances(ref matrix);
                bool is_1 = false;

                if (matrix[A, B] != 0 && matrix[A, B] < 10000)
                {
                    matrix[A, B] = Convert.ToInt32(Math.Round(Math.Sqrt((ar_cities[A].x - ar_cities[B].x) * (ar_cities[A].x - ar_cities[B].x) +
                                (ar_cities[A].y - ar_cities[B].y) * (ar_cities[A].y - ar_cities[B].y))));
                }

                int[,] way_array = new int[Size, Size];

                for (int i = 0; i < Size; i++)
                {
                    long[] array = new long[Size];
                    int[] p = new int[Size];
                    array = Dijkstr_Special(i, out p);
                    for (int j = 0; j < Size; j++)
                    {
                        way_array[i, j] = Convert.ToInt32(array[j]);
                    }

                }

                bool checker = true;
                for (int i = 0; i < Size; i++)
                {
                    for (int j = 0; j < Size; j++)
                    {
                        if (way_array[i, j] >= N)
                        {
                            //Console.Write(" {0} ", way_array[i, j]);
                            checker = false;
                        }
                    }
                    //Console.WriteLine();
                }

                if (checker == false)
                {
                    is_1 = false;
                }
                else
                {
                    is_1 = checker;
                    //if (A != B)
                    //  Console.WriteLine("The way can be built between {0} and {1}", A, B);
                }
                return is_1;

            }

     





        }

        //����� ���������� ���c��
        private Node graph; //�������� ����, ����������� ��� ������
        public Graph(string name) //����������� �������� ������
        {
            using (StreamReader file = new StreamReader(name))
            {
                int n = int.Parse(file.ReadLine());
                City[] ar_1 = new City[n];
                for (int i = 0; i < n; i++)
                {
                    string line = file.ReadLine();
                    string[] mas = line.Split(' ');
                    ar_1[i] = new City(mas[0], int.Parse(mas[1]), int.Parse(mas[2]));

                }

                int[,] a = new int[n, n];
               
                for (int i = 0; i < n; i++)
                {
                    string line = file.ReadLine();
                    string[] mas = line.Split(' ');
                    for (int j = 0; j < n; j++)
                    {
                        a[i, j] = int.Parse(mas[j]);
                    }
                }
                graph = new Node(a, ar_1);
            }
        }

        //����� ������� ������� ��������� �� ���������� ����
        public void Show()
        {
            for (int i = 0; i < graph.Size; i++)
            {
                Console.WriteLine("Number: {0}, Name: {1}, X: {2}, Y: {3} ", i, graph.ar_cities[i].Name, graph.ar_cities[i].x, graph.ar_cities[i].y);
            }
            for (int i = 0; i < graph.Size; i++)
            {
                for (int j = 0; j < graph.Size; j++)
                {
                    Console.Write("{0,4}", graph[i, j]);
                }
                Console.WriteLine();
            }
        }
        public void Show_without_del_vertex(int del_ver)
        {
            if (del_ver > graph.Size || del_ver < 0)
            {
                Console.WriteLine("Incorrect value");
            }
            else
            {
                for (int i = 0; i < graph.Size; i++)
                {
                    for (int j = 0; j < graph.Size; j++)
                    {
                        if (i != del_ver && j != del_ver)
                            Console.Write("{0,4}", graph[i, j]);
                    }
                    Console.WriteLine();
                }
            }

        }
        public void Dfs(int v)
        {
            graph.NovSet();//�������� ��� ������� ����� ��� ���������������
            graph.Dfs(v); //��������� �������� ������ ����� � �������
            Console.WriteLine();
        }

        public void Bfs(int v)
        {
            graph.NovSet();//�������� ��� ������� ����� ��� ���������������
            graph.Bfs(v); //��������� �������� ������ ����� � ������
            Console.WriteLine();
        }
        public void Dijkstr(int v)
        {
            graph.NovSet();//�������� ��� ������� ����� ��� ���������������
            int[] p;
            long[] d = graph.Dijkstr(v, out p); //��������� �������� ��������
                                                //����������� ���������� ������ � ������� �� �� �����
            Console.WriteLine("����� ���������� ���� �� ������� {0} �� �������", v);
            for (int i = 0; i < graph.Size; i++)
            {
                if (i != v)
                {
                    Console.Write("{0} ����� {1}, ", i, d[i]);
                    Console.Write("���� ");
                    if (d[i] != int.MaxValue)
                    {
                        Stack<int> items = new Stack<int>();
                        graph.WayDijkstr(v, i, p, ref items);
                        while (items.Count != 0)
                        {
                            Console.Write("{0} ", items.Pop());
                        }
                    }
                }
                Console.WriteLine();
            }
        }
        public void Dijkstr_Special(int v)
        {
            graph.NovSet();//�������� ��� ������� ����� ��� ���������������
            int[] p;
            long[] d = graph.Dijkstr_Special(v, out p); //��������� �������� ��������
                                                        //����������� ���������� ������ � ������� �� �� �����
            Console.WriteLine("����� ���������� ���� �� ������� {0} �� �������", v);
            for (int i = 0; i < graph.Size; i++)
            {
                if (i != v && d[i] < 10000)
                {
                    Console.Write("{0} ����� {1}, ", i, d[i]);
                    Console.Write("���� ");
                    if (d[i] != int.MaxValue)
                    {
                        Stack<int> items = new Stack<int>();
                        graph.WayDijkstr(v, i, p, ref items);
                        while (items.Count != 0)
                        {
                            Console.Write("{0} ", items.Pop());
                        }
                    }
                }
                else if (d[i] >= 10000)
                {
                    Console.WriteLine("There is no way with vertex {0}", i);
                }
                Console.WriteLine();
            }
        }
        public void Floyd()
        {
            int[,] p;
            long[,] a = graph.Floyd(out p); //��������� �������� ������
            int i, j;
            //����������� ���������� ������ � ������� �� �� �����
            for (i = 0; i < graph.Size; i++)
            {
                for (j = 0; j < graph.Size; j++)
                {
                    if (i != j)
                    {
                        if (a[i, j] == int.MaxValue)
                        {
                            Console.WriteLine("���� �� ������� {0} � ������� {1} �� ����������", i, j);
                        }
                        else
                        {
                            Console.Write("���������� ���� �� ������� {0} �� ������� {1} ����� {2}, ", i, j, a[i, j]);

                            Console.Write(" ���� ");
                            Queue<int> items = new Queue<int>();
                            items.Enqueue(i);
                            graph.WayFloyd(i, j, p, ref items);
                            items.Enqueue(j);
                            while (items.Count != 0)
                            {
                                Console.Write("{0} ", items.Dequeue());
                            }
                            Console.WriteLine();
                        }
                    }
                }
            }
        }

        
        public void Remove(int key)
        {
            if (key >= graph.Size || key < 0)
            {
                Console.WriteLine("Incorrect value");
            }
            else
            {
                for (int i = key + 1; i < graph.Size; i++)
                {
                    for (int j = 0; j < graph.Size; j++)
                    {
                        graph[i - 1, j] = graph[i, j];
                        //graph[j, i-1] = graph[j, i];
                    }

                    for (int j = 0; j < graph.Size; j++)
                    {
                        //graph[i - 1, j] = graph[i, j];
                        graph[j, i - 1] = graph[j, i];
                    }
                }
                graph.Size--;
            }



        }



        public int[,] Matrix_dist()
        {
            int[,] matrix = new int[graph.Size, graph.Size];
            graph.Matrix_of_Distances(ref matrix);
            for (int i = 0; i < graph.Size; i++)
            {
                for (int j = 0; j < graph.Size; j++)
                {
                    Console.Write("{0, 4}", matrix[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            return matrix;
        }

        public void Search_Way_To_Build(int N)
        {
            int[,] matrix = new int[graph.Size, graph.Size];
            bool checker = false;

            for (int A = 0; A < graph.Size; A++)
            {
                for (int B = 0; B < graph.Size; B++)
                {
                    checker = graph.Search_Way_To_Build(matrix, N, A, B);
                    if (checker == true)
                    {
                        Console.WriteLine("The way between {0} and {1}", A, B);
                    }
                }
            }
            if (checker == false)
            {
                Console.WriteLine("THERE IS NO WAY");
            }
        }
       

    }
    class Program
    {
        static void Main()
        {
            Graph g = new Graph("input.txt");
            Console.WriteLine("Graph:");
            g.Show();
            Console.WriteLine();

            int[,] mat = g.Matrix_dist();
            Console.WriteLine("Enter N: ");
            int n = int.Parse(Console.ReadLine());
            g.Search_Way_To_Build(n);
            

        }
    }

}
