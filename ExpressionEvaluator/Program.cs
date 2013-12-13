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
            DateTime d = DateTime.Now;
            Parser p = Parser.GetInstance("if([A] > [B]) { if([A] > 15 ) { 'A > 15' } else { 'A > B' } } else { if([A] < 5) {'A < 5'} else {'A < B'} }");
            for (int i = 0; i < 1000000; i++)
            {
                object ret = p.Evaluate(new object[] { i % 5, i %  5 - 3 });
            }
            TimeSpan ts = DateTime.Now - d;
            Console.WriteLine(ts.Minutes + ":" + ts.Seconds + ":" + ts.Milliseconds);
            Console.ReadKey();
        }
    }
}
