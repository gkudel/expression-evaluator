using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExpressionEvaluator.Evaluator.Expressions
{
    public class VariableExpression : Expression
    {
        #region Members
        private string _name;
        private int _ordinal;
        private object _value;
        #endregion Members

        #region Constructor
        internal VariableExpression(string name)
            : base()
        {
            this._name = name;
            this._ordinal = 0;
        }

        internal VariableExpression(string name, object value)
            : this(name)
        {
            this._value = value;
            this._evaluatedType = Expression.EvaluatedType.None;
        }
        #endregion Constructor

        #region Properties
        public override string Name { get { return "VariableExpression"; } }
        public string VariableName { get { return _name; } }
        internal override int ArgumentsCount { get { return 0; } }
        internal override object Value { get { return _value; } }
        internal override bool Valuable { get { return true; } }
        internal int Ordinal { get { return _ordinal; } set { _ordinal = value; } }         
        #endregion Properties

        #region Set Value
        internal void SetValue(object value)
        {
            _evaluatedType = EvaluatedType.None;
            _value = value;
        }
        #endregion Set Value
    }
}
