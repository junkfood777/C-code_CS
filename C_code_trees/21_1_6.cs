using System;
using System.Text;
using System.IO;
using System.Linq;
using static System.Math;
using static System.Console;
using System.Collections.Generic;

namespace _21_1_6
{
    class BinaryTree
    {
        private class Node
        {
            public object inf;
            public Node left;
            public Node right;

            public Node(object data)
            {
                this.inf = data;
                left = null;
                right = null;
            }

            public static void Add(ref Node r, object nodeInf)
            {
                if (r == null)
                {
                    r = new Node(nodeInf);
                    return;
                }
                if (((IComparable)(r.inf)).CompareTo(nodeInf) > 0)
                {
                    Add(ref r.left, nodeInf);
                }
                else
                {
                    Add(ref r.right, nodeInf);
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
                    Console.WriteLine(r.inf);
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
            //private static void FindReplacement(Node toDelete, ref Node replacement)
            //{
            //    if (replacement.left == null)
            //    {
            //        toDelete.inf = replacement.inf;
            //        replacement = replacement.right;
            //    }
            //    else
            //    {
            //        FindReplacement(toDelete, ref replacement.left);
            //    }
            //}
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
                //    if (t == null)
                //    {
                //        Console.WriteLine("No item to delete");
                //        return;
                //    }

                //    if (((IComparable)(t.inf)).CompareTo(key) > 0)
                //    {
                //        Delete(ref t.left, key);
                //        return;
                //    }
                //    if (((IComparable)(t.inf)).CompareTo(key) < 0)
                //    {
                //        Delete(ref t.right, key);
                //        return;
                //    }
                //    if (t.left == null)
                //    {
                //        t = t.right;
                //        return;
                //    }
                //    if (t.right == null)
                //    {
                //        t = t.left;
                //        return;
                //    }
                //    FindReplacement(t, ref t.right);

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

                    foreach (int item in nodesData)
                    {
                        tree.Add(item);
                    }
                    tree.InOrder();

                    int counter = tree.Even_Counter();

                    WriteLine("Counter: {0} ", counter);


                }
            }
        }

    }
}
