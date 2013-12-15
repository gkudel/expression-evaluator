using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionEvaluator.Evaluator.Expressions.Arithmetic
{
    public class UnaryMinusExpression : UnaryExpression
    {
        #region Constructor
        public UnaryMinusExpression(Expression e1)
            : base(e1)
        {
        }
        #endregion Constructor

        #region Properties
        public override string Name { get { return "UnaryMinusExpression"; } }
        #endregion Properties

        #region Evaluate
        internal override Expression[] Evaluate(Expression[] values, out bool evaluated)
        {
            evaluated = false;
            if (values[0].NumericValue.HasValue)
            {
                evaluated = true;
                return new Expression[] { new ConstExpression(-values[0].NumericValue.Value) };
            }
            return null;
        }
        #endregion Evaluate
    }
}
