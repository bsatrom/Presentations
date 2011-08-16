// <copyright file="GlobalSuppressions.cs" company="Microsoft Corporation">
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>

// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.
//
// To add a suppression to this file, right-click the message in the 
// Error List, point to "Suppress Message(s)", and click 
// "In Project Suppression File".
// You do not need to add suppressions to this file manually.

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA2210:AssembliesShouldHaveValidStrongNames", Justification = "Strong name signing not required for codeplex.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "v", Scope = "member", Target = "Microsoft.ServiceModel.WebSockets.Samples.WebSocketVersion.#v76", Justification = "Lowercase 'v' is more appropriate to describe a version abbreviation.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "v", Scope = "member", Target = "Microsoft.ServiceModel.WebSockets.Samples.WebSocketVersion.#v75", Justification = "Lowercase 'v' is more appropriate to describe a version abbreviation.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Scope = "member", Target = "Microsoft.ServiceModel.WebSockets.Samples.Program.#timer", Justification = "Timer is used on assign.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "Microsoft.ServiceModel.WebSockets.Samples.Program.#AddClient(System.Object)", Justification = "For sample purpose: client must not be disposed, otherwise communication won't happen.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Microsoft.ServiceModel.WebSockets.Samples.Client.#WriteDataCallback(System.IAsyncResult)", Justification = "For sample purpose: On any exception, clear the faulty client.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Microsoft.ServiceModel.WebSockets.Samples.Client.#WriteData(System.Object)", Justification = "For sample purpose: On any exception, clear the faulty client.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Microsoft.ServiceModel.WebSockets.Samples.Client.#ReadDataCallback(System.IAsyncResult)", Justification = "For sample purpose: On any exception, clear the faulty client.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Microsoft.ServiceModel.WebSockets.Samples.Client.#Communicate()", Justification = "For sample purpose: On any exception, clear the faulty client.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Console.WriteLine(System.String)", Scope = "member", Target = "Microsoft.ServiceModel.WebSockets.Samples.Program.#Main()", Justification = "For sample purpose: No localization.")]
