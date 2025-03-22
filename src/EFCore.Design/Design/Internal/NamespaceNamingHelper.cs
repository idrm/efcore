// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Microsoft.EntityFrameworkCore.Design.Internal;

/// <summary>
///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
///     the same compatibility standards as public APIs. It may be changed or removed without notice in
///     any release. You should only use it directly in your code with extreme caution and knowing that
///     doing so can result in application failures when updating to a new Entity Framework Core release.
/// </summary>
public static class NamespaceNamingHelper
{
    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public static string CreateFullyQualifiedName(string rootNamespace, string typeName)
    {
        return typeName.Contains("_")
        ? $"{rootNamespace}.{(
                typeName.Substring(0, typeName.IndexOf("_", StringComparison.Ordinal))
            )}.{(
                typeName.Substring(typeName.IndexOf("_", StringComparison.Ordinal) + 1)
            )}"
        : $"{rootNamespace}.{typeName}";
    }

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public static string CreateBaseNamespace(string rootNamespace, string typeName)
    {
        return typeName.Contains("_") ? $"{rootNamespace}.{typeName.Substring(0, typeName.IndexOf("_", StringComparison.Ordinal))}" : rootNamespace;
    }

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public static string CreateBaseEntityName(string typeName)
    {
        return typeName.Contains("_") ? typeName.Substring(typeName.IndexOf("_", StringComparison.Ordinal) + 1) : typeName;
    }

    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public static string CreateSchemaName(string typeName)
    {
        return typeName.Contains("_") ? typeName.Substring(0, typeName.IndexOf("_", StringComparison.Ordinal)) : "";
    }
}
