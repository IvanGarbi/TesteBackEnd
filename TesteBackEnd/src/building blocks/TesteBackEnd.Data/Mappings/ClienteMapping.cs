using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteBackEnd.Core.Models;

namespace TesteBackEnd.Data.Mappings
{
    internal class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.NomeEmpresa)
                .IsRequired()
                .HasColumnType("VARCHAR(100)");

            builder.Property(x => x.PorteEmpresa)
                .IsRequired()
                .HasConversion<int>();

            builder.ToTable("Clientes");
        }
    }
}
