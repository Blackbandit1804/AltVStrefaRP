﻿// <auto-generated />
using System;
using AltVStrefaRPServer.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AltVStrefaRPServer.Database.Migrations
{
    [DbContext(typeof(ServerContext))]
    partial class ServerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AltVStrefaRPServer.Models.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.HasKey("AccountId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("AltVStrefaRPServer.Models.BankAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountNumber");

                    b.Property<int>("CharacterId");

                    b.Property<float>("Money");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId")
                        .IsUnique();

                    b.ToTable("BankAccounts");
                });

            modelBuilder.Entity("AltVStrefaRPServer.Models.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccountId");

                    b.Property<int>("Age");

                    b.Property<string>("BackgroundImage");

                    b.Property<DateTime>("CreationDate");

                    b.Property<short>("Dimension");

                    b.Property<string>("FirstName");

                    b.Property<int>("Gender");

                    b.Property<string>("LastName");

                    b.Property<DateTime>("LastPlayed");

                    b.Property<float>("Money");

                    b.Property<string>("ProfileImage");

                    b.Property<int>("TimePlayed");

                    b.Property<float>("X");

                    b.Property<float>("Y");

                    b.Property<float>("Z");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("AltVStrefaRPServer.Models.MoneyTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("Amount");

                    b.Property<string>("Date");

                    b.Property<int>("Receiver");

                    b.Property<int>("Source");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("MoneyTransactions");
                });

            modelBuilder.Entity("AltVStrefaRPServer.Models.VehicleModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<short>("Dimension");

                    b.Property<float>("Fuel");

                    b.Property<float>("Heading");

                    b.Property<bool>("IsBlocked");

                    b.Property<bool>("IsJobVehicle");

                    b.Property<bool>("IsLocked");

                    b.Property<bool>("IsSpawned");

                    b.Property<float>("MaxFuel");

                    b.Property<float>("MaxOil");

                    b.Property<float>("Mileage");

                    b.Property<string>("Model");

                    b.Property<float>("Oil");

                    b.Property<int>("Owner");

                    b.Property<float>("X");

                    b.Property<float>("Y");

                    b.Property<float>("Z");

                    b.HasKey("Id");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("AltVStrefaRPServer.Models.BankAccount", b =>
                {
                    b.HasOne("AltVStrefaRPServer.Models.Character", "Character")
                        .WithOne("BankAccount")
                        .HasForeignKey("AltVStrefaRPServer.Models.BankAccount", "CharacterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AltVStrefaRPServer.Models.Character", b =>
                {
                    b.HasOne("AltVStrefaRPServer.Models.Account", "Account")
                        .WithMany("Characters")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
