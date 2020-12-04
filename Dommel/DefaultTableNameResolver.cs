using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Dapper.Core.Extensions;
using static Dommel.DommelMapper;

namespace Luma.Dommel
{
    public class DefaultTableNameResolver : ITableNameResolver
    {
        /// <summary>
        /// Resolve o nome da tabela.
        /// Procura o atributo [Table]. Caso contrário, faça o tipo
        /// plural (por exemplo, Product -> Products) e remover o prefixo 'I' para interfaces.
        /// </summary>
        public virtual string ResolveTableName(Type type)
        {
            return type.GetTableName();
        }
    }
}
