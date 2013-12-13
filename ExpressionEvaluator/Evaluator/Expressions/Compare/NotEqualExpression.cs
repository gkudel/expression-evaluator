using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionEvaluator.Evaluator.Expressions.Compare
{
    internal class NotEqualExpression : BinaryExpression
    {
        #region Constructor
        public NotEqualExpression(Expression e1, Expression e2)
            : base(e1, e2)
        {
        }
        #endregion Constructor

        #region Properties
        internal override string Name { get { return "NotEqualExpression"; } }
        #endregion Properties

        #region Evaluate
        internal override Expression Evaluate(Expression[] values, out bool evaluated)
        {
            evaluated = false;
            if (values[0].NumericValue.HasValue && values[1].NumericValue.HasValue)
            {
                evaluated = true;
                return new ConstExpression(values[0].NumericValue.Value != values[1].NumericValue.Value);
            }
            else if (values[0].BoolValue.HasValue && values[1].BoolValue.HasValue)
            {
                evaluated = true;
                return new ConstExpression(values[0].BoolValue.Value != values[1].BoolValue.Value);
            }
            else if (values[0].StringValue != null && values[1].StringValue != null)
            {
                evaluated = true;
                return new ConstExpression(!values[0].StringValue.Equals(values[1].StringValue));
            }
            return null;
        }
        #endregion Evaluate
    }
}
