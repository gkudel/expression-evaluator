using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionEvaluator.Evaluator.Expressions.Arithmetic
{
    public class AsinExpression : UnaryExpression
    {
        #region Constructor
        internal AsinExpression(Expression e1)
            : base(e1)
        {
        }
        #endregion Constructor

        #region Properties
        public override string Name { get { return "AsinExpression"; } }
        #endregion Properties

        #region Evaluate
        internal override Expression[] Evaluate(Expression[] values, out bool evaluated)
        {
            evaluated = false;
            if (values[0].NumericValue.HasValue)
            {
                evaluated = true;
                return new Expression[] { new ConstExpression(Math.Asin(values[0].NumericValue.Value)) };
            }
            return null;
        }
        #endregion Evaluate
    }
}
