using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Energy.Models.DB;

public partial class EnergyDbContext : DbContext
{
    public EnergyDbContext()
    {
    }

    public EnergyDbContext(DbContextOptions<EnergyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Coal50> Coal50s { get; set; }

    public virtual DbSet<Energy50> Energy50s { get; set; }

    public virtual DbSet<Energy50Db> Energy50Dbs { get; set; }

    public virtual DbSet<Fuel50Db> Fuel50Dbs { get; set; }

    public virtual DbSet<Power50> Power50s { get; set; }

    public virtual DbSet<Power50Db> Power50Dbs { get; set; }

    public virtual DbSet<TAccount> TAccounts { get; set; }

    public virtual DbSet<TAccountRoleMapping> TAccountRoleMappings { get; set; }

    public virtual DbSet<TBanner> TBanners { get; set; }

    public virtual DbSet<TChartDatum> TChartData { get; set; }

    public virtual DbSet<TChild> TChildren { get; set; }

    public virtual DbSet<TEnergy> TEnergies { get; set; }

    public virtual DbSet<TFlow> TFlows { get; set; }

    public virtual DbSet<TIndustry> TIndustries { get; set; }

    public virtual DbSet<TParent> TParents { get; set; }

    public virtual DbSet<TPermission> TPermissions { get; set; }

    public virtual DbSet<TPublication> TPublications { get; set; }

    public virtual DbSet<TPublicationLevel1> TPublicationLevel1s { get; set; }

    public virtual DbSet<TPublicationLevel2> TPublicationLevel2s { get; set; }

    public virtual DbSet<TPublicationLevel3> TPublicationLevel3s { get; set; }

    public virtual DbSet<TRole> TRoles { get; set; }

    public virtual DbSet<TRolePermissionMapping> TRolePermissionMappings { get; set; }

    public virtual DbSet<TSystem> TSystems { get; set; }

    public virtual DbSet<TThematic> TThematics { get; set; }

    public virtual DbSet<TmpCc> TmpCcs { get; set; }

    public virtual DbSet<UpfileLog> UpfileLogs { get; set; }

    public virtual DbSet<Wesdes40> Wesdes40s { get; set; }

    public virtual DbSet<Wesdes50> Wesdes50s { get; set; }

    public virtual DbSet<Wesdes50Db> Wesdes50Dbs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DbConnectonString");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coal50>(entity =>
        {
            entity.HasKey(e => new { e.YrMnth, e.RowNo1 });

            entity.ToTable("coal50");

            entity.Property(e => e.YrMnth).HasColumnName("yr_mnth");
            entity.Property(e => e.RowNo1).HasColumnName("row_no1");
            entity.Property(e => e.Aa).HasColumnName("aa");
            entity.Property(e => e.Ab).HasColumnName("ab");
            entity.Property(e => e.Ac).HasColumnName("ac");
            entity.Property(e => e.Ag).HasColumnName("ag");
            entity.Property(e => e.Ah).HasColumnName("ah");
            entity.Property(e => e.Ai).HasColumnName("ai");
            entity.Property(e => e.Ail).HasColumnName("ail");
            entity.Property(e => e.Ain).HasColumnName("ain");
            entity.Property(e => e.Aj).HasColumnName("aj");
            entity.Property(e => e.Ak).HasColumnName("ak");
            entity.Property(e => e.Am).HasColumnName("am");
            entity.Property(e => e.Amo).HasColumnName("amo");
            entity.Property(e => e.Ank).HasColumnName("ank");
            entity.Property(e => e.Ap).HasColumnName("ap");
            entity.Property(e => e.At).HasColumnName("at");
            entity.Property(e => e.Atr).HasColumnName("atr");
            entity.Property(e => e.Au).HasColumnName("au");
            entity.Property(e => e.Av).HasColumnName("av");
            entity.Property(e => e.Ea).HasColumnName("ea");
            entity.Property(e => e.Eb).HasColumnName("eb");
            entity.Property(e => e.Ed).HasColumnName("ed");
            entity.Property(e => e.Ee).HasColumnName("ee");
            entity.Property(e => e.Eg).HasColumnName("eg");
            entity.Property(e => e.Ek).HasColumnName("ek");
            entity.Property(e => e.En).HasColumnName("en");
            entity.Property(e => e.Eu).HasColumnName("eu");
            entity.Property(e => e.Fa).HasColumnName("fa");
            entity.Property(e => e.Fc).HasColumnName("fc");
            entity.Property(e => e.Fe).HasColumnName("fe");
            entity.Property(e => e.Ff).HasColumnName("ff");
            entity.Property(e => e.Fg).HasColumnName("fg");
            entity.Property(e => e.Fh).HasColumnName("fh");
            entity.Property(e => e.Fk).HasColumnName("fk");
            entity.Property(e => e.Fl).HasColumnName("fl");
            entity.Property(e => e.Fmz).HasColumnName("fmz");
            entity.Property(e => e.Fn).HasColumnName("fn");
            entity.Property(e => e.Fo).HasColumnName("fo");
            entity.Property(e => e.Fr).HasColumnName("fr");
            entity.Property(e => e.Fs).HasColumnName("fs");
            entity.Property(e => e.Fss).HasColumnName("fss");
            entity.Property(e => e.Fu).HasColumnName("fu");
            entity.Property(e => e.Mk).HasColumnName("mk");
            entity.Property(e => e.Mn).HasColumnName("mn");
            entity.Property(e => e.Mo).HasColumnName("mo");
            entity.Property(e => e.Mq).HasColumnName("mq");
            entity.Property(e => e.Mr).HasColumnName("mr");
            entity.Property(e => e.Ms).HasColumnName("ms");
            entity.Property(e => e.Mu).HasColumnName("mu");
            entity.Property(e => e.My).HasColumnName("my");
            entity.Property(e => e.Mz).HasColumnName("mz");
            entity.Property(e => e.Na).HasColumnName("na");
            entity.Property(e => e.Nc).HasColumnName("nc");
            entity.Property(e => e.Np).HasColumnName("np");
            entity.Property(e => e.Nu).HasColumnName("nu");
            entity.Property(e => e.Pa).HasColumnName("pa");
            entity.Property(e => e.Pb).HasColumnName("pb");
            entity.Property(e => e.Sa).HasColumnName("sa");
            entity.Property(e => e.Sbr).HasColumnName("sbr");
            entity.Property(e => e.Sc).HasColumnName("sc");
            entity.Property(e => e.Scl).HasColumnName("scl");
            entity.Property(e => e.Se).HasColumnName("se");
            entity.Property(e => e.Sh).HasColumnName("sh");
            entity.Property(e => e.Sm).HasColumnName("sm");
            entity.Property(e => e.Sp).HasColumnName("sp");
            entity.Property(e => e.Su).HasColumnName("su");
            entity.Property(e => e.Total).HasColumnName("total");
            entity.Property(e => e.Z1).HasColumnName("z1");
            entity.Property(e => e.Z2).HasColumnName("z2");
            entity.Property(e => e.Z3).HasColumnName("z3");
            entity.Property(e => e.Z4).HasColumnName("z4");
            entity.Property(e => e.Zz).HasColumnName("zz");
        });

        modelBuilder.Entity<Energy50>(entity =>
        {
            entity.HasKey(e => new { e.YrMnth, e.RowNo1 }).HasName("PK_energy");

            entity.ToTable("energy50");

            entity.Property(e => e.YrMnth).HasColumnName("yr_mnth");
            entity.Property(e => e.RowNo1).HasColumnName("row_no1");
            entity.Property(e => e.Ei002).HasColumnName("ei002");
            entity.Property(e => e.Ei003).HasColumnName("ei003");
            entity.Property(e => e.Ei004).HasColumnName("ei004");
            entity.Property(e => e.Ei005).HasColumnName("ei005");
            entity.Property(e => e.Ei007).HasColumnName("ei007");
            entity.Property(e => e.Ei008).HasColumnName("ei008");
            entity.Property(e => e.Ei009).HasColumnName("ei009");
            entity.Property(e => e.TwpNet).HasColumnName("twp_net");
        });

        modelBuilder.Entity<Energy50Db>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("energy50_db");

            entity.Property(e => e.Eim001).HasColumnName("eim001");
            entity.Property(e => e.Eim002).HasColumnName("eim002");
            entity.Property(e => e.Eim003).HasColumnName("eim003");
            entity.Property(e => e.Eim004).HasColumnName("eim004");
            entity.Property(e => e.Eim005).HasColumnName("eim005");
            entity.Property(e => e.Eim006).HasColumnName("eim006");
            entity.Property(e => e.Eim007).HasColumnName("eim007");
            entity.Property(e => e.Eim008).HasColumnName("eim008");
            entity.Property(e => e.Eim008q).HasColumnName("eim008q");
            entity.Property(e => e.Eim008y).HasColumnName("eim008y");
            entity.Property(e => e.Eim009).HasColumnName("eim009");
            entity.Property(e => e.Eim009q).HasColumnName("eim009q");
            entity.Property(e => e.Eim009y).HasColumnName("eim009y");
            entity.Property(e => e.Eim010).HasColumnName("eim010");
            entity.Property(e => e.Eim011).HasColumnName("eim011");
            entity.Property(e => e.Eim012).HasColumnName("eim012");
            entity.Property(e => e.Eim013).HasColumnName("eim013");
            entity.Property(e => e.Eim014).HasColumnName("eim014");
            entity.Property(e => e.Eim015).HasColumnName("eim015");
            entity.Property(e => e.Eim016).HasColumnName("eim016");
            entity.Property(e => e.Eim017).HasColumnName("eim017");
            entity.Property(e => e.Eim018).HasColumnName("eim018");
            entity.Property(e => e.Eim019).HasColumnName("eim019");
            entity.Property(e => e.Eim020).HasColumnName("eim020");
            entity.Property(e => e.Eim021).HasColumnName("eim021");
            entity.Property(e => e.Eim022).HasColumnName("eim022");
            entity.Property(e => e.Eim023).HasColumnName("eim023");
            entity.Property(e => e.Eim024).HasColumnName("eim024");
            entity.Property(e => e.Eim024q).HasColumnName("eim024q");
            entity.Property(e => e.Eim024y).HasColumnName("eim024y");
            entity.Property(e => e.Eiq001).HasColumnName("eiq001");
            entity.Property(e => e.Eiq002).HasColumnName("eiq002");
            entity.Property(e => e.Eiq003).HasColumnName("eiq003");
            entity.Property(e => e.Eiq004).HasColumnName("eiq004");
            entity.Property(e => e.Eiq005).HasColumnName("eiq005");
            entity.Property(e => e.Eiq006).HasColumnName("eiq006");
            entity.Property(e => e.Eiq007).HasColumnName("eiq007");
            entity.Property(e => e.Eiq008).HasColumnName("eiq008");
            entity.Property(e => e.Eiq009).HasColumnName("eiq009");
            entity.Property(e => e.Eiq010).HasColumnName("eiq010");
            entity.Property(e => e.Eiq011).HasColumnName("eiq011");
            entity.Property(e => e.Eiq012).HasColumnName("eiq012");
            entity.Property(e => e.Eiq013).HasColumnName("eiq013");
            entity.Property(e => e.Eiq014).HasColumnName("eiq014");
            entity.Property(e => e.Eiq015).HasColumnName("eiq015");
            entity.Property(e => e.Eiq016).HasColumnName("eiq016");
            entity.Property(e => e.Eiq017).HasColumnName("eiq017");
            entity.Property(e => e.Eiq018).HasColumnName("eiq018");
            entity.Property(e => e.Eiq019).HasColumnName("eiq019");
            entity.Property(e => e.Eiq020).HasColumnName("eiq020");
            entity.Property(e => e.Eiq021).HasColumnName("eiq021");
            entity.Property(e => e.Eiq022).HasColumnName("eiq022");
            entity.Property(e => e.Eiq023).HasColumnName("eiq023");
            entity.Property(e => e.Eiy001).HasColumnName("eiy001");
            entity.Property(e => e.Eiy002).HasColumnName("eiy002");
            entity.Property(e => e.Eiy003).HasColumnName("eiy003");
            entity.Property(e => e.Eiy004).HasColumnName("eiy004");
            entity.Property(e => e.Eiy005).HasColumnName("eiy005");
            entity.Property(e => e.Eiy006).HasColumnName("eiy006");
            entity.Property(e => e.Eiy007).HasColumnName("eiy007");
            entity.Property(e => e.Eiy008).HasColumnName("eiy008");
            entity.Property(e => e.Eiy009).HasColumnName("eiy009");
            entity.Property(e => e.Eiy010).HasColumnName("eiy010");
            entity.Property(e => e.Eiy011).HasColumnName("eiy011");
            entity.Property(e => e.Eiy012).HasColumnName("eiy012");
            entity.Property(e => e.Eiy013).HasColumnName("eiy013");
            entity.Property(e => e.Eiy014).HasColumnName("eiy014");
            entity.Property(e => e.Eiy015).HasColumnName("eiy015");
            entity.Property(e => e.Eiy016).HasColumnName("eiy016");
            entity.Property(e => e.Eiy017).HasColumnName("eiy017");
            entity.Property(e => e.Eiy018).HasColumnName("eiy018");
            entity.Property(e => e.Eiy019).HasColumnName("eiy019");
            entity.Property(e => e.Eiy020).HasColumnName("eiy020");
            entity.Property(e => e.Eiy021).HasColumnName("eiy021");
            entity.Property(e => e.Eiy022).HasColumnName("eiy022");
            entity.Property(e => e.Eiy023).HasColumnName("eiy023");
            entity.Property(e => e.Eiy024).HasColumnName("eiy024");
            entity.Property(e => e.Eiy025).HasColumnName("eiy025");
            entity.Property(e => e.Eiy026).HasColumnName("eiy026");
            entity.Property(e => e.Eiy027).HasColumnName("eiy027");
            entity.Property(e => e.Eiy101).HasColumnName("eiy101");
            entity.Property(e => e.Eiy102).HasColumnName("eiy102");
            entity.Property(e => e.Eiy103).HasColumnName("eiy103");
            entity.Property(e => e.Eiy104).HasColumnName("eiy104");
            entity.Property(e => e.Eiy105).HasColumnName("eiy105");
            entity.Property(e => e.Eiy106).HasColumnName("eiy106");
            entity.Property(e => e.RowNo1).HasColumnName("row_no1");
            entity.Property(e => e.YrMnth).HasColumnName("yr_mnth");
        });

        modelBuilder.Entity<Fuel50Db>(entity =>
        {
            entity.HasKey(e => new { e.YrMnth, e.RowNo1 });

            entity.ToTable("fuel50_db");

            entity.Property(e => e.YrMnth).HasColumnName("yr_mnth");
            entity.Property(e => e.RowNo1).HasColumnName("row_no1");
            entity.Property(e => e.Ah1000).HasColumnName("ah1000");
            entity.Property(e => e.Ah1500).HasColumnName("ah1500");
            entity.Property(e => e.Ah1a20).HasColumnName("ah1a20");
            entity.Property(e => e.Ah1a40).HasColumnName("ah1a40");
            entity.Property(e => e.Ah1b10).HasColumnName("ah1b10");
            entity.Property(e => e.Ah1b20).HasColumnName("ah1b20");
            entity.Property(e => e.Ah2025).HasColumnName("ah2025");
            entity.Property(e => e.Ah2030).HasColumnName("ah2030");
            entity.Property(e => e.Ah2100).HasColumnName("ah2100");
            entity.Property(e => e.Ah2110).HasColumnName("ah2110");
            entity.Property(e => e.Ah2155).HasColumnName("ah2155");
            entity.Property(e => e.Ah2180).HasColumnName("ah2180");
            entity.Property(e => e.Ah3000).HasColumnName("ah3000");
            entity.Property(e => e.Ah3100).HasColumnName("ah3100");
            entity.Property(e => e.Ah3200).HasColumnName("ah3200");
            entity.Property(e => e.Ah4100).HasColumnName("ah4100");
            entity.Property(e => e.Ah4110).HasColumnName("ah4110");
            entity.Property(e => e.Ah4130).HasColumnName("ah4130");
            entity.Property(e => e.Ah4200).HasColumnName("ah4200");
            entity.Property(e => e.Ah9030).HasColumnName("ah9030");
            entity.Property(e => e.Aht).HasColumnName("aht");
            entity.Property(e => e.Ao1000).HasColumnName("ao1000");
            entity.Property(e => e.Ao1500).HasColumnName("ao1500");
            entity.Property(e => e.Ao1a20).HasColumnName("ao1a20");
            entity.Property(e => e.Ao1a40).HasColumnName("ao1a40");
            entity.Property(e => e.Ao1b10).HasColumnName("ao1b10");
            entity.Property(e => e.Ao1b20).HasColumnName("ao1b20");
            entity.Property(e => e.Ao2025).HasColumnName("ao2025");
            entity.Property(e => e.Ao2030).HasColumnName("ao2030");
            entity.Property(e => e.Ao2100).HasColumnName("ao2100");
            entity.Property(e => e.Ao2110).HasColumnName("ao2110");
            entity.Property(e => e.Ao2155).HasColumnName("ao2155");
            entity.Property(e => e.Ao2180).HasColumnName("ao2180");
            entity.Property(e => e.Ao3000).HasColumnName("ao3000");
            entity.Property(e => e.Ao3100).HasColumnName("ao3100");
            entity.Property(e => e.Ao3200).HasColumnName("ao3200");
            entity.Property(e => e.Ao4100).HasColumnName("ao4100");
            entity.Property(e => e.Ao4110).HasColumnName("ao4110");
            entity.Property(e => e.Ao4130).HasColumnName("ao4130");
            entity.Property(e => e.Ao4200).HasColumnName("ao4200");
            entity.Property(e => e.Ao9030).HasColumnName("ao9030");
            entity.Property(e => e.Aot).HasColumnName("aot");
            entity.Property(e => e.As1000).HasColumnName("as1000");
            entity.Property(e => e.As1500).HasColumnName("as1500");
            entity.Property(e => e.As1a20).HasColumnName("as1a20");
            entity.Property(e => e.As1a40).HasColumnName("as1a40");
            entity.Property(e => e.As1b10).HasColumnName("as1b10");
            entity.Property(e => e.As1b20).HasColumnName("as1b20");
            entity.Property(e => e.As2025).HasColumnName("as2025");
            entity.Property(e => e.As2030).HasColumnName("as2030");
            entity.Property(e => e.As2100).HasColumnName("as2100");
            entity.Property(e => e.As2110).HasColumnName("as2110");
            entity.Property(e => e.As2155).HasColumnName("as2155");
            entity.Property(e => e.As2180).HasColumnName("as2180");
            entity.Property(e => e.As3000).HasColumnName("as3000");
            entity.Property(e => e.As3100).HasColumnName("as3100");
            entity.Property(e => e.As3200).HasColumnName("as3200");
            entity.Property(e => e.As4100).HasColumnName("as4100");
            entity.Property(e => e.As4110).HasColumnName("as4110");
            entity.Property(e => e.As4130).HasColumnName("as4130");
            entity.Property(e => e.As4200).HasColumnName("as4200");
            entity.Property(e => e.As9030).HasColumnName("as9030");
            entity.Property(e => e.Ast).HasColumnName("ast");
            entity.Property(e => e.Mh1000).HasColumnName("mh1000");
            entity.Property(e => e.Mh1a20).HasColumnName("mh1a20");
            entity.Property(e => e.Mh2100).HasColumnName("mh2100");
            entity.Property(e => e.Mh2110).HasColumnName("mh2110");
            entity.Property(e => e.Mh3000).HasColumnName("mh3000");
            entity.Property(e => e.Mh3100).HasColumnName("mh3100");
            entity.Property(e => e.Mh3200).HasColumnName("mh3200");
            entity.Property(e => e.Mh9030).HasColumnName("mh9030");
            entity.Property(e => e.Mht).HasColumnName("mht");
            entity.Property(e => e.Mo1000).HasColumnName("mo1000");
            entity.Property(e => e.Mo1a20).HasColumnName("mo1a20");
            entity.Property(e => e.Mo2100).HasColumnName("mo2100");
            entity.Property(e => e.Mo2110).HasColumnName("mo2110");
            entity.Property(e => e.Mo3000).HasColumnName("mo3000");
            entity.Property(e => e.Mo3100).HasColumnName("mo3100");
            entity.Property(e => e.Mo3200).HasColumnName("mo3200");
            entity.Property(e => e.Mo9030).HasColumnName("mo9030");
            entity.Property(e => e.Mot).HasColumnName("mot");
            entity.Property(e => e.Ms1000).HasColumnName("ms1000");
            entity.Property(e => e.Ms1a20).HasColumnName("ms1a20");
            entity.Property(e => e.Ms2100).HasColumnName("ms2100");
            entity.Property(e => e.Ms2110).HasColumnName("ms2110");
            entity.Property(e => e.Ms3000).HasColumnName("ms3000");
            entity.Property(e => e.Ms3100).HasColumnName("ms3100");
            entity.Property(e => e.Ms3200).HasColumnName("ms3200");
            entity.Property(e => e.Ms9030).HasColumnName("ms9030");
            entity.Property(e => e.Mst).HasColumnName("mst");
            entity.Property(e => e.Th1000).HasColumnName("th1000");
            entity.Property(e => e.Th1a20).HasColumnName("th1a20");
            entity.Property(e => e.Th1a40).HasColumnName("th1a40");
            entity.Property(e => e.Th2100).HasColumnName("th2100");
            entity.Property(e => e.Th2110).HasColumnName("th2110");
            entity.Property(e => e.Th3000).HasColumnName("th3000");
            entity.Property(e => e.Th3100).HasColumnName("th3100");
            entity.Property(e => e.Th9030).HasColumnName("th9030");
            entity.Property(e => e.Tht).HasColumnName("tht");
            entity.Property(e => e.To1000).HasColumnName("to1000");
            entity.Property(e => e.To1a20).HasColumnName("to1a20");
            entity.Property(e => e.To1a40).HasColumnName("to1a40");
            entity.Property(e => e.To2100).HasColumnName("to2100");
            entity.Property(e => e.To2110).HasColumnName("to2110");
            entity.Property(e => e.To3000).HasColumnName("to3000");
            entity.Property(e => e.To3100).HasColumnName("to3100");
            entity.Property(e => e.To9030).HasColumnName("to9030");
            entity.Property(e => e.Toh1000).HasColumnName("toh1000");
            entity.Property(e => e.Toh1500).HasColumnName("toh1500");
            entity.Property(e => e.Toh1a20).HasColumnName("toh1a20");
            entity.Property(e => e.Toh1a40).HasColumnName("toh1a40");
            entity.Property(e => e.Toh1b10).HasColumnName("toh1b10");
            entity.Property(e => e.Toh1b20).HasColumnName("toh1b20");
            entity.Property(e => e.Toh2025).HasColumnName("toh2025");
            entity.Property(e => e.Toh2030).HasColumnName("toh2030");
            entity.Property(e => e.Toh2100).HasColumnName("toh2100");
            entity.Property(e => e.Toh2110).HasColumnName("toh2110");
            entity.Property(e => e.Toh2155).HasColumnName("toh2155");
            entity.Property(e => e.Toh2180).HasColumnName("toh2180");
            entity.Property(e => e.Toh3000).HasColumnName("toh3000");
            entity.Property(e => e.Toh3100).HasColumnName("toh3100");
            entity.Property(e => e.Toh3200).HasColumnName("toh3200");
            entity.Property(e => e.Toh4100).HasColumnName("toh4100");
            entity.Property(e => e.Toh4110).HasColumnName("toh4110");
            entity.Property(e => e.Toh4130).HasColumnName("toh4130");
            entity.Property(e => e.Toh4200).HasColumnName("toh4200");
            entity.Property(e => e.Toh9030).HasColumnName("toh9030");
            entity.Property(e => e.Toht).HasColumnName("toht");
            entity.Property(e => e.Too1000).HasColumnName("too1000");
            entity.Property(e => e.Too1500).HasColumnName("too1500");
            entity.Property(e => e.Too1a20).HasColumnName("too1a20");
            entity.Property(e => e.Too1a40).HasColumnName("too1a40");
            entity.Property(e => e.Too1b10).HasColumnName("too1b10");
            entity.Property(e => e.Too1b20).HasColumnName("too1b20");
            entity.Property(e => e.Too2025).HasColumnName("too2025");
            entity.Property(e => e.Too2030).HasColumnName("too2030");
            entity.Property(e => e.Too2100).HasColumnName("too2100");
            entity.Property(e => e.Too2110).HasColumnName("too2110");
            entity.Property(e => e.Too2155).HasColumnName("too2155");
            entity.Property(e => e.Too2180).HasColumnName("too2180");
            entity.Property(e => e.Too3000).HasColumnName("too3000");
            entity.Property(e => e.Too3100).HasColumnName("too3100");
            entity.Property(e => e.Too3200).HasColumnName("too3200");
            entity.Property(e => e.Too4100).HasColumnName("too4100");
            entity.Property(e => e.Too4110).HasColumnName("too4110");
            entity.Property(e => e.Too4130).HasColumnName("too4130");
            entity.Property(e => e.Too4200).HasColumnName("too4200");
            entity.Property(e => e.Too9030).HasColumnName("too9030");
            entity.Property(e => e.Toot).HasColumnName("toot");
            entity.Property(e => e.Tos1000).HasColumnName("tos1000");
            entity.Property(e => e.Tos1500).HasColumnName("tos1500");
            entity.Property(e => e.Tos1a20).HasColumnName("tos1a20");
            entity.Property(e => e.Tos1a40).HasColumnName("tos1a40");
            entity.Property(e => e.Tos1b10).HasColumnName("tos1b10");
            entity.Property(e => e.Tos1b20).HasColumnName("tos1b20");
            entity.Property(e => e.Tos2025).HasColumnName("tos2025");
            entity.Property(e => e.Tos2030).HasColumnName("tos2030");
            entity.Property(e => e.Tos2100).HasColumnName("tos2100");
            entity.Property(e => e.Tos2110).HasColumnName("tos2110");
            entity.Property(e => e.Tos2155).HasColumnName("tos2155");
            entity.Property(e => e.Tos2180).HasColumnName("tos2180");
            entity.Property(e => e.Tos3000).HasColumnName("tos3000");
            entity.Property(e => e.Tos3100).HasColumnName("tos3100");
            entity.Property(e => e.Tos3200).HasColumnName("tos3200");
            entity.Property(e => e.Tos4100).HasColumnName("tos4100");
            entity.Property(e => e.Tos4110).HasColumnName("tos4110");
            entity.Property(e => e.Tos4130).HasColumnName("tos4130");
            entity.Property(e => e.Tos4200).HasColumnName("tos4200");
            entity.Property(e => e.Tos9030).HasColumnName("tos9030");
            entity.Property(e => e.Tost).HasColumnName("tost");
            entity.Property(e => e.Tot).HasColumnName("tot");
            entity.Property(e => e.Ts1000).HasColumnName("ts1000");
            entity.Property(e => e.Ts1a20).HasColumnName("ts1a20");
            entity.Property(e => e.Ts1a40).HasColumnName("ts1a40");
            entity.Property(e => e.Ts2100).HasColumnName("ts2100");
            entity.Property(e => e.Ts2110).HasColumnName("ts2110");
            entity.Property(e => e.Ts3000).HasColumnName("ts3000");
            entity.Property(e => e.Ts3100).HasColumnName("ts3100");
            entity.Property(e => e.Ts9030).HasColumnName("ts9030");
            entity.Property(e => e.Tst).HasColumnName("tst");
        });

        modelBuilder.Entity<Power50>(entity =>
        {
            entity.HasKey(e => new { e.YrMnth, e.RowNo1 });

            entity.ToTable("power50");

            entity.Property(e => e.YrMnth).HasColumnName("yr_mnth");
            entity.Property(e => e.RowNo1).HasColumnName("row_no1");
            entity.Property(e => e.ABiom).HasColumnName("a_biom");
            entity.Property(e => e.ABiomInst1).HasColumnName("a_biom_inst");
            entity.Property(e => e.ACoal).HasColumnName("a_coal");
            entity.Property(e => e.ACoalInst).HasColumnName("a_coal_inst");
            entity.Property(e => e.AGeot).HasColumnName("a_geot");
            entity.Property(e => e.AGeotInst).HasColumnName("a_geot_inst");
            entity.Property(e => e.AHydro).HasColumnName("a_hydro");
            entity.Property(e => e.AHydroInst).HasColumnName("a_hydro_inst");
            entity.Property(e => e.ANg).HasColumnName("a_ng");
            entity.Property(e => e.ANgInst).HasColumnName("a_ng_inst");
            entity.Property(e => e.AOil).HasColumnName("a_oil");
            entity.Property(e => e.AOilInst).HasColumnName("a_oil_inst");
            entity.Property(e => e.ASun).HasColumnName("a_sun");
            entity.Property(e => e.ASunInst).HasColumnName("a_sun_inst");
            entity.Property(e => e.AWast).HasColumnName("a_wast");
            entity.Property(e => e.AWastInst1).HasColumnName("a_wast_inst");
            entity.Property(e => e.AWind).HasColumnName("a_wind");
            entity.Property(e => e.AWindInst).HasColumnName("a_wind_inst");
            entity.Property(e => e.AagInst).HasColumnName("aag_inst");
            entity.Property(e => e.AbiomInst).HasColumnName("abiom_inst");
            entity.Property(e => e.AfcInst).HasColumnName("afc_inst");
            entity.Property(e => e.AfnInst).HasColumnName("afn_inst");
            entity.Property(e => e.AfoInst).HasColumnName("afo_inst");
            entity.Property(e => e.AsInst).HasColumnName("as_inst");
            entity.Property(e => e.AwInst).HasColumnName("aw_inst");
            entity.Property(e => e.AwastInst).HasColumnName("awast_inst");
            entity.Property(e => e.AwhInst).HasColumnName("awh_inst");
            entity.Property(e => e.MaInst).HasColumnName("ma_inst");
            entity.Property(e => e.MaNet).HasColumnName("ma_net");
            entity.Property(e => e.MaSt).HasColumnName("ma_st");
            entity.Property(e => e.MagInst).HasColumnName("mag_inst");
            entity.Property(e => e.MfcInst).HasColumnName("mfc_inst");
            entity.Property(e => e.MfcNet).HasColumnName("mfc_net");
            entity.Property(e => e.MfcSt).HasColumnName("mfc_st");
            entity.Property(e => e.MfnInst).HasColumnName("mfn_inst");
            entity.Property(e => e.MfnNet).HasColumnName("mfn_net");
            entity.Property(e => e.MfnSt).HasColumnName("mfn_st");
            entity.Property(e => e.MgInst).HasColumnName("mg_inst");
            entity.Property(e => e.MgNet).HasColumnName("mg_net");
            entity.Property(e => e.MgSt).HasColumnName("mg_st");
            entity.Property(e => e.MsInst).HasColumnName("ms_inst");
            entity.Property(e => e.MsNet).HasColumnName("ms_net");
            entity.Property(e => e.MsSt).HasColumnName("ms_st");
            entity.Property(e => e.MwInst).HasColumnName("mw_inst");
            entity.Property(e => e.MwNet).HasColumnName("mw_net");
            entity.Property(e => e.MwSt).HasColumnName("mw_st");
            entity.Property(e => e.MwhInst).HasColumnName("mwh_inst");
            entity.Property(e => e.TagInst).HasColumnName("tag_inst");
            entity.Property(e => e.TagNet).HasColumnName("tag_net");
            entity.Property(e => e.TagSt).HasColumnName("tag_st");
            entity.Property(e => e.TawInst).HasColumnName("taw_inst");
            entity.Property(e => e.TawNet).HasColumnName("taw_net");
            entity.Property(e => e.TawSt).HasColumnName("taw_st");
            entity.Property(e => e.TfcInst).HasColumnName("tfc_inst");
            entity.Property(e => e.TfcNet).HasColumnName("tfc_net");
            entity.Property(e => e.TfcSt).HasColumnName("tfc_st");
            entity.Property(e => e.TfnInst).HasColumnName("tfn_inst");
            entity.Property(e => e.TfnNet).HasColumnName("tfn_net");
            entity.Property(e => e.TfnSt).HasColumnName("tfn_st");
            entity.Property(e => e.TfoInst).HasColumnName("tfo_inst");
            entity.Property(e => e.TfoNet).HasColumnName("tfo_net");
            entity.Property(e => e.TfoS).HasColumnName("tfo_s");
            entity.Property(e => e.TfoSt).HasColumnName("tfo_st");
            entity.Property(e => e.TnInst).HasColumnName("tn_inst");
            entity.Property(e => e.TnNet).HasColumnName("tn_net");
            entity.Property(e => e.TnSt).HasColumnName("tn_st");
            entity.Property(e => e.ToagInst).HasColumnName("toag_inst");
            entity.Property(e => e.TobiomInst).HasColumnName("tobiom_inst");
            entity.Property(e => e.TofcInst).HasColumnName("tofc_inst");
            entity.Property(e => e.TofnInst).HasColumnName("tofn_inst");
            entity.Property(e => e.TofoInst).HasColumnName("tofo_inst");
            entity.Property(e => e.TonInst).HasColumnName("ton_inst");
            entity.Property(e => e.TosInst).HasColumnName("tos_inst");
            entity.Property(e => e.TowInst).HasColumnName("tow_inst");
            entity.Property(e => e.TowastInst).HasColumnName("towast_inst");
            entity.Property(e => e.TowhInst).HasColumnName("towh_inst");
            entity.Property(e => e.TowpInst).HasColumnName("towp_inst");
            entity.Property(e => e.TsInst).HasColumnName("ts_inst");
            entity.Property(e => e.TsNet).HasColumnName("ts_net");
            entity.Property(e => e.TsSt).HasColumnName("ts_st");
            entity.Property(e => e.TwInst).HasColumnName("tw_inst");
            entity.Property(e => e.TwhInst).HasColumnName("twh_inst");
            entity.Property(e => e.TwhNet).HasColumnName("twh_net");
            entity.Property(e => e.TwhSt).HasColumnName("twh_st");
            entity.Property(e => e.TwpInst).HasColumnName("twp_inst");
            entity.Property(e => e.TwpNet).HasColumnName("twp_net");
            entity.Property(e => e.TwpSt).HasColumnName("twp_st");
        });

        modelBuilder.Entity<Power50Db>(entity =>
        {
            entity.HasKey(e => new { e.YrMnth, e.RowNo1 });

            entity.ToTable("power50_db");

            entity.Property(e => e.YrMnth).HasColumnName("yr_mnth");
            entity.Property(e => e.RowNo1).HasColumnName("row_no1");
            entity.Property(e => e.Aag).HasColumnName("aag");
            entity.Property(e => e.Abiom).HasColumnName("abiom");
            entity.Property(e => e.Afc).HasColumnName("afc");
            entity.Property(e => e.Afn).HasColumnName("afn");
            entity.Property(e => e.Afo).HasColumnName("afo");
            entity.Property(e => e.As).HasColumnName("as");
            entity.Property(e => e.Aw).HasColumnName("aw");
            entity.Property(e => e.Awast).HasColumnName("awast");
            entity.Property(e => e.Awh).HasColumnName("awh");
            entity.Property(e => e.Mag).HasColumnName("mag");
            entity.Property(e => e.MagNet).HasColumnName("mag_net");
            entity.Property(e => e.MagSt).HasColumnName("mag_st");
            entity.Property(e => e.Mfc).HasColumnName("mfc");
            entity.Property(e => e.MfcNet).HasColumnName("mfc_net");
            entity.Property(e => e.MfcSt).HasColumnName("mfc_st");
            entity.Property(e => e.Mfn).HasColumnName("mfn");
            entity.Property(e => e.MfnNet).HasColumnName("mfn_net");
            entity.Property(e => e.MfnSt).HasColumnName("mfn_st");
            entity.Property(e => e.Ms).HasColumnName("ms");
            entity.Property(e => e.MsNet).HasColumnName("ms_net");
            entity.Property(e => e.MsSt).HasColumnName("ms_st");
            entity.Property(e => e.Mw).HasColumnName("mw");
            entity.Property(e => e.MwNet).HasColumnName("mw_net");
            entity.Property(e => e.MwSt).HasColumnName("mw_st");
            entity.Property(e => e.Mwh).HasColumnName("mwh");
            entity.Property(e => e.MwhNet).HasColumnName("mwh_net");
            entity.Property(e => e.MwhSt).HasColumnName("mwh_st");
            entity.Property(e => e.Tag).HasColumnName("tag");
            entity.Property(e => e.TagNet).HasColumnName("tag_net");
            entity.Property(e => e.TagSt).HasColumnName("tag_st");
            entity.Property(e => e.Tfc).HasColumnName("tfc");
            entity.Property(e => e.TfcNet).HasColumnName("tfc_net");
            entity.Property(e => e.TfcSt).HasColumnName("tfc_st");
            entity.Property(e => e.Tfn).HasColumnName("tfn");
            entity.Property(e => e.TfnNet).HasColumnName("tfn_net");
            entity.Property(e => e.TfnSt).HasColumnName("tfn_st");
            entity.Property(e => e.Tfo).HasColumnName("tfo");
            entity.Property(e => e.TfoNet).HasColumnName("tfo_net");
            entity.Property(e => e.TfoSt).HasColumnName("tfo_st");
            entity.Property(e => e.Tn).HasColumnName("tn");
            entity.Property(e => e.TnNet).HasColumnName("tn_net");
            entity.Property(e => e.TnSt).HasColumnName("tn_st");
            entity.Property(e => e.Toag).HasColumnName("toag");
            entity.Property(e => e.Tobiom).HasColumnName("tobiom");
            entity.Property(e => e.Tofc).HasColumnName("tofc");
            entity.Property(e => e.Tofn).HasColumnName("tofn");
            entity.Property(e => e.Tofo).HasColumnName("tofo");
            entity.Property(e => e.Ton).HasColumnName("ton");
            entity.Property(e => e.Tos).HasColumnName("tos");
            entity.Property(e => e.Tow).HasColumnName("tow");
            entity.Property(e => e.Towast).HasColumnName("towast");
            entity.Property(e => e.Towh).HasColumnName("towh");
            entity.Property(e => e.Towp).HasColumnName("towp");
            entity.Property(e => e.Ts).HasColumnName("ts");
            entity.Property(e => e.TsNet).HasColumnName("ts_net");
            entity.Property(e => e.TsSt).HasColumnName("ts_st");
            entity.Property(e => e.Tw).HasColumnName("tw");
            entity.Property(e => e.TwNet).HasColumnName("tw_net");
            entity.Property(e => e.TwSt).HasColumnName("tw_st");
            entity.Property(e => e.Twh).HasColumnName("twh");
            entity.Property(e => e.TwhNet).HasColumnName("twh_net");
            entity.Property(e => e.TwhSt).HasColumnName("twh_st");
            entity.Property(e => e.Twp).HasColumnName("twp");
            entity.Property(e => e.TwpNet).HasColumnName("twp_net");
            entity.Property(e => e.TwpSt).HasColumnName("twp_st");
        });

        modelBuilder.Entity<TAccount>(entity =>
        {
            entity.ToTable("T_Account");

            entity.Property(e => e.Id).HasMaxLength(32);
            entity.Property(e => e.Account).HasMaxLength(256);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreateUserId)
                .HasMaxLength(32)
                .HasColumnName("CreateUserID");
            entity.Property(e => e.Ip)
                .HasMaxLength(256)
                .HasColumnName("IP");
            entity.Property(e => e.LastLoginTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.Password).HasMaxLength(256);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("UpdateDate ");
            entity.Property(e => e.UpdateUserId)
                .HasMaxLength(32)
                .HasColumnName("UpdateUserID");
        });

        modelBuilder.Entity<TAccountRoleMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__T_Accoun__3214EC07DBC00F96");

            entity.ToTable("T_Account_Role_Mapping");

            entity.Property(e => e.Id).HasMaxLength(32);
            entity.Property(e => e.AccountId).HasMaxLength(32);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreateUserId).HasMaxLength(32);
            entity.Property(e => e.RoleId).HasMaxLength(32);
        });

        modelBuilder.Entity<TBanner>(entity =>
        {
            entity.ToTable("T_Banner");

            entity.Property(e => e.Id)
                .HasMaxLength(32)
                .HasComment("geekors=true;displayName=編號;isHidden=true");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("displayName=建立時間;needToUpdate=false")
                .HasColumnType("datetime");
            entity.Property(e => e.Img)
                .HasMaxLength(1024)
                .HasComment("displayName=圖檔;onList=true");
            entity.Property(e => e.Title)
                .HasMaxLength(256)
                .HasComment("displayName=名稱;onList=true");
            entity.Property(e => e.UpdateDate)
                .HasComment("displayName=修改時間;needToUpdate=true")
                .HasColumnType("datetime");
            entity.Property(e => e.Url)
                .HasMaxLength(1024)
                .HasComment("displayName=網址;onList=true");
        });

        modelBuilder.Entity<TChartDatum>(entity =>
        {
            entity.HasKey(e => new { e.Page, e.Year, e.Chart, e.ColumnName });

            entity.ToTable("T_ChartData");

            entity.Property(e => e.Page).HasMaxLength(50);
            entity.Property(e => e.Chart).HasMaxLength(50);
            entity.Property(e => e.ColumnName).HasMaxLength(50);
        });

        modelBuilder.Entity<TChild>(entity =>
        {
            entity.ToTable("T_Child");

            entity.Property(e => e.Id)
                .HasMaxLength(32)
                .HasComment("geekors=true;displayName=編號;isHidden=true");
            entity.Property(e => e.AltName).HasMaxLength(1000);
            entity.Property(e => e.ColumnId1)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("displayName=欄位名");
            entity.Property(e => e.ColumnId10)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("displayName=欄位名");
            entity.Property(e => e.ColumnId2)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("displayName=欄位名");
            entity.Property(e => e.ColumnId3)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("displayName=欄位名");
            entity.Property(e => e.ColumnId4)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("displayName=欄位名");
            entity.Property(e => e.ColumnId5)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("displayName=欄位名");
            entity.Property(e => e.ColumnId6)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("displayName=欄位名");
            entity.Property(e => e.ColumnId7)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("displayName=欄位名");
            entity.Property(e => e.ColumnId8)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("displayName=欄位名");
            entity.Property(e => e.ColumnId9)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasComment("displayName=欄位名");
            entity.Property(e => e.ColumnIdAll)
                .HasMaxLength(4000)
                .IsUnicode(false)
                .HasComment("displayName=所有欄位編號;onList=true");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("displayName=建立時間;needToUpdate=false")
                .HasColumnType("datetime");
            entity.Property(e => e.DecimalPlaces).HasDefaultValueSql("((3))");
            entity.Property(e => e.IndustryId).HasComment("displayName=產業編號;onList=true");
            entity.Property(e => e.IndustryIds)
                .HasMaxLength(300)
                .HasComment("displayName=所有產業別;onList=true");
            entity.Property(e => e.Iorder)
                .HasComment("displayName=排序;onList=true")
                .HasColumnName("IOrder");
            entity.Property(e => e.Name)
                .HasMaxLength(300)
                .HasComment("displayName=名稱;onList=true");
            entity.Property(e => e.ParentId)
                .HasMaxLength(32)
                .HasComment("displayName=父層;onList=true");
            entity.Property(e => e.SelfId).HasMaxLength(32);
            entity.Property(e => e.UnitList).HasMaxLength(200);
            entity.Property(e => e.UpdateDate)
                .HasComment("displayName=修改時間;needToUpdate=true")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TEnergy>(entity =>
        {
            entity.ToTable("T_Energy");

            entity.Property(e => e.Id)
                .HasMaxLength(32)
                .HasComment("geekors=true;displayName=編號;isHidden=true");
            entity.Property(e => e.ColIdList)
                .HasMaxLength(200)
                .HasComment("displayName=欄位編號;");
            entity.Property(e => e.ColorList)
                .HasMaxLength(1000)
                .HasComment("displayName=顏色編號列表;");
            entity.Property(e => e.Depth).HasComment("displayName=深度;");
            entity.Property(e => e.HideList)
                .HasMaxLength(1000)
                .HasComment("displayName=隱藏列表;");
            entity.Property(e => e.Iorder).HasColumnName("IOrder");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasComment("displayName=名稱;");
            entity.Property(e => e.Notes)
                .HasMaxLength(1000)
                .HasComment("displayName=備註;");
            entity.Property(e => e.PageName)
                .HasMaxLength(50)
                .HasComment("displayName=頁面名稱;onList=true");
            entity.Property(e => e.ParentId)
                .HasMaxLength(32)
                .HasComment("displayName=父層編號;");
            entity.Property(e => e.RowNo1)
                .HasMaxLength(100)
                .HasColumnName("row_no1");
            entity.Property(e => e.TableName)
                .HasMaxLength(100)
                .HasComment("displayName=表格名稱;");
            entity.Property(e => e.UnitListBottom)
                .HasMaxLength(100)
                .HasComment("displayName=單位顯示;");
            entity.Property(e => e.UnitListUpper)
                .HasMaxLength(100)
                .HasComment("displayName=單位顯示上;");
        });

        modelBuilder.Entity<TFlow>(entity =>
        {
            entity.ToTable("T_Flow");

            entity.Property(e => e.Id)
                .HasMaxLength(32)
                .HasComment("geekors=true;displayName=編號;isHidden=true");
            entity.Property(e => e.ColIdList).HasMaxLength(100);
            entity.Property(e => e.DecimalPlaces).HasDefaultValueSql("((3))");
            entity.Property(e => e.Depth).HasComment("displayName=深度;");
            entity.Property(e => e.Iorder).HasColumnName("IOrder");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasComment("displayName=名稱;");
            entity.Property(e => e.PageName)
                .HasMaxLength(50)
                .HasComment("displayName=頁面名稱;onList=true");
            entity.Property(e => e.ParentId)
                .HasMaxLength(32)
                .HasComment("displayName=父層編號;");
            entity.Property(e => e.RowNo1)
                .HasMaxLength(100)
                .HasComment("displayName=產業別編號;")
                .HasColumnName("row_no1");
        });

        modelBuilder.Entity<TIndustry>(entity =>
        {
            entity.ToTable("T_Industry");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("geekors=true;displayName=編號;isHidden=true");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("displayName=建立時間;needToUpdate=false")
                .HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasComment("displayName=名稱;onList=true");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("displayName=修改時間;needToUpdate=true")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TParent>(entity =>
        {
            entity.ToTable("T_Parent");

            entity.Property(e => e.Id)
                .HasMaxLength(32)
                .HasComment("geekors=true;displayName=編號;isHidden=true");
            entity.Property(e => e.Color).HasMaxLength(2000);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("displayName=建立時間;needToUpdate=false")
                .HasColumnType("datetime");
            entity.Property(e => e.EnergyName)
                .HasMaxLength(50)
                .HasComment("displayName=名稱;onList=true");
            entity.Property(e => e.HiddenChart).HasMaxLength(50);
            entity.Property(e => e.Iorder)
                .HasComment("displayName=排序;onList=true")
                .HasColumnName("IOrder");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasComment("displayName=名稱2;onList=true");
            entity.Property(e => e.UnitList).HasMaxLength(200);
            entity.Property(e => e.UnitListUpper).HasMaxLength(200);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("displayName=修改時間;needToUpdate=true")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__T_Permis__3214EC074D8E97F4");

            entity.ToTable("T_Permission");

            entity.Property(e => e.Id).HasMaxLength(32);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreateUserId).HasMaxLength(32);
            entity.Property(e => e.Entitle)
                .HasMaxLength(300)
                .HasDefaultValueSql("('')")
                .HasColumnName("ENTitle");
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdateUserId).HasMaxLength(32);
        });

        modelBuilder.Entity<TPublication>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Publication");

            entity.ToTable("T_Publication");

            entity.Property(e => e.Id)
                .HasMaxLength(32)
                .HasComment("geekors=true;displayName=編號;isHidden=true");
            entity.Property(e => e.Allfile).HasComment("displayName=全檔下載;onList=true;fileType=images");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("displayName=建立時間;needToUpdate=false")
                .HasColumnType("datetime");
            entity.Property(e => e.Entitle)
                .HasMaxLength(300)
                .HasDefaultValueSql("('')")
                .HasColumnName("ENTitle");
            entity.Property(e => e.Image).HasComment("displayName=圖檔;onList=true");
            entity.Property(e => e.Iorder).HasColumnName("IOrder");
            entity.Property(e => e.Title).HasComment("displayName=名稱;onList=true");
            entity.Property(e => e.UpdateDate)
                .HasComment("displayName=修改時間;needToUpdate=true")
                .HasColumnType("datetime");
            entity.Property(e => e.Url).HasComment("displayName=網址;onList=true");
        });

        modelBuilder.Entity<TPublicationLevel1>(entity =>
        {
            entity.ToTable("T_Publication_Level1");

            entity.Property(e => e.Id)
                .HasMaxLength(32)
                .HasComment("geekors=true;displayName=編號;isHidden=true");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("displayName=建立時間;needToUpdate=false")
                .HasColumnType("datetime");
            entity.Property(e => e.Entitle)
                .HasMaxLength(300)
                .HasDefaultValueSql("('')")
                .HasColumnName("ENTitle");
            entity.Property(e => e.Iorder)
                .HasComment("displayName=排序;onList=true")
                .HasColumnName("IOrder");
            entity.Property(e => e.ParentId).HasMaxLength(32);
            entity.Property(e => e.Title)
                .HasMaxLength(300)
                .HasComment("displayName=名稱;onList=true");
            entity.Property(e => e.UpdateDate)
                .HasComment("displayName=修改時間;needToUpdate=true")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TPublicationLevel2>(entity =>
        {
            entity.ToTable("T_Publication_Level2");

            entity.Property(e => e.Id)
                .HasMaxLength(32)
                .HasComment("geekors=true;displayName=編號;isHidden=true");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("displayName=建立時間;needToUpdate=false")
                .HasColumnType("datetime");
            entity.Property(e => e.Entitle)
                .HasMaxLength(300)
                .HasDefaultValueSql("('')")
                .HasColumnName("ENTitle");
            entity.Property(e => e.Iorder)
                .HasComment("displayName=排序;onList=true")
                .HasColumnName("IOrder");
            entity.Property(e => e.ParentId).HasMaxLength(32);
            entity.Property(e => e.Title)
                .HasMaxLength(300)
                .HasComment("displayName=名稱;onList=true");
            entity.Property(e => e.UpdateDate)
                .HasComment("displayName=修改時間;needToUpdate=true")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TPublicationLevel3>(entity =>
        {
            entity.ToTable("T_Publication_Level3");

            entity.Property(e => e.Id)
                .HasMaxLength(32)
                .HasComment("geekors=true;displayName=編號;isHidden=true");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("displayName=建立時間;needToUpdate=false")
                .HasColumnType("datetime");
            entity.Property(e => e.Entitle)
                .HasMaxLength(300)
                .HasDefaultValueSql("('')")
                .HasColumnName("ENTitle");
            entity.Property(e => e.Excel).HasComment("displayName=Excel;onList=true;fileType=image");
            entity.Property(e => e.Iorder)
                .HasComment("displayName=排序;onList=true")
                .HasColumnName("IOrder");
            entity.Property(e => e.Json).HasColumnName("JSON");
            entity.Property(e => e.Meta).HasComment("displayName=Meta;onList=true;fileType=image");
            entity.Property(e => e.Ods).HasComment("displayName=Ods;onList=true;fileType=image");
            entity.Property(e => e.ParentId).HasMaxLength(32);
            entity.Property(e => e.Pdf).HasComment("displayName=Pdf;onList=true;fileType=image");
            entity.Property(e => e.Png).HasColumnName("PNG");
            entity.Property(e => e.Title)
                .HasMaxLength(300)
                .HasComment("displayName=名稱;onList=true");
            entity.Property(e => e.UpdateDate)
                .HasComment("displayName=修改時間;needToUpdate=true")
                .HasColumnType("datetime");
            entity.Property(e => e.Word).HasComment("displayName=Word;onList=true;fileType=image");
        });

        modelBuilder.Entity<TRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__T_Role__3214EC0726C3227C");

            entity.ToTable("T_Role");

            entity.Property(e => e.Id)
                .HasMaxLength(32)
                .HasComment("編號");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("建立時間")
                .HasColumnType("datetime");
            entity.Property(e => e.CreateUserId).HasMaxLength(32);
            entity.Property(e => e.Name)
                .HasMaxLength(256)
                .HasComment("群組名稱");
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UpdateUserId).HasMaxLength(32);
        });

        modelBuilder.Entity<TRolePermissionMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_T_Account_Group_Mapping");

            entity.ToTable("T_Role_Permission_Mapping");

            entity.Property(e => e.Id).HasMaxLength(32);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreateUserId).HasMaxLength(32);
            entity.Property(e => e.PermissionId).HasMaxLength(32);
            entity.Property(e => e.RoleId).HasMaxLength(32);
        });

        modelBuilder.Entity<TSystem>(entity =>
        {
            entity.ToTable("T_System");

            entity.Property(e => e.Id)
                .HasMaxLength(32)
                .HasComment("geekors=true;displayName=編號;isHidden=true");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("displayName=建立時間;needToUpdate=false")
                .HasColumnType("datetime");
            entity.Property(e => e.Mk)
                .HasMaxLength(50)
                .HasComment("displayName=key;onList=true");
            entity.Property(e => e.Mv)
                .HasMaxLength(200)
                .HasComment("displayName=value;onList=true");
            entity.Property(e => e.UpdateDate)
                .HasComment("displayName=修改時間;needToUpdate=true")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TThematic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Thematic");

            entity.ToTable("T_Thematic");

            entity.Property(e => e.Id)
                .HasMaxLength(32)
                .HasComment("geekors=true;displayName=編號;isHidden=true");
            entity.Property(e => e.Context).HasComment("displayName=內文;");
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasComment("displayName=建立時間;needToUpdate=false")
                .HasColumnType("datetime");
            entity.Property(e => e.Image).HasComment("displayName=圖檔;onList=true");
            entity.Property(e => e.Title).HasComment("displayName=名稱;onList=true");
            entity.Property(e => e.UpdateDate)
                .HasComment("displayName=修改時間;needToUpdate=true")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TmpCc>(entity =>
        {
            entity.HasKey(e => new { e.YrMnth, e.Code, e.Code1 });

            entity.ToTable("tmp_cc");

            entity.Property(e => e.YrMnth).HasColumnName("yr_mnth");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.Code1)
                .HasMaxLength(10)
                .HasColumnName("code_1");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.CodeName)
                .HasMaxLength(50)
                .HasColumnName("code_name");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .HasColumnName("country");
        });

        modelBuilder.Entity<UpfileLog>(entity =>
        {
            entity.ToTable("upfile_log");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FileName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("File_Name");
            entity.Property(e => e.FileTime)
                .HasColumnType("smalldatetime")
                .HasColumnName("File_Time");
            entity.Property(e => e.FileType)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("File_Type");
            entity.Property(e => e.FileUser)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("File_User");
        });

        modelBuilder.Entity<Wesdes40>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("wesdes40");

            entity.Property(e => e.H1000).HasColumnName("h1000");
            entity.Property(e => e.H1200).HasColumnName("h1200");
            entity.Property(e => e.H1400).HasColumnName("h1400");
            entity.Property(e => e.H1500).HasColumnName("h1500");
            entity.Property(e => e.H1a10).HasColumnName("h1a10");
            entity.Property(e => e.H1a20).HasColumnName("h1a20");
            entity.Property(e => e.H1a30).HasColumnName("h1a30");
            entity.Property(e => e.H1a40).HasColumnName("h1a40");
            entity.Property(e => e.H1a50).HasColumnName("h1a50");
            entity.Property(e => e.H1a60).HasColumnName("h1a60");
            entity.Property(e => e.H1b10).HasColumnName("h1b10");
            entity.Property(e => e.H1b20).HasColumnName("h1b20");
            entity.Property(e => e.H2000).HasColumnName("h2000");
            entity.Property(e => e.H2010).HasColumnName("h2010");
            entity.Property(e => e.H2020).HasColumnName("h2020");
            entity.Property(e => e.H2025).HasColumnName("h2025");
            entity.Property(e => e.H2030).HasColumnName("h2030");
            entity.Property(e => e.H2040).HasColumnName("h2040");
            entity.Property(e => e.H2050).HasColumnName("h2050");
            entity.Property(e => e.H2060).HasColumnName("h2060");
            entity.Property(e => e.H2070).HasColumnName("h2070");
            entity.Property(e => e.H2075).HasColumnName("h2075");
            entity.Property(e => e.H2081).HasColumnName("h2081");
            entity.Property(e => e.H2082).HasColumnName("h2082");
            entity.Property(e => e.H2090).HasColumnName("h2090");
            entity.Property(e => e.H2100).HasColumnName("h2100");
            entity.Property(e => e.H2110).HasColumnName("h2110");
            entity.Property(e => e.H2120).HasColumnName("h2120");
            entity.Property(e => e.H2130).HasColumnName("h2130");
            entity.Property(e => e.H2140).HasColumnName("h2140");
            entity.Property(e => e.H2150).HasColumnName("h2150");
            entity.Property(e => e.H2155).HasColumnName("h2155");
            entity.Property(e => e.H2180).HasColumnName("h2180");
            entity.Property(e => e.H2190).HasColumnName("h2190");
            entity.Property(e => e.H2200).HasColumnName("h2200");
            entity.Property(e => e.H2210).HasColumnName("h2210");
            entity.Property(e => e.H2220).HasColumnName("h2220");
            entity.Property(e => e.H3000).HasColumnName("h3000");
            entity.Property(e => e.H3100).HasColumnName("h3100");
            entity.Property(e => e.H3200).HasColumnName("h3200");
            entity.Property(e => e.H4000).HasColumnName("h4000");
            entity.Property(e => e.H4100).HasColumnName("h4100");
            entity.Property(e => e.H4110).HasColumnName("h4110");
            entity.Property(e => e.H4120).HasColumnName("h4120");
            entity.Property(e => e.H4130).HasColumnName("h4130");
            entity.Property(e => e.H4200).HasColumnName("h4200");
            entity.Property(e => e.H5110).HasColumnName("h5110");
            entity.Property(e => e.H5120).HasColumnName("h5120");
            entity.Property(e => e.H5130).HasColumnName("h5130");
            entity.Property(e => e.H5140).HasColumnName("h5140");
            entity.Property(e => e.H5150).HasColumnName("h5150");
            entity.Property(e => e.H5200).HasColumnName("h5200");
            entity.Property(e => e.H5300).HasColumnName("h5300");
            entity.Property(e => e.H6000).HasColumnName("h6000");
            entity.Property(e => e.H9000).HasColumnName("h9000");
            entity.Property(e => e.O1000).HasColumnName("o1000");
            entity.Property(e => e.O1200).HasColumnName("o1200");
            entity.Property(e => e.O1400).HasColumnName("o1400");
            entity.Property(e => e.O1500).HasColumnName("o1500");
            entity.Property(e => e.O1a10).HasColumnName("o1a10");
            entity.Property(e => e.O1a20).HasColumnName("o1a20");
            entity.Property(e => e.O1a30).HasColumnName("o1a30");
            entity.Property(e => e.O1a40).HasColumnName("o1a40");
            entity.Property(e => e.O1a50).HasColumnName("o1a50");
            entity.Property(e => e.O1a60).HasColumnName("o1a60");
            entity.Property(e => e.O1b10).HasColumnName("o1b10");
            entity.Property(e => e.O1b20).HasColumnName("o1b20");
            entity.Property(e => e.O2000).HasColumnName("o2000");
            entity.Property(e => e.O2010).HasColumnName("o2010");
            entity.Property(e => e.O2020).HasColumnName("o2020");
            entity.Property(e => e.O2025).HasColumnName("o2025");
            entity.Property(e => e.O2030).HasColumnName("o2030");
            entity.Property(e => e.O2040).HasColumnName("o2040");
            entity.Property(e => e.O2050).HasColumnName("o2050");
            entity.Property(e => e.O2060).HasColumnName("o2060");
            entity.Property(e => e.O2070).HasColumnName("o2070");
            entity.Property(e => e.O2075).HasColumnName("o2075");
            entity.Property(e => e.O2081).HasColumnName("o2081");
            entity.Property(e => e.O2082).HasColumnName("o2082");
            entity.Property(e => e.O2090).HasColumnName("o2090");
            entity.Property(e => e.O2100).HasColumnName("o2100");
            entity.Property(e => e.O2110).HasColumnName("o2110");
            entity.Property(e => e.O2120).HasColumnName("o2120");
            entity.Property(e => e.O2130).HasColumnName("o2130");
            entity.Property(e => e.O2140).HasColumnName("o2140");
            entity.Property(e => e.O2150).HasColumnName("o2150");
            entity.Property(e => e.O2155).HasColumnName("o2155");
            entity.Property(e => e.O2180).HasColumnName("o2180");
            entity.Property(e => e.O2190).HasColumnName("o2190");
            entity.Property(e => e.O2200).HasColumnName("o2200");
            entity.Property(e => e.O2210).HasColumnName("o2210");
            entity.Property(e => e.O2220).HasColumnName("o2220");
            entity.Property(e => e.O3000).HasColumnName("o3000");
            entity.Property(e => e.O3100).HasColumnName("o3100");
            entity.Property(e => e.O3200).HasColumnName("o3200");
            entity.Property(e => e.O4000).HasColumnName("o4000");
            entity.Property(e => e.O4100).HasColumnName("o4100");
            entity.Property(e => e.O4110).HasColumnName("o4110");
            entity.Property(e => e.O4120).HasColumnName("o4120");
            entity.Property(e => e.O4130).HasColumnName("o4130");
            entity.Property(e => e.O4200).HasColumnName("o4200");
            entity.Property(e => e.O5110).HasColumnName("o5110");
            entity.Property(e => e.O5120).HasColumnName("o5120");
            entity.Property(e => e.O5130).HasColumnName("o5130");
            entity.Property(e => e.O5140).HasColumnName("o5140");
            entity.Property(e => e.O5150).HasColumnName("o5150");
            entity.Property(e => e.O5200).HasColumnName("o5200");
            entity.Property(e => e.O5300).HasColumnName("o5300");
            entity.Property(e => e.O6000).HasColumnName("o6000");
            entity.Property(e => e.O9000).HasColumnName("o9000");
            entity.Property(e => e.RowNo1).HasColumnName("row_no1");
            entity.Property(e => e.S1000).HasColumnName("s1000");
            entity.Property(e => e.S1200).HasColumnName("s1200");
            entity.Property(e => e.S1400).HasColumnName("s1400");
            entity.Property(e => e.S1500).HasColumnName("s1500");
            entity.Property(e => e.S1a10).HasColumnName("s1a10");
            entity.Property(e => e.S1a20).HasColumnName("s1a20");
            entity.Property(e => e.S1a30).HasColumnName("s1a30");
            entity.Property(e => e.S1a40).HasColumnName("s1a40");
            entity.Property(e => e.S1a50).HasColumnName("s1a50");
            entity.Property(e => e.S1a60).HasColumnName("s1a60");
            entity.Property(e => e.S1b10).HasColumnName("s1b10");
            entity.Property(e => e.S1b20).HasColumnName("s1b20");
            entity.Property(e => e.S2000).HasColumnName("s2000");
            entity.Property(e => e.S2010).HasColumnName("s2010");
            entity.Property(e => e.S2020).HasColumnName("s2020");
            entity.Property(e => e.S2025).HasColumnName("s2025");
            entity.Property(e => e.S2030).HasColumnName("s2030");
            entity.Property(e => e.S2040).HasColumnName("s2040");
            entity.Property(e => e.S2050).HasColumnName("s2050");
            entity.Property(e => e.S2060).HasColumnName("s2060");
            entity.Property(e => e.S2070).HasColumnName("s2070");
            entity.Property(e => e.S2075).HasColumnName("s2075");
            entity.Property(e => e.S2081).HasColumnName("s2081");
            entity.Property(e => e.S2082).HasColumnName("s2082");
            entity.Property(e => e.S2090).HasColumnName("s2090");
            entity.Property(e => e.S2100).HasColumnName("s2100");
            entity.Property(e => e.S2110).HasColumnName("s2110");
            entity.Property(e => e.S2120).HasColumnName("s2120");
            entity.Property(e => e.S2130).HasColumnName("s2130");
            entity.Property(e => e.S2140).HasColumnName("s2140");
            entity.Property(e => e.S2150).HasColumnName("s2150");
            entity.Property(e => e.S2155).HasColumnName("s2155");
            entity.Property(e => e.S2180).HasColumnName("s2180");
            entity.Property(e => e.S2190).HasColumnName("s2190");
            entity.Property(e => e.S2200).HasColumnName("s2200");
            entity.Property(e => e.S2210).HasColumnName("s2210");
            entity.Property(e => e.S2220).HasColumnName("s2220");
            entity.Property(e => e.S3000).HasColumnName("s3000");
            entity.Property(e => e.S3100).HasColumnName("s3100");
            entity.Property(e => e.S3200).HasColumnName("s3200");
            entity.Property(e => e.S4000).HasColumnName("s4000");
            entity.Property(e => e.S4100).HasColumnName("s4100");
            entity.Property(e => e.S4110).HasColumnName("s4110");
            entity.Property(e => e.S4120).HasColumnName("s4120");
            entity.Property(e => e.S4130).HasColumnName("s4130");
            entity.Property(e => e.S4200).HasColumnName("s4200");
            entity.Property(e => e.S5110).HasColumnName("s5110");
            entity.Property(e => e.S5120).HasColumnName("s5120");
            entity.Property(e => e.S5130).HasColumnName("s5130");
            entity.Property(e => e.S5140).HasColumnName("s5140");
            entity.Property(e => e.S5150).HasColumnName("s5150");
            entity.Property(e => e.S5200).HasColumnName("s5200");
            entity.Property(e => e.S5300).HasColumnName("s5300");
            entity.Property(e => e.S6000).HasColumnName("s6000");
            entity.Property(e => e.Yr)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("yr");
        });

        modelBuilder.Entity<Wesdes50>(entity =>
        {
            entity.HasKey(e => new { e.YrMnth, e.RowNo1 });

            entity.ToTable("wesdes50");

            entity.Property(e => e.YrMnth).HasColumnName("yr_mnth");
            entity.Property(e => e.RowNo1).HasColumnName("row_no1");
            entity.Property(e => e.H1000).HasColumnName("h1000");
            entity.Property(e => e.H1200).HasColumnName("h1200");
            entity.Property(e => e.H1400).HasColumnName("h1400");
            entity.Property(e => e.H1500).HasColumnName("h1500");
            entity.Property(e => e.H1a10).HasColumnName("h1a10");
            entity.Property(e => e.H1a20).HasColumnName("h1a20");
            entity.Property(e => e.H1a30).HasColumnName("h1a30");
            entity.Property(e => e.H1a40).HasColumnName("h1a40");
            entity.Property(e => e.H1a50).HasColumnName("h1a50");
            entity.Property(e => e.H1a60).HasColumnName("h1a60");
            entity.Property(e => e.H1b10).HasColumnName("h1b10");
            entity.Property(e => e.H1b20).HasColumnName("h1b20");
            entity.Property(e => e.H2000).HasColumnName("h2000");
            entity.Property(e => e.H2010).HasColumnName("h2010");
            entity.Property(e => e.H2020).HasColumnName("h2020");
            entity.Property(e => e.H2025).HasColumnName("h2025");
            entity.Property(e => e.H2030).HasColumnName("h2030");
            entity.Property(e => e.H2040).HasColumnName("h2040");
            entity.Property(e => e.H2050).HasColumnName("h2050");
            entity.Property(e => e.H2060).HasColumnName("h2060");
            entity.Property(e => e.H2070).HasColumnName("h2070");
            entity.Property(e => e.H2075).HasColumnName("h2075");
            entity.Property(e => e.H2081).HasColumnName("h2081");
            entity.Property(e => e.H2082).HasColumnName("h2082");
            entity.Property(e => e.H2090).HasColumnName("h2090");
            entity.Property(e => e.H2100).HasColumnName("h2100");
            entity.Property(e => e.H2110).HasColumnName("h2110");
            entity.Property(e => e.H2120).HasColumnName("h2120");
            entity.Property(e => e.H2130).HasColumnName("h2130");
            entity.Property(e => e.H2140).HasColumnName("h2140");
            entity.Property(e => e.H2150).HasColumnName("h2150");
            entity.Property(e => e.H2155).HasColumnName("h2155");
            entity.Property(e => e.H2180).HasColumnName("h2180");
            entity.Property(e => e.H2190).HasColumnName("h2190");
            entity.Property(e => e.H2200).HasColumnName("h2200");
            entity.Property(e => e.H2210).HasColumnName("h2210");
            entity.Property(e => e.H2220).HasColumnName("h2220");
            entity.Property(e => e.H3000).HasColumnName("h3000");
            entity.Property(e => e.H3100).HasColumnName("h3100");
            entity.Property(e => e.H3200).HasColumnName("h3200");
            entity.Property(e => e.H4000).HasColumnName("h4000");
            entity.Property(e => e.H4100).HasColumnName("h4100");
            entity.Property(e => e.H4110).HasColumnName("h4110");
            entity.Property(e => e.H4120).HasColumnName("h4120");
            entity.Property(e => e.H4130).HasColumnName("h4130");
            entity.Property(e => e.H4200).HasColumnName("h4200");
            entity.Property(e => e.H5110).HasColumnName("h5110");
            entity.Property(e => e.H5120).HasColumnName("h5120");
            entity.Property(e => e.H5130).HasColumnName("h5130");
            entity.Property(e => e.H5140).HasColumnName("h5140");
            entity.Property(e => e.H5150).HasColumnName("h5150");
            entity.Property(e => e.H5200).HasColumnName("h5200");
            entity.Property(e => e.H5300).HasColumnName("h5300");
            entity.Property(e => e.H6000).HasColumnName("h6000");
            entity.Property(e => e.H9000).HasColumnName("h9000");
            entity.Property(e => e.H9010).HasColumnName("h9010");
            entity.Property(e => e.H9020).HasColumnName("h9020");
            entity.Property(e => e.H9030).HasColumnName("h9030");
            entity.Property(e => e.H9040).HasColumnName("h9040");
            entity.Property(e => e.H9050).HasColumnName("h9050");
            entity.Property(e => e.O1000).HasColumnName("o1000");
            entity.Property(e => e.O1200).HasColumnName("o1200");
            entity.Property(e => e.O1400).HasColumnName("o1400");
            entity.Property(e => e.O1500).HasColumnName("o1500");
            entity.Property(e => e.O1a10).HasColumnName("o1a10");
            entity.Property(e => e.O1a20).HasColumnName("o1a20");
            entity.Property(e => e.O1a30).HasColumnName("o1a30");
            entity.Property(e => e.O1a40).HasColumnName("o1a40");
            entity.Property(e => e.O1a50).HasColumnName("o1a50");
            entity.Property(e => e.O1a60).HasColumnName("o1a60");
            entity.Property(e => e.O1b10).HasColumnName("o1b10");
            entity.Property(e => e.O1b20).HasColumnName("o1b20");
            entity.Property(e => e.O2000).HasColumnName("o2000");
            entity.Property(e => e.O2010).HasColumnName("o2010");
            entity.Property(e => e.O2020).HasColumnName("o2020");
            entity.Property(e => e.O2025).HasColumnName("o2025");
            entity.Property(e => e.O2030).HasColumnName("o2030");
            entity.Property(e => e.O2040).HasColumnName("o2040");
            entity.Property(e => e.O2050).HasColumnName("o2050");
            entity.Property(e => e.O2060).HasColumnName("o2060");
            entity.Property(e => e.O2070).HasColumnName("o2070");
            entity.Property(e => e.O2075).HasColumnName("o2075");
            entity.Property(e => e.O2081).HasColumnName("o2081");
            entity.Property(e => e.O2082).HasColumnName("o2082");
            entity.Property(e => e.O2090).HasColumnName("o2090");
            entity.Property(e => e.O2100).HasColumnName("o2100");
            entity.Property(e => e.O2110).HasColumnName("o2110");
            entity.Property(e => e.O2120).HasColumnName("o2120");
            entity.Property(e => e.O2130).HasColumnName("o2130");
            entity.Property(e => e.O2140).HasColumnName("o2140");
            entity.Property(e => e.O2150).HasColumnName("o2150");
            entity.Property(e => e.O2155).HasColumnName("o2155");
            entity.Property(e => e.O2180).HasColumnName("o2180");
            entity.Property(e => e.O2190).HasColumnName("o2190");
            entity.Property(e => e.O2200).HasColumnName("o2200");
            entity.Property(e => e.O2210).HasColumnName("o2210");
            entity.Property(e => e.O2220).HasColumnName("o2220");
            entity.Property(e => e.O3000).HasColumnName("o3000");
            entity.Property(e => e.O3100).HasColumnName("o3100");
            entity.Property(e => e.O3200).HasColumnName("o3200");
            entity.Property(e => e.O4000).HasColumnName("o4000");
            entity.Property(e => e.O4100).HasColumnName("o4100");
            entity.Property(e => e.O4110).HasColumnName("o4110");
            entity.Property(e => e.O4120).HasColumnName("o4120");
            entity.Property(e => e.O4130).HasColumnName("o4130");
            entity.Property(e => e.O4200).HasColumnName("o4200");
            entity.Property(e => e.O5110).HasColumnName("o5110");
            entity.Property(e => e.O5120).HasColumnName("o5120");
            entity.Property(e => e.O5130).HasColumnName("o5130");
            entity.Property(e => e.O5140).HasColumnName("o5140");
            entity.Property(e => e.O5150).HasColumnName("o5150");
            entity.Property(e => e.O5200).HasColumnName("o5200");
            entity.Property(e => e.O5300).HasColumnName("o5300");
            entity.Property(e => e.O6000).HasColumnName("o6000");
            entity.Property(e => e.O9000).HasColumnName("o9000");
            entity.Property(e => e.O9010).HasColumnName("o9010");
            entity.Property(e => e.O9020).HasColumnName("o9020");
            entity.Property(e => e.O9030).HasColumnName("o9030");
            entity.Property(e => e.O9040).HasColumnName("o9040");
            entity.Property(e => e.O9050).HasColumnName("o9050");
            entity.Property(e => e.S1000).HasColumnName("s1000");
            entity.Property(e => e.S1200).HasColumnName("s1200");
            entity.Property(e => e.S1400).HasColumnName("s1400");
            entity.Property(e => e.S1500).HasColumnName("s1500");
            entity.Property(e => e.S1a10).HasColumnName("s1a10");
            entity.Property(e => e.S1a20).HasColumnName("s1a20");
            entity.Property(e => e.S1a30).HasColumnName("s1a30");
            entity.Property(e => e.S1a40).HasColumnName("s1a40");
            entity.Property(e => e.S1a50).HasColumnName("s1a50");
            entity.Property(e => e.S1a60).HasColumnName("s1a60");
            entity.Property(e => e.S1b10).HasColumnName("s1b10");
            entity.Property(e => e.S1b20).HasColumnName("s1b20");
            entity.Property(e => e.S2000).HasColumnName("s2000");
            entity.Property(e => e.S2010).HasColumnName("s2010");
            entity.Property(e => e.S2020).HasColumnName("s2020");
            entity.Property(e => e.S2025).HasColumnName("s2025");
            entity.Property(e => e.S2030).HasColumnName("s2030");
            entity.Property(e => e.S2040).HasColumnName("s2040");
            entity.Property(e => e.S2050).HasColumnName("s2050");
            entity.Property(e => e.S2060).HasColumnName("s2060");
            entity.Property(e => e.S2070).HasColumnName("s2070");
            entity.Property(e => e.S2075).HasColumnName("s2075");
            entity.Property(e => e.S2081).HasColumnName("s2081");
            entity.Property(e => e.S2082).HasColumnName("s2082");
            entity.Property(e => e.S2090).HasColumnName("s2090");
            entity.Property(e => e.S2100).HasColumnName("s2100");
            entity.Property(e => e.S2110).HasColumnName("s2110");
            entity.Property(e => e.S2120).HasColumnName("s2120");
            entity.Property(e => e.S2130).HasColumnName("s2130");
            entity.Property(e => e.S2140).HasColumnName("s2140");
            entity.Property(e => e.S2150).HasColumnName("s2150");
            entity.Property(e => e.S2155).HasColumnName("s2155");
            entity.Property(e => e.S2180).HasColumnName("s2180");
            entity.Property(e => e.S2190).HasColumnName("s2190");
            entity.Property(e => e.S2200).HasColumnName("s2200");
            entity.Property(e => e.S2210).HasColumnName("s2210");
            entity.Property(e => e.S2220).HasColumnName("s2220");
            entity.Property(e => e.S3000).HasColumnName("s3000");
            entity.Property(e => e.S3100).HasColumnName("s3100");
            entity.Property(e => e.S3200).HasColumnName("s3200");
            entity.Property(e => e.S4000).HasColumnName("s4000");
            entity.Property(e => e.S4100).HasColumnName("s4100");
            entity.Property(e => e.S4110).HasColumnName("s4110");
            entity.Property(e => e.S4120).HasColumnName("s4120");
            entity.Property(e => e.S4130).HasColumnName("s4130");
            entity.Property(e => e.S4200).HasColumnName("s4200");
            entity.Property(e => e.S5110).HasColumnName("s5110");
            entity.Property(e => e.S5120).HasColumnName("s5120");
            entity.Property(e => e.S5130).HasColumnName("s5130");
            entity.Property(e => e.S5140).HasColumnName("s5140");
            entity.Property(e => e.S5150).HasColumnName("s5150");
            entity.Property(e => e.S5200).HasColumnName("s5200");
            entity.Property(e => e.S5300).HasColumnName("s5300");
            entity.Property(e => e.S6000).HasColumnName("s6000");
            entity.Property(e => e.S9000).HasColumnName("s9000");
            entity.Property(e => e.S9010).HasColumnName("s9010");
            entity.Property(e => e.S9020).HasColumnName("s9020");
            entity.Property(e => e.S9030).HasColumnName("s9030");
            entity.Property(e => e.S9040).HasColumnName("s9040");
            entity.Property(e => e.S9050).HasColumnName("s9050");
            entity.Property(e => e.S9060).HasColumnName("s9060");
            entity.Property(e => e.S9070).HasColumnName("s9070");
        });

        modelBuilder.Entity<Wesdes50Db>(entity =>
        {
            entity.HasKey(e => new { e.YrMnth, e.RowNo1 });

            entity.ToTable("wesdes50_db");

            entity.Property(e => e.YrMnth).HasColumnName("yr_mnth");
            entity.Property(e => e.RowNo1).HasColumnName("row_no1");
            entity.Property(e => e.H1000).HasColumnName("h1000");
            entity.Property(e => e.H1200).HasColumnName("h1200");
            entity.Property(e => e.H1400).HasColumnName("h1400");
            entity.Property(e => e.H1500).HasColumnName("h1500");
            entity.Property(e => e.H1a10).HasColumnName("h1a10");
            entity.Property(e => e.H1a20).HasColumnName("h1a20");
            entity.Property(e => e.H1a30).HasColumnName("h1a30");
            entity.Property(e => e.H1a40).HasColumnName("h1a40");
            entity.Property(e => e.H1a50).HasColumnName("h1a50");
            entity.Property(e => e.H1a60).HasColumnName("h1a60");
            entity.Property(e => e.H1b10).HasColumnName("h1b10");
            entity.Property(e => e.H1b20).HasColumnName("h1b20");
            entity.Property(e => e.H2000).HasColumnName("h2000");
            entity.Property(e => e.H2010).HasColumnName("h2010");
            entity.Property(e => e.H2020).HasColumnName("h2020");
            entity.Property(e => e.H2025).HasColumnName("h2025");
            entity.Property(e => e.H2030).HasColumnName("h2030");
            entity.Property(e => e.H2040).HasColumnName("h2040");
            entity.Property(e => e.H2050).HasColumnName("h2050");
            entity.Property(e => e.H2060).HasColumnName("h2060");
            entity.Property(e => e.H2070).HasColumnName("h2070");
            entity.Property(e => e.H2075).HasColumnName("h2075");
            entity.Property(e => e.H2081).HasColumnName("h2081");
            entity.Property(e => e.H2082).HasColumnName("h2082");
            entity.Property(e => e.H2090).HasColumnName("h2090");
            entity.Property(e => e.H2100).HasColumnName("h2100");
            entity.Property(e => e.H2110).HasColumnName("h2110");
            entity.Property(e => e.H2120).HasColumnName("h2120");
            entity.Property(e => e.H2130).HasColumnName("h2130");
            entity.Property(e => e.H2140).HasColumnName("h2140");
            entity.Property(e => e.H2150).HasColumnName("h2150");
            entity.Property(e => e.H2155).HasColumnName("h2155");
            entity.Property(e => e.H2180).HasColumnName("h2180");
            entity.Property(e => e.H2190).HasColumnName("h2190");
            entity.Property(e => e.H2200).HasColumnName("h2200");
            entity.Property(e => e.H2210).HasColumnName("h2210");
            entity.Property(e => e.H2220).HasColumnName("h2220");
            entity.Property(e => e.H3000).HasColumnName("h3000");
            entity.Property(e => e.H3100).HasColumnName("h3100");
            entity.Property(e => e.H3200).HasColumnName("h3200");
            entity.Property(e => e.H4000).HasColumnName("h4000");
            entity.Property(e => e.H4100).HasColumnName("h4100");
            entity.Property(e => e.H4110).HasColumnName("h4110");
            entity.Property(e => e.H4120).HasColumnName("h4120");
            entity.Property(e => e.H4130).HasColumnName("h4130");
            entity.Property(e => e.H4200).HasColumnName("h4200");
            entity.Property(e => e.H5110).HasColumnName("h5110");
            entity.Property(e => e.H5120).HasColumnName("h5120");
            entity.Property(e => e.H5130).HasColumnName("h5130");
            entity.Property(e => e.H5140).HasColumnName("h5140");
            entity.Property(e => e.H5150).HasColumnName("h5150");
            entity.Property(e => e.H5200).HasColumnName("h5200");
            entity.Property(e => e.H5300).HasColumnName("h5300");
            entity.Property(e => e.H6000).HasColumnName("h6000");
            entity.Property(e => e.H9000).HasColumnName("h9000");
            entity.Property(e => e.H9010).HasColumnName("h9010");
            entity.Property(e => e.H9020).HasColumnName("h9020");
            entity.Property(e => e.H9030).HasColumnName("h9030");
            entity.Property(e => e.H9040).HasColumnName("h9040");
            entity.Property(e => e.H9050).HasColumnName("h9050");
            entity.Property(e => e.O1000).HasColumnName("o1000");
            entity.Property(e => e.O1200).HasColumnName("o1200");
            entity.Property(e => e.O1400).HasColumnName("o1400");
            entity.Property(e => e.O1500).HasColumnName("o1500");
            entity.Property(e => e.O1a10).HasColumnName("o1a10");
            entity.Property(e => e.O1a20).HasColumnName("o1a20");
            entity.Property(e => e.O1a30).HasColumnName("o1a30");
            entity.Property(e => e.O1a40).HasColumnName("o1a40");
            entity.Property(e => e.O1a50).HasColumnName("o1a50");
            entity.Property(e => e.O1a60).HasColumnName("o1a60");
            entity.Property(e => e.O1b10).HasColumnName("o1b10");
            entity.Property(e => e.O1b20).HasColumnName("o1b20");
            entity.Property(e => e.O2000).HasColumnName("o2000");
            entity.Property(e => e.O2010).HasColumnName("o2010");
            entity.Property(e => e.O2020).HasColumnName("o2020");
            entity.Property(e => e.O2025).HasColumnName("o2025");
            entity.Property(e => e.O2030).HasColumnName("o2030");
            entity.Property(e => e.O2040).HasColumnName("o2040");
            entity.Property(e => e.O2050).HasColumnName("o2050");
            entity.Property(e => e.O2060).HasColumnName("o2060");
            entity.Property(e => e.O2070).HasColumnName("o2070");
            entity.Property(e => e.O2075).HasColumnName("o2075");
            entity.Property(e => e.O2081).HasColumnName("o2081");
            entity.Property(e => e.O2082).HasColumnName("o2082");
            entity.Property(e => e.O2090).HasColumnName("o2090");
            entity.Property(e => e.O2100).HasColumnName("o2100");
            entity.Property(e => e.O2110).HasColumnName("o2110");
            entity.Property(e => e.O2120).HasColumnName("o2120");
            entity.Property(e => e.O2130).HasColumnName("o2130");
            entity.Property(e => e.O2140).HasColumnName("o2140");
            entity.Property(e => e.O2150).HasColumnName("o2150");
            entity.Property(e => e.O2155).HasColumnName("o2155");
            entity.Property(e => e.O2180).HasColumnName("o2180");
            entity.Property(e => e.O2190).HasColumnName("o2190");
            entity.Property(e => e.O2200).HasColumnName("o2200");
            entity.Property(e => e.O2210).HasColumnName("o2210");
            entity.Property(e => e.O2220).HasColumnName("o2220");
            entity.Property(e => e.O3000).HasColumnName("o3000");
            entity.Property(e => e.O3100).HasColumnName("o3100");
            entity.Property(e => e.O3200).HasColumnName("o3200");
            entity.Property(e => e.O4000).HasColumnName("o4000");
            entity.Property(e => e.O4100).HasColumnName("o4100");
            entity.Property(e => e.O4110).HasColumnName("o4110");
            entity.Property(e => e.O4120).HasColumnName("o4120");
            entity.Property(e => e.O4130).HasColumnName("o4130");
            entity.Property(e => e.O4200).HasColumnName("o4200");
            entity.Property(e => e.O5110).HasColumnName("o5110");
            entity.Property(e => e.O5120).HasColumnName("o5120");
            entity.Property(e => e.O5130).HasColumnName("o5130");
            entity.Property(e => e.O5140).HasColumnName("o5140");
            entity.Property(e => e.O5150).HasColumnName("o5150");
            entity.Property(e => e.O5200).HasColumnName("o5200");
            entity.Property(e => e.O5300).HasColumnName("o5300");
            entity.Property(e => e.O6000).HasColumnName("o6000");
            entity.Property(e => e.O9000).HasColumnName("o9000");
            entity.Property(e => e.O9010).HasColumnName("o9010");
            entity.Property(e => e.O9020).HasColumnName("o9020");
            entity.Property(e => e.O9030).HasColumnName("o9030");
            entity.Property(e => e.O9040).HasColumnName("o9040");
            entity.Property(e => e.O9050).HasColumnName("o9050");
            entity.Property(e => e.S1000).HasColumnName("s1000");
            entity.Property(e => e.S1200).HasColumnName("s1200");
            entity.Property(e => e.S1400).HasColumnName("s1400");
            entity.Property(e => e.S1500).HasColumnName("s1500");
            entity.Property(e => e.S1a10).HasColumnName("s1a10");
            entity.Property(e => e.S1a20).HasColumnName("s1a20");
            entity.Property(e => e.S1a30).HasColumnName("s1a30");
            entity.Property(e => e.S1a40).HasColumnName("s1a40");
            entity.Property(e => e.S1a50).HasColumnName("s1a50");
            entity.Property(e => e.S1a60).HasColumnName("s1a60");
            entity.Property(e => e.S1b10).HasColumnName("s1b10");
            entity.Property(e => e.S1b20).HasColumnName("s1b20");
            entity.Property(e => e.S2000).HasColumnName("s2000");
            entity.Property(e => e.S2010).HasColumnName("s2010");
            entity.Property(e => e.S2020).HasColumnName("s2020");
            entity.Property(e => e.S2025).HasColumnName("s2025");
            entity.Property(e => e.S2030).HasColumnName("s2030");
            entity.Property(e => e.S2040).HasColumnName("s2040");
            entity.Property(e => e.S2050).HasColumnName("s2050");
            entity.Property(e => e.S2060).HasColumnName("s2060");
            entity.Property(e => e.S2070).HasColumnName("s2070");
            entity.Property(e => e.S2075).HasColumnName("s2075");
            entity.Property(e => e.S2081).HasColumnName("s2081");
            entity.Property(e => e.S2082).HasColumnName("s2082");
            entity.Property(e => e.S2090).HasColumnName("s2090");
            entity.Property(e => e.S2100).HasColumnName("s2100");
            entity.Property(e => e.S2110).HasColumnName("s2110");
            entity.Property(e => e.S2120).HasColumnName("s2120");
            entity.Property(e => e.S2130).HasColumnName("s2130");
            entity.Property(e => e.S2140).HasColumnName("s2140");
            entity.Property(e => e.S2150).HasColumnName("s2150");
            entity.Property(e => e.S2155).HasColumnName("s2155");
            entity.Property(e => e.S2180).HasColumnName("s2180");
            entity.Property(e => e.S2190).HasColumnName("s2190");
            entity.Property(e => e.S2200).HasColumnName("s2200");
            entity.Property(e => e.S2210).HasColumnName("s2210");
            entity.Property(e => e.S2220).HasColumnName("s2220");
            entity.Property(e => e.S3000).HasColumnName("s3000");
            entity.Property(e => e.S3100).HasColumnName("s3100");
            entity.Property(e => e.S3200).HasColumnName("s3200");
            entity.Property(e => e.S4000).HasColumnName("s4000");
            entity.Property(e => e.S4100).HasColumnName("s4100");
            entity.Property(e => e.S4110).HasColumnName("s4110");
            entity.Property(e => e.S4120).HasColumnName("s4120");
            entity.Property(e => e.S4130).HasColumnName("s4130");
            entity.Property(e => e.S4200).HasColumnName("s4200");
            entity.Property(e => e.S5110).HasColumnName("s5110");
            entity.Property(e => e.S5120).HasColumnName("s5120");
            entity.Property(e => e.S5130).HasColumnName("s5130");
            entity.Property(e => e.S5140).HasColumnName("s5140");
            entity.Property(e => e.S5150).HasColumnName("s5150");
            entity.Property(e => e.S5200).HasColumnName("s5200");
            entity.Property(e => e.S5300).HasColumnName("s5300");
            entity.Property(e => e.S6000).HasColumnName("s6000");
            entity.Property(e => e.S9000).HasColumnName("s9000");
            entity.Property(e => e.S9010).HasColumnName("s9010");
            entity.Property(e => e.S9020).HasColumnName("s9020");
            entity.Property(e => e.S9030).HasColumnName("s9030");
            entity.Property(e => e.S9040).HasColumnName("s9040");
            entity.Property(e => e.S9050).HasColumnName("s9050");
            entity.Property(e => e.S9060).HasColumnName("s9060");
            entity.Property(e => e.S9070).HasColumnName("s9070");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
