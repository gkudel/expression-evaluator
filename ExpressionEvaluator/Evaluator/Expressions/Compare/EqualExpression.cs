using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionEvaluator.Evaluator.Expressions.Compare
{
    public class EqualExpression : BinaryExpression
    {
        #region Constructor
        public EqualExpression(Expression e1, Expression e2)
            : base(e1, e2)
        {
        }
        #endregion Constructor

        #region Properties
        public override string Name { get { return "EqualExpression"; } }
        #endregion Properties

        #region Evaluate
        internal override Expression[] Evaluate(Expression[] values, out bool evaluated)
        {
            evaluated = false;
            if (values[0].NumericValue.HasValue && values[1].NumericValue.HasValue)
            {
                evaluated = true;
                return new Expression[] { new ConstExpression(values[0].NumericValue.Value == values[1].NumericValue.Value) } ;
            }
            else if (values[0].BoolValue.HasValue && values[1].BoolValue.HasValue)
            {
                evaluated = true;
                return new Expression[] { new ConstExpression(values[0].BoolValue.Value == values[1].BoolValue.Value) }; 
            }
            else if (values[0].StringValue != null && values[1].StringValue != null)
            {
                evaluated = true;
                return new Expression[] { new ConstExpression(values[0].StringValue.Equals(values[1].StringValue)) } ;
            }
            else if (values[0].DataTimeValue.HasValue && values[1].DataTimeValue.HasValue)
            {
                evaluated = true;
                return new Expression[] { new ConstExpression(values[0].DataTimeValue.Value.CompareTo(values[1].DataTimeValue.Value) == 0) };
            }
            return null;
        }
        #endregion Evaluate
    }
}
