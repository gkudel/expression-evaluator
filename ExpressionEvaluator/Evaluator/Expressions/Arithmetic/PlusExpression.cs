using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEvaluator.Evaluator.Expressions.Arithmetic
{
    public class PlusExpression : BinaryExpression
    {
        #region Constructor
        public PlusExpression(Expression e1, Expression e2)
            : base (e1, e2)
        {
        }
        #endregion Constructor

        #region Properties
        public override string Name { get { return "PlusExpression"; } }
        #endregion Properties

        #region Evaluate
        internal override Expression[] Evaluate(Expression[] values, out bool evaluated)
        {
            evaluated = false;
            if (values[0].NumericValue.HasValue && values[1].NumericValue.HasValue)
            {
                evaluated = true;
                return new Expression[] { new ConstExpression(values[0].NumericValue.Value + values[1].NumericValue.Value) };
            } 
            else if (values[0].StringValue != null && values[1].StringValue != null)
            {
                evaluated = true;
                return new Expression[] { new ConstExpression(values[0].StringValue + values[1].StringValue) };
            }
            return null;
        }
        #endregion Evaluate
    }
}
