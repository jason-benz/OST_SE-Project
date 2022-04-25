﻿// <auto-generated />
using MediaHub.Data.Persistency;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MediaHub.Data.Migrations
{
    [DbContext(typeof(MediaHubDBContext))]
    [Migration("20220409123424_UserProfile")]
    partial class UserProfile
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

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
#pragma warning restore 612, 618
        }
    }
}