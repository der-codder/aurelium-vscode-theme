using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;

namespace Abp.MemoryDb.Repositories
{
  //TODO: Implement thread-safety..?
  public class MemoryRepository<TEntity, TPrimaryKey> : AbpRepositoryBase<TEntity, TPrimaryKey>
    where TEntity : class, IEntity<TPrimaryKey>
  {
    private readonly IMemoryDatabaseProvider _databaseProvider;
    protected MemoryDatabase Database { get { return _databaseProvider.Database; } }

    protected List<TEntity> Table { get { return Database.Set<TEntity>(); } }

    private readonly MemoryPrimaryKeyGenerator<TPrimaryKey> _primaryKeyGenerator;

    public MemoryRepository(IMemoryDatabaseProvider databaseProvider)
    {
      this._databaseProvider = databaseProvider;
      _primaryKeyGenerator = new MemoryPrimaryKeyGenerator<TPrimaryKey>();
    }

    public override IQueryable<TEntity> GetAll()
    {
      return Table.AsQueryable();
    }

    public override TEntity Insert(TEntity entity)
    {
      if (entity.IsTransient())
      {
        entity.Id = _primaryKeyGenerator.GetNext();
      }

      Table.Add(entity);
      return entity;
    }

    public override TEntity Update(TEntity entity)
    {
      var index = Table.FindIndex(e => EqualityComparer<TPrimaryKey>.Default.Equals(e.Id, entity.Id));
      if (index >= 0)
      {
        Table[index] = entity;
      }

      return entity;
    }

    public override void Delete(TEntity entity)
    {
      Delete(entity.Id);
    }

    public override void Delete(TPrimaryKey id)
    {
      var index = Table.FindIndex(e => EqualityComparer<TPrimaryKey>.Default.Equals(e.Id, id));
      if (index >= 0)
      {
        Table.RemoveAt(index);
      }
    }
  }

  public class PhoneNumber
  {
    public string Area { get; }
    public string Major { get; }
    public string Minor { get; }

    private PhoneNumber(string area, string major, string minor)
    {
      Area = area;
      Major = major;
      Minor = minor;
    }
    
    public static PhoneNumber Parse(string number)
    {
      if (String.IsNullOrWhiteSpace(number))
          throw new ArgumentException("Phone number cannot be blank.");
      
      if (number.Length != 10)
          throw new ArgumentException("Phone number should be 10 digits long.");

      var area = number.Substring(0, 3);
      var major = number.Substring(3, 3);
      var minor = number.Substring(6);
      
      return new PhoneNumber(area, major, minor);
    }

    public override string ToString()
    {
      return String.Format($"({Area}){Major}-{Minor}");
    }
  }
}
