using System.Collections.Generic;
using System.Text;
using GcmCmnTools;
using System;
using System.Globalization;
using System.Xml;


namespace ExpressionEvaluator.Evaluator.Expressions
{
    class Scanner
    {
        private Dictionary<char, Token> simpleTokens =
            new Dictionary<char, Token>();

        private Dictionary<string, Token> complexTokens =
            new Dictionary<string, Token>();

        private List<char> gramar =
            new List<char>();

        public Scanner()
        {
            simpleTokens.Add('+', new BareToken(TokenKind.Plus, TokenGroup.BianaryOperator));
            simpleTokens.Add('-', new BareToken(TokenKind.Minus, TokenGroup.Minus));
            simpleTokens.Add('*', new BareToken(TokenKind.Multiply, TokenGroup.BianaryOperator));
            simpleTokens.Add('/', new BareToken(TokenKind.Divide, TokenGroup.BianaryOperator));
            simpleTokens.Add('^', new BareToken(TokenKind.Power, TokenGroup.BianaryOperator));
            simpleTokens.Add('(', new BareToken(TokenKind.LeftBracket, TokenGroup.LeftBracket));
            simpleTokens.Add(',', new BareToken(TokenKind.Comma, TokenGroup.Comma));
            simpleTokens.Add(')', new BareToken(TokenKind.RightBracket, TokenGroup.RightBracket));
            simpleTokens.Add('{', new BareToken(TokenKind.LeftBrace, TokenGroup.LeftBrace));
            simpleTokens.Add('}', new BareToken(TokenKind.RightBrace, TokenGroup.RightBrace));

            //complexTokens.Add("isnull", new BareToken(TokenKind.IsNull, TokenGroup.Function));
            complexTokens.Add("sqrt", new BareToken(TokenKind.Sqrt, TokenGroup.Function));
            complexTokens.Add("avg", new BareToken(TokenKind.Avg, TokenGroup.Function));
            complexTokens.Add("std", new BareToken(TokenKind.Std, TokenGroup.Function));
            complexTokens.Add("cv", new BareToken(TokenKind.CV, TokenGroup.Function));
            complexTokens.Add("log", new BareToken(TokenKind.Log10, TokenGroup.Function));
            complexTokens.Add("lg", new BareToken(TokenKind.Log, TokenGroup.Function));
            complexTokens.Add("cos", new BareToken(TokenKind.Cos, TokenGroup.Function));
            complexTokens.Add("sin", new BareToken(TokenKind.Sin, TokenGroup.Function));
            complexTokens.Add("tan", new BareToken(TokenKind.Tan, TokenGroup.Function));
            complexTokens.Add("abs", new BareToken(TokenKind.Abs, TokenGroup.Function));
            complexTokens.Add("acos", new BareToken(TokenKind.Acos, TokenGroup.Function));
            complexTokens.Add("asin", new BareToken(TokenKind.Asin, TokenGroup.Function));
            complexTokens.Add("atan", new BareToken(TokenKind.Atan, TokenGroup.Function));
            complexTokens.Add("ceiling", new BareToken(TokenKind.Ceiling, TokenGroup.Function));
            complexTokens.Add("cosh", new BareToken(TokenKind.Cosh, TokenGroup.Function));
            complexTokens.Add("exp", new BareToken(TokenKind.Exp, TokenGroup.Function));
            complexTokens.Add("floor", new BareToken(TokenKind.Floor, TokenGroup.Function));
            complexTokens.Add("max", new BareToken(TokenKind.Max, TokenGroup.Function));
            complexTokens.Add("min", new BareToken(TokenKind.Min, TokenGroup.Function));
            complexTokens.Add("sinh", new BareToken(TokenKind.Sinh, TokenGroup.Function));
            complexTokens.Add("tanh", new BareToken(TokenKind.Tanh, TokenGroup.Function));
            complexTokens.Add("truncate", new BareToken(TokenKind.Truncate, TokenGroup.Function));
            complexTokens.Add("round", new BareToken(TokenKind.Round, TokenGroup.Function));
            complexTokens.Add("CountItems", new BareToken(TokenKind.CountItems, TokenGroup.Function));
            complexTokens.Add("Check", new BareToken(TokenKind.Check, TokenGroup.Function));
            complexTokens.Add("patientage", new BareToken(TokenKind.AgeOfThePatient, TokenGroup.Function));
            complexTokens.Add("busulfancalculation", new BareToken(TokenKind.BusulfanCalculation, TokenGroup.Function));
            complexTokens.Add("converttodays", new BareToken(TokenKind.ConvertToDays, TokenGroup.Function));
            complexTokens.Add("Sum_row", new BareToken(TokenKind.Sum_Row, TokenGroup.Function));
            complexTokens.Add("Sum_col", new BareToken(TokenKind.Sum_Col, TokenGroup.Function));
            complexTokens.Add("Avgwks", new BareToken(TokenKind.AvgWks, TokenGroup.Function));
            complexTokens.Add("Sumwks", new BareToken(TokenKind.SumWks, TokenGroup.Function));
            complexTokens.Add("count_sig", new BareToken(TokenKind.Count_Sig, TokenGroup.Function));           
            complexTokens.Add("sum", new BareToken(TokenKind.Sum, TokenGroup.Function));
            complexTokens.Add("PI", new BareToken(TokenKind.PI, TokenGroup.Const));
            complexTokens.Add("E", new BareToken(TokenKind.E, TokenGroup.Const));
            complexTokens.Add("if", new BareToken(TokenKind.If, TokenGroup.If));
            complexTokens.Add("else", new BareToken(TokenKind.Else, TokenGroup.Else));
            complexTokens.Add("!", new BareToken(TokenKind.Negative, TokenGroup.LeftUnaryOperator));
            complexTokens.Add("==", new BareToken(TokenKind.Equal, TokenGroup.BianaryOperator));
            complexTokens.Add("!=", new BareToken(TokenKind.NotEqual, TokenGroup.BianaryOperator));
            complexTokens.Add(">=", new BareToken(TokenKind.GreaterOrEqual, TokenGroup.BianaryOperator));
            complexTokens.Add("<=", new BareToken(TokenKind.LessOrEqual, TokenGroup.BianaryOperator));
            complexTokens.Add(">", new BareToken(TokenKind.Greater, TokenGroup.BianaryOperator));
            complexTokens.Add("<", new BareToken(TokenKind.Less, TokenGroup.BianaryOperator));
            complexTokens.Add("&&", new BareToken(TokenKind.And, TokenGroup.BianaryOperator));
            complexTokens.Add("||", new BareToken(TokenKind.Or, TokenGroup.BianaryOperator));
            complexTokens.Add("IsNull", new BareToken(TokenKind.IsNull, TokenGroup.NullOperator));
            complexTokens.Add("IsNotNull", new BareToken(TokenKind.IsNotNull, TokenGroup.NullOperator));
            complexTokens.Add("ForEach", new BareToken(TokenKind.ForEach, TokenGroup.ForEach));

            gramar.Add('+');
            gramar.Add('-');
            gramar.Add('*');
            gramar.Add('/');
            gramar.Add('^');
            gramar.Add('(');
            gramar.Add(',');
            gramar.Add(')');
            gramar.Add('{');
            gramar.Add('}');
            gramar.Add('!');
            gramar.Add('=');
            gramar.Add('<');
            gramar.Add('>');
            gramar.Add('&');
            gramar.Add('|');
            gramar.Add(',');
        }

        public IEnumerator<Token> Scan(string expression)
        {
            return Scan(new System.IO.StringReader(expression));
        }
        
        public IEnumerator<Token> Scan(System.IO.TextReader reader)
        {
            char c;
            Token token;
            while ( reader.Peek() != -1)
            {
                c = (char)reader.Peek();
                if (char.IsWhiteSpace(c))
                {
                    reader.Read();
                }
                else if (simpleTokens.TryGetValue(c, out token))
                {
                    yield return token;
                    reader.Read();
                }
                else if (char.IsDigit(c))
                {
                    yield return ScanDouble(reader);
                }
                else if (c == '[')
                {
                    yield return ScanBracketsExpression(reader);
                }
                else if (c == '\'')
                {
                    yield return ScanConstStringToken(reader);
                }
                else if (gramar.Contains(c) || char.IsLetter(c))
                {
                    yield return ScanGramarToken(reader);
                }
                else
                {
                    ParseError();
                }
            }
            yield return new BareToken(TokenKind.End);
        }

        public IEnumerator<string> ScanTokenString(string expression)
        {
            return ScanTokenString(new System.IO.StringReader(expression));
        }

        public IEnumerator<string> ScanTokenString(System.IO.TextReader reader)
        {
            char c;
            Token token;
            while (reader.Peek() != -1)
            {
                c = (char)reader.Peek();
                if (char.IsWhiteSpace(c))
                {
                    reader.Read();
                }
                else if (simpleTokens.TryGetValue(c, out token))
                {
                    yield return c.ToString();
                    reader.Read();
                }
                else if (char.IsDigit(c))
                {
                    yield return ScanDoubleAsString(reader);
                }
                else if (c == '[')
                {
                    token = ScanBracketsExpression(reader);
                    if (token is VariableToken)
                    {
                        yield return "[" + ((VariableToken)token).Name + "]";
                    }
                    else if (token is StringToken)
                    {
                        yield return "[#" + ((StringToken)token).Value + "#]";
                    }
                }
                else if (c == '\'')
                {
                    yield return "'" + ScanConstStringToken(reader).ToString() + "'";
                }
                else if (gramar.Contains(c) || char.IsLetter(c))
                {
                    yield return ScanGramarTokenString(reader);
                }
                else
                {
                    ParseError();
                }
            }
            yield return "";
        }

        private void ParseError()
        {
            throw new ParseErrorException(
                        "Scan error: malformaed input.");
        }

        private DoubleToken ScanDouble(System.IO.TextReader reader)
        {
            double result = 0.0d;
            double factor = 0.1d;
            char c;
            bool dotNotRead = true;
            while (reader.Peek() != -1)
            {
                c = (char)reader.Peek();
                if (System.Char.IsDigit(c))
                {
                    double dv = c - 48;
                    if (dotNotRead)
                    {
                        result *= 10;
                        result += dv;
                    }
                    else
                    {
                        result += factor * dv;
                        factor /= 10;
                    }
                    reader.Read();
                }
                else if (dotNotRead && c == '.')
                {
                    dotNotRead = false;
                    reader.Read();
                }
                else
                {
                    break;
                }
            }
            return new DoubleToken(result);
        }

        private string ScanDoubleAsString(System.IO.TextReader reader)
        {
            string result = string.Empty;
            char c;
            while (reader.Peek() != -1)
            {
                c = (char)reader.Peek();
                if (System.Char.IsDigit(c) || c == '.')
                {
                    result += c;
                    reader.Read();
                }
                else
                {
                    break;
                }
            }
            return result;
        }

        private Token ScanConstStringToken(System.IO.TextReader reader)
        {
            StringBuilder builder = new StringBuilder();
            char c;
            reader.Read();
            while (reader.Peek() != -1)
            {
                c = (char)reader.Peek();
                if (c != '\'')
                {
                    reader.Read();
                    builder.Append(c);
                }
                else
                {
                    reader.Read();
                    break;
                }
            }
            Token token;
            string input = builder.ToString();
            token = new StringToken(input);
            if (input != string.Empty)
            {
                DateTime dt = DateTime.MinValue;
                if (ConvertStringToDateTime(input, ref dt))
                {
                    token = new DateTimeToken(dt);
                }
            }
            return token;
        }

        private Token ScanBracketsExpression(System.IO.TextReader reader)
        {
            StringBuilder builder = new StringBuilder();
            char c;
            reader.Read();
            while (reader.Peek() != -1)
            {
                c = (char)reader.Peek();
                if (c != ']')
                {
                    reader.Read();
                    builder.Append(c);
                }
                else
                {
                    reader.Read();
                    break;
                }
            }

            if (IsUserDefinedResultString(builder))
                return CreateStringTokenForUdr(builder);

            Token token;
            string input = builder.ToString();
            
            if (!complexTokens.TryGetValue(input, out token))
            {
                token = new VariableToken(input);
            }
            return token;
        }
        
        private Token ScanGramarToken(System.IO.TextReader reader)
        {
            StringBuilder builder = new StringBuilder();
            char c;
            Token tk;
            while (reader.Peek() != -1)
            {
                c = (char)reader.Peek();
                if (char.IsWhiteSpace(c))
                {
                    break;
                }
                else
                {
                    string str = builder.ToString();
                    if (simpleTokens.TryGetValue(c, out tk))
                    {
                        break;
                    }
                    if (complexTokens.TryGetValue(str, out tk))
                    {
                        if (complexTokens.TryGetValue(str + c, out tk))
                        {
                            reader.Read();
                            builder.Append(c);
                        }
                        break;
                    }

                    if (gramar.Contains(c) && str != string.Empty)
                    {
                        if (complexTokens.TryGetValue(str + c, out tk))
                        {
                            reader.Read();
                            builder.Append(c);
                        }
                        break;
                    }
                    reader.Read();
                    builder.Append(c);
                }
            }
            Token token;
            string input = builder.ToString();

            /*
             *  All unknown letter-only strings default to varaibles.
             */
            if (!complexTokens.TryGetValue(input, out token))
            {
                token = new VariableToken(input);
            }
            return token;
        }

        private string ScanGramarTokenString(System.IO.TextReader reader)
        {
            StringBuilder builder = new StringBuilder();
            char c;
            Token tk;
            while (reader.Peek() != -1)
            {
                c = (char)reader.Peek();
                if (char.IsWhiteSpace(c))
                {
                    break;
                }
                else
                {
                    string str = builder.ToString();
                    if (simpleTokens.TryGetValue(c, out tk))
                    {
                        break;
                    }
                    if (complexTokens.TryGetValue(str, out tk))
                    {
                        if (complexTokens.TryGetValue(str + c, out tk))
                        {
                            reader.Read();
                            builder.Append(c);
                        }
                        break;
                    }

                    if (gramar.Contains(c) && str != string.Empty)
                    {
                        if (complexTokens.TryGetValue(str + c, out tk))
                        {
                            reader.Read();
                            builder.Append(c);
                        }
                        break;
                    }
                    reader.Read();
                    builder.Append(c);
                }
            }
            string input = builder.ToString();
            Token token;
            if (!complexTokens.TryGetValue(input, out token))
            {

                if (input.StartsWith("[") && input.EndsWith("]"))
                    return input;
                else
                    return "[" + input + "]";
            }
            else
            {
                return builder.ToString();
            }
        }

        private bool IsUserDefinedResultString(StringBuilder builder)
        {
            return builder.Length > 1 && builder[0] == '#' && builder[builder.Length - 1] == '#';
        }
        private Token CreateStringTokenForUdr(StringBuilder builder)
        {
            // user defined result is miss-considered as variable
            builder.Remove(builder.Length - 1, 1);
            builder.Remove(0, 1);
            return new StringToken(builder.ToString());
        }
        public bool ConvertStringToDateTime(string dt, ref DateTime date)
        {
            bool ret = false;
            try
            {
                date = CmnTools.ConvertStringToDateTime(dt);
                ret = true;
            }
            catch 
            {
            }
            if (!ret)
            {
                try
                {
                    date = DateTime.Parse(dt);
                    ret = true;
                }
                catch
                {
                }
            }
            return ret;
        }

    }
}
