using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using System;
using GcmCmnTools;
using System.Linq;
using ExpressionEvaluator.Evaluator.Expressions.Arithmetic;
using ExpressionEvaluator.Evaluator.Expressions.Compare;
using ExpressionEvaluator.Evaluator.Expressions.Logic;
using ExpressionEvaluator.Evaluator.Expressions.Block;

namespace ExpressionEvaluator.Evaluator.Expressions
{
    public partial class Parser 
    {
        #region Members
        private LinkedList<Token>[,] M =
                new LinkedList<Token>[
                    System.Enum.GetValues(typeof(TokenKind)).Length,
                    System.Enum.GetValues(typeof(NonterminalSymbolKind)).Length];
        private Dictionary<TokenGroup, TokenGroup[]> previoustoken = new Dictionary<TokenGroup, TokenGroup[]>();
        private Expression es = new Expression()
        {
            InnerStack = true
        };

        private bool _calculationSucced = false;
        private string _expression = string.Empty;
        private static List<Parser> _expressions;
        #endregion Members

        #region Constructor
        static Parser()
        {
            _expressions = new List<Parser>();
        }

        private Parser(String expression)
        {
            DoParse(new Scanner().Scan(expression));
            _expression = expression;
        }
        #endregion

        #region GetInstance
        public static Parser GetInstance(string expression)
        {
            Parser p = _expressions.FirstOrDefault(e => e.Expression == expression);
            if(p == null)
            {
                p = new Parser(expression);
                _expressions.Add(p);
            }
            return p;
        }
        #endregion GetInstance

        #region Methods
        public string[] GetVariables()
        {
            return new string[] { };// es.GetVariables();
        }
               
        private IEnumerator<string> Scan(IEnumerator<string> en)
        {
            while (true)
            {
                if (en.MoveNext())
                {
                    yield return en.Current.ToString();
                }
                else
                {
                    yield return "";
                }
            }
        }

        private void DoParse(IEnumerator<Token> input)
        {
            InitializeTab();

            Stack<Token> ts = new Stack<Token>();
            List<Token> tokens = new List<Token>();
            ts.Push(new BareToken(TokenKind.End));
            ts.Push(new NonterminalSymbol(NonterminalSymbolKind.C));
            input.MoveNext();
            Token c = input.Current;
            tokens.Add(c);

            Token X;
            while (true)
            {                
                X = ts.Pop();
                if (X.Kind == TokenKind.End)
                {
                    break;
                }
                else if (X.Kind == TokenKind.Translation)
                {
                    TranslationSymbol ns = (TranslationSymbol)X;
                    Expression e3 = null;
                    Expression e2 = null;
                    Expression e1 = null;
                    switch (ns.KindTR)
                    {
                        case TranslationSymbolKind.PlusOperator:
                        case TranslationSymbolKind.MinusOperator:
                        case TranslationSymbolKind.MultiplyOperator:
                        case TranslationSymbolKind.DivideOpearator:
                        case TranslationSymbolKind.PowerOperator:
                        case TranslationSymbolKind.GreaterOperator:
                        case TranslationSymbolKind.GreaterOrEqualOperator:
                        case TranslationSymbolKind.LessOperator:
                        case TranslationSymbolKind.LessOrEqualOperator:
                        case TranslationSymbolKind.EqualOperator:
                        case TranslationSymbolKind.NotEqualOperator:
                        case TranslationSymbolKind.AndOperator:
                        case TranslationSymbolKind.OrOperator:
                        case TranslationSymbolKind.MaxOperator:
                        case TranslationSymbolKind.MinOperator:
                        case TranslationSymbolKind.LogOperator:
                        case TranslationSymbolKind.RoundOperator:
                        case TranslationSymbolKind.BusulfanCalculationOperator:
                            e2 = es.Pop();
                            break;
                        case TranslationSymbolKind.AvgOperator:
                        case TranslationSymbolKind.StdOperator:
                        case TranslationSymbolKind.CVOperator:
                        case TranslationSymbolKind.AgeOfThePatientOperator:
                            e3 = e2;
                            e2 = es.Pop();
                            e1 = es.Pop();
                            break;
                    }

                    e1 = es.Pop();

                    Expression ex = null;
                    switch (ns.KindTR)                    
                    {
                        case TranslationSymbolKind.PlusOperator:
                           ex = BinaryExpression.trySimplify(
                                new PlusExpression(e1, e2), es);
                            break;
                        case TranslationSymbolKind.MinusOperator:
                           ex = BinaryExpression.trySimplify(
                                new MinusExpression(e1, e2), es);
                            break;
                        case TranslationSymbolKind.MultiplyOperator:
                           ex = BinaryExpression.trySimplify(
                                new MultiplyExpression(e1, e2), es);
                            break;
                        case TranslationSymbolKind.DivideOpearator:
                           ex = BinaryExpression.trySimplify(
                                new DivideExpression(e1, e2), es);
                            break;
                        case TranslationSymbolKind.PowerOperator:
                           ex = BinaryExpression.trySimplify(
                                new PowerExpression(e1, e2), es);
                            break;
                        case TranslationSymbolKind.SqrtOperator:
                           ex = UnaryExpression.trySimplify(
                                new SqrtExpression(e1), es);
                            break;
                        case TranslationSymbolKind.Log10Operator:
                           ex = UnaryExpression.trySimplify(
                                new Log10Expression(e1), es);
                            break;
                        case TranslationSymbolKind.SinOperator:
                           ex = UnaryExpression.trySimplify(
                                new SinExpression(e1), es);
                            break;
                        case TranslationSymbolKind.CosOperator:
                           ex = UnaryExpression.trySimplify(
                                new CosExpression(e1), es);
                            break;
                        case TranslationSymbolKind.TanOperator:
                           ex = UnaryExpression.trySimplify(
                                new TanExpression(e1), es);
                            break;
                        case TranslationSymbolKind.AbsOperator:
                            ex = UnaryExpression.trySimplify(
                                 new AbsExpression(e1), es);
                            break;
                        case TranslationSymbolKind.AcosOperator:
                            ex = UnaryExpression.trySimplify(
                                 new AcosExpression(e1), es);
                            break;
                        case TranslationSymbolKind.AsinOperator:
                            ex = UnaryExpression.trySimplify(
                                 new AsinExpression(e1), es);
                            break;
                        case TranslationSymbolKind.AtanOperator:
                            ex = UnaryExpression.trySimplify(
                                 new AtanExpression(e1), es);
                            break;
                        case TranslationSymbolKind.CeilingOperator:
                            ex = UnaryExpression.trySimplify(
                                 new CeilingExpression(e1), es);
                            break;
                        case TranslationSymbolKind.CoshOperator:
                            ex = UnaryExpression.trySimplify(
                                 new CoshExpression(e1), es);
                            break;
                        case TranslationSymbolKind.ExpOperator:
                            ex = UnaryExpression.trySimplify(
                                 new ExpExpression(e1), es);
                            break;
                        case TranslationSymbolKind.FloorOperator:
                            ex = UnaryExpression.trySimplify(
                                 new FloorExpression(e1), es);
                            break;
                        case TranslationSymbolKind.MaxOperator:
                            ex = BinaryExpression.trySimplify(
                                 new MaxExpression(e1, e2), es);
                            break;
                        case TranslationSymbolKind.MinOperator:
                            ex = BinaryExpression.trySimplify(
                                 new MinExpression(e1, e2), es);
                            break;
                        case TranslationSymbolKind.RoundOperator:
                            ex = BinaryExpression.trySimplify(
                                 new RoundExpression(e1, e2), es);
                            break;
                        case TranslationSymbolKind.LogOperator:
                            ex = BinaryExpression.trySimplify(
                                 new LogExpression(e1, e2), es);
                            break;
                        case TranslationSymbolKind.SinhOperator:
                            ex = UnaryExpression.trySimplify(
                                 new SinhExpression(e1), es);
                            break;
                        case TranslationSymbolKind.TanhOperator:
                            ex = UnaryExpression.trySimplify(
                                 new TanhExpression(e1), es);
                            break;
                        case TranslationSymbolKind.TruncateOperator:
                            ex = UnaryExpression.trySimplify(
                                 new TruncateExpression(e1), es);
                            break;
                        case TranslationSymbolKind.UnaryMinusOperator:
                            ex = UnaryExpression.trySimplify(
                                 new UnaryMinusExpression(e1), es);
                            break;
                        case TranslationSymbolKind.GreaterOperator:
                            ex = BinaryExpression.trySimplify(
                                 new GreterExpression(e1, e2), es);
                            break;
                        case TranslationSymbolKind.LessOperator:
                            ex = BinaryExpression.trySimplify(
                                 new LessExpression(e1, e2), es);
                            break;
                        case TranslationSymbolKind.LessOrEqualOperator:
                            ex = BinaryExpression.trySimplify(
                                 new LessOrEqualExpression(e1, e2), es);
                            break;
                        case TranslationSymbolKind.EqualOperator:
                            ex = BinaryExpression.trySimplify(
                                 new EqualExpression(e1, e2), es);
                            break;
                        case TranslationSymbolKind.NotEqualOperator:
                            ex = BinaryExpression.trySimplify(
                                 new NotEqualExpression(e1, e2), es);
                            break;
                        case TranslationSymbolKind.GreaterOrEqualOperator:
                            ex = BinaryExpression.trySimplify(
                                 new GreaterOrEqualExpression(e1, e2), es);
                            break;
                        case TranslationSymbolKind.NegativeOperator:
                            ex = UnaryExpression.trySimplify(
                                 new NegativExpression(e1), es);
                            break;
                        case TranslationSymbolKind.AndOperator:
                           ex = BinaryExpression.trySimplify(
                                new AndExpression(e1, e2), es);
                            break;
                        case TranslationSymbolKind.OrOperator:
                           ex = BinaryExpression.trySimplify(
                                new OrExpression(e1, e2), es);
                            break;
                        case TranslationSymbolKind.IfOperator:
                            ex = UnaryExpression.trySimplify(
                                 new BlockConditionExpression(e1, true), es);
                            break;
                        case TranslationSymbolKind.ElseOperator:
                            ex = UnaryExpression.trySimplify(
                                 new BlockConditionExpression(e1, false), es) ;
                            break;
                        /*case TranslationSymbolKind.AvgOperator:
                           ex = ThreeArgumentsExpression.trySimplify(
                                new AvgExpression(e1, e2, e3), es.Stack));
                            break;
                         case TranslationSymbolKind.StdOperator:
                           ex = ThreeArgumentsExpression.trySimplify(
                                new StdExpression(e1, e2, e3), es.Stack));
                            break;
                        case TranslationSymbolKind.CVOperator:
                           ex = ThreeArgumentsExpression.trySimplify(
                                new CVExpression(e1, e2, e3), es.Stack));
                            break;
                        case TranslationSymbolKind.AgeOfThePatientOperator:
                           ex = ThreeArgumentsExpression.trySimplify(
                                new AgeOfThePatientExpression(e1, e2, e3), es.Stack));
                            break;
                        case TranslationSymbolKind.IsNotNullOperator:
                           ex = UnaryExpression.trySimplify(
                                new IsNotNullExpression(e2, (ILogic)operators[OperatorKind.Logic]), es.Stack));
                            break;
                        case TranslationSymbolKind.IsNullOperator:
                           ex = UnaryExpression.trySimplify(
                                new IsNullExpression(e1), es));
                            break;
                        case TranslationSymbolKind.CountItemsOperator:
                            ex = UnaryExpression.trySimplify(
                                 new CountItemsExpression(e2), es.Stack));
                            break;
                        case TranslationSymbolKind.CheckOperator:
                            ex = UnaryExpression.trySimplify(
                                 new CheckExpression(e2), es.Stack));
                            break;
                        case TranslationSymbolKind.BusulfanCalculationOperator:
                           ex = BinaryExpression.trySimplify(
                                new BusulfanCalculationExpression(e1, e2, (IFunction)operators[OperatorKind.Function]), es.Stack));
                            break;
                        case TranslationSymbolKind.ConvertToDaysOperator:
                           ex = UnaryExpression.trySimplify(
                                new ConvertToDaysExpression(e2), es.Stack));
                            break;
                        case TranslationSymbolKind.SumColOperator:
                            ex = UnaryExpression.trySimplify(
                                new SumColExpression(e2), es.Stack));
                            break;
                        case TranslationSymbolKind.SumRowOperator:
                            ex = UnaryExpression.trySimplify(
                                new SumRowExpression(e2), es.Stack));
                            break;
                        case TranslationSymbolKind.AvgWksOperator:
                            ex = UnaryExpression.trySimplify(
                                new AvgWksExpression(e2), es.Stack));
                            break;
                        case TranslationSymbolKind.SumWksOperator:
                            ex = UnaryExpression.trySimplify(
                                new SumWksExpression(e2), es.Stack));
                            break;
                        case TranslationSymbolKind.CountSigOperator:
                            ex = UnaryExpression.trySimplify(
                                new CountSigExpression(e2), es.Stack));
                            break;
                        case TranslationSymbolKind.SumOperator:
                           ex = UnaryExpression.trySimplify(
                                new SumExpression(e2), es.Stack));
                            break;
                         ------
                        case TranslationSymbolKind.ForEachOperator:                        
                           ex = UnaryExpression.trySimplify(
                                new ForEachExpression(noForEach++, e2, (IForEach)operators[OperatorKind.ForEach]), es.Stack));
                            break;*/
                    }
                    if(ex != null)
                    {
                        es.Push(ex);
                    }
                }
                else if (X.Kind == TokenKind.Nonterminal)
                {
                    LinkedList<Token> lst;
                    if ((lst = M[(int)c.Kind, (int)((NonterminalSymbol)X).KindNT])
                        != null)
                    {
                        LinkedListNode<Token> nd = lst.Last;
                        while (nd != null)
                        {
                            ts.Push(nd.Value);
                            nd = nd.Previous;
                        }
                    }
                    else
                    {
                        throw new ParseErrorException
                            ("Unexpected token: " + c.Kind + ".");
                    }
                }
                else 
                {                    
                    if (X.Kind == c.Kind)
                    {
                        Expression ex = null;
                        switch (c.Kind)
                        {
                            case TokenKind.Double:
                                ex = new ConstExpression(((DoubleToken)c).Value);
                                break;
                            case TokenKind.String:
                                ex = new ConstExpression(((StringToken)c).Value);
                                break;
                            case TokenKind.DateTime:
                                ex = new ConstExpression(((DateTimeToken)c).Value);
                                break;
                            case TokenKind.Variable:
                                ex = new VariableExpression(((VariableToken)c).Name);
                                break;
                            case TokenKind.E:
                                ex = new ConstExpression(Math.E);
                                break;
                            case TokenKind.PI:
                                ex = new ConstExpression(Math.PI);
                                break;
                            case TokenKind.RightBrace:
                                ex = new RightBraceExpression();
                                break;
                        }
                        if (ex != null)
                        {
                            es.Push(ex);
                        }
                        input.MoveNext();
                        Token prev = c;
                        c = input.Current;
                        tokens.Add(c);
                        if (previoustoken.ContainsKey(c.Group))
                        {
                            bool find = false;
                            foreach (TokenGroup group in previoustoken[c.Group])
                            {
                                if (prev.Group == group)
                                {
                                    find = true;
                                    break;
                                }
                            }
                            if (!find)
                            {
                                throw new ParseErrorException
                                     ("UnExpected " + prev.Group + " - " + c.Group + ".");
                            }
                        }
                    }
                    else
                    {
                        throw new ParseErrorException
                            ("Expected " + X.Kind + ", got " + c.Kind + ".");
                    }
                }
            }
            es.SetVariablesOrdinal();
        }

        public object Evaluate(object[] values)        
        {
            es.SetValues(values);
            return es.Evaluate();
        }
        #endregion Methods

        #region Properties
        public bool CalculationSucced
        {
            get { return _calculationSucced; }
        }

        public string Expression
        {
            get { return _expression; }
        }
        #endregion Properties
    }
}
