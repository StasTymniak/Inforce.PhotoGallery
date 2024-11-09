﻿// <auto-generated />
using System;
using Inforce.PhotoGallery.Api.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Inforce.PhotoGallery.Api.Context.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20241107161036_update_useractitity_table")]
    partial class update_useractitity_table
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Inforce.PhotoGallery.Api.Context.Models.Album", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Albums");
                });

            modelBuilder.Entity("Inforce.PhotoGallery.Api.Context.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AlbumId")
                        .HasColumnType("int");

                    b.Property<string>("Base64Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("AlbumId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Inforce.PhotoGallery.Api.Context.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Inforce.PhotoGallery.Api.Context.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Inforce.PhotoGallery.Api.Context.Models.UserActivity", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnOrder(0);

                    b.Property<int>("ImageId")
                        .HasColumnType("int")
                        .HasColumnOrder(1);

                    b.Property<bool>("IsLiked")
                        .HasColumnType("bit");

                    b.HasKey("UserId", "ImageId");

                    b.HasIndex("ImageId");

                    b.ToTable("UserActivities");
                });

            modelBuilder.Entity("Inforce.PhotoGallery.Api.Context.Models.Album", b =>
                {
                    b.HasOne("Inforce.PhotoGallery.Api.Context.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Inforce.PhotoGallery.Api.Context.Models.Image", b =>
                {
                    b.HasOne("Inforce.PhotoGallery.Api.Context.Models.Album", "Album")
                        .WithMany("Images")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Album");
                });

            modelBuilder.Entity("Inforce.PhotoGallery.Api.Context.Models.User", b =>
                {
                    b.HasOne("Inforce.PhotoGallery.Api.Context.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Inforce.PhotoGallery.Api.Context.Models.UserActivity", b =>
                {
                    b.HasOne("Inforce.PhotoGallery.Api.Context.Models.Image", null)
                        .WithMany("UserActivities")
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Inforce.PhotoGallery.Api.Context.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Inforce.PhotoGallery.Api.Context.Models.Album", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("Inforce.PhotoGallery.Api.Context.Models.Image", b =>
                {
                    b.Navigation("UserActivities");
                });
#pragma warning restore 612, 618
        }
    }
}
