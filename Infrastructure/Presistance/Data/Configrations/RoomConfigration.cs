using Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace Presistance.Data.Configrations
{
    public class RoomConfigration : IEntityTypeConfiguration<Room>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Room> builder)
        {
            builder.HasOne(r => r.Hotel)
                   .WithMany(h => h.Rooms)
                   .HasForeignKey(r => r.HotelId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(R => R.RoomPictures)
                   .WithMany(RP => RP.Rooms)
                   .UsingEntity(j => j.ToTable("Room_Pictures"));


            builder.Property(R => R.Price)
                   .HasColumnType("decimal(18,3)");

            builder.Property(R => R.ID).ValueGeneratedOnAdd();

        }
    }
}
