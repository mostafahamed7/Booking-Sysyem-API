using Domain.Entites.Reservations_Mod;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistance.Data.Configrations
{
    public class ReservationRoomConfiguration : IEntityTypeConfiguration<ReservationRoom>
    {
        public void Configure(EntityTypeBuilder<ReservationRoom> builder)
        {
            builder.HasKey(rr => new { rr.ReservationId, rr.RoomId });

            builder.HasOne(rr => rr.Reservation)
                .WithMany(r => r.ReservationRooms)
                .HasForeignKey(rr => rr.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(rr => rr.Room)
                .WithMany(r => r.ReservationRooms)
                .HasForeignKey(rr => rr.RoomId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
