using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEvaluator.Evaluator.Expressions
{
    internal class UnaryExpression : Expression
    {
        #region Members
        private Expression e1;
        #endregion Members

        #region Constructor
        public UnaryExpression(Expression e1)
        {
            this.e1 = e1;
            _evaluable = e1.Evaluable;
        }
        #endregion Constructor

        #region Properties
        internal override int ArgumentsCount { get { return 1; } }
        internal override object Value
        {
            get
            {
                return Evaluate(new Expression[] { e1 }).Value;
            }
        }
        #endregion Properties

        #region Try Simplify
        public static Expression trySimplify(UnaryExpression e, Expression es)
        {
            if (e.Evaluable)
            {
                return new ConstExpression(e.Value);
            }
            else
            {
                es.Push(e.e1);
                return e;
            }
        }
        #endregion Try Simplify
    }
}
