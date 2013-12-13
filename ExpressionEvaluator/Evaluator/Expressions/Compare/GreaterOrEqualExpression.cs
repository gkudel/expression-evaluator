using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionEvaluator.Evaluator.Expressions.Compare
{
    internal class GreaterOrEqualExpression : BinaryExpression
    {
        #region Constructor
        public GreaterOrEqualExpression(Expression e1, Expression e2)
            : base(e1, e2)
        {
        }
        #endregion Constructor

        #region Properties
        internal override string Name { get { return "GreaterOrEqualExpression"; } }
        #endregion Properties

        #region Evaluate
        internal override Expression[] Evaluate(Expression[] values, out bool evaluated)
        {
            evaluated = false;
            if (values[0].NumericValue.HasValue && values[1].NumericValue.HasValue)
            {
                evaluated = true;
                return new Expression[] { new ConstExpression(values[0].NumericValue.Value >= values[1].NumericValue.Value) };
            }
            else if (values[0].DataTimeValue.HasValue && values[1].DataTimeValue.HasValue)
            {
                evaluated = true;
                return new Expression[] { new ConstExpression(values[0].DataTimeValue.Value.CompareTo(values[1].DataTimeValue.Value) >= 0) };
            }
            return null;
        }
        #endregion Evaluate
    }
}
