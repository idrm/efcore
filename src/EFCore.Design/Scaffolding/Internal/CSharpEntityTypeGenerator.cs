﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 17.0.0.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace Microsoft.EntityFrameworkCore.Scaffolding.Internal
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using Microsoft.EntityFrameworkCore.Design.Internal;

    /// <summary>
    /// Class to produce the template output
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public partial class CSharpEntityTypeGenerator : CSharpEntityTypeGeneratorBase
    {
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {

    if (EntityType.IsSimpleManyToManyJoinEntityType())
    {
        // Don't scaffold these
        return "";
    }

    var services = (IServiceProvider)Host;
    var annotationCodeGenerator = services.GetRequiredService<IAnnotationCodeGenerator>();
    var code = services.GetRequiredService<ICSharpHelper>();

    var usings = new List<string>
    {
        "System",
        "System.Collections.Generic"
    };

    if (Options.UseDataAnnotations)
    {
        usings.Add("System.ComponentModel.DataAnnotations");
        usings.Add("System.ComponentModel.DataAnnotations.Schema");
        usings.Add("Microsoft.EntityFrameworkCore");
    }

    if (!string.IsNullOrEmpty(NamespaceHint))
    {

            this.Write("namespace ");
            this.Write(NamespaceNamingHelper.CreateBaseNamespace(NamespaceHint, EntityType.Name));
            this.Write(";\r\n\r\n");

    }

    if (!string.IsNullOrEmpty(EntityType.GetComment()))
    {

            this.Write("/// <summary>\r\n/// ");
            this.Write(this.ToStringHelper.ToStringWithCulture(code.XmlComment(EntityType.GetComment())));
            this.Write("\r\n/// </summary>\r\n");

    }

    if (Options.UseDataAnnotations)
    {
        foreach (var dataAnnotation in EntityType.GetDataAnnotations(annotationCodeGenerator))
        {

            this.Write(this.ToStringHelper.ToStringWithCulture(code.Fragment(dataAnnotation)));
            this.Write("\r\n");

        }
    }

            this.Write("public partial class ");
            this.Write(NamespaceNamingHelper.CreateBaseEntityName(EntityType.Name));
            this.Write("\r\n{\r\n");

    var firstProperty = true;
    foreach (var property in EntityType.GetProperties().OrderBy(p => p.GetColumnOrder() ?? -1))
    {
        if (!firstProperty)
        {
            WriteLine("");
        }

        if (!string.IsNullOrEmpty(property.GetComment()))
        {

            this.Write("    /// <summary>\r\n    /// ");
            this.Write(this.ToStringHelper.ToStringWithCulture(code.XmlComment(property.GetComment(), indent: 1)));
            this.Write("\r\n    /// </summary>\r\n");

        }

        if (Options.UseDataAnnotations)
        {
            var dataAnnotations = property.GetDataAnnotations(annotationCodeGenerator)
                .Where(a => !(a.Type == typeof(RequiredAttribute) && Options.UseNullableReferenceTypes && !property.ClrType.IsValueType));
            foreach (var dataAnnotation in dataAnnotations)
            {

            this.Write("    ");
            this.Write(this.ToStringHelper.ToStringWithCulture(code.Fragment(dataAnnotation)));
            this.Write("\r\n");

            }
        }

        usings.AddRange(code.GetRequiredUsings(property.ClrType));

        var needsNullable = Options.UseNullableReferenceTypes && property.IsNullable && !property.ClrType.IsValueType;
        var needsInitializer = Options.UseNullableReferenceTypes && !property.IsNullable && !property.ClrType.IsValueType;

            this.Write("    public ");
            this.Write(this.ToStringHelper.ToStringWithCulture(code.Reference(property.ClrType)));
            this.Write(this.ToStringHelper.ToStringWithCulture(needsNullable ? "?" : ""));
            this.Write(" ");
            this.Write(this.ToStringHelper.ToStringWithCulture(property.Name));
            this.Write(" { get; set; }");
            this.Write(this.ToStringHelper.ToStringWithCulture(needsInitializer ? " = null!;" : ""));
            this.Write("\r\n");

        firstProperty = false;
    }

    foreach (var navigation in EntityType.GetNavigations())
    {
        WriteLine("");

        if (Options.UseDataAnnotations)
        {
            foreach (var dataAnnotation in navigation.GetDataAnnotations(annotationCodeGenerator))
            {

            this.Write("    ");
            this.Write(this.ToStringHelper.ToStringWithCulture(code.Fragment(dataAnnotation)));
            this.Write("\r\n");

            }
        }

        var targetType = navigation.TargetEntityType.Name;
        if (navigation.IsCollection)
        {

            this.Write("    public virtual ICollection<");
            this.Write(NamespaceNamingHelper.CreateFullyQualifiedName(NamespaceHint, targetType));
            this.Write("> ");
            this.Write(NamespaceNamingHelper.CreateBaseEntityName(navigation.Name));
            this.Write(" { get; set; } = new List<");
            this.Write(NamespaceNamingHelper.CreateFullyQualifiedName(NamespaceHint, targetType));
            this.Write(">();\r\n");

        }
        else
        {
            var needsNullable = Options.UseNullableReferenceTypes && !(navigation.ForeignKey.IsRequired && navigation.IsOnDependent);
            var needsInitializer = Options.UseNullableReferenceTypes && navigation.ForeignKey.IsRequired && navigation.IsOnDependent;

            this.Write("    public virtual ");
            this.Write(NamespaceNamingHelper.CreateFullyQualifiedName(NamespaceHint, targetType));
            this.Write(this.ToStringHelper.ToStringWithCulture(needsNullable ? "?" : ""));
            this.Write(" ");
            this.Write(NamespaceNamingHelper.CreateBaseEntityName(navigation.Name));
            this.Write(" { get; set; }");
            this.Write(this.ToStringHelper.ToStringWithCulture(needsInitializer ? " = null!;" : ""));
            this.Write("\r\n");

        }
    }

    foreach (var skipNavigation in EntityType.GetSkipNavigations())
    {
        WriteLine("");

        if (Options.UseDataAnnotations)
        {
            foreach (var dataAnnotation in skipNavigation.GetDataAnnotations(annotationCodeGenerator))
            {

            this.Write("    ");
            this.Write(this.ToStringHelper.ToStringWithCulture(code.Fragment(dataAnnotation)));
            this.Write("\r\n");

            }
        }

            this.Write("    public virtual ICollection<");
            this.Write(NamespaceNamingHelper.CreateFullyQualifiedName(NamespaceHint, skipNavigation.TargetEntityType.Name));
            this.Write("> ");
            this.Write(NamespaceNamingHelper.CreateBaseEntityName(skipNavigation.Name));
            this.Write(" { get; set; } = new List<");
            this.Write(NamespaceNamingHelper.CreateFullyQualifiedName(NamespaceHint, skipNavigation.TargetEntityType.Name));
            this.Write(">();\r\n");

    }

            this.Write("}\r\n");

    var previousOutput = GenerationEnvironment;
    GenerationEnvironment = new StringBuilder();

    foreach (var ns in usings.Distinct().OrderBy(x => x, new NamespaceComparer()))
    {

            this.Write("using ");
            this.Write(this.ToStringHelper.ToStringWithCulture(ns));
            this.Write(";\r\n");

    }

    WriteLine("");

    GenerationEnvironment.Append(previousOutput);

            return this.GenerationEnvironment.ToString();
        }
        private global::Microsoft.VisualStudio.TextTemplating.ITextTemplatingEngineHost hostValue;
        /// <summary>
        /// The current host for the text templating engine
        /// </summary>
        public virtual global::Microsoft.VisualStudio.TextTemplating.ITextTemplatingEngineHost Host
        {
            get
            {
                return this.hostValue;
            }
            set
            {
                this.hostValue = value;
            }
        }

private global::Microsoft.EntityFrameworkCore.Metadata.IEntityType _EntityTypeField;

/// <summary>
/// Access the EntityType parameter of the template.
/// </summary>
private global::Microsoft.EntityFrameworkCore.Metadata.IEntityType EntityType
{
    get
    {
        return this._EntityTypeField;
    }
}

private global::Microsoft.EntityFrameworkCore.Scaffolding.ModelCodeGenerationOptions _OptionsField;

/// <summary>
/// Access the Options parameter of the template.
/// </summary>
private global::Microsoft.EntityFrameworkCore.Scaffolding.ModelCodeGenerationOptions Options
{
    get
    {
        return this._OptionsField;
    }
}

private string _NamespaceHintField;

/// <summary>
/// Access the NamespaceHint parameter of the template.
/// </summary>
private string NamespaceHint
{
    get
    {
        return this._NamespaceHintField;
    }
}


/// <summary>
/// Initialize the template
/// </summary>
public virtual void Initialize()
{
    if ((this.Errors.HasErrors == false))
    {
bool EntityTypeValueAcquired = false;
if (this.Session.ContainsKey("EntityType"))
{
    this._EntityTypeField = ((global::Microsoft.EntityFrameworkCore.Metadata.IEntityType)(this.Session["EntityType"]));
    EntityTypeValueAcquired = true;
}
if ((EntityTypeValueAcquired == false))
{
    string parameterValue = this.Host.ResolveParameterValue("Property", "PropertyDirectiveProcessor", "EntityType");
    if ((string.IsNullOrEmpty(parameterValue) == false))
    {
        global::System.ComponentModel.TypeConverter tc = global::System.ComponentModel.TypeDescriptor.GetConverter(typeof(global::Microsoft.EntityFrameworkCore.Metadata.IEntityType));
        if (((tc != null)
                    && tc.CanConvertFrom(typeof(string))))
        {
            this._EntityTypeField = ((global::Microsoft.EntityFrameworkCore.Metadata.IEntityType)(tc.ConvertFrom(parameterValue)));
            EntityTypeValueAcquired = true;
        }
        else
        {
            this.Error("The type \'Microsoft.EntityFrameworkCore.Metadata.IEntityType\' of the parameter \'E" +
                    "ntityType\' did not match the type of the data passed to the template.");
        }
    }
}
if ((EntityTypeValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("EntityType");
    if ((data != null))
    {
        this._EntityTypeField = ((global::Microsoft.EntityFrameworkCore.Metadata.IEntityType)(data));
    }
}
bool OptionsValueAcquired = false;
if (this.Session.ContainsKey("Options"))
{
    this._OptionsField = ((global::Microsoft.EntityFrameworkCore.Scaffolding.ModelCodeGenerationOptions)(this.Session["Options"]));
    OptionsValueAcquired = true;
}
if ((OptionsValueAcquired == false))
{
    string parameterValue = this.Host.ResolveParameterValue("Property", "PropertyDirectiveProcessor", "Options");
    if ((string.IsNullOrEmpty(parameterValue) == false))
    {
        global::System.ComponentModel.TypeConverter tc = global::System.ComponentModel.TypeDescriptor.GetConverter(typeof(global::Microsoft.EntityFrameworkCore.Scaffolding.ModelCodeGenerationOptions));
        if (((tc != null)
                    && tc.CanConvertFrom(typeof(string))))
        {
            this._OptionsField = ((global::Microsoft.EntityFrameworkCore.Scaffolding.ModelCodeGenerationOptions)(tc.ConvertFrom(parameterValue)));
            OptionsValueAcquired = true;
        }
        else
        {
            this.Error("The type \'Microsoft.EntityFrameworkCore.Scaffolding.ModelCodeGenerationOptions\' o" +
                    "f the parameter \'Options\' did not match the type of the data passed to the templ" +
                    "ate.");
        }
    }
}
if ((OptionsValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("Options");
    if ((data != null))
    {
        this._OptionsField = ((global::Microsoft.EntityFrameworkCore.Scaffolding.ModelCodeGenerationOptions)(data));
    }
}
bool NamespaceHintValueAcquired = false;
if (this.Session.ContainsKey("NamespaceHint"))
{
    this._NamespaceHintField = ((string)(this.Session["NamespaceHint"]));
    NamespaceHintValueAcquired = true;
}
if ((NamespaceHintValueAcquired == false))
{
    string parameterValue = this.Host.ResolveParameterValue("Property", "PropertyDirectiveProcessor", "NamespaceHint");
    if ((string.IsNullOrEmpty(parameterValue) == false))
    {
        global::System.ComponentModel.TypeConverter tc = global::System.ComponentModel.TypeDescriptor.GetConverter(typeof(string));
        if (((tc != null)
                    && tc.CanConvertFrom(typeof(string))))
        {
            this._NamespaceHintField = ((string)(tc.ConvertFrom(parameterValue)));
            NamespaceHintValueAcquired = true;
        }
        else
        {
            this.Error("The type \'System.String\' of the parameter \'NamespaceHint\' did not match the type " +
                    "of the data passed to the template.");
        }
    }
}
if ((NamespaceHintValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("NamespaceHint");
    if ((data != null))
    {
        this._NamespaceHintField = ((string)(data));
    }
}


    }
}


    }
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "17.0.0.0")]
    public class CSharpEntityTypeGeneratorBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0)
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
