using System;
using System.Text;
using System.IO;
using System.Linq;
using static System.Math;
using static System.Console;
using System.Collections.Generic;

namespace _21_3_4
{
    class BinaryTree
    {
        private class Node
        {
            public object inf;
            public Node left;
            public Node right;
            public int level;

            public Node(object inf)
            {
                this.inf = inf;
                left = null;
                right = null;
            }
            public Node(object inf, int level) : this(inf)
            {
                this.level = level;
            }

            public static void Add(ref Node cur, object nodeInf, int level = 1)
            {
                if (cur == null)
                {
                    cur = new Node(nodeInf, level);
                    return;
                }
                level++;
                if (((IComparable)(cur.inf)).CompareTo(nodeInf) > 0)
                {
                    Add(ref cur.left, nodeInf, level);
                }
                else
                {
                    Add(ref cur.right, nodeInf, level);
                }
            }

            public static void PreOrder(Node r)
            {
                if (r != null)
                {
                    Console.WriteLine(r.inf);
                    PreOrder(r.left);
                    PreOrder(r.right);
                }
            }

            public static void InOrder(Node r)
            {
                if (r != null)
                {
                    InOrder(r.left);
                    Console.WriteLine(r.inf + " " + r.level);
                    InOrder(r.right);
                }
            }

            public static void PostOrder(Node r)
            {
                if (r != null)
                {
                    PostOrder(r.left);
                    PostOrder(r.right);
                    Console.WriteLine(r.inf);
                }
            }

            public static void Even_Counter(Node r, ref int counter)
            {
                if (r != null)
                {
                    if (r.left == null && r.right == null
                    && (int)r.inf % 2 == 0)
                    {
                        counter++;
                    }
                    Even_Counter(r.left, ref counter);
                    Even_Counter(r.right, ref counter);
                }

            }

            public static void Counter(Node r, ref int counter)
            {
                if (r != null)
                {
                    counter++;
                    Counter(r.left, ref counter);
                    Counter(r.right, ref counter);
                }
            }
           

            public static void Is_Id_Balanced(Node r, ref bool is_b)
            {
                if (r != null && is_b == true)
                {
                    int counter_r = 0, counter_l = 0;
                    Counter(r.right, ref counter_r);
                    Counter(r.left, ref counter_l);
                    if (Abs(counter_l - counter_r) < 2)
                    {
                        Is_Id_Balanced(r.left, ref is_b);
                        Is_Id_Balanced(r.right, ref is_b);
                    }
                    else
                    {
                        is_b = false;

                    }
                }
            }
            public static void Counter_Spec(Node r, ref int counter, Node exc)
            {
                if (r != null && r.inf != exc.inf)
                {
                    counter++;
                    Counter_Spec(r.left, ref counter, exc);
                    Counter_Spec(r.right, ref counter, exc);
                }
            }
            public static void Is_Id_Balanced_Spec(Node r, ref bool is_b, Node exc)
            {
                if (r != null && is_b == true && r.inf != exc.inf)
                {
                    int counter_r = 0, counter_l = 0;
                    Counter_Spec(r.right, ref counter_r, exc);
                    Counter_Spec(r.left, ref counter_l, exc);
                    if (Abs(counter_l - counter_r) < 2)
                    {
                        Is_Id_Balanced_Spec(r.left, ref is_b, exc);
                        Is_Id_Balanced_Spec(r.right, ref is_b, exc);
                    }
                    else
                    {
                        is_b = false;

                    }
                }
            }

           

            public static void Is_Tree_Stay(Node node, Node mainNode, ref bool is_id)
            {
                if (node != null)
                {
                    if ((node.left != null) || (node.right != null))
                    {
                        Is_Tree_Stay(node.left, mainNode, ref is_id);
                        Is_Tree_Stay(node.right, mainNode, ref is_id);
                    }
                    else
                    {
                       
                        Node temp = node;
                        node = null;
                        bool is_id_b = true;
                        Is_Id_Balanced_Spec(mainNode, ref is_id_b, temp);

                        is_id = is_id_b;
                        if (is_id_b == true)
                        {
                            WriteLine("Tree becomes Ideal-Balanced, The node: {0}", temp.inf);
                        }
                        if (is_id_b == false)
                        {
                            WriteLine("Tree does not become Ideal-Balanced, The node: {0}", temp.inf);
                        }

                        node = temp;

                    }
                }

            }

            

            public static void Level_Searcher(Node cur, ref int answer, in int targetLevel)
            {
                if (cur != null)
                {

                    if (cur.level == targetLevel)
                    {
                        answer++;
                    }
                    else if (cur.level < targetLevel)
                    {
                        Level_Searcher(cur.left, ref answer, targetLevel);
                        Level_Searcher(cur.right, ref answer, targetLevel);
                    }

                }
            }
            public static void Search(Node r, object key, out Node item)
            {
                if (r == null)
                {
                    item = null;
                    return;
                }
                if (((IComparable)(r.inf)).CompareTo(key) == 0)
                {
                    item = r;
                    return;
                }

                if (((IComparable)(r.inf)).CompareTo(key) > 0)
                {
                    Search(r.left, key, out item);
                }
                else
                {
                    Search(r.right, key, out item);
                }
            }
            private static void Del(Node t, ref Node tr)
            {
                if (tr.right != null)
                {
                    Del(t, ref tr.right);
                }
                else
                {
                    t.inf = tr.inf;
                    tr = tr.left;
                }
            }

            public static void Delete(ref Node t, object key)
            {
                if (t == null)
                {
                    throw new Exception("Missing Value");
                }
                else
                {
                    if (((IComparable)(t.inf)).CompareTo(key) > 0)
                    {
                        Delete(ref t.left, key);
                    }
                    else
                    {
                        if (((IComparable)(t.inf)).CompareTo(key) > 0)
                        {
                            Delete(ref t.right, key);
                        }
                        else
                        {
                            if (t.left == null)
                            {
                                t = t.right;
                            }
                            else
                            {
                                if (t.right == null)
                                {
                                    t = t.left;
                                }
                                else
                                {
                                    Del(t, ref t.left);
                                }
                            }
                        }
                    }

                }

            }


        }
        private Node tree;

        public object Inf
        {
            set { tree.inf = value; }
            get { return tree.inf; }
        }
        public BinaryTree()
        {
            tree = null;
        }

        private BinaryTree(Node node)
        {
            tree = node;
        }
        public void Add(object nodeInf)
        {
            Node.Add(ref tree, nodeInf);
        }

        public void PreOrder()
        {
            Node.PreOrder(tree);
        }
        public void InOrder()
        {
            Node.InOrder(tree);
        }
        public void PostOrder()
        {
            Node.PostOrder(tree);
        }

        public int Even_Counter()
        {
            int counter = 0;
            Node.Even_Counter(tree, ref counter);
            return counter;
        }
        public int Counter()
        {
            int counter = 0;
            Node.Counter(tree, ref counter);
            return counter;
        }

        public bool Is_Id_Balanced()
        {
            bool is_b = true;
            Node.Is_Id_Balanced(tree, ref is_b);
            return is_b;
        }

        public bool Is_Tree_Stays()
        {
            bool is_id = true;
            Node.Is_Tree_Stay(tree, tree, ref is_id);
            return is_id;
        }
 

        public BinaryTree Search(object key)
        {
            Node toFind;
            Node.Search(tree, key, out toFind);
            BinaryTree targetNode = new BinaryTree(toFind);
            return targetNode;
        }
        public void Delete(object key)
        {
            Node.Delete(ref tree, key);
        }
        public int Level_Searcher(in int targetLevel)
        {
            int answer = 0;
            Node.Level_Searcher(tree, ref answer, in targetLevel);
            return answer;
        }
    }

    class Program
    {

        static void Main()
        {
            using (StreamWriter out1 = new StreamWriter("output.txt", true))
            {
                using (StreamReader in1 = new StreamReader("input.txt"))
                {
                    char[] splitters = { ' ', '\n', '\t', '\r' };

                    StreamReader inputFile = new StreamReader("input.txt");

                    string[] s = inputFile.ReadToEnd().Split(splitters, StringSplitOptions.RemoveEmptyEntries);

                    List<int> nodesData = new List<int>();

                    foreach (string num in s)
                    {
                        nodesData.Add(int.Parse(num));
                    }

                    BinaryTree tree = new BinaryTree();
                    BinaryTree tree_exp = new BinaryTree();

                    foreach (int item in nodesData)
                    {
                        tree.Add(item);
                        tree_exp.Add(item);
                    }
                    tree.InOrder();

               

                    WriteLine("Enter the level:");
                    WriteLine("Count of the level:{0}", tree.Level_Searcher(int.Parse(ReadLine())));


                    if (tree.Is_Id_Balanced() == true)
                    {
                        WriteLine("The tree is already Ideal Balanced");

                    }
                    else
                    {
                        WriteLine("The tree is NOT Ideal Balanced");

                        bool is_ideal_becomes = tree.Is_Tree_Stays();




                    }

                }
            }
        }

    }
}
