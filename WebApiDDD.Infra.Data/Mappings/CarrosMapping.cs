using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiDDD.Domain.Models;
using WebApiDDD.Infra.Data.Mappings.Base;

namespace WebApiDDD.Infra.Data.Mappings
{
    public class CarrosMapping : BaseMapping<Carros>
    {
        public override void Configure(EntityTypeBuilder<Carros> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Modelo)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Marca)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
