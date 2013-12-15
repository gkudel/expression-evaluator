using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionEvaluator.Evaluator.Expressions.Arithmetic
{
    public class MaxExpression : BinaryExpression
    {
        #region Constructor
        public MaxExpression(Expression e1, Expression e2)
            : base(e1, e2)
        {
        }
        #endregion Constructor

        #region Properties
        public override string Name { get { return "MaxExpression"; } }
        #endregion Properties

        #region Evaluate
        internal override Expression[] Evaluate(Expression[] values, out bool evaluated)
        {
            evaluated = false;
            if (values[0].NumericValue.HasValue && values[1].NumericValue.HasValue)
            {
                evaluated = true;
                return new Expression[] { new ConstExpression(Math.Max(values[0].NumericValue.Value, values[1].NumericValue.Value)) };
            }
            return null;
        }
        #endregion Evaluate
    }
}
