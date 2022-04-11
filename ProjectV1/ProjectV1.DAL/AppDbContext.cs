using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectV1.DAL.Entities;
using ProjectV1.DAL.Entities.Auth;

namespace ProjectV1.DAL
{
    public class AppDbContext : IdentityDbContext<
       User,
       Role,
       int,
       IdentityUserClaim<int>,
       UserRole,
       IdentityUserLogin<int>,
       IdentityRoleClaim<int>,
       IdentityUserToken<int>>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Player> Players { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<StaffMember> Staff{ get; set; }

        public DbSet<Stadium> Stadiums { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchPlayer> MatchPlayers { get; set; }
        public DbSet<MatchStaff> MatchStaffs { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Player>()
                .HasOne(c => c.Contract)
                .WithOne(cc => cc.Player);


            builder.Entity<StaffMember>()
               .HasOne(c => c.Contract)
               .WithOne(cc => cc.StaffMember);


            builder.Entity<MatchPlayer>().HasKey(mp => new { mp.MatchId, mp.PlayerId });

            builder.Entity<MatchPlayer>()
                .HasOne<Player>(mp => mp.Player)
                .WithMany(a => a.MatchPlayers)
                .HasForeignKey(mp => mp.PlayerId);

            builder.Entity<MatchPlayer>()
                .HasOne<Match>(mp => mp.Match)
                .WithMany(a => a.MatchPlayers)
                .HasForeignKey(mp => mp.MatchId);



            builder.Entity<MatchStaff>().HasKey(mp => new { mp.MatchId, mp.StaffMemberId });

            builder.Entity<MatchStaff>()
                .HasOne<StaffMember>(mp => mp.StaffMember)
                .WithMany(a => a.MatchStaffs)
                .HasForeignKey(mp => mp.StaffMemberId);

            builder.Entity<MatchStaff>()
                .HasOne<Match>(mp => mp.Match)
                .WithMany(a => a.MatchStaffs)
                .HasForeignKey(mp => mp.MatchId);


            builder.Entity<Stadium>()
                .HasMany(a => a.Matches)
                .WithOne(b => b.Stadium);


            base.OnModelCreating(builder);

        }

        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PersonConfiguration());
        }
        */
    }
}
