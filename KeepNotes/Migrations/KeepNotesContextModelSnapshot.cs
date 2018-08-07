﻿// <auto-generated />
using System;
using KeepNotes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KeepNotes.Migrations
{
    [DbContext(typeof(KeepNotesContext))]
    partial class KeepNotesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("KeepNotes.Models.CheckList", b =>
                {
                    b.Property<int>("Check_ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("NotesID");

                    b.Property<string>("list");

                    b.HasKey("Check_ID");

                    b.HasIndex("NotesID");

                    b.ToTable("CheckList");
                });

            modelBuilder.Entity("KeepNotes.Models.Label", b =>
                {
                    b.Property<int>("Label_ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("NotesID");

                    b.Property<string>("label");

                    b.HasKey("Label_ID");

                    b.HasIndex("NotesID");

                    b.ToTable("Label");
                });

            modelBuilder.Entity("KeepNotes.Models.Notes", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("PinStat");

                    b.Property<string>("Text");

                    b.Property<string>("Title");

                    b.HasKey("ID");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("KeepNotes.Models.CheckList", b =>
                {
                    b.HasOne("KeepNotes.Models.Notes")
                        .WithMany("checklist")
                        .HasForeignKey("NotesID");
                });

            modelBuilder.Entity("KeepNotes.Models.Label", b =>
                {
                    b.HasOne("KeepNotes.Models.Notes")
                        .WithMany("label")
                        .HasForeignKey("NotesID");
                });
#pragma warning restore 612, 618
        }
    }
}
