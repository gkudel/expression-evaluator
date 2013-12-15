using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lambda = System.Linq.Expressions;

namespace ExpressionEvaluator.Evaluator.Expressions.Aggregation
{
    public class CountItemsExpression : UnaryExpression
    {
        #region Constructor
        internal CountItemsExpression(Expression e1)
            : base(e1)
        {
            _acceptedType = AcceptedType.Array;
        }
        #endregion Constructor

        #region Properties
        public override string Name { get { return "CountItemsExpression"; } }
        #endregion Properties

        #region Evaluate
        internal override Expression[] Evaluate(Expression[] values, out bool evaluated)
        {
            evaluated = false;
            if (values[0].ArrayValue != null)
            {
                evaluated = true;
                return new Expression[] { new ConstExpression(values[0].ArrayValue.Count(o => o != null && o.Equals(true) || o.ToString() == "true")) };
            }
            return null;
        }
        #endregion Evaluate

        #region Lambda Compilation
        internal override lambda.Expression CompileArrayBlock(lambda.ParameterExpression paramArray1, lambda.LabelTarget fault)
        {
            lambda.ParameterExpression counter = lambda.Expression.Variable(typeof(int), "counter");
            lambda.ParameterExpression countitem = lambda.Expression.Variable(typeof(int), "countitem");

            lambda.LabelTarget label = lambda.Expression.Label(typeof(object));

            lambda.BlockExpression block = lambda.Expression.Block(
                    new[] { counter, countitem },
                    lambda.Expression.Assign(counter, lambda.Expression.Constant(0)),
                    lambda.Expression.Assign(countitem, lambda.Expression.Constant(0)),
                    lambda.Expression.Loop(
                       lambda.Expression.IfThenElse(
                           lambda.Expression.LessThan(counter, lambda.Expression.ArrayLength(paramArray1)),
                           lambda.Expression.Block(
                           lambda.Expression.TryCatch(lambda.Expression.Block(
                           lambda.Expression.IfThen(lambda.Expression.IsTrue(lambda.Expression.Convert(lambda.Expression.ArrayIndex(paramArray1, counter), typeof(bool))),
                                             lambda.Expression.PostIncrementAssign(countitem)), countitem),
                                             lambda.Expression.Catch(typeof(Exception), countitem)),
                           lambda.Expression.PostIncrementAssign(counter)),
                           lambda.Expression.Break(label, lambda.Expression.Convert(countitem, typeof(object)))
                       ), label));
            return block; 
        }
        #endregion Lambda Compilation
    }
}
