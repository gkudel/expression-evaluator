using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace ExpressionEvaluator
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random();
            ExpressionEvaluator.Evaluator.Expressions.Parser p = ExpressionEvaluator.Evaluator.Expressions.Parser.GetInstance("CountItems(ForEach([Rows]){ Check([1] > [2]) })");
            int size = 1000;
            object[] o = new object[size + 1];
            object[] results = new object[size];
            o[size] = results;
            for (int i = 0; i < size; i++)
            {
                object[] tab = new object[2];
                tab[0] = r.Next();
                tab[1] = r.Next();
                o[i] = tab;
            }

            Console.WriteLine("Evalute starting...");
            DateTime d = DateTime.Now;
            object retEval = p.Evaluate(new object[] { o });
            TimeSpan ts = DateTime.Now - d;
            Console.WriteLine(ts.Minutes + ":" + ts.Seconds + ":" + ts.Milliseconds);
            Console.WriteLine("Evalute finished");

            Console.WriteLine("Evalute starting...");
            d = DateTime.Now;
            int count = 0;
            for (int i = 0; i < size ; i++)
            {
                object[] tab = (object[])o[i];
                if (int.Parse(tab[0].ToString()) > int.Parse(tab[1].ToString())) count++;
            }
            ts = DateTime.Now - d;
            Console.WriteLine(ts.Minutes + ":" + ts.Seconds + ":" + ts.Milliseconds);
            Console.WriteLine("Evalute finished");

            p.Compile();            
            Console.WriteLine("Evalute starting...");
            d = DateTime.Now;
            object lambda = p.Evaluate(new object[] { o });
            ts = DateTime.Now - d;
            Console.WriteLine(ts.Minutes + ":" + ts.Seconds + ":" + ts.Milliseconds);
            Console.WriteLine("Evalute finished");

            Console.WriteLine("Test[" + count + "] evaluation[" + retEval + "] Lambda[" + lambda.ToString() + "]");
            Console.WriteLine("Press key");
            o = null;
            p = null;
            Console.ReadKey();
        }
    }
}
