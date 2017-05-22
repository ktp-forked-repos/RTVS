﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Microsoft.R.Editor {
    using System;
    using System.Reflection;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Microsoft.R.Editor.Resources", typeof(Resources).GetTypeInfo().Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Automatic formatting.
        /// </summary>
        internal static string AutoFormat {
            get {
                return ResourceManager.GetString("AutoFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Comment selection.
        /// </summary>
        internal static string CommentSelection {
            get {
                return ResourceManager.GetString("CommentSelection", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ’&lt;-’ should always be used for assignment.
        /// </summary>
        internal static string Lint_Assignment {
            get {
                return ResourceManager.GetString("Lint_Assignment", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Objects names should not be in camelCase.
        /// </summary>
        internal static string Lint_CamelCase {
            get {
                return ResourceManager.GetString("Lint_CamelCase", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Closing curly brace should be on its own line unless it is followed by an &apos;else&apos;.
        /// </summary>
        internal static string Lint_CloseCurlySeparateLine {
            get {
                return ResourceManager.GetString("Lint_CloseCurlySeparateLine", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Comma should be followed by space, but should not have space before it.
        /// </summary>
        internal static string Lint_CommaSpaces {
            get {
                return ResourceManager.GetString("Lint_CommaSpaces", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Only double quotes should used to delimit strings..
        /// </summary>
        internal static string Lint_DoubleQuotes {
            get {
                return ResourceManager.GetString("Lint_DoubleQuotes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Line is {0} characters long. Max {1} characters is recommended..
        /// </summary>
        internal static string Lint_LineTooLong {
            get {
                return ResourceManager.GetString("Lint_LineTooLong", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Objects names should not have.multiple.dots.
        /// </summary>
        internal static string Lint_MultileDots {
            get {
                return ResourceManager.GetString("Lint_MultileDots", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Multiple statements in line.
        /// </summary>
        internal static string Lint_MultipleStatementsInLine {
            get {
                return ResourceManager.GetString("Lint_MultipleStatementsInLine", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Objects name is {0} characters long. Max {1} characters is recommended..
        /// </summary>
        internal static string Lint_NameTooLong {
            get {
                return ResourceManager.GetString("Lint_NameTooLong", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Insert space between ] or ]] and preceding a comma.
        /// </summary>
        internal static string Lint_NoSpaceBetweenCommaAndClosingBracket {
            get {
                return ResourceManager.GetString("Lint_NoSpaceBetweenCommaAndClosingBracket", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Opening curly brace must not be on its own line and should always be followed by a newline.
        /// </summary>
        internal static string Lint_OpenCurlyPosition {
            get {
                return ResourceManager.GetString("Lint_OpenCurlyPosition", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Infix operators should have spaces around them..
        /// </summary>
        internal static string Lint_OperatorSpaces {
            get {
                return ResourceManager.GetString("Lint_OperatorSpaces", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Objects names should not be in PascalCase.
        /// </summary>
        internal static string Lint_PascalCase {
            get {
                return ResourceManager.GetString("Lint_PascalCase", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Use of semicolons is not recommended.
        /// </summary>
        internal static string Lint_Semicolons {
            get {
                return ResourceManager.GetString("Lint_Semicolons", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Object names should not be in snake_case..
        /// </summary>
        internal static string Lint_SnakeCase {
            get {
                return ResourceManager.GetString("Lint_SnakeCase", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There should not be spaces between function name and opening brace..
        /// </summary>
        internal static string Lint_SpaceAfterFunctionName {
            get {
                return ResourceManager.GetString("Lint_SpaceAfterFunctionName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There should not be spaces after (, [ or [[.
        /// </summary>
        internal static string Lint_SpaceAfterLeftParenthesis {
            get {
                return ResourceManager.GetString("Lint_SpaceAfterLeftParenthesis", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There should not be spaces before ).
        /// </summary>
        internal static string Lint_SpaceBeforeClosingBrace {
            get {
                return ResourceManager.GetString("Lint_SpaceBeforeClosingBrace", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Opening brace should have a space before it unless it is in a function call.
        /// </summary>
        internal static string Lint_SpaceBeforeOpenBrace {
            get {
                return ResourceManager.GetString("Lint_SpaceBeforeOpenBrace", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Files should not contain tabs.
        /// </summary>
        internal static string Lint_Tabs {
            get {
                return ResourceManager.GetString("Lint_Tabs", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to There should not be trailing blank lines in the file..
        /// </summary>
        internal static string Lint_TrailingBlankLines {
            get {
                return ResourceManager.GetString("Lint_TrailingBlankLines", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Lines should not contain trailing whitespace characters..
        /// </summary>
        internal static string Lint_TrailingWhitespace {
            get {
                return ResourceManager.GetString("Lint_TrailingWhitespace", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Objects names should not be in all uppercase.
        /// </summary>
        internal static string Lint_Uppercase {
            get {
                return ResourceManager.GetString("Lint_Uppercase", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ) expected.
        /// </summary>
        internal static string ParseError_CloseBraceExpected {
            get {
                return ResourceManager.GetString("ParseError_CloseBraceExpected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to } expected.
        /// </summary>
        internal static string ParseError_CloseCurlyBraceExpected {
            get {
                return ResourceManager.GetString("ParseError_CloseCurlyBraceExpected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ] or ]] expected.
        /// </summary>
        internal static string ParseError_CloseSquareBracketExpected {
            get {
                return ResourceManager.GetString("ParseError_CloseSquareBracketExpected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Expression expected.
        /// </summary>
        internal static string ParseError_ExpressionExpected {
            get {
                return ResourceManager.GetString("ParseError_ExpressionExpected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Function body expected.
        /// </summary>
        internal static string ParseError_FunctionBodyExpected {
            get {
                return ResourceManager.GetString("ParseError_FunctionBodyExpected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Function expected.
        /// </summary>
        internal static string ParseError_FunctionExpected {
            get {
                return ResourceManager.GetString("ParseError_FunctionExpected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Syntax error.
        /// </summary>
        internal static string ParseError_General {
            get {
                return ResourceManager.GetString("ParseError_General", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Identifier expected.
        /// </summary>
        internal static string ParseError_IndentifierExpected {
            get {
                return ResourceManager.GetString("ParseError_IndentifierExpected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;in&apos; expected.
        /// </summary>
        internal static string ParseError_InKeywordExpected {
            get {
                return ResourceManager.GetString("ParseError_InKeywordExpected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Operand to the left expected.
        /// </summary>
        internal static string ParseError_LeftOperandExpected {
            get {
                return ResourceManager.GetString("ParseError_LeftOperandExpected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Logical expected.
        /// </summary>
        internal static string ParseError_LogicalExpected {
            get {
                return ResourceManager.GetString("ParseError_LogicalExpected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Number expected.
        /// </summary>
        internal static string ParseError_NumberExpected {
            get {
                return ResourceManager.GetString("ParseError_NumberExpected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ( expected.
        /// </summary>
        internal static string ParseError_OpenBraceExpected {
            get {
                return ResourceManager.GetString("ParseError_OpenBraceExpected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to { expected.
        /// </summary>
        internal static string ParseError_OpenCurlyBraceExpected {
            get {
                return ResourceManager.GetString("ParseError_OpenCurlyBraceExpected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to [ or [[ expected.
        /// </summary>
        internal static string ParseError_OpenSquareBracketExpected {
            get {
                return ResourceManager.GetString("ParseError_OpenSquareBracketExpected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Operator expected.
        /// </summary>
        internal static string ParseError_OperatorExpected {
            get {
                return ResourceManager.GetString("ParseError_OperatorExpected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Operand to the right expected.
        /// </summary>
        internal static string ParseError_RightOperandExpected {
            get {
                return ResourceManager.GetString("ParseError_RightOperandExpected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to String expected.
        /// </summary>
        internal static string ParseError_StringExpected {
            get {
                return ResourceManager.GetString("ParseError_StringExpected", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unexpected end of file.
        /// </summary>
        internal static string ParseError_UnexpectedEndOfFile {
            get {
                return ResourceManager.GetString("ParseError_UnexpectedEndOfFile", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unexpected token.
        /// </summary>
        internal static string ParseError_UnexpectedToken {
            get {
                return ResourceManager.GetString("ParseError_UnexpectedToken", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Uncomment selection.
        /// </summary>
        internal static string UncommentSelection {
            get {
                return ResourceManager.GetString("UncommentSelection", resourceCulture);
            }
        }
    }
}
