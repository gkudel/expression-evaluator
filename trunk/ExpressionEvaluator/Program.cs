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
            Parser p = Parser.GetInstance("tan(log([A]) + sqrt([B] ^ [C]))");
            object ret = p.Evaluate(new object[] { 10, 2, 4 });
        }
    }
}
