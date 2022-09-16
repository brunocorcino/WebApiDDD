using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiDDD.Domain.Models;
using WebApiDDD.Infra.Data.Mappings.Base;

namespace WebApiDDD.Infra.Data.Mappings
{
    public class MarcasMapping : BaseMapping<Marcas>
    {
        public override void Configure(EntityTypeBuilder<Marcas> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(70);
        }
    }
}
