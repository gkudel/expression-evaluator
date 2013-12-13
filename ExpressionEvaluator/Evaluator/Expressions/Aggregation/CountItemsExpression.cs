using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEvaluator.Evaluator.Expressions.Aggregation
{
    internal class CountItemsExpression : UnaryExpression
    {
        #region Constructor
        public CountItemsExpression(Expression e1)
            : base(e1)
        {
        }
        #endregion Constructor

        #region Properties
        internal override string Name { get { return "CountItemsExpression"; } }
        #endregion Properties

        #region Evaluate
        internal override Expression[] Evaluate(Expression[] values, out bool evaluated)
        {
            evaluated = false;
            if (values[0].ArrayValue != null)
            {
                evaluated = true;
                return new Expression[] { new ConstExpression(values[0].ArrayValue.Count(o => o.Equals(true) || o.ToString() == "true")) };
            }
            return null;
        }
        #endregion Evaluate
    }
}
