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
        internal override object Value { get { throw new EvaluateException("Syntax Error");  } }
        #endregion Properties

        #region Evaluate
        internal override Expression Evaluate(Expression[] values, out bool evaluted)
        {
            evaluted = false;
            if (values[0].NumericValue.HasValue)
            {
                evaluted = true;
                return new ConstExpression(Math.Log10(values[0].NumericValue.Value));
            }
            return null;
        }
        #endregion Evaluate
    }
}
