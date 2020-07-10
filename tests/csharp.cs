using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;

namespace Abp.MemoryDb.Repositories
{
  interface IInterface
  {
    void UpdateInt(int number);
  }

  enum Colors
  {
    Red,
    Yellow = 255
  }

  class Class1 : IInterface
  {
    private const int CONST_NUMBER = 42;

    // some comment
    private readonly string stringField = "It's a string";
    private int intField = 6_000_000;

    public int MyProperty { get; set; }

    public Class1()
    {
      Console.WriteLine($"stringField: {stringField}, intField: {intField} test\n");
      intField = (int) Colors.Red;
    }

    public void UpdateInt(int number)
    {
      intField = MyProperty + CONST_NUMBER + number;

      var piDigital = 3.14;

      Console.WriteLine(piDigital);
    }

    public void SayHello(string name)
    {
      Console.WriteLine($"Hello, {name}!");
    }
  }

  class MyClass2
  {
    public void Foo()
    {
      var obj = new Class1();
      obj.UpdateInt(123);
      obj.SayHello("John");
    }
  }

  public enum PieceColor
  {
    Red,
    Yellow,
    Blank
  }

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
      if (index >= 0)
      {
        Table.RemoveAt(index);
      }
      if (index != 0)
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
