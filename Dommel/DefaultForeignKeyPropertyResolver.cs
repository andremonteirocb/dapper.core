using Dommel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using static Dommel.DommelMapper;

namespace Luma.Dommel
{
    public class DefaultForeignKeyPropertyResolver : IForeignKeyPropertyResolver
    {
        /// <summary>
        /// Resolve a propriedade de chave estrangeira para o tipo de fonte especificado e incluindo o tipo
        /// usando <paramref name = "includingType" /> + Id como nome da propriedade.
        /// </summary>
        /// <param name = "sourceType"> O tipo de fonte que deve conter a propriedade da chave estrangeira. </param>
        /// <param name = "includingType"> O tipo da relação de chave estrangeira. </param>
        /// <param name = "ForeignKeyRelation"> O tipo de relação de chave estrangeira. </param>
        /// <returns> A propriedade de chave estrangeira para <paramref name = "sourceType" /> e <paramref name = "includingType" />. </returns>
        public virtual PropertyInfo ResolveForeignKeyProperty(Type sourceType, Type includingType, out ForeignKeyRelation foreignKeyRelation)
        {
            var oneToOneFk = ResolveOneToOne(sourceType, includingType);
            if (oneToOneFk != null)
            {
                foreignKeyRelation = ForeignKeyRelation.OneToOne;
                return oneToOneFk;
            }

            var oneToManyFk = ResolveOneToMany(sourceType, includingType);
            if (oneToManyFk != null)
            {
                foreignKeyRelation = ForeignKeyRelation.OneToMany;
                return oneToManyFk;
            }

            throw new InvalidOperationException($"Não foi possível resolver a propriedade da chave estrangeira. Tipo de fonte '{sourceType.FullName}'; incluindo o tipo: '{includingType.FullName}'.");
        }

        private static PropertyInfo? ResolveOneToOne(Type sourceType, Type includingType)
        {
            // Procure a chave estrangeira no tipo de fonte fazendo um palpite sobre o nome da propriedade.
            var foreignKeyName = includingType.Name + "Id";
            var foreignKeyProperty = sourceType.GetProperty(foreignKeyName);
            if (foreignKeyProperty != null && !foreignKeyProperty.IsDefined(typeof(IgnoreAttribute)))
                return foreignKeyProperty;

            // Determina se o tipo de fonte contém uma propriedade de navegação para o tipo de inclusão.
            var navigationProperty = sourceType.GetProperties().FirstOrDefault(p => p.PropertyType == includingType && !p.IsDefined(typeof(IgnoreAttribute)));
            if (navigationProperty != null)
            {
                // Resolva a propriedade de chave estrangeira do atributo.
                var fkAttr = navigationProperty.GetCustomAttribute<ForeignKeyAttribute>();
                if (fkAttr != null)
                    return sourceType.GetProperty(fkAttr.Name);
            }

            return null;
        }

        private static PropertyInfo? ResolveOneToMany(Type sourceType, Type includingType)
        {
            // Procure a chave estrangeira no tipo de inclusão, fazendo um palpite sobre o nome da propriedade.
            var foreignKeyName = sourceType.Name + "Id";
            var foreignKeyProperty = includingType.GetProperty(foreignKeyName);
            if (foreignKeyProperty != null && !foreignKeyProperty.IsDefined(typeof(IgnoreAttribute)))
                return foreignKeyProperty;

            var collectionType = typeof(IEnumerable<>).MakeGenericType(includingType);
            var navigationProperty = sourceType.GetProperties().FirstOrDefault(p => collectionType.IsAssignableFrom(p.PropertyType) && !p.IsDefined(typeof(IgnoreAttribute)));
            if (navigationProperty != null)
            {
                // Resolva a propriedade de chave estrangeira do atributo.
                var fkAttr = navigationProperty.GetCustomAttribute<ForeignKeyAttribute>();
                if (fkAttr != null)
                    return includingType.GetProperty(fkAttr.Name);
            }

            return null;
        }
    }
}
