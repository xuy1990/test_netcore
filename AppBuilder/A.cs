using System;
using System.Collections.Generic;
using System.Text;

namespace AppBuilder
{
    // 领域实体接口
    public interface IEntity
    {
        // 当前领域实体的全局唯一标识
        Guid Id { get; }
    }

    // 聚合根接口，继承于该接口的对象是外部唯一操作的对象
    public interface IAggregateRoot : IEntity
    {
    }

    public class AggregateRoot : IAggregateRoot
    {
        public Guid Id => throw new NotImplementedException();
    }

    // 商品类
    public class Product : AggregateRoot
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal UnitPrice { get; set; }

        public string ImageUrl { get; set; }

        public bool IsNew { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
