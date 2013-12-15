using GcmCmnTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressionEvaluator.Evaluator.Expressions.Block;
using System.Collections;
using lambda = System.Linq.Expressions;
using System.Reflection;

namespace ExpressionEvaluator.Evaluator.Expressions
{
    public class Expression
    {
        #region Enum
        [Flags]
        protected enum EvaluatedType { None = 0, Numeric = 1, Integer = 2, String = 4, Bool = 8, Array = 16, DataTime = 32 };
        [Flags]
        protected enum AcceptedType { Numeric = 1, String = 2, Bool = 4, Array = 8, DataTime = 16 };
        #endregion Enum

        #region Member
        protected bool _evaluable = false;
        private Lazy<ListStack> _expressionstack;
        private Lazy<string[]> _variables;
        protected EvaluatedType _evaluatedType = EvaluatedType.None;
        protected AcceptedType _acceptedType = AcceptedType.Numeric;
        private double? _numericValue;
        private int? _integerValue;
        private string _stringValue;
        private bool? _boolValue;
        private object[] _arrayValue;
        private DateTime? _dataTimeValue;
        protected static MethodInfo _miChangeType;        
        #endregion Member

        #region Constructor
        static Expression()
        {
            _miChangeType = typeof(Convert).GetMethod("ChangeType", new[] { typeof(object), typeof(Type) });
        }

        internal Expression()
        {
            InnerStack = false;
            LocalVariable = false;
            InnerStackCompleted = false;
            _expressionstack = new Lazy<ListStack>(() =>
            {
                return new ListStack();
            });
            _variables = new Lazy<string[]>(() =>
            {
                return GetVariables();
            });
        }
        #endregion Constructor

        #region Properties
        internal bool InnerStack { get; set; }
        internal bool InnerStackCompleted { get; set; }
        internal bool LocalVariable { get; set; }
        internal ListStack ExpressionStack { get { return _expressionstack.Value; } }
        internal bool Evaluable { get { return _evaluable; }  }
        internal virtual bool Valuable { get { return false; } }
        public virtual string Name { get { return "Expression"; } }
        internal virtual int ArgumentsCount { get { return 0; } }
        internal virtual object Value { get { throw new EvaluateException("Syntax Error"); } }
        internal double? NumericValue
        {
            get
            {
                if ((_evaluatedType & EvaluatedType.Numeric) != EvaluatedType.Numeric)
                {
                    _numericValue = GetNumericValue();
                    _evaluatedType |= EvaluatedType.Numeric;
                }
                return _numericValue;
            }
        }
        
        internal int? IntegerValue
        {
            get
            {
                if ((_evaluatedType & EvaluatedType.Integer) != EvaluatedType.Integer)
                {
                    _integerValue = GetIntegerValue();
                    _evaluatedType |= EvaluatedType.Integer;
                }
                return _integerValue;
            }
        }
        
        internal string StringValue
        {
            get
            {
                if ((_evaluatedType & EvaluatedType.String) != EvaluatedType.String)
                {
                    _stringValue = GetStringValue();
                    _evaluatedType |= EvaluatedType.String;
                }
                return _stringValue;
            }
        }

        internal bool? BoolValue
        {
            get
            {
                if ((_evaluatedType & EvaluatedType.Bool) != EvaluatedType.Bool)
                {
                    _boolValue = GetBoolValue();
                    _evaluatedType |= EvaluatedType.Bool;
                }
                return _boolValue;
            }
        }

        internal object[] ArrayValue
        {
            get
            {
                if ((_evaluatedType & EvaluatedType.Array) != EvaluatedType.Array)
                {
                    _arrayValue = GetArrayValue();
                    _evaluatedType |= EvaluatedType.Array;
                }
                return _arrayValue;
            }
        }

        internal DateTime? DataTimeValue
        {
            get
            {
                if ((_evaluatedType & EvaluatedType.DataTime) != EvaluatedType.DataTime)
                {
                    _dataTimeValue = GetDateTimeValue();
                    _evaluatedType |= EvaluatedType.DataTime;
                }
                return _dataTimeValue;
            }
        }
        #endregion Properties

        #region GetValue Method
        private double? GetNumericValue()
        {
            if (Value != null)
            {
                double d = double.NaN;
                if (CmnTools.TryConvertToDouble(Value, out d))
                {
                    if (!(double.IsNaN(d) || double.IsInfinity(d)))
                    {
                        return d;
                    }
                }
            }
            return null;
        }

        private int? GetIntegerValue()
        {
            if (Value != null)
            {
                int i = -1;
                if (int.TryParse(Value.ToString(), out i))
                {
                    return i;
                }
            }
            return null; 
        }

        private string GetStringValue()
        {
            if (Value != null
                && Value.ToString() != double.NaN.ToString()
                && Value.ToString() != "Infinity"
                && Value.ToString() != double.PositiveInfinity.ToString()
                && Value.ToString() != double.NegativeInfinity.ToString())
            {
                return Value.ToString();
            }
            return null; 
        }

        private bool? GetBoolValue()
        {
            if (Value != null)
            {
                bool b = false;
                if (Boolean.TryParse(Value.ToString(), out b))
                {
                    return b;
                }
            }
            return null; 
        }

        private object[] GetArrayValue()
        {
            if (Value != null)
            {
                object[] array = Value as object[];
                if (array != null)
                {
                    return array;
                }
            }
            return null; 
        }

        private DateTime? GetDateTimeValue()
        {
            if (Value != null)
            {
                if (Value is DateTime)
                {
                    return (DateTime)Value;
                }
            }
            return null;
        }
        #endregion GetValue Method

        #region Push & Pop
        internal virtual void Push(Expression e)
        {
            if (ExpressionStack.Count == 0)
            {
                ExpressionStack.Push(e);
            }
            else
            {
                Expression ex = ExpressionStack.Peek();
                if(ex.InnerStack && !ex.InnerStackCompleted)
                {
                    ex.Push(e);
                }
                else
                {
                    ExpressionStack.Push(e);
                }
            }
        }

        internal virtual Expression Pop()
        {
            Expression ex = ExpressionStack.Peek();
            if (ex.InnerStack && !ex.InnerStackCompleted)
            {
                ex = ex.Pop();
            }
            else
            {
                ex = ExpressionStack.Pop();
            }
            return ex;
        }
        #endregion Push & Pop

        #region SetVariablesOrdinal
        internal void SetVariablesOrdinal()
        {
            List<string> ordinals = new List<string>();
            SetVariablesOrdinal(this, ordinals);
        }

        private void SetVariablesOrdinal(Expression stackExpression, List<string> ordinals)
        {           
            foreach (Expression e in stackExpression.ExpressionStack)
            {
                VariableExpression v = e as VariableExpression;
                if (v != null)
                {
                    if (!ordinals.Contains(v.VariableName))
                    {
                        ordinals.Add(v.VariableName);
                    }
                    v.Ordinal = ordinals.IndexOf(v.VariableName);
                }
                if (e.InnerStack)
                {
                    if (e.LocalVariable)
                    {
                        SetVariablesOrdinal(e, new List<string>());
                    }
                    else
                    {
                        SetVariablesOrdinal(e, ordinals);
                    }
                }
            }
        }
        #endregion SetVariablesOrdinal

        #region GetVariables
        internal string[] Variables { get { return _variables.Value; } }

        private string[] GetVariables()
        {
            List<string> vars = new List<string>();
            GetVariables(this, vars);
            return vars.ToArray();
        }

        private void GetVariables(Expression expression, List<string> vars)
        {
            foreach (Expression e in expression.ExpressionStack)
            {
                VariableExpression v = e as VariableExpression;
                if (v != null)
                {
                    if(!vars.Contains(v.VariableName))
                    {
                        vars.Add(v.VariableName);
                    }
                }
                if (e.InnerStack && !e.LocalVariable) GetVariables(e, vars);
            }
        }

        #endregion GetVariables

        #region Evaluate
        internal object Evaluate(object[] variables)
        {
            bool evaluated = false;
            return Evaluate(variables, out evaluated);
        }

        internal object Evaluate(object[] variables, out bool evaluated)
        {
            evaluated = true;
            ListStack evaluationstack = new ListStack();
            for (int j = 0; j < ExpressionStack.Count; j++)
            {
                Expression e = ExpressionStack[j];
                if (e.Valuable)
                {
                    VariableExpression v = e as VariableExpression;
                    if (v != null)
                    {
                        e = new ConstExpression(variables[v.Ordinal]);
                    }
                    evaluationstack.Push(e);
                }
                else
                {
                    Expression[] values = new Expression[e.ArgumentsCount];
                    if (ExpressionStack.Count < e.ArgumentsCount) throw new EvaluateException("Syntax Error");
                    for (int i = e.ArgumentsCount - 1; i >= 0; i--) values[i] = evaluationstack.Pop();
                    evaluated = true;
                    Expression[] ret = e.Evaluate(values, out evaluated);
                    if (evaluated && ret != null)
                    {
                        for (int i = 0; i < ret.Length; i++)
                        {
                            evaluationstack.Push(ret[i]);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (evaluated)
            {
                if (evaluationstack.Count == 1)
                {
                    if (evaluationstack[0].NumericValue.HasValue)
                    {
                        return evaluationstack[0].NumericValue.Value;
                    }
                    else if (evaluationstack[0].BoolValue.HasValue)
                    {
                        return evaluationstack[0].BoolValue.Value;
                    }
                    else if (evaluationstack[0].ArrayValue != null)
                    {
                        return evaluationstack[0].ArrayValue;
                    }
                    else if (evaluationstack[0].DataTimeValue != null)
                    {
                        return evaluationstack[0].DataTimeValue;
                    }
                    else if (evaluationstack[0].StringValue != null)
                    {
                        return evaluationstack[0].StringValue;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    throw new EvaluateException("Syntax Error");
                }
            }
            else
            {
                return null;
            } 
        }

        internal Expression[] Evaluate(Expression[] values)
        {
            bool evalueted = false;
            return Evaluate(values, out evalueted);
        }

        internal virtual Expression[] Evaluate(Expression[] values, out bool evalueted)
        {
            throw new EvaluateException("Syntax Error");
        }
        #endregion Evaluate

        #region Evaluation Tree
        internal IEnumerator<EvaluationNode> EvaluationTree()
        {
            return EvaluationTree(this, null);
        }

        private IEnumerator<EvaluationNode> EvaluationTree(Expression expression, EvaluationNode parentNode)
        {
            ListStack evaluationstack = new ListStack();
            EvaluationNode prevNode = null;
            for (int j = 0; j < expression.ExpressionStack.Count; j++)
            {
                Expression e = expression.ExpressionStack[j];
                if (e.Valuable)
                {
                    evaluationstack.Push(e);
                }
                else
                {
                    Expression[] values = new Expression[e.ArgumentsCount];
                    if (expression.ExpressionStack.Count < e.ArgumentsCount) throw new EvaluateException("Syntax Error");
                    for (int i = e.ArgumentsCount - 1; i >= 0; i--) values[i] = evaluationstack.Pop();
                    evaluationstack.Push(e);
                    prevNode = new EvaluationNode() { Node = e, Params = values, Prevoius = prevNode, ParentNode = parentNode };
                    yield return prevNode;
                    if (e.InnerStack)
                    {
                        IEnumerator<EvaluationNode> enChild = e.EvaluationTree(e, prevNode);
                        while (enChild.MoveNext())
                        {
                            EvaluationNode childNode = enChild.Current;
                            yield return childNode;
                        }
                    }
                }
            }
        }
        #endregion Evaluation Tree

        #region Lambda Compilation
        internal virtual lambda.Expression ParametrExpression(lambda.ParameterExpression values)
        {
            throw new Exception("Syntax Error");
        }

        internal lambda.BlockExpression Compile(lambda.ParameterExpression values, lambda.LabelTarget fault)
        {
            Stack<lambda.Expression> stack = new Stack<lambda.Expression>();
            for (int j = 0; j < ExpressionStack.Count; j++)
            {
                Expression e = ExpressionStack[j];
                if (e.Valuable)
                {
                    stack.Push(e.ParametrExpression(values));
                }
                else
                {
                    lambda.Expression[] param = new lambda.Expression[e.ArgumentsCount];
                    if (ExpressionStack.Count < e.ArgumentsCount) throw new EvaluateException("Syntax Error");
                    for (int i = e.ArgumentsCount - 1; i >= 0; i--) param[i] = stack.Pop();
                    stack.Push(e.Compile(param, fault));
                }
            }
            if (stack.Count == 1 && stack.Peek() is lambda.BlockExpression) return (lambda.BlockExpression)stack.Pop();
            else throw new EvaluateException("Syntax Error");
        }

        internal virtual lambda.Expression Compile(lambda.Expression[] param, lambda.LabelTarget fault)
        {
            throw new EvaluateException("Syntax Error");
        }
        #endregion Lambda Compilation
    }

    #region Evaluation Node
    public class EvaluationNode
    {
        public EvaluationNode Prevoius { get; internal set; }
        public Expression Node { get; internal set; }
        public Expression[] Params { get; internal set; }
        public EvaluationNode ParentNode { get; internal set; }
    }
    #endregion Evaluation Node
}
