using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEvaluator.Evaluator.Expressions.Arithmetic
{
    internal class DivideExpression : BinaryExpression
    {
        #region Constructor
        public DivideExpression(Expression e1, Expression e2)
            : base (e1, e2)
        {
        }
        #endregion Constructor

        #region Properties
        internal override string Name { get { return "DivideExpression"; } }
        #endregion Properties

        #region Evaluate
        internal override Expression Evaluate(Expression[] values, out bool evaluated)
        {
            evaluated = false;
            if (values[0].NumericValue.HasValue && values[1].NumericValue.HasValue)
            {
                evaluated = true;
                return new ConstExpression(values[0].NumericValue.Value / values[1].NumericValue.Value);
            }
            return null;
        }
        #endregion Evaluate
    }
}
