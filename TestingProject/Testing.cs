using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingProject
{
    class Testing
    {
        static void Main(string[] args)
        {
            string ss = "(head, (l1, (l2, (tail, _, _), (r2, _, _)), _), (r1, _, _))";
            string s = "(tail, _, _)";
            s = s.Substring(1, s.Length - 1);
            string s1 = s.Split(new char[] { ',' }, 2)[0];

            string s2 = s.Split(new char[] { ',' }, 2)[1].TrimStart(' ');

            int leftBr = 0;
            int rightBr = 0;

            StringBuilder strTmp = new StringBuilder();
            int k = 0;

            if (s2[0] == '_')
                strTmp.Append(s2[k]);
            else
                while (leftBr + rightBr == 0 || leftBr != rightBr)
                    {
                        strTmp.Append(s2[k]);
                        if (s2[k] == '(')
                            leftBr++;
                        else if (s2[k] == ')')
                            rightBr++;
                        k++;
                    }

            Console.WriteLine(s1);
            Console.WriteLine(strTmp);
            Console.WriteLine(s2.Substring(strTmp.Length+2, s2.Length - strTmp.Length - 3));

            Console.Read();
        }
    }
}
