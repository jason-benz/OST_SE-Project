﻿// <auto-generated />
using System;
using MediaHub.Data.PersistencyLayer;
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

            modelBuilder.Entity("MediaHub.Data.ContactsModule.Model.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ContactId")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(450)")
                        .HasColumnName("ContactId");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("bit")
                        .HasColumnName("isBlocked");

                    b.Property<bool>("OpenRequest")
                        .HasColumnType("bit")
                        .HasColumnName("openRequest");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(450)")
                        .HasColumnName("userId");

                    b.HasKey("Id");

                    b.ToTable("Contact");
                });

            modelBuilder.Entity("MediaHub.Data.MediaModule.Model.MediaRating", b =>
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

            modelBuilder.Entity("MediaHub.Data.MessagingModule.Model.Message", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MessageId"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReceiverUserId")
                        .HasColumnType("NVARCHAR(450)");

                    b.Property<string>("SenderUserId")
                        .IsRequired()
                        .HasColumnType("NVARCHAR(450)");

                    b.Property<DateTime?>("TimeReceived")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TimeSent")
                        .HasColumnType("datetime2")
                        .HasColumnOrder(3);

                    b.HasKey("MessageId");

                    b.HasIndex("ReceiverUserId");

                    b.HasIndex("SenderUserId");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("MediaHub.Data.ProfileModule.Model.UserProfile", b =>
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

            modelBuilder.Entity("MediaHub.Data.UserSuggestionModule.Model.UserSuggestion", b =>
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

            modelBuilder.Entity("MediaHub.Data.MediaModule.Model.MediaRating", b =>
                {
                    b.HasOne("MediaHub.Data.ProfileModule.Model.UserProfile", "Profile")
                        .WithMany("Ratings")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("MediaHub.Data.MessagingModule.Model.Message", b =>
                {
                    b.HasOne("MediaHub.Data.ProfileModule.Model.UserProfile", "Receiver")
                        .WithMany()
                        .HasForeignKey("ReceiverUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MediaHub.Data.ProfileModule.Model.UserProfile", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("MediaHub.Data.UserSuggestionModule.Model.UserSuggestion", b =>
                {
                    b.HasOne("MediaHub.Data.ProfileModule.Model.UserProfile", "UserProfile1")
                        .WithMany()
                        .HasForeignKey("UserId1")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("MediaHub.Data.ProfileModule.Model.UserProfile", "UserProfile2")
                        .WithMany()
                        .HasForeignKey("UserId2")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("UserProfile1");

                    b.Navigation("UserProfile2");
                });

            modelBuilder.Entity("MediaHub.Data.ProfileModule.Model.UserProfile", b =>
                {
                    b.Navigation("Ratings");
                });
#pragma warning restore 612, 618
        }
    }
}
