using uBee.Domain.Entities;
using uBee.Persistence.Core.Primitives;

namespace uBee.Persistence.Seeds
{
    internal sealed class LocationSeed : EntitySeedConfiguration<Location>
    {
        public override IEnumerable<object> Seed()
        {
            var locations = new List<(int Id, int DDD, string Region)>
            {
                (1, 11, "São Paulo (capital)"),
                (2, 12, "São José dos Campos e região"),
                (3, 13, "Baixada Santista e região"),
                (4, 14, "Bauru e região"),
                (5, 15, "Sorocaba e região"),
                (6, 16, "Ribeirão Preto e região"),
                (7, 17, "São José do Rio Preto e região"),
                (8, 18, "Presidente Prudente e região"),
                (9, 19, "Campinas e região"),
                (10, 21, "Rio de Janeiro (capital)"),
                (11, 22, "Campos dos Goytacazes e região"),
                (12, 24, "Volta Redonda e região"),
                (13, 27, "Vitória e região"),
                (14, 28, "Sul do Espírito Santo"),
                (15, 31, "Belo Horizonte e região"),
                (16, 32, "Juiz de Fora e região"),
                (17, 33, "Governador Valadares e região"),
                (18, 34, "Uberlândia e região"),
                (19, 35, "Poços de Caldas e região"),
                (20, 37, "Divinópolis e região"),
                (21, 38, "Montes Claros e região"),
                (22, 41, "Curitiba e região"),
                (23, 42, "Ponta Grossa e região"),
                (24, 43, "Londrina e região"),
                (25, 44, "Maringá e região"),
                (26, 45, "Foz do Iguaçu e região"),
                (27, 46, "Pato Branco e região"),
                (28, 47, "Joinville e região"),
                (29, 48, "Florianópolis e região"),
                (30, 49, "Chapecó e região"),
                (31, 51, "Porto Alegre e região"),
                (32, 53, "Pelotas e região"),
                (33, 54, "Caxias do Sul e região"),
                (34, 55, "Santa Maria e região"),
                (35, 61, "Distrito Federal"),
                (36, 62, "Goiânia e região"),
                (37, 63, "Tocantins"),
                (38, 64, "Rio Verde e região"),
                (39, 65, "Cuiabá e região"),
                (40, 66, "Rondonópolis e região"),
                (41, 67, "Campo Grande e região"),
                (42, 68, "Acre"),
                (43, 69, "Rondônia"),
                (44, 71, "Salvador e região"),
                (45, 73, "Ilhéus e região"),
                (46, 74, "Juazeiro e região"),
                (47, 75, "Feira de Santana e região"),
                (48, 77, "Barreiras e região"),
                (49, 79, "Sergipe"),
                (50, 81, "Recife e região"),
                (51, 82, "Alagoas"),
                (52, 83, "Paraíba"),
                (53, 84, "Rio Grande do Norte"),
                (54, 85, "Fortaleza e região"),
                (55, 86, "Teresina e região"),
                (56, 87, "Petrolina e região"),
                (57, 88, "Região do Cariri (Ceará)"),
                (58, 89, "Piauí (exceto Teresina)"),
                (59, 91, "Belém e região"),
                (60, 92, "Manaus e região"),
                (61, 93, "Santarém e região"),
                (62, 94, "Marabá e região"),
                (63, 95, "Roraima"),
                (64, 96, "Amapá"),
                (65, 97, "Amazonas"),
                (66, 98, "São Luís e região"),
                (67, 99, "Imperatriz e região")
            };

            return locations.Select(location => new
            {
                Id = location.Id,
                location.DDD,
                location.Region
            });
        }
    }
}
