﻿namespace System.CLI
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Linq;

    /// <summary>
    ///   Class for parsing command line arguments
    /// </summary>
    public static class CommandLine
    {
        #region Constants and Fields

        public static readonly string[] DefaultSwitchSeparators = new[] { "/", "-" };

        public static readonly string[] DefaultValueSeparators = new[] { ":", "=" };

        internal const string PositionalValueGroup = "PositionalValue";

        internal const string SwitchNameGroup = "SwitchName";

        internal const string SwitchOptionGroup = "SwitchOption";

        internal const string SwitchSeperatorGroup = "SwitchSeperator";

        internal const string ValueGroup = "Value";

        internal const string ValueSeperatorGroup = "ValueSeperator";

        /// <summary>
        ///   Expression for a switch with a value i.e. /S:Value or /S:Some Value
        /// </summary>
        /// <remarks>
        /// This expression divides the token into groups
        /// </remarks>
        private const string TokenizeExpressionFormat =
            @"(?{0}i) # Case Sensitive Option
# Capture the switch begin of string or preceeded by whitespace
(?<SwitchSeperator>\A[{1}])
# Capture the switch name
(?<SwitchName>[^{2}+-]+) 
# Capture switch option or end of string
(?<SwitchOption>[{2}+-]|\z) 
# Capture the switch value or end of string 
(?<Value>.*)\Z";

        private static string[] args;

        private static ICommandEnvironment commandEnvironment = new CommandEnvironment();

        private static List<string> commandSeparatorList = new List<string>(DefaultSwitchSeparators);

        private static CommandLineParametersCollection parameters;

        private static List<string> valueSeparatorList = new List<string>(DefaultValueSeparators);

        #endregion

        #region Properties

        public static string[] Args
        {
            get
            {
                // GetCommandLineArgs puts the program in element 0
                // The args passed to Main do not do this so remove it
                var commandLineArgs = CommandEnvironment.GetCommandLineArgs();
                args = new string[commandLineArgs.Length - 1];
                for (var i = 0; i < commandLineArgs.Length - 1; i++)
                {
                    args[i] = commandLineArgs[i + 1];
                }

                return args;
            }
            internal set
            {
                args = value;
            }
        }

        public static bool CaseSensitive { get; set; }

        public static ICommandEnvironment CommandEnvironment
        {
            get
            {
                return commandEnvironment;
            }
            set
            {
                commandEnvironment = value;
            }
        }

        public static IEnumerable<string> CommandSeparators
        {
            get
            {
                return commandSeparatorList;
            }
            set
            {
                commandSeparatorList = new List<string>(value);
            }
        }

        public static string Program
        {
            get
            {
                return CommandEnvironment.GetCommandLineArgs()[0];
            }
        }

        public static string Text
        {
            get
            {
                return CommandEnvironment.CommandLine;
            }
        }

        public static IEnumerable<string> ValueSeparators
        {
            get
            {
                return valueSeparatorList;
            }
            set
            {
                valueSeparatorList = new List<string>(value);
            }
        }

        private static Regex RegexTokenize
        {
            get
            {
                return new Regex(TokenizePattern, RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
            }
        }

        private static string TokenizePattern
        {
            get
            {
                return string.Format(TokenizeExpressionFormat, GetCaseSensitiveOption(), string.Join("", CommandSeparators), string.Join("", ValueSeparators));
            }
        }

        #endregion

        #region Public Methods

        public static List<CommandArgument> GetParameters(List<CommandArgument> tokens)
        {
            return tokens == null ? null : tokens.FindAll(arg => arg.IsParameter());
        }

        public static List<CommandArgument> GetSwitches(List<CommandArgument> tokens)
        {
            return tokens == null ? null : tokens.FindAll(arg => arg.IsCommand());
        }

        public static T Parse<T>() where T : class, new()
        {
            Debug.WriteLine(string.Format("Parsing argument class [{0}] Command Line: [{1}]", typeof(T), Text));

            var argument = InitializeNewArgument<T>();

            var tokens = Tokenize();

            foreach (var commandArgument in tokens)
            {
                ApplyCommandArgument(commandArgument, argument);
            }

            // Some commands signify a request for command line help
            if (parameters.Values.Any(p => p.Attribute.IsHelp && p.ArgumentSupplied))
            {
                throw new CommandLineHelpException(new CommandArgumentHelp(typeof(T)));
            }

            parameters.VerifyRequiredArguments();

            return argument;
        }

        //public static void Pause(string text = "Press any key to continue...", ConsoleColor color = ConsoleColor.Yellow)
        //{
        //    WriteLineColor(color, text);
        //    Console.ReadKey(true);
        //}

        //public static char PromptKey(string prompt, params char[] allowedKeys)
        //{
        //    if (allowedKeys == null)
        //    {
        //        throw new ArgumentNullException("allowedKeys");
        //    }

        //    char keyChar;
        //    bool validKey;
        //    var allowedString = ToDelimitedList(allowedKeys);
        //    do
        //    {
        //        Console.Write("{0} ({1}) ", prompt, allowedString);
        //        keyChar = ToLower(Console.ReadKey(false).KeyChar);
        //        validKey = allowedKeys.Contains(keyChar);
        //        if (!validKey)
        //        {
        //            Console.WriteLine("\r\n\"{0}\" is not a valid choice, valid keys are \"{1}\"", keyChar, allowedString);
        //        }
        //        else
        //        {
        //            Console.WriteLine();
        //        }
        //    }
        //    while (!validKey);

        //    return keyChar;
        //}

        public static List<CommandArgument> Tokenize()
        {
            var tokenList = new List<CommandArgument>();

            if (string.IsNullOrWhiteSpace(Text))
            {
                return tokenList;
            }

            var nextPosition = 1;

            DumpTokens();

            tokenList.AddRange(
                from arg in Args
                let matches = RegexTokenize.Matches(arg)
                select matches.Count == 1
                           ? new CommandArgument(matches[0]) // Command argument
                           : new CommandArgument(arg, nextPosition++));

            return tokenList;
        }

        //public static void WriteLineColor(ConsoleColor color, string format, params object[] formatArgs)
        //{
        //    var saveColor = Console.ForegroundColor;
        //    Console.ForegroundColor = color;
        //    Console.WriteLine(format, formatArgs);
        //    Console.ForegroundColor = saveColor;
        //}

        #endregion

        #region Methods

        private static void ApplyCommandArgument(CommandArgument cmd, object argument)
        {
            var parameter = parameters.Get(cmd);

            // No command parameter matching this command switch or position
            if (parameter == null)
            {
                throw new CommandLineArgumentInvalidException(argument.GetType(), cmd);
            }

            parameter.SetValue(argument, cmd);
        }

        [Conditional("DEBUG")]
        private static void DumpTokens()
        {
            Debug.WriteLine(string.Format("\r\nRegex Pattern <{0}>", TokenizePattern));

            for (var index = 0; index < Args.Length; index++)
            {
                var arg = Args[index];
                Debug.WriteLine(string.Format("\r\nTokenizing Args[{0}] Value: \"{1}\"", index, Args[index]));

                var matches = RegexTokenize.Matches(arg);

                for (var i = 0; i < matches.Count; i++)
                {
                    for (var j = 0; j < matches[i].Groups.Count; j++)
                    {
                        Debug.WriteLine(string.Format("Token[{0}].Groups[{1}] =\"{2}\"", i, RegexTokenize.GroupNameFromNumber(j), matches[i].Groups[j]));
                    }
                }
            }
        }

        /// <summary>
        ///   Returns a string with the case sensitive option
        /// </summary>
        /// <returns>null when case sensitive is on, "-" when it is off</returns>
        private static string GetCaseSensitiveOption()
        {
            return CaseSensitive ? null : "-";
        }

        private static IEnumerable<CommandLineParameterAttribute> InferCommandLineParameterAttribute(PropertyInfo property)
        {
            return new[] { new CommandLineParameterAttribute { Name = property.Name, Command = property.Name } };
        }

        private static T InitializeNewArgument<T>() where T : new()
        {
            parameters = new CommandLineParametersCollection(typeof(T));

            var argument = new T();

            foreach (var parameter in parameters.Values)
            {
                parameter.SetDefaultValue(argument);
            }

            return argument;
        }

        private static string ToDelimitedList(char[] allowedKeys)
        {
            var sb = new StringBuilder();
            for (var index = 0; index < allowedKeys.Length; index++)
            {
                var allowedKey = allowedKeys[index];
                sb.Append(allowedKey);
                if (index + 1 < allowedKeys.Length)
                {
                    sb.Append(',');
                }
            }
            return sb.ToString();
        }

        private static char ToLower(char keyChar)
        {
            return keyChar.ToString().ToLowerInvariant()[0];
        }

        #endregion

        private class CommandLineParametersCollection
        {
            #region Constructors and Destructors

            internal CommandLineParametersCollection(Type argumentType)
            {
                this.Parameters = new Dictionary<string, CommandLineParameter>();
                this.ArgumentType = argumentType;
                this.Load();
            }

            #endregion

            #region Properties

            public IEnumerable<CommandLineParameter> Values
            {
                get
                {
                    return this.Parameters.Values;
                }
            }

            private Type ArgumentType { get; set; }

            private Dictionary<string, CommandLineParameter> Parameters { get; set; }

            #endregion

            #region Public Methods

            public CommandLineParameter Get(CommandArgument cmd)
            {
                CommandLineParameter parameter;

                this.Parameters.TryGetValue(cmd.Key, out parameter);

                return parameter;
            }

            public void VerifyRequiredArguments()
            {
                foreach (var parameter in this.Values.Where(parameter => !parameter.RequiredArgumentSupplied))
                {
                    throw new CommandLineRequiredArgumentMissingException(this.ArgumentType, parameter.Attribute.NameOrCommand, parameter.Attribute.ParameterIndex);
                }
            }

            #endregion

            #region Methods

            private static bool IsParameter(CommandLineParameter parameter)
            {
                return parameter.IsParameter();
            }

            private static int ParameterIndex(CommandLineParameter parameter)
            {
                return parameter.Attribute.ParameterIndex;
            }

            private void Add(CommandLineParameter parameter)
            {
                // Each attribute is uniquely keyed
                try
                {
                    this.Parameters.Add(parameter.Key, parameter);
                }
                catch (ArgumentException exception)
                {
                    throw new CommandLineException(
                        new CommandArgumentHelp(
                            parameter.Property.DeclaringType,
                            parameter.IsCommand()
                                ? string.Format("Duplicate Command \"{0}\"", parameter.Command)
                                : string.Format("Duplicate Parameter Index [{0}] on Property \"{1}\"", parameter.Attribute.ParameterIndex, parameter.Property.Name)),
                        exception);
                }
            }

            private void Load()
            {
                // Select all the CommandLineParameterAttribute attributes from all the properties on the type and create a command line parameter for it
                CommandLineParameterAttribute.ForEach(this.ArgumentType, this.Add);

                this.VerifyPositionalArgumentsInSequence();

                // If there are no attributed properties
                if (this.Parameters.Count == 0)
                {
                    // infer them based on the property names
                    CommandLineParameterAttribute.ForEach(this.ArgumentType, InferCommandLineParameterAttribute, this.Add);
                }
            }

            private void VerifyPositionalArgumentsInSequence()
            {
                // Get the positional arguments ordered by position
                var parameters = this.Parameters.Values.Where(IsParameter).OrderBy(ParameterIndex);

                for (var i = 0; i < parameters.Count(); i++)
                {
                    var arg = parameters.ElementAt(i);

                    // Parameter Indexes are 1 based so add 1 to i
                    var expectedIndex = i + 1;
                    if (arg.Attribute.ParameterIndex != expectedIndex)
                    {
                        throw new CommandLineException(
                            new CommandArgumentHelp(
                                arg.Property.DeclaringType,
                                string.Format(
                                    "Out of order parameter \"{0}\" should have be at parameter index {1} but was found at {2}",
                                    arg.Attribute.Name,
                                    expectedIndex,
                                    arg.Attribute.ParameterIndex)));
                    }
                }
            }

            #endregion
        }
    }
}