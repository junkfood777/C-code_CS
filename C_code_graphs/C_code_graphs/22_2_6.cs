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
        private class Node //вложенный класс для скрытия данных и алгоритмов
        {
            private int[,] array; //матрица смежности
            public int this[int i, int j] //индексатор для обращения к матрице смежности
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
            public int Size //свойство для получения размерности матрицы смежности
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

            private bool[] nov; //вспомогательный массив: если i-ый элемент массива равен
                                //true, то i-ая вершина еще не просмотрена; если i-ый
                                //элемент равен false, то i-ая вершина просмотрена
            public void NovSet() //метод помечает все вершины графа как непросмотреные
            {
                for (int i = 0; i < Size; i++)
                {
                    nov[i] = true;
                }
            }
            //конструктор вложенного класса, инициализирует матрицу смежности и
            // вспомогательный массив
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


            //реализация алгоритма обхода графа в глубину
            public void Dfs(int v)
            {
                Console.Write("{0} ", v); //просматриваем текущую вершину
                nov[v] = false; //помечаем ее как просмотренную
                                // в матрице смежности просматриваем строку с номером v
                for (int u = 0; u < Size; u++)
                {
                    //если вершины v и u смежные, к тому же вершина u не просмотрена,
                    if (array[v, u] != 0 && nov[u])
                    {
                        Dfs(u); // то рекурсивно просматриваем вершину
                    }
                }
            }
            //реализация алгоритма обхода графа в ширину
            public void Bfs(int v)
            {
                Queue<int> q = new Queue<int>(); // инициализируем очередь
                q.Enqueue(v); //помещаем вершину v в очередь
                nov[v] = false; // помечаем вершину v как просмотренную
                while (q.Count != 0) // пока очередь не пуста
                {
                    v = q.Dequeue(); //извлекаем вершину из очереди
                    Console.Write("{0} ", v); //просматриваем ее
                    for (int u = 0; u < Size; u++) //находим все вершины
                    {
                        if (array[v, u] != 0 && nov[u]) // смежные с данной и еще не просмотренные
                        {
                            q.Enqueue(u); //помещаем их в очередь
                            nov[u] = false; //и помечаем как просмотренные
                        }
                    }
                }
            }
            //реализация алгоритма Дейкстры
            public long[] Dijkstr(int v, out int[] p)
            {
                nov[v] = false; // помечаем вершину v как просмотренную
                                //создаем матрицу с
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
                //создаем матрицы d и p
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
                for (int i = 0; i < Size - 1; i++) // на каждом шаге цикла
                {
                    // выбираем из множества V\S такую вершину w, что D[w] минимально
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
                    nov[w] = false; //помещаем w в множество S
                                    //для каждой вершины из множества V\S определяем кратчайший путь от
                                    // источника до этой вершины
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
                return d; //в качестве результата возвращаем массив кратчайших путей для
            } //заданного источника
              //восстановление пути от вершины a до вершины b для алгоритма Дейкстры
            public void WayDijkstr(int a, int b, int[] p, ref Stack<int> items)
            {
                items.Push(b); //помещаем вершину b в стек
                if (a == p[b]) //если предыдущей для вершины b является вершина а, то
                {
                    items.Push(a); //помещаем а в стек и завершаем восстановление пути
                }
                else //иначе метод рекурсивно вызывает сам себя для поиска пути
                { //от вершины а до вершины, предшествующей вершине b
                    WayDijkstr(a, p[b], p, ref items);
                }
            }

            //реализация алгоритма Флойда
            public long[,] Floyd(out int[,] p)
            {
                int i, j, k;
                //создаем массивы р и а
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
                //осуществляем поиск кратчайших путей
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
                return a;//в качестве результата возвращаем массив кратчайших путей между
            } //всеми парами вершин
              //восстановление пути от вершины a до вершины в для алгоритма Флойда
            public void WayFloyd(int a, int b, int[,] p, ref Queue<int> items)
            {
                int k = p[a, b];
                //если k<> -1, то путь состоит более чем из двух вершин а и b, и проходит через
                //вершину k, поэтому
                if (k != -1)
                {
                    // рекурсивно восстанавливаем путь между вершинами а и k
                    WayFloyd(a, k, p, ref items);
                    items.Enqueue(k); //помещаем вершину к в очередь
                    // рекурсивно восстанавливаем путь между вершинами k и b
                    WayFloyd(k, b, p, ref items);
                }
            }
        } //конец вложенного клаcса
        private Node graph; //закрытое поле, реализующее АТД «граф»
        public Graph(string name) //конструктор внешнего класса
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
        //метод выводит матрицу смежности на консольное окно
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
            graph.NovSet();//помечаем все вершины графа как непросмотренные
            graph.Dfs(v); //запускаем алгоритм обхода графа в глубину
            Console.WriteLine();
        }

        public void Bfs(int v)
        {
            graph.NovSet();//помечаем все вершины графа как непросмотренные
            graph.Bfs(v); //запускаем алгоритм обхода графа в ширину
            Console.WriteLine();
        }
        public void Dijkstr(int v)
        {
            graph.NovSet();//помечаем все вершины графа как непросмотренные
            int[] p;
            long[] d = graph.Dijkstr(v, out p); //запускаем алгоритм Дейкстры
                                                //анализируем полученные данные и выводим их на экран
            Console.WriteLine("Длина кратчайшие пути от вершины {0} до вершины", v);
            for (int i = 0; i < graph.Size; i++)
            {
                if (i != v)
                {
                    Console.Write("{0} равна {1}, ", i, d[i]);
                    Console.Write("путь ");
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


     


        //вспомогательная функция
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


        //Массив roads - первый город, второй город, длина пути. Массив отсортирован по длинам пути (по третьему параметру)
        //ans - пара городов между которыми нужно провести дорогу
        public void ThirdAnswer(int N, (int, int, int)[] roads, ref (int, int) ans)
        {
            if (!DoWeNeedRoad(N))
                throw new Exception("Length of each road is already less than N");

            //проверим медианную из возможных дорог
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


            //проверим кратчайшую из возможных дорог
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


            //проверим самую длинную из дорог
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

            //считываем файл
            //n - количество городов, N - максимальное допустимое расстояние между каждой парой городов
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
            //Массив roads - первый город, второй город, длина пути.
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
