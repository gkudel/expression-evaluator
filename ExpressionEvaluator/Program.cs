using ExpressionEvaluator.Evaluator.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEvaluator
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random();
            Parser p = Parser.GetInstance("if([regdt] < '2012-10-12') { CountItems(ForEach([Rows]){ Check([1] > [2]) }) } else { 'za wczesnie' }");
            object[] o = new object[100000];
            for (int i = 0; i < 100000; i++)
            {
                object[] tab = new object[2];
                tab[0] = r.Next();
                tab[1] = r.Next();
                o[i] = tab;
            }
            
            DateTime d = DateTime.Now;
            object ret = p.Evaluate(new object[] { DateTime.Now, o });
            TimeSpan ts = DateTime.Now - d;
            Console.WriteLine(ts.Minutes + ":" + ts.Seconds + ":" + ts.Milliseconds);
            Console.ReadKey();
        }
    }
}
