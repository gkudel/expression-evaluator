using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionEvaluator.Evaluator.Expressions.Arithmetic
{
    internal class FloorExpression : UnaryExpression
    {
        #region Constructor
        public FloorExpression(Expression e1)
            : base(e1)
        {
        }
        #endregion Constructor

        #region Properties
        internal override string Name { get { return "FloorExpression"; } }
        #endregion Properties

        #region Evaluate
        internal override Expression Evaluate(Expression[] values, out bool evaluated)
        {
            evaluated = false;
            if (values[0].NumericValue.HasValue)
            {
                evaluated = true;
                return new ConstExpression(Math.Floor(values[0].NumericValue.Value));
            }
            return null;
        }
        #endregion Evaluate
    }
}
