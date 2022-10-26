using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using JccApi.Application.Abstractions.UseCases;
using JccApi.Infrastructure.Repository.Abstractions;
using JccApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JccApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ChildrenController : ControllerBase
    {
        private readonly ILogger<ChildrenController> _logger;
        private readonly ICreateChildUseCaseAsync _createChildUseCaseAsync;
        private readonly IChildRepository _childRepository;
        private readonly IGodParentRepository _godParentRepository;
        private readonly IUserRepository _userRepository;

        public ChildrenController(
            ILogger<ChildrenController> logger,
            IChildRepository childRepository,
            IGodParentRepository godParentRepository,
            IUserRepository userRepository,
            ICreateChildUseCaseAsync createChildUseCaseAsync)
        {
            _logger = logger;
            _childRepository = childRepository;
            _godParentRepository = godParentRepository;
            _userRepository = userRepository;
            _createChildUseCaseAsync = createChildUseCaseAsync;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetChild(Guid id)
        {
            return Ok(id);
        }

        [HttpPost]
        public async Task<IActionResult> CreateChild([FromBody] CreateChildRequest request)
        {
            var id = await _createChildUseCaseAsync.Execute(request);

            return CreatedAtAction(
                nameof(GetChild), new { id },
                new { request.Age, request.ClotheSize, request.FamilyId, request.Genre, request.Name, request.ShoeSize, Id = id }
            );
        }

        // [HttpGet]
        // public async Task<IActionResult> GetChildren()
        // {
        //     var childEntities = await _childRepository.GetAll();
        //     var dashboardChildModel = childEntities
        //         .Select(child => new DashboardChildModel
        //         {
        //             Id = child.Id,
        //             FamilyAcronym = child.FamilyAcronym,
        //             Name = child.Name,
        //             LegalResponsible = child.LegalResponsible,
        //         })
        //         .ToList();

        //     return Ok(dashboardChildModel);
        // }

        // [HttpGet("{id}")]
        // public async Task<IActionResult> GetChild(Guid id)
        // {
        //     var child = await _childRepository.GetById(id);
        //     if (child is null)
        //     {
        //         return NotFound();
        //     }

        //     var childModel = new ChildModel
        //     {
        //         Id = child.Id,
        //         Name = child.Name,
        //         Age = child.Age,
        //         LegalResponsible = child.LegalResponsible,
        //         ClothesSize = child.ClothesSize,
        //         ShoeSize = child.ShoesSize,
        //         FamilyPhone = child.FamilyPhone,
        //         FamilyAddress = child.FamilyAddress,
        //         FamilyAcronym = child.FamilyAcronym,
        //         GodParents = child.GodParents.Select(
        //             godParent => new GodParentModel
        //             {
        //                 Id = godParent.Id,
        //                 Name = godParent.Name,
        //                 Phone = godParent.Phone,
        //                 IsClothesSelected = godParent.IsClothesSelected,
        //                 IsGiftSelected = godParent.IsGiftSelected,
        //                 IsShoeSelected = godParent.IsShoesSelected,
        //             }).ToList(),
        //     };

        //     return Ok(childModel);
        // }

        // [HttpGet("export")]
        // public async Task<IActionResult> GetChildrenReport()
        // {
        //     var columns = new string[] { "Família", "Criança", "Roupa?", "Calçado?", "Brinquedo?", "Padrinho", "Telefone Padrinho" };
        //     var children = await _childRepository.GetAllWithGodParents();

        //     using (var stream = new MemoryStream())
        //     {
        //         using (var workbook = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook))
        //         {
        //             List<OpenXmlAttribute> attributeList;
        //             OpenXmlWriter writer;

        //             workbook.AddWorkbookPart();
        //             WorksheetPart workSheetPart = workbook.WorkbookPart.AddNewPart<WorksheetPart>();

        //             writer = OpenXmlWriter.Create(workSheetPart);
        //             writer.WriteStartElement(new Worksheet());
        //             writer.WriteStartElement(new SheetData());

        //             attributeList = new List<OpenXmlAttribute>();
        //             // this is the row index
        //             int rowIndex = 1;
        //             attributeList.Add(new OpenXmlAttribute("r", null, rowIndex.ToString()));

        //             writer.WriteStartElement(new Row(), attributeList);

        //             for (int j = 0; j < columns.Length; ++j)
        //             {
        //                 attributeList = new List<OpenXmlAttribute>();
        //                 // this is the data type ("t"), with CellValues.String ("str")
        //                 attributeList.Add(new OpenXmlAttribute("t", null, "str"));

        //                 writer.WriteStartElement(new Cell(), attributeList);
        //                 writer.WriteElement(new CellValue(columns[j]));

        //                 // this is for Cell
        //                 writer.WriteEndElement();
        //             }

        //             // this is for Row
        //             writer.WriteEndElement();

        //             // VALUES
        //             foreach (var child in children)
        //             {
        //                 if (!child.GodParents.Any())
        //                 {
        //                     rowIndex++;
        //                     attributeList = new List<OpenXmlAttribute>();
        //                     // this is the row index
        //                     attributeList.Add(new OpenXmlAttribute("r", null, rowIndex.ToString()));

        //                     writer.WriteStartElement(new Row(), attributeList);

        //                     // ### Primeira célula
        //                     attributeList = CreateCell(writer, child.FamilyAcronym);
        //                     writer.WriteEndElement();
        //                     // ### FIM Primeira célula

        //                     // ### Segunda célula
        //                     attributeList = CreateCell(writer, child.Name);
        //                     writer.WriteEndElement();
        //                     // ### FIM Segunda célula

        //                     // this is for Row
        //                     writer.WriteEndElement();
        //                 }
        //                 else
        //                 {
        //                     foreach (var godParent in child.GodParents)
        //                     {
        //                         rowIndex++;
        //                         attributeList = new List<OpenXmlAttribute>();
        //                         // this is the row index
        //                         attributeList.Add(new OpenXmlAttribute("r", null, rowIndex.ToString()));

        //                         writer.WriteStartElement(new Row(), attributeList);

        //                         // ### 1a célula
        //                         attributeList = CreateCell(writer, child.FamilyAcronym);
        //                         writer.WriteEndElement();
        //                         // ### FIM 1a célula

        //                         // ### 2a célula
        //                         attributeList = CreateCell(writer, child.Name);
        //                         writer.WriteEndElement();
        //                         // ### FIM 2a célula

        //                         // ### 3a célula
        //                         attributeList = CreateCell(writer, godParent.IsClothesSelected ? "x" : string.Empty);
        //                         writer.WriteEndElement();
        //                         // ### FIM 3a célula

        //                         // ### 4a célula
        //                         attributeList = CreateCell(writer, godParent.IsShoesSelected ? "x" : string.Empty);
        //                         writer.WriteEndElement();
        //                         // ### FIM 4a célula

        //                         // ### 5a célula
        //                         attributeList = CreateCell(writer, godParent.IsGiftSelected ? "x" : string.Empty);
        //                         writer.WriteEndElement();
        //                         // ### FIM 5a célula

        //                         // ### 6a célula
        //                         attributeList = CreateCell(writer, godParent.Name);
        //                         writer.WriteEndElement();
        //                         // ### FIM 6a célula

        //                         // ### 7a célula
        //                         attributeList = CreateCell(writer, godParent.Phone);
        //                         writer.WriteEndElement();
        //                         // ### FIM 7a célula

        //                         // this is for Row
        //                         writer.WriteEndElement();
        //                     }
        //                 }
        //             }

        //             // this is for SheetData
        //             writer.WriteEndElement();
        //             // this is for Worksheet
        //             writer.WriteEndElement();
        //             writer.Close();

        //             writer = OpenXmlWriter.Create(workbook.WorkbookPart);
        //             writer.WriteStartElement(new Workbook());
        //             writer.WriteStartElement(new Sheets());

        //             writer.WriteElement(new Sheet()
        //             {
        //                 Name = "Sheet1",
        //                 SheetId = 1,
        //                 Id = workbook.WorkbookPart.GetIdOfPart(workSheetPart)
        //             });

        //             writer.WriteEndElement(); // Write end for WorkSheet Element
        //             writer.WriteEndElement(); // Write end for WorkBook Element
        //             writer.Close();

        //             workbook.Close();

        //             return File(
        //                 stream.ToArray(),
        //                 "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //                 "criancas.xlsx");
        //         }
        //     }

        //     static List<OpenXmlAttribute> CreateCell(OpenXmlWriter writer, string cellValue)
        //     {
        //         List<OpenXmlAttribute> attributeList = new List<OpenXmlAttribute>();
        //         attributeList.Add(new OpenXmlAttribute("t", null, "str"));

        //         writer.WriteStartElement(new Cell(), attributeList);
        //         writer.WriteElement(new CellValue(cellValue));
        //         return attributeList;
        //     }
        // }

        //     [HttpPost("batch")]
        //     public async Task<IActionResult> AddChild([FromBody] List<ChildModel> requests)
        //     {
        //         foreach (var request in requests)
        //         {
        //             var child = new Child_Old(request.Name, request.Age, request.ClothesSize, request.ShoeSize, request.LegalResponsible,
        //             request.FamilyAcronym, request.FamilyPhone, request.FamilyAddress);

        //             await _childRepository.Create(child);
        //         }

        //         return CreatedAtAction(nameof(GetChildren), null);
        //     }

        //     [HttpPatch("{childId}")]
        //     public async Task<IActionResult> AddOrUpdateChildGodParents(Guid childId, [FromBody] AddOrUpdateChildGodParentsRequest request)
        //     {
        //         var child = await _childRepository.GetById(childId);
        //         var user = await _userRepository.GetUserByLogin(request.UserLogin);

        //         if (child is null || user is null)
        //         {
        //             return NotFound();
        //         }

        //         var newGodParents = request.GodParents.Select(gp =>
        //             new GodParent_Old(gp.Name, gp.Phone,
        //                 gp.IsClothesSelected, gp.IsShoeSelected, gp.IsGiftSelected, DateTime.Now, user.Id, childId)
        //             ).ToList();

        //         await _godParentRepository.DeleteOldThenCreateNewGodParents(child.GodParents, newGodParents);

        //         return Ok();
        //     }
    }
}
