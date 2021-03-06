﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using smartdressroom.Storage;

namespace smartdressroom.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20190718120138_02")]
    partial class _02
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("smartdressroom.Models.AdminModel", b =>
                {
                    b.Property<byte[]>("ID")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<string>("Login");

                    b.Property<string>("Password");

                    b.HasKey("ID");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("smartdressroom.Models.ClothesModel", b =>
                {
                    b.Property<byte[]>("ID")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<string>("Brand")
                        .IsRequired();

                    b.Property<byte[]>("CollectionID")
                        .IsRequired()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<int>("ImagesCount");

                    b.Property<string>("ImgFormat")
                        .IsRequired();

                    b.Property<string>("ImgPath");

                    b.Property<int>("Price");

                    b.Property<string>("Sizes")
                        .IsRequired();

                    b.Property<string>("VendorCode")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("CollectionID");

                    b.ToTable("ClothesModels");
                });

            modelBuilder.Entity("smartdressroom.Models.CollectionModel", b =>
                {
                    b.Property<byte[]>("ID")
                        .ValueGeneratedOnAdd()
                        .HasConversion(new ValueConverter<byte[], byte[]>(v => default(byte[]), v => default(byte[]), new ConverterMappingHints(size: 16)));

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("CollectionModels");
                });

            modelBuilder.Entity("smartdressroom.Models.ClothesModel", b =>
                {
                    b.HasOne("smartdressroom.Models.CollectionModel", "Collection")
                        .WithMany("ClothesModels")
                        .HasForeignKey("CollectionID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
