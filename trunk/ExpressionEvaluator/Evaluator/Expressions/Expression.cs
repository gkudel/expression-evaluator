using GcmCmnTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressionEvaluator.Evaluator.Expressions.Block;
using System.Collections;

namespace ExpressionEvaluator.Evaluator.Expressions
{
    internal class Expression
    {
        #region Member
        protected bool _evaluable = false;
        private Lazy<ListStack> _expressionstack;
        private Lazy<string[]> _variables;
        #endregion Member

        #region Constructor
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
        internal virtual string Name { get { return "Expression"; } }
        internal virtual int ArgumentsCount { get { return 0; } }
        internal virtual object Value { get { throw new EvaluateException("Syntax Error"); } }
        internal double? NumericValue
        {
            get
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
        }
        
        internal int? IntegerValue
        {
            get
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
        }
        
        internal string StringValue
        {
            get
            {
                if (Value != null 
                    && Value.ToString() != double.NaN.ToString()
                    && Value.ToString() != "Infinity"
                    && Value.ToString() !=  double.PositiveInfinity.ToString()
                    && Value.ToString() !=  double.NegativeInfinity.ToString())
                {
                    return Value.ToString();
                }
                return null;
            }
        }

        internal bool? BoolValue
        {
            get
            {
                if (Value != null)
                {
                    bool b = false;
                    if(Boolean.TryParse(Value.ToString(), out b))
                    {
                        return b;
                    }
                }
                return null;
            }
        }

        internal object[] ArrayValue
        {
            get
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
        }

        internal DateTime? DataTimeValue
        {
            get
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
        }
        #endregion Properties

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

        #region SetValues
        internal void SetValues(object[] values)
        {
            SetValues(this, values);
        }

        private void SetValues(Expression stackExpression, object[] values)
        {
            foreach (Expression e in stackExpression.ExpressionStack)
            {
                VariableExpression v = e as VariableExpression;
                if (v != null)
                {
                    v.SetValue(values[v.Ordinal]);
                }
                if (e.InnerStack && !e.LocalVariable) SetValues(e, values);
            }
        }
        #endregion SetValues

        #region Evaluate
        internal object Evaluate()
        {
            bool evaluated = false;
            return Evaluate(out evaluated);
        }

        internal object Evaluate(out bool evaluated)
        {
            evaluated = true;
            ListStack evaluationstack = new ListStack();
            for (int j = 0; j < ExpressionStack.Count; j++)
            {
                Expression e = ExpressionStack[j];
                if (e.Valuable)
                {
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
    }
}
