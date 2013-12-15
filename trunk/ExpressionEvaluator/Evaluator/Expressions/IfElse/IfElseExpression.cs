using ExpressionEvaluator.Evaluator.Expressions.Block;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEvaluator.Evaluator.Expressions.IfElse
{
    public class IfElseExpression : BlockUnaryExpression
    {
        #region Members
        private bool _condition = true;
        #endregion Members

        #region Constructor
        public IfElseExpression(Expression e1, bool condition)
            : base(e1)
        {
            this._condition = condition;
        }
        #endregion Constructor

        #region Properties
        public override string Name { get { return "IfElseExpression"; } }
        #endregion Properties

        #region Evaluate
        internal override Expression[] Evaluate(Expression[] values, out bool evaluated)
        {
            evaluated = false;
            if (values[0].BoolValue.HasValue)
            {
                evaluated = true;
                if (values[0].BoolValue.Value == _condition)
                {
                    object o = base.Evaluate(null, out evaluated);
                    if(evaluated)
                    {
                        if (_condition)
                        {
                            return new Expression[] { new ConstExpression(o), new ConstExpression(true) };
                        }
                        else
                        {
                            return new Expression[] { new ConstExpression(o) };
                        }
                    }
                }
                else
                {
                    if (_condition)
                    {
                        return new Expression[] { new ConstExpression(false) };
                    }
                }
            }
            return null;
        }
        #endregion Evaluate
    }
}
