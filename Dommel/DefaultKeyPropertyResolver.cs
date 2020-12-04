using System;
using System.Reflection;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Dommel;
using static Dommel.DommelMapper;

namespace Luma.Dommel
{
    public class DefaultKeyPropertyResolver : IKeyPropertyResolver
    {
        /// <summary>
        /// Encontra as propriedades da chave procurando propriedades com o atributo [Key].
        /// </summary>
        public KeyPropertyInfo[] ResolveKeyProperties(Type type)
        {
            var keyProps = Resolvers
                    .Properties(type)
                    .Where(p => string.Equals(p.Name, "Id", StringComparison.OrdinalIgnoreCase) || p.GetCustomAttribute<KeyAttribute>() != null)
                    .ToArray();

            if (keyProps.Length == 0)
                throw new InvalidOperationException($"Não foi possível encontrar as propriedades principais do tipo '{type.FullName}'.");

            return keyProps.Select(p => new KeyPropertyInfo(p)).ToArray();
        }
    }
}
