﻿// <auto-generated />
using System;
using MediaHub.Data.Persistency;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MediaHub.Data.Migrations
{
    [DbContext(typeof(MediaHubDBContext))]
    partial class MediaHubDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<string>("ProfileId")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(450)");

                    b.Property<byte?>("Rating")
                        .HasColumnType("tinyint");

                    b.Property<bool>("ShowInProfile")
                        .HasColumnType("bit");

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
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePicture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("UserProfile");
                });

            modelBuilder.Entity("MediaHub.Data.Model.MediaRating", b =>
                {
                    b.HasOne("MediaHub.Data.Model.UserProfile", "User")
                        .WithMany("Ratings")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MediaHub.Data.Model.UserProfile", b =>
                {
                    b.Navigation("Ratings");
                });
#pragma warning restore 612, 618
        }
    }
}
