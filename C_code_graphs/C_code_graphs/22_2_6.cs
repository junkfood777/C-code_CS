using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _22_3_6_1

{
    public class Graph
    {
        private class Node //��������� ����� ��� ������� ������ � ����������
        {
            private int[,] array; //������� ���������
            public int this[int i, int j] //���������� ��� ��������� � ������� ���������
            {
                get
                {
                    return array[i, j];
                }
                set
                {
                    array[i, j] = value;
                }
            }
            public int Size //�������� ��� ��������� ����������� ������� ���������
            {
                get
                {
                    return array.GetLength(0);
                }
            }


            public static Node GetCopy(Node old)
            {
                int[,] newArray = new int[old.Size, old.Size];
                for (int i = 0; i < old.Size; i++)
                    for (int j = 0; j < old.Size; j++)
                        newArray[i, j] = old[i, j];
                return new Node(newArray);
            }

            private bool[] nov; //��������������� ������: ���� i-�� ������� ������� �����
                                //true, �� i-�� ������� ��� �� �����������; ���� i-��
                                //������� ����� false, �� i-�� ������� �����������
            public void NovSet() //����� �������� ��� ������� ����� ��� ��������������
            {
                for (int i = 0; i < Size; i++)
                {
                    nov[i] = true;
                }
            }
            //����������� ���������� ������, �������������� ������� ��������� �
            // ��������������� ������
            public Node(int[,] a)
            {
                array = a;
                nov = new bool[a.GetLength(0)];
            }

            public void AddNewVertex(int[] _string, int[] _column)
            {
                int[,] newArr = new int[Size + 1, Size + 1];
                for (int i = 0; i < Size; i++)
                    for (int j = 0; j < Size; j++)
                        newArr[i, j] = array[i, j];
                for (int i = 0; i < Size + 1; i++)
                    newArr[Size, i] = _string[i];
                for (int i = 0; i < Size + 1; i++)
                    newArr[i, Size] = _column[i];
                array = newArr;
            }


            //���������� ��������� ������ ����� � �������
            public void Dfs(int v)
            {
                Console.Write("{0} ", v); //������������� ������� �������
                nov[v] = false; //�������� �� ��� �������������
                                // � ������� ��������� ������������� ������ � ������� v
                for (int u = 0; u < Size; u++)
                {
                    //���� ������� v � u �������, � ���� �� ������� u �� �����������,
                    if (array[v, u] != 0 && nov[u])
                    {
                        Dfs(u); // �� ���������� ������������� �������
                    }
                }
            }
            //���������� ��������� ������ ����� � ������
            public void Bfs(int v)
            {
                Queue<int> q = new Queue<int>(); // �������������� �������
                q.Enqueue(v); //�������� ������� v � �������
                nov[v] = false; // �������� ������� v ��� �������������
                while (q.Count != 0) // ���� ������� �� �����
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
        } //����� ���������� ���c��
        private Node graph; //�������� ����, ����������� ��� ������
        public Graph(string name) //����������� �������� ������
        {
            using (StreamReader file = new StreamReader(name))
            {
                int n = int.Parse(file.ReadLine());
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
                graph = new Node(a);
            }
        }



        public Graph(int[,] a)
        {
            graph = new Node(a);
        }
        //����� ������� ������� ��������� �� ���������� ����
        public void Show()
        {
            for (int i = 0; i < graph.Size; i++)
            {
                for (int j = 0; j < graph.Size; j++)
                {
                    Console.Write("{0,4}", graph[i, j]);
                }
                Console.WriteLine();
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


     


        //��������������� �������
        public bool DoWeNeedRoad(int N)
        {
            int[,] p;
            long[,] a = graph.Floyd(out p);
            for (int i = 0; i < graph.Size - 1; i++)
                for (int j = i + 1; j < graph.Size; j++)
                    if (a[i, j] > N)
                        return true;
            return false;
        }


        //������ roads - ������ �����, ������ �����, ����� ����. ������ ������������ �� ������ ���� (�� �������� ���������)
        //ans - ���� ������� ����� �������� ����� �������� ������
        public void ThirdAnswer(int N, (int, int, int)[] roads, ref (int, int) ans)
        {
            if (!DoWeNeedRoad(N))
                throw new Exception("Length of each road is already less than N");

            //�������� ��������� �� ��������� �����
            int curRoad = roads.Length / 2;
            int newRoad1 = roads[curRoad].Item1, newRoad2 = roads[curRoad].Item2;
            graph[newRoad1, newRoad2] = roads[curRoad].Item3;
            graph[newRoad2, newRoad1] = roads[curRoad].Item3;
            if (!DoWeNeedRoad(N))
            {
                ans.Item1 = roads[curRoad].Item1;
                ans.Item2 = roads[curRoad].Item2;
                return;
            }
            graph[newRoad1, newRoad2] = 0;
            graph[newRoad2, newRoad1] = 0;


            //�������� ���������� �� ��������� �����
            curRoad = 0;
            newRoad1 = roads[curRoad].Item1;
            newRoad2 = roads[curRoad].Item2;
            graph[newRoad1, newRoad2] = roads[curRoad].Item3;
            graph[newRoad2, newRoad1] = roads[curRoad].Item3;
            if (!DoWeNeedRoad(N))
            {
                ans.Item1 = roads[curRoad].Item1;
                ans.Item2 = roads[curRoad].Item2;
                return;
            }
            graph[newRoad1, newRoad2] = 0;
            graph[newRoad2, newRoad1] = 0;


            //�������� ����� ������� �� �����
            curRoad = roads.Length - 1;
            newRoad1 = roads[curRoad].Item1;
            newRoad2 = roads[curRoad].Item2;
            graph[newRoad1, newRoad2] = roads[curRoad].Item3;
            graph[newRoad2, newRoad1] = roads[curRoad].Item3;
            if (!DoWeNeedRoad(N))
            {
                ans.Item1 = roads[curRoad].Item1;
                ans.Item2 = roads[curRoad].Item2;
                return;
            }
            graph[newRoad1, newRoad2] = 0;
            graph[newRoad2, newRoad1] = 0;


            throw new Exception("There is no way to cross the road");
        }



    
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            StreamReader FileReader = new StreamReader("input.txt");
            StreamWriter FileWriter = new StreamWriter("output.txt");
            string[] lines;

            //��������� ����
            //n - ���������� �������, N - ������������ ���������� ���������� ����� ������ ����� �������
            int n, N;
            n = Convert.ToInt32(FileReader.ReadLine());
            (string, int, int)[] cities = new (string, int, int)[n];
            for (int i = 0; i < n; i++)
            {
                lines = FileReader.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                cities[i].Item1 = lines[0];
                cities[i].Item2 = Convert.ToInt32(lines[1]);
                cities[i].Item3 = Convert.ToInt32(lines[2]);
            }
            int[,] matrix = new int[n, n];
            int freeRoads = 0;
            for (int i = 0; i < n; i++)
            {
                lines = FileReader.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < n; j++)
                {
                    if (Convert.ToInt32(lines[j]) == 1)
                        matrix[i, j] = (int)Math.Sqrt((cities[i].Item2 - cities[j].Item2) * (cities[i].Item2 - cities[j].Item2)
                                                   + (cities[i].Item3 - cities[j].Item3) * (cities[i].Item3 - cities[j].Item3));
                    else
                    {
                        matrix[i, j] = 0;
                        freeRoads = i > j ? freeRoads + 1 : freeRoads;
                    }

                }
            }
            N = Convert.ToInt32(FileReader.ReadLine());

            Graph g = new Graph(matrix);
            //������ roads - ������ �����, ������ �����, ����� ����.
            (int, int, int)[] roads = new (int, int, int)[freeRoads];
            int curRoad = 0;
            for (int i = 0; i < n - 1; i++)
                for (int j = i + 1; j < n; j++)
                    if (matrix[i, j] == 0)
                    {
                        roads[curRoad].Item1 = i;
                        roads[curRoad].Item2 = j;
                        roads[curRoad].Item3 = (int)Math.Sqrt((cities[i].Item2 - cities[j].Item2) * (cities[i].Item2 - cities[j].Item2)
                                                   + (cities[i].Item3 - cities[j].Item3) * (cities[i].Item3 - cities[j].Item3));
                        curRoad++;
                    }
            (int, int, int)[] sortedRoads = roads.OrderBy(x => x.Item3).ToArray();
            (int, int) ans = (0, 0);
            //g.Floyd(FileWriter);
            //FileWriter.WriteLine("---------------------------------------------------");
            try
            {
                g.ThirdAnswer(N, sortedRoads, ref ans);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //FileWriter.WriteLine(e.Message);
                FileReader.Close();
                FileWriter.Close();
                return;
            }
            Console.WriteLine("Have to build the road between {0} and {1}", cities[ans.Item1].Item1, cities[ans.Item2].Item1);
            //FileWriter.WriteLine("Have to build the road between {0} and {1}", cities[ans.Item1].Item1, cities[ans.Item2].Item1);
            //FileWriter.WriteLine("---------------------------------------------------");
            //g.Floyd(FileWriter);

            FileReader.Close();
            FileWriter.Close();
        }
    }
}
