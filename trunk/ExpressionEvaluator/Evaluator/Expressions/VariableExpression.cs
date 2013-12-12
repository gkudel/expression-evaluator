using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEvaluator.Evaluator.Expressions
{
    internal class VariableExpression : Expression
    {
        #region Members
        private object _value;
        private string _name;
        private int _ordinal;
        #endregion Members

        #region Constructor
        internal VariableExpression(string name)
            : base()
        {
            _value = null;
            _name = name;
            _ordinal = 0;
        }
        #endregion Constructor

        #region Properties
        internal override string Name { get { return "VariableExpression"; } }
        internal override int ArgumentsCount { get { return 0; } }
        internal override object Value { get { return _value; } }
        internal override bool Valuable { get { return true; } }
        internal string VariableName { get { return _name; } }
        internal int Ordinal { get { return _ordinal; } set { _ordinal = value; } }                
        #endregion Properties

        #region Methods
        internal void SetValue(object value)
        {
            _value = value;
        }
        #endregion Methods
    }
}
