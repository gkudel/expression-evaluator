using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionEvaluator.Evaluator.Expressions.Arithmetic
{
    internal class RoundExpression : BinaryExpression
    {
        #region Constructor
        public RoundExpression(Expression e1, Expression e2)
            : base(e1, e2)
        {
        }
        #endregion Constructor

        #region Properties
        internal override string Name { get { return "RoundExpression"; } }
        #endregion Properties

        #region Evaluate
        internal override Expression[] Evaluate(Expression[] values, out bool evaluated)
        {
            evaluated = false;
            if (values[0].NumericValue.HasValue && values[1].IntegerValue.HasValue)
            {
                evaluated = true;
                return new Expression[] { new ConstExpression(Math.Round(values[0].NumericValue.Value, values[1].IntegerValue.Value)) };
            }
            return null;
        }
        #endregion Evaluate
    }
}
