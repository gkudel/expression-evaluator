using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionEvaluator.Evaluator.Expressions.Block
{
    internal class BlockConditionExpression : UnaryExpression
    {
        #region Member
        private object _value;
        private bool _conditionValue = true;
        private bool _evaluated = false;
        private bool? _blockCondtion;
        #endregion Member

        #region Constructor
        public BlockConditionExpression(Expression e1, bool conditionValue)
            : base(e1)
        {
            LocalVariable = false;
            InnerStack = true;
            _conditionValue = conditionValue;
        }
        #endregion Constructor

        #region Properties
        internal override string Name { get { return "ConditionBlockExpression"; } }
        internal override object Value
        {
            get
            {
                return _value;
            }
        }
        
        internal override bool Valuable 
        { 
            get 
            { 
                return _evaluated; 
            } 
        }

        internal bool? BlockCondtion
        {
            get
            {
                return _blockCondtion;
            }
        }
        #endregion Properties

        #region Evaluate
        internal override Expression Evaluate(Expression[] values, out bool evaluated)
        {
            evaluated = false;
            _evaluated = true;
            _blockCondtion = values[0].BoolValue;
            if (values[0] is BlockConditionExpression)
            {
                _blockCondtion = ((BlockConditionExpression)values[0]).BlockCondtion;
            }
            if (_blockCondtion.HasValue)
            {
                evaluated = true;
                if (_blockCondtion.Value == _conditionValue)
                {
                    object o = base.Evaluate(out evaluated);
                    if (evaluated)
                    {
                        _value = o;
                        return this;
                    }
                }
                else
                {
                    return values[0];
                }
            }
            return null;
        }
        #endregion Evaluate

        #region ResetEvalueted
        internal void ResetEvalueted()
        {
            _evaluated = false;
            _blockCondtion = false;
            _value = null;
        }
        #endregion ResetEvalueted

        #region Push
        internal override void Push(Expression e)
        {
            if (e is RightBraceExpression)
            {
                Expression expression = ExpressionStack.Peek();
                if (!expression.InnerStack)
                {
                    InnerStackCompleted = true;
                }
                else if (expression.InnerStack)
                {
                    if (expression.InnerStackCompleted)
                    {
                        InnerStackCompleted = true;
                    }
                    else
                    {
                        expression.Push(e);
                    }
                }
            }
            else
            {
                base.Push(e);
            }
        }
        #endregion Push
    }
}
