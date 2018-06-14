using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TavernAPI.Models
{
    public class TavernContext : DbContext
    {
        public TavernContext(DbContextOptions<TavernContext> options)
            : base(options)
        { }

        public DbSet<Character> Character { get; set; }
        public DbSet<Player> Player { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=TavernDB.db");
        }
    }


    public class Character
    {
        public int CharacterId { get; set; }
        public int PlayerId { get; set; }
        public string name { get; set; }
        public string race { get; set; }
        public string subRace { get; set; }
        public string charClass { get; set; }
        public string alignment { get; set; }
        public string background { get; set; }
        public string size { get; set; }
        public int speed { get; set; }
        public int HitPoints { get; set; }
        public string SavingThrows { get; set; }
        public int Strength { get; set; }
        public int StrMod { get; set; }
        public int Dexterity { get; set; }
        public int DexMod { get; set; }
        public int Constitution { get; set; }
        public int ConMod { get; set; }
        public int Intelligence { get; set; }
        public int IntMod { get; set; }
        public int Wisdom { get; set; }
        public int WisMod { get; set; }
        public int Charisma { get; set; }
        public int ChaMod { get; set; }
        public string Skills { get; set; }

    }
    public class Player
    {
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public string EmailAddress { get; set; }
        public ICollection<Character> charList { get; set; }
    }

}

