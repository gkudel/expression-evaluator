using System.Collections.Generic;

namespace ExpressionEvaluator.Evaluator.Expressions
{
    public partial class Parser
    {
        private void InitializeTab()
        {
            Token[] FinC = new Token[] {
                new NonterminalSymbol(NonterminalSymbolKind.D),
                new NonterminalSymbol(NonterminalSymbolKind.Co)
            };

            Token[] FinD = new Token[] {
                new NonterminalSymbol(NonterminalSymbolKind.E),
                new NonterminalSymbol(NonterminalSymbolKind.Do)
            };

            Token[] FinE = new Token[] {
                new NonterminalSymbol(NonterminalSymbolKind.T),
                new NonterminalSymbol(NonterminalSymbolKind.Eo)
            };

            Token[] FinT = new Token[] {
                new NonterminalSymbol(NonterminalSymbolKind.F),
                new NonterminalSymbol(NonterminalSymbolKind.To)
            };

            Token[] FinF = new Token[] {
                new NonterminalSymbol(NonterminalSymbolKind.G),//
                new NonterminalSymbol(NonterminalSymbolKind.Fo)
            };

            Token[] FinH = new Token[] {
                new NonterminalSymbol(NonterminalSymbolKind.G)
            };

            Token[] DoubleG = new Token[] {
                new BareToken(TokenKind.Double)
            };
            
            Token[] StringG = new Token[] {
                new BareToken(TokenKind.String)
            };

            Token[] DateTimeG = new Token[] {
                new BareToken(TokenKind.DateTime)
            };

            Token[] EG = new Token[] {
                new BareToken(TokenKind.E)
            };

            Token[] PIG = new Token[] {
                new BareToken(TokenKind.PI)
            };

            Token[] VariableG = new Token[] {
                new BareToken(TokenKind.Variable)
            };

            Token[] LeftBraceG = new Token[] {
                new BareToken(TokenKind.LeftBrace)
            };

            Token[] RightBraceG = new Token[] {
                new BareToken(TokenKind.RightBrace)
            };

            Token[] LeftBracketG = new Token[] {
                new BareToken(TokenKind.LeftBracket),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.RightBracket)
            };

            Token[] SqrtG = new Token[] {
                new BareToken(TokenKind.Sqrt),
                new BareToken(TokenKind.LeftBracket),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.RightBracket),
                new TranslationSymbol(TranslationSymbolKind.SqrtOperator)
            };
           
            Token[] AvgG = new Token[] {
                new BareToken(TokenKind.Avg),
                new BareToken(TokenKind.LeftBracket),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.Comma),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.Comma),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.RightBracket),
                new TranslationSymbol(TranslationSymbolKind.AvgOperator)
            };

            Token[] StdG = new Token[] {
                new BareToken(TokenKind.Std),
                new BareToken(TokenKind.LeftBracket),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.Comma),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.Comma),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.RightBracket),
                new TranslationSymbol(TranslationSymbolKind.StdOperator)
            };

            Token[] CVG = new Token[] {
                new BareToken(TokenKind.CV),
                new BareToken(TokenKind.LeftBracket),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.Comma),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.Comma),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.RightBracket),
                new TranslationSymbol(TranslationSymbolKind.CVOperator)
            };

            Token[] Log10G = new Token[] {
                new BareToken(TokenKind.Log10),
                new BareToken(TokenKind.LeftBracket),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.RightBracket),
                new TranslationSymbol(TranslationSymbolKind.Log10Operator)
            };

            Token[] SinG = new Token[] {
                new BareToken(TokenKind.Sin),
                new BareToken(TokenKind.LeftBracket),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.RightBracket),
                new TranslationSymbol(TranslationSymbolKind.SinOperator)
            };

            Token[] CosG = new Token[] {
                new BareToken(TokenKind.Cos),
                new BareToken(TokenKind.LeftBracket),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.RightBracket),
                new TranslationSymbol(TranslationSymbolKind.CosOperator)
            };

            Token[] TanG = new Token[] {
                new BareToken(TokenKind.Tan),
                new BareToken(TokenKind.LeftBracket),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.RightBracket),
                new TranslationSymbol(TranslationSymbolKind.TanOperator)
            };

            Token[] AbsG = new Token[] {
                new BareToken(TokenKind.Abs),
                new BareToken(TokenKind.LeftBracket),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.RightBracket),
                new TranslationSymbol(TranslationSymbolKind.AbsOperator)
            };
            
            Token[] AcosG = new Token[] {
                new BareToken(TokenKind.Acos),
                new BareToken(TokenKind.LeftBracket),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.RightBracket),
                new TranslationSymbol(TranslationSymbolKind.AcosOperator)
            };
            
            Token[] AsinG = new Token[] {
                new BareToken(TokenKind.Asin),
                new BareToken(TokenKind.LeftBracket),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.RightBracket),
                new TranslationSymbol(TranslationSymbolKind.AsinOperator)
            };
            
            Token[] AtanG = new Token[] {
                new BareToken(TokenKind.Atan),
                new BareToken(TokenKind.LeftBracket),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.RightBracket),
                new TranslationSymbol(TranslationSymbolKind.AtanOperator)
            };

            Token[] CeilingG = new Token[] {
                new BareToken(TokenKind.Ceiling),
                new BareToken(TokenKind.LeftBracket),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.RightBracket),
                new TranslationSymbol(TranslationSymbolKind.CeilingOperator)
            };

            Token[] CoshG = new Token[] {
                new BareToken(TokenKind.Cosh),
                new BareToken(TokenKind.LeftBracket),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.RightBracket),
                new TranslationSymbol(TranslationSymbolKind.CoshOperator)
            };

            Token[] ExpG = new Token[] {
                new BareToken(TokenKind.Exp),
                new BareToken(TokenKind.LeftBracket),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.RightBracket),
                new TranslationSymbol(TranslationSymbolKind.ExpOperator)
            };

            Token[] FloorG = new Token[] {
                new BareToken(TokenKind.Floor),
                new BareToken(TokenKind.LeftBracket),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.RightBracket),
                new TranslationSymbol(TranslationSymbolKind.FloorOperator)
            };

            Token[] MaxG = new Token[] {
                new BareToken(TokenKind.Max),
                new BareToken(TokenKind.LeftBracket),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.Comma),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.RightBracket),
                new TranslationSymbol(TranslationSymbolKind.MaxOperator)
            };

            Token[] MinG = new Token[] {
                new BareToken(TokenKind.Min),
                new BareToken(TokenKind.LeftBracket),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.Comma),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.RightBracket),
                new TranslationSymbol(TranslationSymbolKind.MinOperator)
            };

            Token[] LogG = new Token[] {
                new BareToken(TokenKind.Log),
                new BareToken(TokenKind.LeftBracket),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.Comma),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.RightBracket),
                new TranslationSymbol(TranslationSymbolKind.LogOperator)
            };

            Token[] SinhG = new Token[] {
                new BareToken(TokenKind.Sinh),
                new BareToken(TokenKind.LeftBracket),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.RightBracket),
                new TranslationSymbol(TranslationSymbolKind.SinhOperator)
            };

            Token[] TanhG = new Token[] {
                new BareToken(TokenKind.Tanh),
                new BareToken(TokenKind.LeftBracket),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.RightBracket),
                new TranslationSymbol(TranslationSymbolKind.TanhOperator)
            };

            Token[] TruncateG = new Token[] {
                new BareToken(TokenKind.Truncate),
                new BareToken(TokenKind.LeftBracket),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.RightBracket),
                new TranslationSymbol(TranslationSymbolKind.TruncateOperator)
            };

            Token[] RoundG = new Token[] {
                new BareToken(TokenKind.Round),
                new BareToken(TokenKind.LeftBracket),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.Comma),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.RightBracket),
                new TranslationSymbol(TranslationSymbolKind.RoundOperator)
            };

            Token[] CountItemsG = new Token[] {
                new BareToken(TokenKind.CountItems),
                new BareToken(TokenKind.LeftBracket),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.RightBracket),
                new TranslationSymbol(TranslationSymbolKind.CountItemsOperator)
            };

            Token[] CheckG = new Token[] {
                new BareToken(TokenKind.Check),
                new BareToken(TokenKind.LeftBracket),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.RightBracket),
                new TranslationSymbol(TranslationSymbolKind.CheckOperator)
            };

            Token[] AgeOfThePatientG = new Token[] {
                new BareToken(TokenKind.AgeOfThePatient),
                new BareToken(TokenKind.LeftBracket),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.Comma),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.Comma),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.RightBracket),
                new TranslationSymbol(TranslationSymbolKind.AgeOfThePatientOperator)
            };

            Token[] BusulfanCalculationG = new Token[] {
                new BareToken(TokenKind.BusulfanCalculation),
                new BareToken(TokenKind.LeftBracket),
                new BareToken(TokenKind.Variable),
                new BareToken(TokenKind.Comma),
                new BareToken(TokenKind.Variable),
                new BareToken(TokenKind.RightBracket),
                new TranslationSymbol(TranslationSymbolKind.BusulfanCalculationOperator)
            };

            Token[] ConvertToDaysG = new Token[] {
                new BareToken(TokenKind.ConvertToDays),
                new BareToken(TokenKind.LeftBracket),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.RightBracket),
                new TranslationSymbol(TranslationSymbolKind.ConvertToDaysOperator)
            };
            
            Token[] SumRowG = new Token[] {
                new BareToken(TokenKind.Sum_Row),
                new BareToken(TokenKind.LeftBracket),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.RightBracket),
                new TranslationSymbol(TranslationSymbolKind.SumRowOperator)
            };
            Token[] SumColG = new Token[] {
                new BareToken(TokenKind.Sum_Col),
                new BareToken(TokenKind.LeftBracket),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.RightBracket),
                new TranslationSymbol(TranslationSymbolKind.SumColOperator)
            };

            Token[] AvgWksG = new Token[] {
                new BareToken(TokenKind.AvgWks),
                new BareToken(TokenKind.LeftBracket),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.RightBracket),
                new TranslationSymbol(TranslationSymbolKind.AvgWksOperator)
            };
            Token[] SumWksG = new Token[] {
                new BareToken(TokenKind.SumWks),
                new BareToken(TokenKind.LeftBracket),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.RightBracket),
                new TranslationSymbol(TranslationSymbolKind.SumWksOperator)
            };
            Token[] CountSigG = new Token[] {
                new BareToken(TokenKind.Count_Sig),
                new BareToken(TokenKind.LeftBracket),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.RightBracket),
                new TranslationSymbol(TranslationSymbolKind.CountSigOperator)
            };

            Token[] SumG = new Token[] {
                new BareToken(TokenKind.Sum),
                new BareToken(TokenKind.LeftBracket),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.RightBracket),
                new TranslationSymbol(TranslationSymbolKind.SumOperator)
            };

            Token[] MinusG = new Token[] {
                new BareToken(TokenKind.Minus),
                new NonterminalSymbol(NonterminalSymbolKind.G),
                new TranslationSymbol(TranslationSymbolKind.UnaryMinusOperator)
            };

            Token[] PlusEo = new Token[] {
                new BareToken(TokenKind.Plus),
                new NonterminalSymbol(NonterminalSymbolKind.T),
                new TranslationSymbol(TranslationSymbolKind.PlusOperator),
                new NonterminalSymbol(NonterminalSymbolKind.Eo)
            };

            Token[] MinusEo = new Token[] {                
                new BareToken(TokenKind.Minus),                
                new NonterminalSymbol(NonterminalSymbolKind.T),                                                                
                new TranslationSymbol(TranslationSymbolKind.MinusOperator),
                new NonterminalSymbol(NonterminalSymbolKind.Eo)                
            };

            Token[] AsteriskTo = new Token[] {
                new BareToken(TokenKind.Multiply),
                new NonterminalSymbol(NonterminalSymbolKind.F),
                new TranslationSymbol(TranslationSymbolKind.MultiplyOperator),
                new NonterminalSymbol(NonterminalSymbolKind.To)
            };

            Token[] SlashTo = new Token[] {
                new BareToken(TokenKind.Divide),
                new NonterminalSymbol(NonterminalSymbolKind.F),
                new TranslationSymbol(TranslationSymbolKind.DivideOpearator),
                new NonterminalSymbol(NonterminalSymbolKind.To)
            };

            Token[] PowerTo = new Token[] {
                new BareToken(TokenKind.Power),
                new NonterminalSymbol(NonterminalSymbolKind.F),
                new TranslationSymbol(TranslationSymbolKind.PowerOperator),
                new NonterminalSymbol(NonterminalSymbolKind.To)                
            };

            Token[] IfG = new Token[] { 
                new BareToken(TokenKind.If),                
                new BareToken(TokenKind.LeftBracket),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.RightBracket),                
                new TranslationSymbol(TranslationSymbolKind.IfOperator),
                new BareToken(TokenKind.LeftBrace),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.RightBrace),
                new BareToken(TokenKind.Else),                
                new TranslationSymbol(TranslationSymbolKind.ElseOperator),
                new BareToken(TokenKind.LeftBrace),                
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.RightBrace)
            };

            Token[] ForEachG = new Token[] { 
                new BareToken(TokenKind.ForEach),                
                new BareToken(TokenKind.LeftBracket),
                new BareToken(TokenKind.Variable),
                new BareToken(TokenKind.RightBracket),                
                new TranslationSymbol(TranslationSymbolKind.ForEachOperator),
                new BareToken(TokenKind.LeftBrace),
                new NonterminalSymbol(NonterminalSymbolKind.C),
                new BareToken(TokenKind.RightBrace),
            };

            Token[] GreaterDo = new Token[] {
                new BareToken(TokenKind.Greater),
                new NonterminalSymbol(NonterminalSymbolKind.E),
                new TranslationSymbol(TranslationSymbolKind.GreaterOperator),
                new NonterminalSymbol(NonterminalSymbolKind.Do)
            };

            Token[] GreaterOrEqualDo = new Token[] {
                new BareToken(TokenKind.GreaterOrEqual),
                new NonterminalSymbol(NonterminalSymbolKind.E),
                new TranslationSymbol(TranslationSymbolKind.GreaterOrEqualOperator),
                new NonterminalSymbol(NonterminalSymbolKind.Do)
            };

            Token[] LessDo = new Token[] {
                new BareToken(TokenKind.Less),
                new NonterminalSymbol(NonterminalSymbolKind.E),
                new TranslationSymbol(TranslationSymbolKind.LessOperator),
                new NonterminalSymbol(NonterminalSymbolKind.Do)
            };

            Token[] LessOrEqualDo = new Token[] {
                new BareToken(TokenKind.LessOrEqual),
                new NonterminalSymbol(NonterminalSymbolKind.E),
                new TranslationSymbol(TranslationSymbolKind.LessOrEqualOperator),
                new NonterminalSymbol(NonterminalSymbolKind.Do)
            };

            Token[] EqualDo = new Token[] {
                new BareToken(TokenKind.Equal),
                new NonterminalSymbol(NonterminalSymbolKind.E),
                new TranslationSymbol(TranslationSymbolKind.EqualOperator),
                new NonterminalSymbol(NonterminalSymbolKind.Do)
            };

            Token[] NotEqualDo = new Token[] {
                new BareToken(TokenKind.NotEqual),
                new NonterminalSymbol(NonterminalSymbolKind.E),
                new TranslationSymbol(TranslationSymbolKind.NotEqualOperator),
                new NonterminalSymbol(NonterminalSymbolKind.Do)
            };

            Token[] IsNullDo = new Token[] {
                new BareToken(TokenKind.IsNull),
                new TranslationSymbol(TranslationSymbolKind.IsNullOperator)
            };

            Token[] IsNotNullDo = new Token[] {
                new BareToken(TokenKind.IsNotNull),
                new TranslationSymbol(TranslationSymbolKind.IsNotNullOperator)
            };

            Token[] NegativeG = new Token[] {
                new BareToken(TokenKind.Negative),
                new NonterminalSymbol(NonterminalSymbolKind.G),
                new TranslationSymbol(TranslationSymbolKind.NegativeOperator)
            };

            Token[] AndCo = new Token[] {
                new BareToken(TokenKind.And),
                new NonterminalSymbol(NonterminalSymbolKind.D),
                new TranslationSymbol(TranslationSymbolKind.AndOperator),
                new NonterminalSymbol(NonterminalSymbolKind.Co)
            };

            Token[] OrCo = new Token[] {
                new BareToken(TokenKind.Or),
                new NonterminalSymbol(NonterminalSymbolKind.D),
                new TranslationSymbol(TranslationSymbolKind.OrOperator),
                new NonterminalSymbol(NonterminalSymbolKind.Co)
            };

            LinkedList<Token> el = new LinkedList<Token>();
            for (int i = 0; i < System.Enum.GetValues(typeof(TokenKind)).Length; ++i)
                M[i, (int)NonterminalSymbolKind.Co] =
                M[i, (int)NonterminalSymbolKind.Do] =
                M[i, (int)NonterminalSymbolKind.Eo] =
                M[i, (int)NonterminalSymbolKind.To] =
                M[i, (int)NonterminalSymbolKind.Fo] = el;

            M[(int)TokenKind.Variable, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.Double, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.String, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.DateTime, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.E, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.PI, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.LeftBracket, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.Sqrt, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.Avg, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.Std, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.CV, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.Sin, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.Log10, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.Log, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.Cos, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.Tan, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.Abs, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.Acos, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.Asin, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.Atan, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.Ceiling, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.Cosh, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.Exp, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.Floor, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.Max, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.Min, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.Sinh, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.Tanh, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.Truncate, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.Round, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.CountItems, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.Check, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.AgeOfThePatient, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.BusulfanCalculation, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.ConvertToDays, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.Sum_Row, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.Sum_Col, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.SumWks, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.AvgWks, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.Count_Sig, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.Sum, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.If, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.Else, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.Minus, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.Negative, (int)NonterminalSymbolKind.C] =
            M[(int)TokenKind.ForEach, (int)NonterminalSymbolKind.C] =
                new LinkedList<Token>(FinC);

            M[(int)TokenKind.Variable, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.Double, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.String, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.DateTime, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.E, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.PI, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.LeftBracket, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.Sqrt, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.Avg, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.Std, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.CV, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.Sin, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.Log10, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.Log, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.Cos, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.Tan, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.Abs, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.Acos, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.Asin, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.Atan, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.Ceiling, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.Cosh, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.Exp, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.Floor, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.Max, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.Min, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.Sinh, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.Tanh, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.Truncate, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.Round, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.CountItems, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.Check, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.AgeOfThePatient, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.BusulfanCalculation, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.ConvertToDays, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.Sum_Row, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.Sum_Col, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.SumWks, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.AvgWks, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.Count_Sig, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.Sum, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.If, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.Else, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.Minus, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.Negative, (int)NonterminalSymbolKind.D] =
            M[(int)TokenKind.ForEach, (int)NonterminalSymbolKind.D] =
                new LinkedList<Token>(FinD);

            M[(int)TokenKind.Variable, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.Double, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.String, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.DateTime, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.E, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.PI, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.LeftBracket, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.Sqrt, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.Avg, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.Std, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.CV, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.Sin, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.Log10, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.Log, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.Cos, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.Tan, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.Abs, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.Acos, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.Asin, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.Atan, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.Ceiling, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.Cosh, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.Exp, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.Floor, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.Max, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.Min, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.Sinh, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.Tanh, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.Truncate, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.Round, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.CountItems, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.Check, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.AgeOfThePatient, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.BusulfanCalculation, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.ConvertToDays, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.Sum_Row, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.Sum_Col, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.SumWks, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.AvgWks, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.Count_Sig, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.Sum, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.If, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.Else, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.Minus, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.Negative, (int)NonterminalSymbolKind.E] =
            M[(int)TokenKind.ForEach, (int)NonterminalSymbolKind.E] =
                new LinkedList<Token>(FinE);

            M[(int)TokenKind.Variable, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.Double, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.String, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.DateTime, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.E, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.PI, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.LeftBracket, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.Sqrt, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.Avg, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.Std, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.CV, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.Sin, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.Log10, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.Log, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.Cos, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.Tan, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.Abs, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.Acos, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.Asin, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.Atan, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.Ceiling, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.Cosh, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.Exp, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.Floor, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.Max, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.Min, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.Sinh, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.Tanh, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.Truncate, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.Round, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.CountItems, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.Check, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.AgeOfThePatient, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.BusulfanCalculation, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.ConvertToDays, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.Sum_Row, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.Sum_Col, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.SumWks, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.AvgWks, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.Count_Sig, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.Sum, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.If, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.Else, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.Minus, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.Negative, (int)NonterminalSymbolKind.T] =
            M[(int)TokenKind.ForEach, (int)NonterminalSymbolKind.T] =
               new LinkedList<Token>(FinT);

            M[(int)TokenKind.Variable, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.Double, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.String, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.DateTime, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.E, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.PI, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.LeftBracket, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.Sqrt, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.Avg, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.Std, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.CV, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.Sin, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.Log10, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.Log, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.Cos, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.Tan, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.Abs, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.Acos, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.Asin, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.Atan, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.Ceiling, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.Cosh, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.Exp, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.Floor, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.Max, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.Min, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.Sinh, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.Tanh, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.Truncate, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.Round, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.CountItems, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.Check, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.AgeOfThePatient, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.BusulfanCalculation, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.ConvertToDays, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.Sum_Row, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.Sum_Col, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.SumWks, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.AvgWks, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.Count_Sig, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.Sum, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.If, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.Else, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.Minus, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.Negative, (int)NonterminalSymbolKind.F] =
            M[(int)TokenKind.ForEach, (int)NonterminalSymbolKind.F] =
              new LinkedList<Token>(FinF);

            M[(int)TokenKind.Variable, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(VariableG);

            M[(int)TokenKind.Double, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(DoubleG);
            
            M[(int)TokenKind.String, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(StringG);

            M[(int)TokenKind.DateTime, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(DateTimeG);

            M[(int)TokenKind.E, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(EG);

            M[(int)TokenKind.PI, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(PIG);

            M[(int)TokenKind.LeftBracket, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(LeftBracketG);
            
            M[(int)TokenKind.Sqrt, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(SqrtG);
            
            M[(int)TokenKind.Avg, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(AvgG);

            M[(int)TokenKind.Std, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(StdG);

            M[(int)TokenKind.CV, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(CVG);

            M[(int)TokenKind.Log10, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(Log10G);

            M[(int)TokenKind.Log, (int)NonterminalSymbolKind.G] =
               new LinkedList<Token>(LogG);

            M[(int)TokenKind.Sin, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(SinG);

            M[(int)TokenKind.Cos, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(CosG);

            M[(int)TokenKind.Tan, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(TanG);

            M[(int)TokenKind.Abs, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(AbsG);

            M[(int)TokenKind.Acos, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(AcosG);

            M[(int)TokenKind.Asin, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(AsinG);
            
            M[(int)TokenKind.Atan, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(AtanG);
            
            M[(int)TokenKind.Ceiling, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(CeilingG);

            M[(int)TokenKind.Cosh, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(CoshG);

            M[(int)TokenKind.Exp, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(ExpG);

            M[(int)TokenKind.Floor, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(FloorG);

            M[(int)TokenKind.Max, (int)NonterminalSymbolKind.G] =
               new LinkedList<Token>(MaxG);

            M[(int)TokenKind.Min, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(MinG);

            M[(int)TokenKind.Sinh, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(SinhG);

            M[(int)TokenKind.Tanh, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(TanhG);

            M[(int)TokenKind.Truncate, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(TruncateG);

            M[(int)TokenKind.Round, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(RoundG);

            M[(int)TokenKind.CountItems, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(CountItemsG);

            M[(int)TokenKind.Check, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(CheckG);

            M[(int)TokenKind.AgeOfThePatient, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(AgeOfThePatientG);

            M[(int)TokenKind.BusulfanCalculation, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(BusulfanCalculationG);

            M[(int)TokenKind.ConvertToDays, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(ConvertToDaysG);

            M[(int)TokenKind.Sum_Row, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(SumRowG);

            M[(int)TokenKind.Sum_Col, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(SumColG);

            M[(int)TokenKind.SumWks, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(SumWksG);

            M[(int)TokenKind.AvgWks, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(AvgWksG);

            M[(int)TokenKind.Count_Sig, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(CountSigG);

            M[(int)TokenKind.Sum, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(SumG);

            M[(int)TokenKind.Minus, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(MinusG);

            M[(int)TokenKind.Plus, (int)NonterminalSymbolKind.Eo] =
               new LinkedList<Token>(PlusEo);

            M[(int)TokenKind.Minus, (int)NonterminalSymbolKind.Eo] =
               new LinkedList<Token>(MinusEo);

            M[(int)TokenKind.Multiply, (int)NonterminalSymbolKind.To] =
               new LinkedList<Token>(AsteriskTo);
           
            M[(int)TokenKind.Divide, (int)NonterminalSymbolKind.To] =
               new LinkedList<Token>(SlashTo);

            M[(int)TokenKind.Greater, (int)NonterminalSymbolKind.Do] =
                new LinkedList<Token>(GreaterDo);

            M[(int)TokenKind.GreaterOrEqual, (int)NonterminalSymbolKind.Do] =
                new LinkedList<Token>(GreaterOrEqualDo);
            
            M[(int)TokenKind.Less, (int)NonterminalSymbolKind.Do] =
                new LinkedList<Token>(LessDo);

            M[(int)TokenKind.LessOrEqual, (int)NonterminalSymbolKind.Do] =
                new LinkedList<Token>(LessOrEqualDo);
            
            M[(int)TokenKind.Equal, (int)NonterminalSymbolKind.Do] =
                new LinkedList<Token>(EqualDo);

            M[(int)TokenKind.NotEqual, (int)NonterminalSymbolKind.Do] =
                new LinkedList<Token>(NotEqualDo);

            M[(int)TokenKind.IsNull, (int)NonterminalSymbolKind.Do] =
                new LinkedList<Token>(IsNullDo);

            M[(int)TokenKind.IsNotNull, (int)NonterminalSymbolKind.Do] =
                new LinkedList<Token>(IsNotNullDo);

            M[(int)TokenKind.Negative, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(NegativeG);

            M[(int)TokenKind.And, (int)NonterminalSymbolKind.Co] =
                new LinkedList<Token>(AndCo);

            M[(int)TokenKind.Or, (int)NonterminalSymbolKind.Co] =
                new LinkedList<Token>(OrCo);

            M[(int)TokenKind.If, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(IfG);

            M[(int)TokenKind.ForEach, (int)NonterminalSymbolKind.G] =
                new LinkedList<Token>(ForEachG);

            M[(int)TokenKind.Power, (int)NonterminalSymbolKind.To] =
               new LinkedList<Token>(PowerTo);

            previoustoken.Add(TokenGroup.Variable, new TokenGroup[] { 
                TokenGroup.LeftBrace, 
                TokenGroup.LeftBracket, 
                TokenGroup.BianaryOperator,
                TokenGroup.Minus,
                TokenGroup.Comma,
                TokenGroup.LeftUnaryOperator
            });

            previoustoken.Add(TokenGroup.Const, new TokenGroup[] { 
                TokenGroup.LeftBrace, 
                TokenGroup.LeftBracket, 
                TokenGroup.BianaryOperator,
                TokenGroup.Minus,
                TokenGroup.Comma,
                TokenGroup.LeftUnaryOperator
            });

            previoustoken.Add(TokenGroup.BianaryOperator, new TokenGroup[] { 
                TokenGroup.RightBracket,
                TokenGroup.Variable,
                TokenGroup.Const,
                TokenGroup.NullOperator
            });

            previoustoken.Add(TokenGroup.Minus, new TokenGroup[] { 
                TokenGroup.LeftBrace,
                TokenGroup.LeftBracket,
                TokenGroup.RightBracket,
                TokenGroup.Variable,
                TokenGroup.Const
            });
            
            previoustoken.Add(TokenGroup.LeftBracket, new TokenGroup[] { 
                TokenGroup.If,
                TokenGroup.ForEach,
                TokenGroup.BianaryOperator,
                TokenGroup.Minus,
                TokenGroup.Comma,
                TokenGroup.Function,
                TokenGroup.LeftBracket,
                TokenGroup.LeftUnaryOperator,
                TokenGroup.LeftBrace
            });

            previoustoken.Add(TokenGroup.RightBracket, new TokenGroup[] { 
                TokenGroup.Const,
                TokenGroup.RightBracket,
                TokenGroup.Variable,
                TokenGroup.NullOperator,
                TokenGroup.RightBrace
            });

            previoustoken.Add(TokenGroup.LeftBrace, new TokenGroup[] { 
                TokenGroup.Else,
                TokenGroup.RightBracket
            });

            previoustoken.Add(TokenGroup.RightBrace, new TokenGroup[] { 
                TokenGroup.Const,
                TokenGroup.RightBracket,
                TokenGroup.RightBrace,
                TokenGroup.Variable
            });

            previoustoken.Add(TokenGroup.Comma, new TokenGroup[] { 
                TokenGroup.Const,
                TokenGroup.RightBracket,
                TokenGroup.RightBrace,
                TokenGroup.Variable
            });
            
            previoustoken.Add(TokenGroup.If, new TokenGroup[] { 
                TokenGroup.LeftBrace
            });

            previoustoken.Add(TokenGroup.Else, new TokenGroup[] { 
                TokenGroup.RightBrace
            });
            
            previoustoken.Add(TokenGroup.Function, new TokenGroup[] { 
                TokenGroup.BianaryOperator,
                TokenGroup.Minus,
                TokenGroup.Comma,
                TokenGroup.Const,
                TokenGroup.LeftBrace,
                TokenGroup.LeftBracket,
                TokenGroup.LeftUnaryOperator,
                TokenGroup.Variable
            });

            previoustoken.Add(TokenGroup.ForEach, new TokenGroup[] {
                TokenGroup.LeftBracket
            });
        }
    }
}