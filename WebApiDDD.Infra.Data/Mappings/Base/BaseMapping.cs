using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiDDD.Domain.Models.Base;

namespace WebApiDDD.Infra.Data.Mappings.Base
{
    public class BaseMapping<TModel> : IEntityTypeConfiguration<TModel>
        where TModel : BaseModel
    {
        public virtual void Configure(EntityTypeBuilder<TModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
