// <copyright>
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
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "Websocket", Scope = "member", Target = "Microsoft.ServiceModel.WebSockets.Samples.Program.#Main()", Justification = "Literal needs to be added to dictionary.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "EchoService", Scope = "member", Target = "Microsoft.ServiceModel.WebSockets.Samples.Program.#Main()", Justification = "Literal needs to be added to dictionary.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "clientaccesspolicy", Scope = "member", Target = "Microsoft.ServiceModel.WebSockets.Samples.Program.#Main()", Justification = "Literal needs to be added to dictionary.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Console.WriteLine(System.String)", Scope = "member", Target = "Microsoft.ServiceModel.WebSockets.Samples.Program.#Main()", Justification = "For sample purpose: No localization.")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "Microsoft.ServiceModel.WebSockets.Samples.Program.#Main()", Justification = "sh.Close() is called in place of Dispose()")]
