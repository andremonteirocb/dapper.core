using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using static Dommel.DommelMapper;
using Dommel;

namespace Luma.Dommel
{
    public class DefaultPropertyResolver : IPropertyResolver
    {
        private static readonly HashSet<Type> PrimitiveTypesSet = new HashSet<Type>
            {
                typeof(object),
                typeof(string),
                typeof(Guid),
                typeof(decimal),
                typeof(double),
                typeof(float),
                typeof(DateTime),
                typeof(DateTimeOffset),
                typeof(TimeSpan),
                typeof(byte[])
            };

        /// <summary>
        /// Resolve as propriedades a serem mapeadas para o tipo especificado.
        /// </summary>
        /// <param name = "type"> O tipo para resolver as propriedades a serem mapeadas. </param>
        /// <returns> Uma coleção de <see cref = "PropertyInfo" /> do <paramref name = "type" />. </returns>
        public virtual IEnumerable<PropertyInfo> ResolveProperties(Type type) =>
            FilterComplexTypes(type.GetRuntimeProperties())
            .Where(p =>
            p.GetSetMethod() is object
            && !p.IsDefined(typeof(IgnoreAttribute), false)
            );

        /// <summary>
        /// Obtém uma coleção de tipos que são considerados 'primitivos' para Dommel, mas não para o CLR.
        /// Substitua isso para especificar seu próprio conjunto de tipos.
        /// </summary>
        protected virtual HashSet<Type> PrimitiveTypes => PrimitiveTypesSet;

        /// <summary>
        /// Filtra os tipos complexos da coleção de propriedades especificada.
        /// </summary>
        /// <param name = "properties"> Uma coleção de propriedades. </param>
        /// <returns> As propriedades consideradas 'primitivas' de <paramref name = "properties" />. </returns>
        protected virtual IEnumerable<PropertyInfo> FilterComplexTypes(IEnumerable<PropertyInfo> properties)
        {
            foreach (var property in properties)
            {
                var type = property.PropertyType;
                type = Nullable.GetUnderlyingType(type) ?? type;
                if (type.GetTypeInfo().IsPrimitive || type.GetTypeInfo().IsEnum || PrimitiveTypes.Contains(type))
                {
                    yield return property;
                }
            }
        }
    }
}