using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Qubiz.QuizEngine.Infrastructure
{
    public static class Mapper
    {
		public static TDestination DeepCopyTo<TDestination>(this object source)
			 where TDestination : class
		{
			if (source == null)
				return default(TDestination);

			Type sourceType = source.GetType();
			Type destinationType = typeof(TDestination);

			CreateMap(sourceType, destinationType);

			return (TDestination)AutoMapper.Mapper.Map(source, sourceType, typeof(TDestination));
		}

		public static void Map<TSource, TDestination>(TSource source, TDestination destination)
			where TSource : class
			where TDestination : class
		{
			if (source == default(TSource) || destination == default(TDestination))
				return;

			Type sourceType = source.GetType();
			Type destinationType = destination.GetType();

			CreateMap(sourceType, destinationType);

			AutoMapper.Mapper.Map(source, destination);
		}

		internal static void CreateMap(Type sourceType, Type destinationType)
		{
			Type actualSourceType = sourceType;
			Type actualDestinationType = destinationType;

			if (sourceType.IsArray)
				actualSourceType = sourceType.GetElementType();

			if (destinationType.IsArray)
				actualDestinationType = destinationType.GetElementType();

			typeof(AutoMapper.Mapper)
				.GetMethods(BindingFlags.Static | BindingFlags.Public)
				.First(mi => mi.Name == "CreateMap")
				.MakeGenericMethod(actualSourceType, actualDestinationType)
				.Invoke(null, null);
		}
    }
}