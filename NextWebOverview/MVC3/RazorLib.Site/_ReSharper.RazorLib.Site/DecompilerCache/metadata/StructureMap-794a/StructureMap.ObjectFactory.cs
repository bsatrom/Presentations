// Type: StructureMap.ObjectFactory
// Assembly: StructureMap, Version=2.6.2.0, Culture=neutral, PublicKeyToken=e60ad81abae3c223
// Assembly location: C:\Development\RazorLib\lib\structuremap\StructureMap.dll

using StructureMap.Pipeline;
using StructureMap.Query;
using System;
using System.Collections;
using System.Collections.Generic;

namespace StructureMap
{
    public static class ObjectFactory
    {
        public static IModel Model { get; }
        public static IContainer Container { get; }
        public static string Profile { get; set; }
        public static void ReleaseAndDisposeAllHttpScopedObjects();
        public static void Initialize(Action<IInitializationExpression> action);
        public static void Inject(Type pluginType, object instance);
        public static void Inject<PLUGINTYPE>(PLUGINTYPE instance);
        public static string WhatDoIHave();
        public static void AssertConfigurationIsValid();
        public static object GetInstance(Type pluginType);
        public static PLUGINTYPE GetInstance<PLUGINTYPE>();
        public static object GetInstance(Type targetType, Instance instance);
        public static T GetInstance<T>(Instance instance);
        public static object GetNamedInstance(Type pluginType, string name);
        public static PLUGINTYPE GetNamedInstance<PLUGINTYPE>(string name);
        public static IList GetAllInstances(Type pluginType);
        public static IList<PLUGINTYPE> GetAllInstances<PLUGINTYPE>();
        public static ExplicitArgsExpression With<T>(T arg);
        public static IExplicitProperty With(string argName);
        public static ExplicitArgsExpression With(Type pluginType, object arg);
        public static void EjectAllInstancesOf<T>();
        public static T GetInstance<T>(ExplicitArguments args);
        public static object TryGetInstance(Type pluginType, string instanceKey);
        public static object TryGetInstance(Type pluginType);
        public static T TryGetInstance<T>();
        public static T TryGetInstance<T>(string instanceKey);
        public static void BuildUp(object target);
        public static Container.OpenGenericTypeExpression ForGenericType(Type templateType);
        public static CloseGenericTypeExpression ForObject(object subject);
        public static void Configure(Action<ConfigurationExpression> configure);
        public static void ResetDefaults();
        public static event Notify Refresh;
    }
}
