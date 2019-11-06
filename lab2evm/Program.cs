using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab34EVM
{
    class Program
    {
        public class NumSS
        {
            public bool isNegative { private set; get; }
            public string num { private set; get; }
            public int ss { private set; get; }

            public NumSS(string num, int ss)
            {
                if (num != null)
                {
                    if (num[0] == '-')
                    {
                        this.isNegative = true;
                        this.num = num.Trim('-');
                    }
                    else
                    {
                        this.isNegative = false;
                        this.num = num;
                    }
                }
                this.ss = ss;
            }

            public NumSS Translate(int toSS)
            {
                NumSS a = new NumSS(num, ss);
                if (ss != 10)
                {
                    a = a.ConvertTo10();
                }

                if (toSS != ss)
                {
                    a = a.ConvertFrom10(toSS);
                }
                return a;
            }

            public static NumSS operator +(NumSS a, NumSS b)
            {
                string res = "";

                string first = a.num;
                string second = b.num;

                int maxlen = first.Length > second.Length ? first.Length : second.Length;

                int curSS = 2;

                bool overflow = false;

                for (int i = first.Length - 1; i >= 0; i--)
                {
                    if (first[i] == '0')
                    {
                        if (second[i] == '1')
                        {
                            if (overflow)
                            {
                                res += '0';
                                overflow = true;
                            }
                            else
                            {
                                res += '1';
                                overflow = false;
                            }
                        }
                        if (second[i] == '0')
                        {
                            if (overflow)
                            {
                                res += '1';
                                overflow = false;
                            }
                            else
                            {
                                res += '0';
                                overflow = false;
                            }
                        }
                    }
                    if (first[i] == '1')
                    {
                        if (second[i] == '1')
                        {
                            if (overflow)
                            {
                                res += '1';
                                overflow = true;
                            }
                            else
                            {
                                res += '0';
                                overflow = true;
                            }
                        }
                        if (second[i] == '0')
                        {
                            if (overflow)
                            {
                                res += '0';
                                overflow = true;
                            }
                            else
                            {
                                res += '1';
                                overflow = false;
                            }
                        }
                    }
                }

                return new NumSS(res, curSS);
            }

            public bool CheckInvalid()
            {
                bool check = false;
                foreach (char c in num)
                {
                    if (ConvertCharToInt(c) == -1 || ConvertCharToInt(c) >= ss)
                    {
                        check = true;
                        break;
                    }
                }
                return check;
            }

            public static int ConvertCharToInt(char c)
            {
                int res = 0;
                switch (c)
                {
                    case '0':
                        res = 0;
                        break;
                    case '1':
                        res = 1;
                        break;
                    case '2':
                        res = 2;
                        break;
                    case '3':
                        res = 3;
                        break;
                    case '4':
                        res = 4;
                        break;
                    case '5':
                        res = 5;
                        break;
                    case '6':
                        res = 6;
                        break;
                    case '7':
                        res = 7;
                        break;
                    case '8':
                        res = 8;
                        break;
                    case '9':
                        res = 9;
                        break;
                    case 'A':
                        res = 10;
                        break;
                    case 'B':
                        res = 11;
                        break;
                    case 'C':
                        res = 12;
                        break;
                    case 'D':
                        res = 13;
                        break;
                    case 'E':
                        res = 14;
                        break;
                    case 'F':
                        res = 15;
                        break;
                    default:
                        res = -1;
                        break;
                }
                return res;
            }

            public static char ConvertIntToChar(int i)
            {
                char res;
                switch (i)
                {
                    case 0:
                        res = '0';
                        break;
                    case 1:
                        res = '1';
                        break;
                    case 2:
                        res = '2';
                        break;
                    case 3:
                        res = '3';
                        break;
                    case 4:
                        res = '4';
                        break;
                    case 5:
                        res = '5';
                        break;
                    case 6:
                        res = '6';
                        break;
                    case 7:
                        res = '7';
                        break;
                    case 8:
                        res = '8';
                        break;
                    case 9:
                        res = '9';
                        break;
                    case 10:
                        res = 'A';
                        break;
                    case 11:
                        res = 'B';
                        break;
                    case 12:
                        res = 'C';
                        break;
                    case 13:
                        res = 'D';
                        break;
                    case 14:
                        res = 'E';
                        break;
                    case 15:
                        res = 'F';
                        break;
                    default:
                        res = ' ';
                        break;
                }
                return res;
            }

            private NumSS ConvertTo10()
            {
                int res = 0;

                for (int i = num.Length - 1; i >= 0; i--)
                {
                    int pow = num.Length - 1 - i;
                    if (ConvertCharToInt(num[i]) != -1)
                    {
                        res += ConvertCharToInt(num[i]) * Convert.ToInt32(Math.Pow(ss, pow));
                    }
                    pow++;
                }
                return new NumSS(res.ToString(), 10);
            }

            private NumSS ConvertFrom10(int toSS)
            {
                Stack<int> ost = new Stack<int>();
                int temp = Convert.ToInt32(num);
                while (temp > 0)
                {
                    ost.Push(temp % toSS);
                    temp /= toSS;
                }

                string strnum = "";
                while (ost.Count() != 0)
                {
                    strnum += ConvertIntToChar(ost.Pop());
                }

                return new NumSS(strnum, toSS);
            }

            public override string ToString()
            {
                return "Число " + num + " в " + ss + " сс";
            }

        }

        public static void CrashReport()
        {
            Console.WriteLine("Я устал, босс\n");
            System.Threading.Thread.Sleep(2000);
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Что будем делать, босс? \n" +
                    "0 - перевод чисел между системами счисления \n" +
                    "1 - выполнение арифметических операций");
                switch (Console.ReadLine())
                {
                    case "0":
                        Console.Write("Введи число = ");
                        string startNum = Console.ReadLine();

                        Console.Write("Введи исходную систему счисления = ");
                        int startSS = Convert.ToInt32(Console.ReadLine());
                        if (startSS < 2 || startSS > 16)
                        {
                            CrashReport();
                            break;
                        }

                        NumSS input = new NumSS(startNum, startSS);
                        if (input.CheckInvalid())
                        {
                            CrashReport();
                            break;
                        }

                        Console.Write("Введи конечную систему счисления = ");
                        int toSS = Convert.ToInt32(Console.ReadLine());
                        if (toSS < 2 || startSS > 16)
                        {
                            CrashReport();
                            break;
                        }

                        NumSS output = input.Translate(toSS);

                        Console.WriteLine(input);
                        Console.WriteLine(output + "\n");
                        break;
                    case "1":
                        Console.Write("Введи систему счисления = ");
                        int inputSS = Convert.ToInt32(Console.ReadLine());
                        if (inputSS < 2 || inputSS > 16)
                        {
                            CrashReport();
                            break;
                        }

                        Console.Write("Введи выражение: ");
                        var parts = Console.ReadLine().Split(' ');
                        if (parts.Length != 3)
                        {
                            CrashReport();
                            break;
                        }

                        var firstNum = new NumSS(parts[0], inputSS);
                        var op = parts[1];
                        var secondNum = new NumSS(parts[2], inputSS);
                        if (firstNum.CheckInvalid() || secondNum.CheckInvalid())
                        {
                            CrashReport();
                            break;
                        }

                        firstNum = firstNum.Translate(2);
                        secondNum = secondNum.Translate(2);

                        switch (op)
                        {
                            case "+":
                                Console.WriteLine(" = " + (firstNum + secondNum)+ "\n");
                                break;
                            case "-":

                                break;
                            case "*":

                                break;
                            case "/":

                                break;
                            default:
                                CrashReport();
                                break;
                        }

                        break;
                    default:
                        Console.WriteLine("Это слишком много... здесь в голове, как будто осколки стекла...\n");
                        System.Threading.Thread.Sleep(5000);
                        break;
                }
            }
        }
    }
}
