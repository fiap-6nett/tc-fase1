using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using uBee.Domain.Entities;

namespace uBee.Persistence.Mappings
{
    public class LocationMapping : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("locations");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.DDD)
                   .IsRequired();

            builder.Property(x => x.Region)
                   .HasMaxLength(100)
                   .IsRequired();

            SeedLocations(builder);
        }

        private void SeedLocations(EntityTypeBuilder<Location> builder)
        {
            var locations = new List<(int DDD, string Region)>
            {
                (11, "São Paulo (capital)"),
                (12, "São José dos Campos e região"),
                (13, "Baixada Santista e região"),
                (14, "Bauru e região"),
                (15, "Sorocaba e região"),
                (16, "Ribeirão Preto e região"),
                (17, "São José do Rio Preto e região"),
                (18, "Presidente Prudente e região"),
                (19, "Campinas e região"),
                (21, "Rio de Janeiro (capital)"),
                (22, "Campos dos Goytacazes e região"),
                (24, "Volta Redonda e região"),
                (27, "Vitória e região"),
                (28, "Sul do Espírito Santo"),
                (31, "Belo Horizonte e região"),
                (32, "Juiz de Fora e região"),
                (33, "Governador Valadares e região"),
                (34, "Uberlândia e região"),
                (35, "Poços de Caldas e região"),
                (37, "Divinópolis e região"),
                (38, "Montes Claros e região"),
                (41, "Curitiba e região"),
                (42, "Ponta Grossa e região"),
                (43, "Londrina e região"),
                (44, "Maringá e região"),
                (45, "Foz do Iguaçu e região"),
                (46, "Pato Branco e região"),
                (47, "Joinville e região"),
                (48, "Florianópolis e região"),
                (49, "Chapecó e região"),
                (51, "Porto Alegre e região"),
                (53, "Pelotas e região"),
                (54, "Caxias do Sul e região"),
                (55, "Santa Maria e região"),
                (61, "Distrito Federal"),
                (62, "Goiânia e região"),
                (63, "Tocantins"),
                (64, "Rio Verde e região"),
                (65, "Cuiabá e região"),
                (66, "Rondonópolis e região"),
                (67, "Campo Grande e região"),
                (68, "Acre"),
                (69, "Rondônia"),
                (71, "Salvador e região"),
                (73, "Ilhéus e região"),
                (74, "Juazeiro e região"),
                (75, "Feira de Santana e região"),
                (77, "Barreiras e região"),
                (79, "Sergipe"),
                (81, "Recife e região"),
                (82, "Alagoas"),
                (83, "Paraíba"),
                (84, "Rio Grande do Norte"),
                (85, "Fortaleza e região"),
                (86, "Teresina e região"),
                (87, "Petrolina e região"),
                (88, "Região do Cariri (Ceará)"),
                (89, "Piauí (exceto Teresina)"),
                (91, "Belém e região"),
                (92, "Manaus e região"),
                (93, "Santarém e região"),
                (94, "Marabá e região"),
                (95, "Roraima"),
                (96, "Amapá"),
                (97, "Amazonas"),
                (98, "São Luís e região"),
                (99, "Imperatriz e região")
            };

            builder.HasData(locations.Select(location => new
            {
                Id = Guid.NewGuid(),
                location.DDD,
                location.Region
            }));
        }
    }
}
