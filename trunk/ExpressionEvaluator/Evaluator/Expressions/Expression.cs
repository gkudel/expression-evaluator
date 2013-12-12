using GcmCmnTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEvaluator.Evaluator.Expressions
{
    internal class Expression
    {
        #region Member
        protected bool _evaluable = false;
        private Lazy<ListStack> _expressionstack = new Lazy<ListStack>(() =>
        {
            return new ListStack();
        });
        #endregion Member

        #region Properties
        internal bool InnerStack { get; set; }
        internal ListStack ExpressionStack { get { return _expressionstack.Value; } }
        internal bool Evaluable { get { return _evaluable; }  }
        internal virtual bool Valuable { get { return false; } }
        internal virtual string Name { get { return "Expression"; } }
        internal virtual int ArgumentsCount { get { return 0; } }
        internal virtual object Value 
        {
            get
            {
                bool evalueted = true;
                ListStack evaluationstack = new ListStack();
                for(int j=0; j<ExpressionStack.Count; j++)
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
                        evalueted = true;
                        Expression ret = e.Evaluate(values, out evalueted);
                        if (!evalueted) break;
                        else evaluationstack.Push(ret);
                    }
                }
                if (evalueted)
                {
                    if (evaluationstack.Count == 1)
                    {
                        return evaluationstack[0].Value;
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
        }

        internal double? NumericValue
        {
            get
            {
                if (Value != null)
                {
                    double d = double.NaN;
                    if (CmnTools.TryConvertToDouble(Value, out d))
                    {
                        return d;
                    }
                }
                return null;
            }
        }

        internal string StringValue
        {
            get
            {
                if (Value != null)
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
        #endregion Properties

        #region Push
        internal void Push(Expression e)
        {
            VariableExpression v = e as VariableExpression;
            if (v != null && ExpressionStack.Count > 0)
            {
                VariableExpression expression = ExpressionStack.Where(ex => ex is VariableExpression && (ex as VariableExpression).VariableName == v.VariableName).
                                                Select(ex => ex as VariableExpression).FirstOrDefault();
                if (expression == null)
                {
                    List<int> ordinals = ExpressionStack.Where(ex => ex is VariableExpression).
                                                Select(ex => (ex as VariableExpression).Ordinal).ToList();
                    if (ordinals.Count > 0)
                    {
                        v.Ordinal = ordinals.Max() + 1;
                    }
                }
                else
                {
                    v.Ordinal = expression.Ordinal;
                }
            }
            ExpressionStack.Push(e);
        }
        #endregion Push

        #region Evaluate
        internal virtual Expression Evaluate(Expression[] values, out bool evalueted)
        {
            throw new EvaluateException("Syntax Error");
        }
        #endregion Evaluate
    }
}
