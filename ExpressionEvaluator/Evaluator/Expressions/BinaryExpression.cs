using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEvaluator.Evaluator.Expressions
{
    internal class BinaryExpression : Expression
    {
        #region Members
        private Expression e1, e2;
        #endregion Members

        #region Constructor
        public BinaryExpression(Expression e1, Expression e2)
        {
            this.e1 = e1;
            this.e2 = e2;
            _evaluable = e1.Evaluable && e2.Evaluable;
        }
        #endregion Constructor

        #region Properties
        internal override int ArgumentsCount { get { return 2; } }
        internal override object Value
        {
            get
            {
                return Evaluate(new Expression[] { e1, e2 }).Value;
            }
        }
        #endregion Properties

        #region Try Simplify
        public static Expression trySimplify(BinaryExpression e, Expression es)
        {
            if (e.Evaluable)
            {
                return new ConstExpression(e.Value);
            }
            else
            {
                es.Push(e.e1);
                es.Push(e.e2);
                return e;
            }
        }
        #endregion Try Simplify
    }
}
