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
            int size = 4000000;
            object[] o = new object[size];
            for (int i = 0; i < size; i++)
            {
                object[] tab = new object[2];
                tab[0] = r.Next();
                tab[1] = r.Next();
                o[i] = tab;
            }

            //Console.WriteLine("Evalute starting...");
            //DateTime d = DateTime.Now;            
            //object ret = p.Evaluate(new object[] { o });
            //TimeSpan ts = DateTime.Now - d;
            //Console.WriteLine(ts.Minutes + ":" + ts.Seconds + ":" + ts.Milliseconds);
            
            DateTime d = DateTime.Now;
            int count = 0;
            for (int i = 0; i < size; i++)
            {
                object[] tab = (object[])o[i];
                if (int.Parse(tab[0].ToString()) > int.Parse(tab[1].ToString())) count++;
            }
            TimeSpan ts = DateTime.Now - d;
            Console.WriteLine(ts.Minutes + ":" + ts.Seconds + ":" + ts.Milliseconds);

            Func<object[], object> func = GetLambda();
            d = DateTime.Now;
            object c = func(new object[] { o });

            ts = DateTime.Now - d;
            Console.WriteLine(ts.Minutes + ":" + ts.Seconds + ":" + ts.Milliseconds);
            Console.WriteLine("Expect result " + count + " Lambda + " + c.ToString());
            Console.WriteLine("Press key");
            Console.ReadKey();
        }


        private static Func<object[], object> GetLambda()
        {
            ParameterExpression value = Expression.Parameter(typeof(object[]), "value");
            ParameterExpression result = Expression.Variable(typeof(object), "result");
            
            BlockExpression block = Expression.Block(
                new[] { result },
                Expression.Assign(result, GetCountItem(value)),
                result
            );
            return Expression.Lambda<Func<object[], object>>(block, value).Compile();
        }

        private static BlockExpression GetCountItem(ParameterExpression value)
        {
            ParameterExpression param1 = Expression.Variable(typeof(object[]), "param");
            ParameterExpression counter = Expression.Variable(typeof(int), "counter");
            ParameterExpression countitem = Expression.Variable(typeof(int), "countitem");
            
            LabelTarget label = Expression.Label(typeof(object));

            BlockExpression block = Expression.Block(
                    new[] { param1, counter, countitem },
                    Expression.Assign(param1, Expression.Convert(GetForEach(value), typeof(object[]))),
                    Expression.Assign(counter, Expression.Constant(0)),
                    Expression.Assign(countitem, Expression.Constant(0)),
                    Expression.Loop(
                       Expression.IfThenElse(
                           Expression.LessThan(counter, Expression.ArrayLength(param1)),
                           Expression.Block(
                           Expression.IfThen(Expression.IsTrue(
                                             Expression.Convert(Expression.ArrayIndex(param1, counter), typeof(bool))),
                                             Expression.PostIncrementAssign(countitem)),
                           Expression.PostIncrementAssign(counter)),
                           Expression.Break(label, Expression.Convert(countitem, typeof(object)))
                       ),label));
           return block;
        }

        private static BlockExpression GetForEach(ParameterExpression value)
        {
            ParameterExpression param1 = Expression.Variable(typeof(object[]), "param1");
            ParameterExpression param2 = Expression.Variable(typeof(object[]), "param2");
            ParameterExpression counter = Expression.Variable(typeof(int), "counter");
            ParameterExpression ret = Expression.Variable(typeof(object[]), "ret");
            LabelTarget label = Expression.Label(typeof(object));

            BlockExpression block = Expression.Block(
                    new[] { param1, counter, ret },
                    Expression.Assign(param1, Expression.Convert(Expression.ArrayIndex(value, Expression.Constant(0)), typeof(object[]))),
                    Expression.Assign(counter, Expression.Constant(0)),
                    Expression.Assign(ret, Expression.NewArrayBounds(typeof(object), Expression.ArrayLength(param1))),
                    Expression.Loop(
                       Expression.IfThenElse(
                           Expression.LessThan(counter, Expression.ArrayLength(param1)),
                           Expression.Block(
                            new [] { param2 },
                            Expression.Assign(param2, Expression.Convert(Expression.ArrayIndex(param1, counter), typeof(object[]))),
                            Expression.IfThenElse(
                               Expression.GreaterThan(Expression.Convert(Expression.ArrayIndex(param2, Expression.Constant(0)), typeof(int)), 
                                                      Expression.Convert(Expression.ArrayIndex(param2, Expression.Constant(1)), typeof(int))),
                               Expression.Assign(Expression.ArrayAccess(ret, counter), Expression.Convert(Expression.Constant(true), typeof(object))),
                               Expression.Assign(Expression.ArrayAccess(ret, counter), Expression.Convert(Expression.Constant(false), typeof(object)))
                            ), Expression.PostIncrementAssign(counter)),
                           Expression.Break(label, Expression.Convert(ret, typeof(object)))
                       ), label));
            return block;
        }
    }
}
