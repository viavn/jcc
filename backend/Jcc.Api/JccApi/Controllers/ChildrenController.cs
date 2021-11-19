using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JccApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ChildrenController : ControllerBase
    {
        private readonly ILogger<ChildrenController> _logger;

        public ChildrenController(ILogger<ChildrenController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetChildren()
        {
            var children = new List<DashboardChildModel>();
            for (int i = 1; i <= 100; i++)
            {
                children.Add(new DashboardChildModel
                {
                    Id = Guid.NewGuid().ToString(),
                    FamilyAcronym = i.ToString(),
                    Name = "Nome criança " + i,
                    LegalResponsible = "Nome responsável " + i,
                });
            }
            await Task.Delay(700);
            return Ok(children);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChild(Guid id)
        {
            var godParents = new List<GodParentModel>();
            for (int i = 1; i <= 3; i++)
            {
                godParents.Add(new GodParentModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Nome Padrinho/Madrinha" + i,
                    Phone = "19981694333",
                    IsClothesSelected = i % 2 == 0,
                    IsGiftSelected = i % 2 == 0,
                    IsShoeSelected = i % 2 == 0,
                });
            }
            var child = new ChildModel
            {
                Id = id.ToString(),
                Name = "Nome criança",
                Age = "7",
                ClothesSize = "M",
                LegalResponsible = "Syrlene Aparecida de Souza Avansini",
                ShoeSize = "41",
                FamilyPhone = "19981192732",
                FamilyAddress = "Rua Pirassununga, 743, Pq. Novo Mundo - Americana",
                FamilyAcronym = "AA",
                GodParents = godParents
            };
            await Task.Delay(700);
            return Ok(child);
        }

        [HttpPatch("{childId}")]
        public async Task<IActionResult> AddOrUpdateChildGodParents(Guid childId, [FromBody] AddOrUpdateChildGodParentsRequest request)
        {
            await Task.Delay(700);
            return Ok();
        }
    }

    public class DashboardChildModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LegalResponsible { get; set; }
        public string FamilyAcronym { get; set; }
    }

    public class ChildModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string ClothesSize { get; set; }
        public string ShoeSize { get; set; }
        public string LegalResponsible { get; set; }
        public string FamilyAcronym { get; set; }
        public string FamilyPhone { get; set; }
        public string FamilyAddress { get; set; }
        public IEnumerable<GodParentModel> GodParents { get; set; } = new List<GodParentModel>();
    }

    public class GodParentModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public bool IsClothesSelected { get; set; }
        public bool IsShoeSelected { get; set; }
        public bool IsGiftSelected { get; set; }
    }

    public class AddOrUpdateChildGodParentsRequest
    {
        public IEnumerable<GodParentModel> GodParents { get; set; } = new List<GodParentModel>();
    }
}
