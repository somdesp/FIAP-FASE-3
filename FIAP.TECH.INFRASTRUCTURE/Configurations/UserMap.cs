using FIAP.TECH.CORE.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.TECH.INFRASTRUCTURE.Configurations;

internal class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).UseIdentityColumn().IsRequired();
        builder.Property(x => x.CreatedDate).HasColumnType("datetime").IsRequired();
        builder.Property(x => x.Name).HasColumnType("varchar(50)").IsRequired();
        builder.Property(x => x.Email).HasColumnType("varchar(50)").IsRequired();
        builder.Property(x => x.Password).HasColumnType("varchar(16)").IsRequired();
        builder.Property(x => x.IsActive).HasColumnType("bit").IsRequired();

        builder.HasData(new User
        {
            Id = 1,
            Email = "tester@fiaptest.com.br",
            Name = "Tech Challenge Fase1",
            Password = "Senha@123",
            IsActive = true
        });
    }
}
