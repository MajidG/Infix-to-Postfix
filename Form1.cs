using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Arbre
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<char> sym = new List<char>();
            Stack<char> ops = new Stack<char>();
            string exp = textBox1.Text;
            for (int i = 0; i < exp.Length; i++)
            {
                char c = exp[i];
                if (char.IsLetter(c))
                    sym.Add(c);
                else if (c == '(')
                    ops.Push(c);
                else if (c == ')')
                {
                    while (ops.Count > 0 && ops.Peek() != '(')
                        sym.Add(ops.Pop());
                    ops.Pop();// pop '('
                }
                else
                {
                    if (ops.Count > 0)
                        while (ops.Count > 0 && op_prec(ops.Peek()) > op_prec(c))
                            sym.Add(ops.Pop());
                    ops.Push(c);
                }
            }
            while (ops.Count > 0)
                sym.Add(ops.Pop());

            string tmp = "";
            for (int i = 0; i < sym.Count; i++)
            {
                tmp += sym[i];
            }
            textBox2.Text = tmp;
            Stack<TreeNode> tree = new Stack<TreeNode>();

            foreach (char c in sym)
            {
                var cnode = new TreeNode(c.ToString());
                if (!char.IsLetter(c))
                    cnode.Nodes.AddRange(new TreeNode [] { tree.Pop(), tree.Pop()});
                tree.Push(cnode);
            }
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add(tree.Pop());
            treeView1.ExpandAll();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private int op_prec(char op)
        {
            switch (op)
            {
                case '+':
                case '-':
                    return 1;

                case '*':
                case '/':
                    return 2;

                case '^':
                    return 3;

                default:
                    break;
            }
            return 0;
        }
    }
}