using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionEvaluator.Evaluator.Expressions.Logic
{
    internal class NegativExpression : UnaryExpression
    {
        #region Constructor
        public NegativExpression(Expression e1)
            : base(e1)
        {
        }
        #endregion Constructor

        #region Properties
        internal override string Name { get { return "NegativExpression"; } }
        #endregion Properties

        #region Evaluate
        internal override Expression[] Evaluate(Expression[] values, out bool evaluated)
        {
            evaluated = false;
            if (values[0].BoolValue.HasValue)
            {
                evaluated = true;
                return new Expression[] { new ConstExpression(!values[0].BoolValue.Value) };
            }
            return null;
        }
        #endregion Evaluate
    }
}
