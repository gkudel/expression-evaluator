using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace ExpressionEvaluator.Evaluator.Expressions
{
    internal class ListStack : List<Expression>
    {
        public ListStack()
            : base()
        {
        }

        public void Push(Expression v) 
        {
            Add(v);
        }

        public Expression Pop()
        {
            Expression t = this[Count - 1];
            RemoveAt(this.Count - 1);
            return t;
        }

        public Expression Peek()
        {
            Expression t = this[Count - 1];
            return t;
        }

        public string[] GetVariables()
        {
            List<string> list = new List<string>();
            for(int i = 0; i< Count; i++)
            {
                if (this[i] is VariableExpression)
                {
                    if (!list.Contains(this[i].Name))
                    {
                        list.Add(this[i].Name);
                    }
                }
            }
            return list.ToArray();
        }
    }
}