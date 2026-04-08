using Microsoft.EntityFrameworkCore;
using Dolphin.Models;

namespace Dolphin.Data
{
    public class DolphinDbContext : DbContext
    {
        public DolphinDbContext(DbContextOptions<DolphinDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<LikePost> PostsLikes { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ── Utente ────────────────────────────────────────────────────────
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.Role).HasDefaultValue("user");
            });

            // ── Post ──────────────────────────────────────────────────────────
            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasOne(p => p.User)
                      .WithMany(u => u.Posts)
                      .HasForeignKey(p => p.UserId)
                      .OnDelete(DeleteBehavior.Cascade); // cancella i post se si cancella l'utente
            });

            // ── Commento ──────────────────────────────────────────────────────
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasOne(c => c.Post)
                      .WithMany(p => p.Comments)
                      .HasForeignKey(c => c.PostId)
                      .OnDelete(DeleteBehavior.NoAction); // evita cicli di cascade con SQL Server

                entity.HasOne(c => c.User)
                      .WithMany(u => u.Comments)
                      .HasForeignKey(c => c.UserId)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            // ── MiPiacePost ───────────────────────────────────────────────────
            modelBuilder.Entity<LikePost>(entity =>
            {
                // Un utente può mettere like a un post una sola volta
                entity.HasIndex(m => new { m.PostId, m.UserId }).IsUnique();

                entity.HasOne(m => m.Post)
                      .WithMany(p => p.PostsLikes)
                      .HasForeignKey(m => m.PostId)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(m => m.User)
                      .WithMany(u => u.PostsLikes)
                      .HasForeignKey(m => m.UserId)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            // ── Amicizia ──────────────────────────────────────────────────────
            modelBuilder.Entity<Friendship>(entity =>
            {
                // Un'amicizia tra A e B non può esistere due volte
                entity.HasIndex(a => new { a.UserId1, a.UserId2 }).IsUnique();

                entity.HasOne(a => a.User1)
                      .WithMany(u => u.FriendsAsLato1)
                      .HasForeignKey(a => a.UserId1)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(a => a.User2)
                      .WithMany(u => u.FriendsAsLato2)
                      .HasForeignKey(a => a.UserId2)
                      .OnDelete(DeleteBehavior.NoAction);
            });

            // ── RichiestaAmicizia ─────────────────────────────────────────────
            modelBuilder.Entity<FriendRequest>(entity =>
            {
                // Non si può mandare due richieste alla stessa persona
                entity.HasIndex(r => new { r.IdApplicant, r.IdReceiver }).IsUnique();

                entity.HasOne(r => r.Receiver)
                      .WithMany(u => u.RequestsSent)
                      .HasForeignKey(r => r.IdApplicant)
                      .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(r => r.Receiver)
                      .WithMany(u => u.RequestsReceived)
                      .HasForeignKey(r => r.IdReceiver)
                      .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}