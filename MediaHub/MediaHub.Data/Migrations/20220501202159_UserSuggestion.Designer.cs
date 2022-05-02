﻿// <auto-generated />
using MediaHub.Data.PersistencyLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MediaHub.Data.Migrations
{
    [DbContext(typeof(MediaHubDBContext))]
    [Migration("20220501225930_UserSuggestion")]
    partial class UserSuggestion
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MediaHub.Data.Model.MediaRating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsAddedToProfile")
                        .HasColumnType("bit");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<string>("ProfileId")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(450)");

                    b.Property<byte>("Rating")
                        .HasColumnType("tinyint");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.ToTable("MediaRating");
                });

            modelBuilder.Entity("MediaHub.Data.Model.UserProfile", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("NVARCHAR(450)")
                        .HasColumnName("Id");

                    b.Property<string>("Biography")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(256)")
                        .HasColumnName("Biography");

                    b.Property<string>("ProfilePicture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(50)")
                        .HasColumnName("Username");

                    b.HasKey("UserId");

                    b.ToTable("UserProfile");
                });

            modelBuilder.Entity("MediaHub.Data.Model.UserSuggestion", b =>
                {
                    b.Property<string>("UserId1")
                        .HasColumnType("NVARCHAR(450)");

                    b.Property<string>("UserId2")
                        .HasColumnType("NVARCHAR(450)");

                    b.Property<bool>("IgnoreSuggestion")
                        .HasColumnType("bit");

                    b.HasKey("UserId1", "UserId2");

                    b.HasIndex("UserId2");

                    b.ToTable("UserSuggestion");
                });

            modelBuilder.Entity("MediaHub.Data.Model.MediaRating", b =>
                {
                    b.HasOne("MediaHub.Data.Model.UserProfile", "Profile")
                        .WithMany("Ratings")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("MediaHub.Data.Model.UserSuggestion", b =>
                {
                    b.HasOne("MediaHub.Data.Model.UserProfile", "UserProfile1")
                        .WithMany()
                        .HasForeignKey("UserId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MediaHub.Data.Model.UserProfile", "UserProfile2")
                        .WithMany()
                        .HasForeignKey("UserId2")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserProfile1");

                    b.Navigation("UserProfile2");
                });

            modelBuilder.Entity("MediaHub.Data.Model.UserProfile", b =>
                {
                    b.Navigation("Ratings");
                });
#pragma warning restore 612, 618
        }
    }
}
