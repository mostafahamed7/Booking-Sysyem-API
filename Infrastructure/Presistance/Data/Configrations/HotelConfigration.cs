using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistance.Data.Configrations
{
    public class HotelConfigration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasMany(H => H.HotelPictures)
                   .WithMany(P => P.Hotels)
                   .UsingEntity(j => j.ToTable("Hotel_Pictures"));

            builder.Property(H => H.ID).ValueGeneratedOnAdd();
        }
    }
}
