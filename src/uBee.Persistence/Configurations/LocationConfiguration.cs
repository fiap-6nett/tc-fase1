using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using uBee.Domain.Entities;

namespace uBee.Persistence.Configurations
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("locations");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Id)
                .ValueGeneratedOnAdd();

            builder.Property(l => l.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(l => l.Number)
                .HasColumnName("DDD")
                .IsRequired();

            builder.Property(l => l.CreatedAt)
                .IsRequired();

            builder.Property(l => l.LastUpdatedAt)
                .IsRequired(false);

            builder.Property(l => l.IsDeleted)
                .IsRequired();

            builder.HasMany(l => l.Users)
                   .WithOne(u => u.Location)
                   .HasForeignKey(u => u.LocationId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.IsDeleted);
            builder.Property(p => p.CreatedAt).IsRequired();
            builder.Property(p => p.LastUpdatedAt);

            builder.HasQueryFilter(p => !p.IsDeleted);

            SeedBuiltInLocations(builder);
        }

        private void SeedBuiltInLocations(EntityTypeBuilder<Location> builder)
        {
            var locations = new List<(byte Id, string Name, int Number)>
            {
                (1,     "São Paulo - Capital",              11),
                (2,     "São José dos Campos e Região",     12),
                (3,     "Baixada Santista e Região",        13),
                (4,     "Bauru e Região",                   14),
                (5,     "Sorocaba e Região",                15),
                (6,     "Ribeirão Preto e Região",          16),
                (7,     "São José do Rio Preto e Região",   17),
                (8,     "Presidente Prudente e Região",     18),
                (9,     "Campinas e Região",                19),
                (10,    "Rio de Janeiro - Capital",         21),
                (11,    "Campos dos Goytacazes e Região",   22),
                (12,    "Volta Redonda e Região",           24),
                (13,    "Vitória e Região",                 27),
                (14,    "Sul do Espírito Santo",            28),
                (15,    "Belo Horizonte e Região",          31),
                (16,    "Juiz de Fora e Região",            32),
                (17,    "Governador Valadares e Região",    33),
                (18,    "Uberlândia e Região",              34),
                (19,    "Poços de Caldas e Região",         35),
                (20,    "Divinópolis e Região",             37),
                (21,    "Montes Claros e Região",           38),
                (22,    "Curitiba e Região",                41),
                (23,    "Ponta Grossa e Região",            42),
                (24,    "Londrina e Região",                43),
                (25,    "Maringá e Região",                 44),
                (26,    "Foz do Iguaçu e Região",           45),
                (27,    "Pato Branco e Região",             46),
                (28,    "Joinville e Região",               47),
                (29,    "Florianópolis e Região",           48),
                (30,    "Chapecó e Região",                 49),
                (31,    "Porto Alegre e Região",            51),
                (32,    "Pelotas e Região",                 53),
                (33,    "Caxias do Sul e Região",           54),
                (34,    "Santa Maria e Região",             55),
                (35,    "Distrito Federal",                 61),
                (36,    "Goiânia e Região",                 62),
                (37,    "Tocantins",                        63),
                (38,    "Rio Verde e Região",               64),
                (39,    "Cuiabá e Região",                  65),
                (40,    "Rondonópolis e Região",            66),
                (41,    "Campo Grande e Região",            67),
                (42,    "Acre",                             68),
                (43,    "Rondônia",                         69),
                (44,    "Salvador e Região",                71),
                (45,    "Ilhéus e Região",                  73),
                (46,    "Juazeiro e Região",                74),
                (47,    "Feira de Santana e Região",        75),
                (48,    "Barreiras e Região",               77),
                (49,    "Sergipe",                          79),
                (50,    "Recife e Região",                  81),
                (51,    "Alagoas",                          82),
                (52,    "Paraíba",                          83),
                (53,    "Rio Grande do Norte",              84),
                (54,    "Fortaleza e Região",               85),
                (55,    "Teresina e Região",                86),
                (56,    "Petrolina e Região",               87),
                (57,    "Região do Cariri",                 88),
                (58,    "Piauí exceto Teresina",            89),
                (59,    "Belém e Região",                   91),
                (60,    "Manaus e Região",                  92),
                (61,    "Santarém e Região",                93),
                (62,    "Marabá e Região",                  94),
                (63,    "Roraima",                          95),
                (64,    "Amapá",                            96),
                (65,    "Amazonas",                         97),
                (66,    "São Luís e Região",                98),
                (67,    "Imperatriz e Região",              99)
            };

            builder.HasData(locations.Select(location => new
            {
                location.Id,
                location.Name,
                location.Number,
                CreatedAt = DateTime.MinValue.Date,
                IsDeleted = false
            }));
        }
    }
}
