using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEvaluator.Evaluator.Expressions.Arithmetic
{
    internal class PlusExpression : BinaryExpression
    {
        #region Constructor
        public PlusExpression(Expression e1, Expression e2)
            : base (e1, e2)
        {
        }
        #endregion Constructor

        #region Properties
        internal override string Name { get { return "PlusExpression"; } }
        internal override object Value { get { throw new EvaluateException("Syntax Error");  } }
        #endregion Properties

        #region Evaluate
        internal override Expression Evaluate(Expression[] values, out bool evaluted)
        {
            evaluted = false;
            if (values[0].NumericValue.HasValue && values[1].NumericValue.HasValue)
            {
                evaluted = true;
                return new ConstExpression(values[0].NumericValue + values[1].NumericValue);
            }
            else if (values[0].StringValue != null && values[1].StringValue != null)
            {
                evaluted = true;
                return new ConstExpression(values[0].StringValue + values[1].StringValue);
            }
            return null;
        }
        #endregion Evaluate
    }
}
