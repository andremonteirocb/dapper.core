using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using static Dommel.DommelMapper;

namespace Luma.Dommel
{
    public class DefaultColumnNameResolver : IColumnNameResolver
    {
        /// <summary>
        /// Resolve o nome da coluna para a propriedade.
        /// Procura o atributo [Column]. Caso contrário, é apenas o nome da propriedade.
        /// </summary>
        public virtual string ResolveColumnName(PropertyInfo propertyInfo)
        {
            var columnAttr = propertyInfo.GetCustomAttribute<ColumnAttribute>();
            if (columnAttr != null)
            {
                return columnAttr.Name;
            }

            return propertyInfo.Name;
        }
    }
}
