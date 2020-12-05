# dapper.core
.NET Class Library for integration with DAPPER <br />
.NET Core 3.0

## Configurações
### Startup.cs
```c# 
Dapper.Core.Configurations.ConfiguracaoRepository.Configure(Configuration.GetConnectionString("connectionstring_name"));
```

## Implementação
### IRepository
```c#
  public class LogRepository : Repository<Log>, ILogRepository
  {
  }
```

## Obter
### GetAsync
```c#
  public async Task<Classe> ObterPorId(Guid id)
  {
      var objeto = await base.GetAsync(id);
      return objeto;
  }
```

### GetAllAsync
```c#
  public async Task<IEnumerable<Classe>> ObterTodos()
  {
      var lista = await base.GetAllAsync(id);
      return lista;
  }
```

### Utilizando ExecuteMultiply
```c#
  var sql = "SELECT * FROM Classe WHERE Id = 1; SELECT * FROM Classe2;";
  var grid = await base.ExecuteMultiply(sql);
  var objeto = grid.Read<Classe>();
  var lista = grid.Read<IEnumerable<Classe>>();
```

### Utilizando MultiMapAsync
```c#
  var map = await base.MultiMapAsync<classe1, classe2, classe1>($@"
      SELECT DISTINCT classe1.*,classe2.*
      FROM classe1 
      JOIN classe2 ON classe2.EstadoId = classe1.Id
      ", (cl1, cl2) =>
  {
      cl1.Classe2 = cl2;
      return c;
  });
```

### Utilizando QueryAsync
```c#
  public async Task<IEnumerable<Classe>> Listar()
  {
      var sql = "script_sql"
      var lista = await base.QueryAsync(sql);
      return lista;
  }
```

### Utilizando QueryFirstOrDefaultAsync
```c#
    public async Task<Classe> ObterPorId(Guid id)
    {
        var sql = "script_sql"
        var objeto = await base.QueryFirstOrDefaultAsync(sql);
        return objeto;
    }
```

### Utilizando SelectFirstOrDefaultAsync
```c#
    public async Task<Classe> ObterPorId(Guid id)
    {
        var objeto = await base.SelectFirstOrDefaultAsync(c => c.Id = id);
        return objeto;
    }
```

### Utilizando SelectAsync
```c#
    public async Task<IEnumerable<Classe>> ObterPorId(Guid id)
    {
        var lista = await base.SelectAsync(log => log.Id == responsavelId);
        return lista;
    }
```

### GetPagedAsync
```c#
    public async Task<IEnumerable<Classe>> GetPagedAsync(int pageNumber, int pageSize)
    {
        return await base.GetPagedAsync(pageNumber, pageSize);
    }
```
## Salvar
### SaveAsync
```c#
    public virtual async Task SaveAsync(Classe objeto)
    {
        await base.SaveAsync(objeto);
    }
```

### SaveInListAsync
```c#
    public virtual async Task SaveInListAsync(IEnumerable<Classe> lista)
    {
        await base.SaveInListAsync(lista);
    }
```

## Deletar
### DeleteAsync
```c#
    public async virtual Task<bool> DeleteAsync(Classe objeto)
    {
        return await base.DeleteAsync(objeto);
    }
```

### DeleteMultipleAsync
```c#
    public virtual async Task DeleteMultipleAsync(Expression<Func<TEntity, bool>> predicate)
    {
        await base.DeleteMultipleAsync(predicate);
    }
```

## Validação
### Utilizando AnyAsync
```c#
    public async Task<bool> Existe(Guid id)
    {
        return await base.AnyAsync(p => p.Id == id);
    }
```

## Transação
### Utilizando Utilizando Transação
```c#
    using (var trans = new Dapper.Core.Base.TransactionScope(_repository1, _repository2, _repository3))
    {
        await _repository1.SaveAsync(obj1);
        
        if (!await base.ExecutarValidacao(new ObjetoValidation(_repository2), obj2)) return;
        await _repository2.SaveAsync(obj2);
        
        await _repository3.SaveAsync(obj3);
        
        trans.Complete();
    }
```
