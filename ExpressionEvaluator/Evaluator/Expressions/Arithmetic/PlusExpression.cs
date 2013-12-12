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
        internal override int ArgumentsCount { get { return 2; } }
        internal override object Value { get { throw new EvaluateException("Syntax Error");  } }
        #endregion Properties

        #region Evaluate
        internal override Expression Evaluate(Expression[] values, out bool evaluted)
        {
            evaluted = true;
            return new ConstExpression(double.Parse(values[0].Value.ToString()) + double.Parse(values[1].Value.ToString()));
        }
        #endregion Evaluate
    }
}
