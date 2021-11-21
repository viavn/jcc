using JccApi.Entities;
using JccApi.Infrastructure.Repository;
using JccApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JccApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ChildrenController : ControllerBase
    {
        private readonly ILogger<ChildrenController> _logger;
        private readonly IChildRepository _childRepository;
        private readonly IGodParentRepository _godParentRepository;
        private readonly IUserRepository _userRepository;

        public ChildrenController(
            ILogger<ChildrenController> logger,
            IChildRepository childRepository,
            IGodParentRepository godParentRepository,
            IUserRepository userRepository)
        {
            _logger = logger;
            _childRepository = childRepository;
            _godParentRepository = godParentRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetChildren()
        {
            var childEntities = await _childRepository.GetAll();
            var dashboardChildModel = childEntities
                .Select(child => new DashboardChildModel
                {
                    Id = child.Id,
                    FamilyAcronym = child.FamilyAcronym,
                    Name = child.Name,
                    LegalResponsible = child.LegalResponsible,
                })
                .ToList();

            return Ok(dashboardChildModel);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChild(Guid id)
        {
            var child = await _childRepository.GetById(id);
            if (child is null)
            {
                return NotFound();
            }

            var childModel = new ChildModel
            {
                Id = child.Id,
                Name = child.Name,
                Age = child.Age,
                LegalResponsible = child.LegalResponsible,
                ClothesSize = child.ClothesSize,
                ShoeSize = child.ShoesSize,
                FamilyPhone = child.FamilyPhone,
                FamilyAddress = child.FamilyAddress,
                FamilyAcronym = child.FamilyAcronym,
                GodParents = child.GodParents.Select(
                    godParent => new GodParentModel
                    {
                        Id = godParent.Id,
                        Name = godParent.Name,
                        Phone = godParent.Phone,
                        IsClothesSelected = godParent.IsClothesSelected,
                        IsGiftSelected = godParent.IsGiftSelected,
                        IsShoeSelected = godParent.IsShoesSelected,
                    }).ToList(),
            };

            return Ok(childModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddChild([FromBody] ChildModel request)
        {
            var child = new Child(request.Name, request.Age, request.ClothesSize, request.ShoeSize, request.LegalResponsible,
                request.FamilyAcronym, request.FamilyPhone, request.FamilyAddress);

            await _childRepository.Create(child);

            return CreatedAtAction(nameof(GetChild), new { id = child.Id }, child);
        }

        [HttpPost("batch")]
        public async Task<IActionResult> AddChild([FromBody] List<ChildModel> requests)
        {
            foreach (var request in requests)
            {
                var child = new Child(request.Name, request.Age, request.ClothesSize, request.ShoeSize, request.LegalResponsible,
                request.FamilyAcronym, request.FamilyPhone, request.FamilyAddress);

                await _childRepository.Create(child);
            }

            return CreatedAtAction(nameof(GetChildren), null);
        }

        [HttpPatch("{childId}")]
        public async Task<IActionResult> AddOrUpdateChildGodParents(Guid childId, [FromBody] AddOrUpdateChildGodParentsRequest request)
        {
            var child = await _childRepository.GetById(childId);
            var user = await _userRepository.GetUserByLogin(request.UserLogin);

            if (child is null || user is null)
            {
                return NotFound();
            }

            var newGodParents = request.GodParents.Select(gp =>
                new GodParent(gp.Name, gp.Phone,
                    gp.IsClothesSelected, gp.IsShoeSelected, gp.IsGiftSelected, DateTime.Now, user.Id, childId)
                ).ToList();

            await _godParentRepository.DeleteOldThenCreateNewGodParents(child.GodParents, newGodParents);

            return Ok();
        }
    }
}
