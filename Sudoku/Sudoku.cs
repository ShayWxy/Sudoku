using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Node
    {
        public int col;
        public int row;
        public int[] value;
        public Node()
        {
            col = 0;row = 0;
            value = new int[10];
        }
    }
    class Sudoku
    {
        private int MAX = 9;
        private int[,] flag_num;
        private int[,] board;
        private int flag_bulid;
        private Random ran = new Random();
        private  int ws(int num)
        {
            int t = 0;
            while ((num >>= 1)!=0) t++;
            return t;
        }
        private void bulid(int k)
        {
            if (k > 80)
            {
                flag_bulid = 1;
            }
            else
            {
                int x = k / MAX, y = k % MAX;
                if (flag_num[x,y] < 1022)
                {
                    int avail = flag_num[x,y] ^ 1022;
                    List<int> q=new List<int>();
                    int temp;
                    while (avail != 0)
                    {
                        temp = avail & (avail - 1);
                        q.Add(avail - temp);
                        avail = temp;
                    }
                    int tot = 0;
                    int[] hsh=new int[10];
                    Array.Clear(hsh,0,10);
                    while (tot < q.Count)
                    {
                        int num = ran.Next(q.Count);
                        while (hsh[num] != 0) num = ran.Next(q.Count);
                        hsh[num] = 1; tot++;
                        int p = q[num];
                        board[x, y] = ws(p);
                        int[,] c = new int[MAX,MAX];
                        for (int i = 0; i < MAX; ++i)
                            for (int j = 0; j < MAX; ++j)
                                c[i, j] = flag_num[i, j];
                        for (int i = 0; i<MAX; ++i)
                        {
                            flag_num[i,y] |= p;
                            flag_num[x,i] |= p;
                        }
                        int x0 = x / 3 * 3, y0 = y / 3 * 3;
                        for (int i = x0; i<x0 + 3; ++i)
                            for (int j = y0; j<y0 + 3; ++j)
                                flag_num[i,j] |= p;
                        bulid(k + 1);
                        if (flag_bulid == 1) return;
                        for (int i = 0; i < MAX; ++i)
                            for (int j = 0; j < MAX; ++j)
                                flag_num[i, j] = c[i, j];
                    }
                }
            }
        }
        private void remove_random(int num, ref int[,] problm)
        {
            int tot = 0;
            while (tot < num)
            {

                int x = ran.Next(MAX), y = ran.Next(MAX);
                while (problm[x,y]==0 || !test(x, y,ref problm))
                {

                    x = ran.Next(MAX); y = ran.Next(MAX);
                }
                problm[x,y] = 0;
                tot++;
            }
        }
        private bool remove_space(int num, ref int[,] problm)
        {
            int tot = 0, position = ran.Next(2), x = position / MAX, y = position % MAX; ;
            while (tot < num && position < MAX * MAX)
            {
                while (position < MAX * MAX && !test(x, y,ref problm))
                {
                    position += ran.Next(2, 4);
                    x = position / MAX; y = position % MAX;
                }
                if (position < MAX * MAX)
                    problm[x, y] = 0;
                else return false;
                position += ran.Next(2, 4);
                x = position / MAX; y = position % MAX;
                tot++;
            }
            if (tot < num) return false;
            else return true;
        }
        private bool remove_sequence(int num,ref int[,] problm)
        {
            int tot = 0, position = ran.Next(2), x = position / MAX, y = position % MAX; ;
            while (tot < num && position < MAX * MAX)
            {
                while (position < MAX * MAX && !test(x, y,ref problm))
                {
                    position += 1;
                    x = position / MAX; y = position % MAX;
                }
                if (position < MAX * MAX)
                    problm[x,y] = 0;
                else return false;
                position += 1;
                x = position / MAX; y = position % MAX;
                tot++;
            }
            if (tot < num) return false;
            else return true;
        }
        private bool test(int x, int y, ref int[,] Sudoku)
        {
            int[,] temp=new int[9,9];
            for (int i = 1; i<MAX+1;++i)
                if (i!=Sudoku[x,y])
                {
                    for (int j = 0; j < MAX; ++j)
                        for (int k = 0; k < MAX; ++k)
                            temp[j,k] = Sudoku[j, k];
                    temp[x,y]=i;
                    if (backtrack(ref temp)) return false;
                }
            return true;
        }
        private bool istrue(int[,] Sudoku)
        {
            int[] hsh=new int[MAX + 1];
            for (int i = 0; i < MAX; i++)
            {
                Array.Clear(hsh, 0, MAX + 1);
                for (int j = 0; j < MAX; j++)
                    if (Sudoku[i,j]!=0 && hsh[Sudoku[i,j]]!=0)  return false;
                    else hsh[Sudoku[i,j]] = 1;
                Array.Clear(hsh, 0, MAX + 1);
                for (int j = 0; j < MAX; j++)
                    if (Sudoku[j,i]!=0 && hsh[Sudoku[j,i]]!=0) return false;
                    else hsh[Sudoku[j,i]] = 1;
                Array.Clear(hsh, 0, MAX + 1);
                int x = i / 3 * 3, y = (i - x) * 3;
                for (int x1 = x; x1 < x + 3; x1++)
                    for (int y1 = y; y1 < y + 3; y1++)
                        if (Sudoku[x1,y1]!=0 && hsh[Sudoku[x1,y1]]!=0)     
                            return false;
                        else hsh[Sudoku[x1,y1]] = 1;
            }
            return true;
        }
        private bool backtrack(ref int[,] Sudoku)
        {
            if (!istrue(Sudoku)) return false;
            int num_empty = count_num_empty(ref Sudoku);
            Node[] node_stack = new Node[num_empty];
            int i = 0;
            int j = 0;
            int k = 0;
            int flag = 0;
            for (i = 0; i < num_empty; ++i)
                node_stack[i] = new Node();
            while (num_empty!=0)
            {
                for(i = 0; i<MAX; ++i)
                {
                    for(j = 0; j<MAX; ++j)
                    {
                        if(Sudoku[i,j] == 0)
                        {
                            node_stack[k].col = i;
                            node_stack [k].row = j;
                            Sudoku[i,j] = findvalue(ref Sudoku, node_stack[k]);
                            if(Sudoku[i,j] == -1)
                            {
                                Sudoku[i,j] = 0;
                                if (k == 0)
                                {
                                    return false;
                                }
                                k--;
                                while (node_stack[k].value[0] == 0)
                                {
                                    if(k == 0)
                                    {
                                        return false;
                                    }
                                    Sudoku[node_stack[k].col,node_stack[k].row] = 0;
                                    num_empty++;
                                    k--;
                                }
                                for (flag = 1; flag<MAX+1; ++flag)
                                {
                                    if(node_stack[k].value[flag] == 0)
                                    {
                                        Sudoku[node_stack[k].col,node_stack[k].row] = flag;
                                        node_stack[k].value[flag] = 1;
                                        node_stack[k].value[0]--;
                                        break;
                                    }
                                }
                                num_empty++;
                                i = node_stack[k].col;
                                j = node_stack[k].row;
                            }
                            k++;
                            num_empty--;
                        }
                    }
                }
            }
            return true;
        }
        private int findvalue(ref int[,] Sudoku, Node node_stack)
        {
            int i = node_stack.col;
            int j = node_stack.row;
            int k = 0;
            int n = 0;
            for (k = 0; k < MAX + 1; ++k)
                node_stack.value[k] = 0;
            for (k = 1; k < MAX + 1; ++k)
            {
                node_stack.value[Sudoku[i,k - 1]] = 1;
                node_stack.value[Sudoku[k - 1,j]] = 1;
            }
            for (k = 0; k < 3; ++k)
            {
                for (n = 0; n < 3; ++n)
                {
                    node_stack.value[Sudoku[i / 3 * 3 + k,j / 3 * 3 + n]] = 1;
                }
            }
            node_stack.value[0] = 0;
            for (k = 1; k < MAX + 1; ++k)
                if (node_stack.value[k] == 0)
                    node_stack.value[0]++;
            for (k = 1; k < MAX + 1; ++k)
            {
                if (node_stack.value[k] == 0)
                {
                    node_stack.value[k] = 1;
                    node_stack.value[0]--;
                    break;
                }
            }
            if (k == MAX + 1)
                return -1;
            else
                return k;
        }
        private int count_num_empty(ref int[,] Sudoku)
        {
            int num = 0;
            for (int i = 0; i < MAX; ++i)
            {
                for (int j = 0; j < MAX; ++j)
                {
                    if (Sudoku[i,j] == 0)
                        num++;
                }
            }
            return num;
        }
        public  Sudoku(ref int[,] a)
        {
            flag_num = new int[MAX, MAX];
            board = a;
            flag_bulid = 0;
        }
        public bool question_bulid(int rnk,ref int[, ] problm)
        {
            bulid(0);
            for (int i = 0; i < MAX; ++i)
                for (int j=0;j<MAX;++j)
                problm[i, j] = board[i, j];
            int r;
            switch (rnk)
            {
                case 1: r = ran.Next(20, 31); remove_random(r,ref problm); break;
                case 2:
                    r = ran.Next(31, 55);
                    while (!remove_space(r,ref problm))
                    {
                        for (int i = 0; i < MAX; ++i)
                            for (int j = 0; j < MAX; ++j)
                                problm[i, j] = board[i, j];
                        r = ran.Next(31, 50);
                    }
                    break;
                case 3:
                    r = ran.Next(50, 55);
                    while (!remove_sequence(r,ref problm))
                    {
                        for (int i = 0; i < MAX; ++i)
                            for (int j = 0; j < MAX; ++j)
                                problm[i, j] = board[i, j];
                        r = ran.Next(50, 55);
                    }
                    break;
                default: return false;
            }
            return true;
        }
        public bool question_solve()
        {
            return backtrack(ref board);
        }
    }
}