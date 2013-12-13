using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEvaluator.Evaluator.Expressions.Arithmetic
{
    internal class Log10Expression : UnaryExpression
    {
        #region Constructor
        public Log10Expression(Expression e1)
            : base (e1)
        {
        }
        #endregion Constructor

        #region Properties
        internal override string Name { get { return "Log10Expression"; } }
        #endregion Properties

        #region Evaluate
        internal override Expression[] Evaluate(Expression[] values, out bool evaluated)
        {
            evaluated = false;
            if (values[0].NumericValue.HasValue)
            {
                evaluated = true;
                return new Expression[] { new ConstExpression(Math.Log10(values[0].NumericValue.Value)) };
            }
            return null;
        }
        #endregion Evaluate
    }
}
