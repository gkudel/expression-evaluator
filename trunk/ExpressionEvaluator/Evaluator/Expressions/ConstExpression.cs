using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEvaluator.Evaluator.Expressions
{
    internal class ConstExpression : Expression
    {
        #region Members
        private object _value;
        #endregion Members

        #region Constructor
        internal ConstExpression(object value)
        {
            this._value = value;
            this._evaluable = true;
        }
        #endregion Constructor

        #region Properties
        internal override string Name { get { return "ConstExpression"; } }
        internal override int ArgumentsCount { get { return 0; } }
        internal override object Value { get { return _value; } }
        internal override bool Valuable { get { return true; } }
        #endregion Properties
    }
}
