﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using pokusaj.Data;

#nullable disable

namespace pokusaj.Migrations
{
    [DbContext(typeof(StudentContext))]
    [Migration("20240324114629_addProfs")]
    partial class addProfs
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.10");

            modelBuilder.Entity("pokusaj.Models.Professor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<int?>("InstructionsCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProfilePictureUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Professors");
                });

            modelBuilder.Entity("pokusaj.Models.Student", b =>
                {
                    b.Property<int?>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
