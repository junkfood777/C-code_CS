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

            //public static void Add(ref Node r, object nodeInf)
            //{
            //    if (r == null)
            //    {
            //        r = new Node(nodeInf);
            //        return;
            //    }
            //    if (((IComparable)(r.inf)).CompareTo(nodeInf) > 0)
            //    {
            //        Add(ref r.left, nodeInf);
            //    }
            //    else
            //    {
            //        Add(ref r.right, nodeInf);
            //    }
            //}
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
            //public static void Is_Id_Balanced(Node r, ref bool is_b, Node exp)
            //{
            //    if (r != null && is_b == true)
            //    {
            //        int counter_r = 0, counter_l = 0;
            //        //Node exp = null;

            //        Counter(r.right, ref counter_r);
            //        Counter(r.left, ref counter_l);
            //        if (Abs(counter_l - counter_r) < 2)
            //        {
            //            Is_Id_Balanced(r.left, ref is_b, exp);
            //            Is_Id_Balanced(r.right, ref is_b, exp);
            //        }
            //        else
            //        {
            //            is_b = false;
            //            exp = r;    
            //        }
            //    }
            //}

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

            //public static bool Is_Possible_Delete(Node mainNode, Node nodeToRemove)
            //{






            //    //if (node.left != null && node.right == null)
            //    //{
            //    //    // проверяем, является ли левый потомок листом
            //    //    if (node.left.Left == null && node.left.Right == null)
            //    //    {
            //    //        return true;
            //    //    }
            //    //    // проверяем, имеет ли левый потомок только одного потомка
            //    //    else if ((node.Left.Left != null && node.Left.Right == null) || (node.Left.Left == null && node.Left.Right != null))
            //    //    {
            //    //        return true;
            //    //    }
            //    //}
            //    //// проверяем, есть ли у узла только правый потомок
            //    //else if (node.Left == null && node.Right != null)
            //    //{
            //    //    // проверяем, является ли правый потомок листом
            //    //    if (node.Right.Left == null && node.Right.Right == null)
            //    //    {
            //    //        return true;
            //    //    }
            //    //    // проверяем, имеет ли правый потомок только одного потомка
            //    //    else if ((node.Right.Left != null && node.Right.Right == null) || (node.Right.Left == null && node.Right.Right != null))
            //    //    {
            //    //        return true;
            //    //    }
            //    //}
            //    //// проверяем, есть ли у узла оба потомка
            //    //else if (node.Left != null && node.Right != null)
            //    //{
            //    //    // проверяем, являются ли оба потомка листьями
            //    //    if (node.Left.Left == null && node.Left.Right == null && node.Right.Left == null && node.Right.Right == null)
            //    //    {
            //    //        return true;
            //    //    }
            //    //}

            //    //return false;



            //    //if (nodeToRemove == null) // если узел для удаления не задан, возвращаем false
            //    //{
            //    //    return false;
            //    //}

            //    //if (nodeToRemove.left == null || nodeToRemove.right == null) // если у узла есть меньше двух потомков, то можно его безопасно удалить
            //    //{
            //    //    return true;
            //    //}

            //    //Node r = nodeToRemove;



            //    // Иначе, находим самый левый узел в правом поддереве или самый правый узел в левом поддереве
            //    //Node replacementNode = nodeToRemove.right;
            //    //while (replacementNode.left != null)
            //    //{
            //    //    replacementNode = replacementNode.left;
            //    //}

            //    //// Проверяем, можно ли безопасно удалить найденный узел
            //    //if (replacementNode.right == null)
            //    //{
            //    //    return true;
            //    //}

            //    //return false;
            //}


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
                        //bool is_b = true;
                        //Is_Id_Balanced(node, ref is_b);
                        //Is_Id_Balanced(mainNode, ref is_b);
                        //if (is_b == false)
                        //{
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

            //public static void Is_Possible_Delete(Node r, ref bool is_p_d)
            //{
            //    if (r != null && is_p_d == true)
            //    {
            //        int counter_r = 0, counter_l = 0;
            //        Counter(r.right, ref counter_r);
            //        Counter(r.left, ref counter_l);
            //        if (Abs(counter_l - counter_r) < 3)
            //        {
            //            Is_Id_Balanced(r.left, ref is_p_d);
            //            Is_Id_Balanced(r.right, ref is_p_d);
            //            //value = r.inf;
            //        }
            //        else
            //        {
            //            is_p_d = false;
            //        }
            //    }
            //}

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
        //public static void Is_Tree_Stay(Node node, Node mainNode)
        //{
        //    if ((node.left != null) && (node.right != null))
        //    {
        //        Is_Tree_Stay(node.left, mainNode);
        //        Is_Tree_Stay(node.right, mainNode);
        //    }
        //    else
        //    {

        //    }
        //}
        //public bool Is_Possible_Delete()
        //{
        //    bool is_p_d = true;

        //    Node.Is_Possible_Delete(tree, ref is_p_d);
        //    return is_p_d;
        //}

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

                    //int counter = tree.Even_Counter();

                    //WriteLine("Counter: {0} ", counter);

                    //int needeed_level = int.Parse(ReadLine());
                    //int level = tree.Level_Searcher(needeed_level);
                    //WriteLine("Count of the level:{0}", level);

                    WriteLine("Enter the level:");
                    WriteLine("Count of the level:{0}", tree.Level_Searcher(int.Parse(ReadLine())));


                    if (tree.Is_Id_Balanced() == true)
                    {
                        WriteLine("The tree is already Ideal Balanced");

                        //bool is_id = tree.Is_Tree_Stays();
                    }
                    else
                    {
                        WriteLine("The tree is NOT Ideal Balanced");

                        bool is_ideal_becomes = tree.Is_Tree_Stays();



                        //tree_exp.


                        //object value = null;


                        //if (tree.Is_Possible_Delete() == true)
                        //{
                        //    WriteLine("It is possible to delete one Node to make the Tree Ideal Balanced with the {0} value");
                        //}
                        //else
                        //{
                        //    WriteLine("It is impossible to Delete");
                        //}

                    }

                }
            }
        }

    }
}
//чек изначальное дерево - если true, 